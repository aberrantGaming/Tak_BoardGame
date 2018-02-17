using Com.aberrantGames.Tak.GameEngine;
using UnityEditor;
using UnityEngine;

public static class CreateGamemodeMenuItem
{    
    [MenuItem("Custom/Game Modes/Create New Gamemode Holder")]
	public static void CreateGameLevelHolder()
    {
        GameHolder gamemodeHolder =
            ScriptableObject.CreateInstance<GameHolder>();

        AssetDatabase.CreateAsset(gamemodeHolder,
            "Assets/Resources/Gamemodes/NewGamemodeHolder.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = gamemodeHolder;
    }
}
