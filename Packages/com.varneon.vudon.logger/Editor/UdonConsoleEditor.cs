using UnityEditor;
using UnityEngine;
using Varneon.VUdon.Editors.Editor;

namespace Varneon.VUdon.Logger.Editor
{
    /// <summary>
    /// Custom inspector for UdonConsole prefab
    /// </summary>
    [CustomEditor(typeof(UdonConsole))]
    public class UdonConsoleEditor : InspectorBase
    {
        [SerializeField]
        private Texture2D bannerIcon;

        private bool showWindowSettings;

        private RectTransform canvasRectTransform;

        private float windowWidth, windowHeight, windowScale;

        private static readonly GUIContent WindowFoldoutContent = new GUIContent("Window", "Adjust console window");

        private const string SHOW_WINDOW_SETTINGS_PERSISTENCE_KEY = "Varneon/VUdon/Logger/UdonConsole/Editor/Window";

        protected override string FoldoutPersistenceKey => "Varneon/VUdon/Logger/UdonConsole/Editor/Foldouts";

        protected override InspectorHeader Header => new InspectorHeaderBuilder()
            .WithTitle("VUdon - UdonConsole")
            .WithDescription("In-game console window for debugging UdonBehaviours")
            .WithURL("GitHub", "https://github.com/Varneon/VUdon-Logger")
            .WithIcon(bannerIcon)
            .Build();

        protected override void OnEnable()
        {
            base.OnEnable();

            canvasRectTransform = ((UdonConsole)target).GetComponentInChildren<Canvas>().GetComponent<RectTransform>();

            Vector2 windowResolution = canvasRectTransform.sizeDelta;

            windowWidth = windowResolution.x;

            windowHeight = windowResolution.y;

            windowScale = canvasRectTransform.localScale.x;

            if (EditorPrefs.HasKey(SHOW_WINDOW_SETTINGS_PERSISTENCE_KEY))
            {
                showWindowSettings = EditorPrefs.GetBool(SHOW_WINDOW_SETTINGS_PERSISTENCE_KEY);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            EditorPrefs.SetBool(SHOW_WINDOW_SETTINGS_PERSISTENCE_KEY, showWindowSettings);
        }

        protected override void OnPreDrawFields()
        {
            if (showWindowSettings = EditorGUILayout.BeginFoldoutHeaderGroup(showWindowSettings, WindowFoldoutContent))
            {
                GUI.color = Color.black;

                using (EditorGUILayout.VerticalScope verticalScope = new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    GUI.Box(verticalScope.rect, string.Empty);

                    GUI.color = Color.white;

                    using (new EditorGUI.IndentLevelScope(1))
                    {
                        using (EditorGUI.ChangeCheckScope changeScope = new EditorGUI.ChangeCheckScope())
                        {
                            windowWidth = EditorGUILayout.Slider("Width", windowWidth, 300f, 1000f);

                            windowHeight = EditorGUILayout.Slider("Height", windowHeight, 200f, 1000f);

                            windowScale = Mathf.Clamp(EditorGUILayout.FloatField("Scale", windowScale), 0.0001f, 10f);

                            if (changeScope.changed)
                            {
                                Undo.RecordObject(canvasRectTransform, "Adjust UdonConsole Window");

                                canvasRectTransform.sizeDelta = new Vector2(windowWidth, windowHeight);

                                canvasRectTransform.localScale = Vector3.one * windowScale;
                            }
                        }
                    }
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}
