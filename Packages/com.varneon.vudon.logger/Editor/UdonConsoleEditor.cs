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

        protected override string FoldoutPersistenceKey => "Varneon/VUdon/Logger/UdonConsole/Editor/Foldouts";

        protected override InspectorHeader Header => new InspectorHeaderBuilder()
            .WithTitle("VUdon - UdonConsole")
            .WithDescription("In-game console window for debugging UdonBehaviours")
            .WithURL("GitHub", "https://github.com/Varneon/VUdon-Logger")
            .WithIcon(bannerIcon)
            .Build();
    }
}
