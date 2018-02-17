using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

namespace Com.aberrantGames.Tak
{
    [InitializeOnLoad]
    static class SceneAutoLoader
    {

        static string sceneToLoad = "Assets/Scenes/_preload.unity";
        private static string PreviousScenePath
        {

            get
            {

                return EditorPrefs.GetString("SceneAutoLoader.PreviousScenePath", SceneManager.GetActiveScene().path);
            }

            set
            {

                EditorPrefs.SetString("SceneAutoLoader.PreviousScenePath", value);
            }
        }

        static SceneAutoLoader()
        {

            EditorApplication.playmodeStateChanged += OnPlayModeChanged;
        }

        private static void OnPlayModeChanged()
        {

            if (!EditorApplication.isPlaying && EditorApplication.isPlayingOrWillChangePlaymode)
            {

                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {

                    PreviousScenePath = SceneManager.GetActiveScene().path;
                    EditorSceneManager.OpenScene(sceneToLoad);
                }

                return;
            }

            if (!EditorApplication.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
            {

                EditorSceneManager.OpenScene(PreviousScenePath);
            }
        }
    }
}