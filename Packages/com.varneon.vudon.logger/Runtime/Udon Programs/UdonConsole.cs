#pragma warning disable IDE0044 // Making serialized fields readonly hides them from the inspector

using System;
using UnityEngine;
using UnityEngine.UI;
using Varneon.VInspector;
using Varneon.VUdon.Logger.Abstract;
using VRC.SDKBase;

namespace Varneon.UdonPrefabs.RuntimeTools
{
    /// <summary>
    /// In-game console window for debugging UdonBehaviours
    /// </summary>
    [SelectionBase]
    [DefaultExecutionOrder(-2146483648)]
    public class UdonConsole : UdonLogger
    {
        #region Variables

        #region Serialized
        [Header("Settings")]
        [SerializeField]
        [FieldParentElement("Foldout_Settings")]
        private bool showTimestamps = false;

        [SerializeField]
        [Tooltip("How many entries are ensured to always be visible in the console")]
        [FieldParentElement("Foldout_Settings")]
        private int minLogEntries = 10;

        [SerializeField]
        [FieldParentElement("Foldout_Settings")]
        private int maxLogEntries = 100;

        [SerializeField]
        [Tooltip("How many entries should be incremented/decremented from MaxLogEntries when buttons on the UI are pressed")]
        [FieldParentElement("Foldout_Settings")]
        private int maxLogEntriesStep = 50;

        [SerializeField, Range(8, 32)]
        [FieldParentElement("Foldout_Settings")]
        private int fontSize = 24;

        [SerializeField]
        [FieldParentElement("Foldout_Settings")]
        private bool proxyEntriesToLogs;

        [Space]
        [Header("References")]
        [SerializeField]
        [FieldParentElement("Foldout_References")]
        private RectTransform logWindow;

        [SerializeField]
        [FieldParentElement("Foldout_References")]
        private GameObject logItem;

        [SerializeField]
        [FieldParentElement("Foldout_References")]
        private Toggle logToggle, warningToggle, errorToggle, timestampsToggle;

        [SerializeField]
        [FieldParentElement("Foldout_References")]
        private InputField maxLogEntriesField, fontSizeField;
        #endregion

        #region Private
        private Scrollbar scrollbar;

        private RectTransform canvasRoot;
        #endregion

        #region Constants
        private const string LOG_PREFIX = "[<color=#00FFFF>UdonConsole</color>]:";

        private const string
            JOIN_PREFIX = "[<color=lime>JOIN</color>]",
            LEAVE_PREFIX = "[<color=red>LEAVE</color>]";

        private const string WHITESPACE = " ";

        private const int
            FONT_MIN_SIZE = 8,
            FONT_MAX_SIZE = 32;

        private const int ENTRIES_HARDCAP = 1000;
        #endregion

        #endregion

        #region Private Methods
        private void Start()
        {
            logItem.GetComponentInChildren<Text>(true).fontSize = fontSize;
            timestampsToggle.isOn = !showTimestamps;
            scrollbar = GetComponentInChildren<Scrollbar>(true);
            canvasRoot = GetComponentInChildren<Canvas>(true).GetComponent<RectTransform>();

            Log($"{LOG_PREFIX} This is Varneon's Udon Essentials Console!");
            LogWarning($"{LOG_PREFIX} It can show warnings if something is out of the ordinary");
            LogError($"{LOG_PREFIX} And errors can also be shown if something goes completely wrong");
            Log($"{LOG_PREFIX} Context objects are also supported:", this);
            Log($"{LOG_PREFIX} As well as assertions:");
            Assert(false, null);
        }

