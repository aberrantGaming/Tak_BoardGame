using Com.aberrantGames.Tak;
using UnityEditor;
using UnityEngine;

public static class CreateGamemodeMenuItem
{    
    [MenuItem("Custom/Game Modes/Create New Gamemode Holder")]
	public static void CreateGameLevelHolder()
    {
        Gamemode gamemodeHolder =
            ScriptableObject.CreateInstance<Gamemode>();

        AssetDatabase.CreateAsset(gamemodeHolder,
            "Assets/Resources/Gamemodes/NewGamemodeHolder.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = gamemodeHolder;
    }
}