        /// <summary>
        /// Reloads all entries and applies filters
        /// </summary>
        private void ReloadLogs()
        {
            int entryOverflow = GetCurrentLogEntryCount() - maxLogEntries;

            if (entryOverflow > 0)
            {
                for (int i = 0; i < entryOverflow; i++)
                {
                    Destroy(logWindow.GetChild(i).gameObject);
                }
            }

            for (int i = 0; i < GetCurrentLogEntryCount(); i++)
            {
                GameObject item = logWindow.GetChild(i).gameObject;

                string[] info = item.name.Split(' ');

                LogType type = (LogType)int.Parse(info[0]);

                string timestamp = string.Join(WHITESPACE, new string[] { info[1], info[2] });

                Text text = item.GetComponent<Text>();

                string textContent = text.text;

                bool hasTimestamp = textContent.StartsWith(timestamp);

                if (showTimestamps && !hasTimestamp) { text.text = string.Join(WHITESPACE, new string[] { timestamp, textContent }); }
                else if (!showTimestamps && hasTimestamp) { text.text = text.text.Substring(timestamp.Length + 1); }

                SetLogEntryActive(item, type);
            }
        }

        /// <summary>
        /// Sets the log entry active based on current filter states
        /// </summary>
        /// <param name="logEntry"></param>
        /// <param name="type"></param>
        private void SetLogEntryActive(GameObject logEntry, LogType type)
        {
            logEntry.SetActive(
                (type == LogType.Log && !logToggle.isOn) ||
                (type == LogType.Warning && !warningToggle.isOn) ||
                ((type == LogType.Error || type == LogType.Exception || type == LogType.Assert) && !errorToggle.isOn)
                );
        }

        /// <summary>
        /// Gets the current timestamp formatted as "yyyy.MM.dd HH:mm:ss"
        /// </summary>
        /// <returns>Formatted timestamp string</returns>
        private string GetTimestamp()
        {
            return DateTime.UtcNow.ToLocalTime().ToString("yyyy.MM.dd HH:mm:ss");
        }

        /// <summary>
        /// Write line to the console
        /// </summary>
        /// <param name="logType"></param>
        /// <param name="text"></param>
        private void WriteLine(LogType logType, string message)
        {
            Text textComponent;

            Transform newEntry;

            string timestamp = GetTimestamp();

            if (GetCurrentLogEntryCount() < maxLogEntries)
            {
                newEntry = Instantiate(logItem).transform;
                newEntry.SetParent(logWindow);
                newEntry.localPosition = Vector3.zero;
                newEntry.localRotation = Quaternion.identity;
                newEntry.localScale = Vector3.one;
            }
            else
            {
                newEntry = logWindow.GetChild(0);
                newEntry.SetAsLastSibling();
            }

            GameObject newEntryGO = newEntry.gameObject;

            newEntryGO.name = string.Join(WHITESPACE, new string[] { ((int)logType).ToString(), timestamp });
            textComponent = newEntry.GetComponent<Text>();

            textComponent.text = showTimestamps ? message : message.Substring(timestamp.Length + 1);

            SetLogEntryActive(newEntryGO, logType);

            LayoutRebuilder.ForceRebuildLayoutImmediate(canvasRoot);

            SendCustomEventDelayedFrames(nameof(ScrollToBottom), 3);
        }

        /// <summary>
        /// Get the number of entries in the console
        /// </summary>
        /// <returns>Log entry count</returns>
        private int GetCurrentLogEntryCount()
        {
            return logWindow.childCount;
        }

        private string BuildLogStringOutput(LogType logType, object message)
        {
            return string.Join(" ", GetTimestamp(), GetLogTypePrefix(logType), MessageObjectToString(message));
        }

        private string BuildLogStringOutput(LogType logType, object message, UnityEngine.Object context)
        {
            return string.Join(" ", GetTimestamp(), GetLogTypePrefix(logType), MessageObjectToString(message), ContextObjectToString(context));
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Scrolls to the bottom of the window
        /// </summary>
        public void ScrollToBottom()
        {
            scrollbar.value = 0f;
        }

        /// <summary>
        /// Clears the log entries
        /// </summary>
        [Obsolete("Use ClearLogs() instead.")]
        public void Clear()
        {
            ClearLogs();
        }
        #endregion

        #region Logger Overrides
        protected override void Log(LogType logType, object message)
        {
            WriteLine(logType, BuildLogStringOutput(logType, message));
        }

        protected override void Log(LogType logType, object message, UnityEngine.Object context)
        {
            WriteLine(logType, BuildLogStringOutput(logType, message, context));
        }

        protected override void LogFormat(LogType logType, string format, params object[] args)
        {
            WriteLine(logType, BuildLogStringOutput(logType, string.Format(format, args)));
        }

        protected override void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
        {
            WriteLine(logType, BuildLogStringOutput(logType, string.Format(format, args), context));
        }

        public override void ClearLogs()
        {
            for (int i = 0; i < GetCurrentLogEntryCount(); i++)
            {
                Destroy(logWindow.GetChild(i).gameObject);
            }
        }
        #endregion

        #region Player Events
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            Log(string.Join(WHITESPACE, new string[] { LOG_PREFIX, JOIN_PREFIX, player.displayName }));
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            if (!Utilities.IsValid(player)) { return; }

            Log(string.Join(WHITESPACE, new string[] { LOG_PREFIX, LEAVE_PREFIX, player.displayName }));
        }
        #endregion

        #region Filter Toggles
        /// <summary>
        /// Toggles Log entry type filtering
        /// </summary>
        public void ToggleFilterLog()
        {
            ReloadLogs();
        }

        /// <summary>
        /// Toggles Warning entry type filtering
        /// </summary>
        public void ToggleFilterWarning()
        {
            ReloadLogs();
        }

        /// <summary>
        /// Toggles Error entry type filtering
        /// </summary>
        public void ToggleFilterError()
        {
            ReloadLogs();
        }

        /// <summary>
        /// Toggles timestamp display on log entries
        /// </summary>
        public void ToggleTimestamps()
        {
            showTimestamps = !timestampsToggle.isOn;

            ReloadLogs();
        }
        #endregion

        #region Log Entry Limit
        /// <summary>
        /// Applies the max log entry value from MaxLogEntriesField
        /// </summary>
        public void ApplyMaxLogEntries()
        {
            int.TryParse(maxLogEntriesField.text, out maxLogEntries);

            SetMaxLogEntries(maxLogEntries);
        }

        /// <summary>
        /// Decreases the maximum amount of log entries by 10
        /// </summary>
        public void DecreaseMaxEntries()
        {
            SetMaxLogEntries(maxLogEntries - maxLogEntriesStep);
        }

        /// <summary>
        /// Increases the maximum amount of log entries by 10
        /// </summary>
        public void IncreaseMaxEntries()
        {
            SetMaxLogEntries(maxLogEntries + maxLogEntriesStep);
        }

        /// <summary>
        /// Changes the maximum number of log entries based on the provided number
        /// </summary>
        /// <param name="maxEntries"></param>
        private void SetMaxLogEntries(int maxEntries)
        {
            maxLogEntries = Mathf.Clamp(maxEntries, minLogEntries, ENTRIES_HARDCAP);

            maxLogEntriesField.text = maxLogEntries.ToString();

            ReloadLogs();
        }
        #endregion

        #region Font Size
        /// <summary>
        /// Applies the size of the log window font from FontSizeField
        /// </summary>
        public void ApplyFontSize()
        {
            int.TryParse(fontSizeField.text, out fontSize);

            SetFontSize(fontSize);
        }

        /// <summary>
        /// Decreases the size of the log window font
        /// </summary>
        public void DecreaseFontSize()
        {
            SetFontSize(--fontSize);
        }

        /// <summary>
        /// Increases the size of the log window font
        /// </summary>
        public void IncreaseFontSize()
        {
            SetFontSize(++fontSize);
        }

        /// <summary>
        /// Changes the size of the log window font based on the provided number
        /// </summary>
        /// <param name="fontSize"></param>
        private void SetFontSize(int fontSize)
        {
            this.fontSize = Mathf.Clamp(fontSize, FONT_MIN_SIZE, FONT_MAX_SIZE);

            fontSizeField.text = this.fontSize.ToString();

            logItem.GetComponentInChildren<Text>(true).fontSize = this.fontSize;

            foreach (Text text in logWindow.GetComponentsInChildren<Text>(true))
            {
                text.fontSize = this.fontSize;
            }
        }
        #endregion
    }
}
