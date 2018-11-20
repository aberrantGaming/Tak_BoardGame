using aberrantGames.Utilities;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace aberrantGames.Tak.Scenes
{
    public class MainMenu : MonoBehaviour
    {
        public List<SceneReference> additiveScenes;
        
        GameManager gm;

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;

            gm.SetGameState(GameState.MENU);
        }

        private void Start()
        {
            foreach (SceneReference sceneToLoad in additiveScenes)
            {
                SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Additive);
            }
        }

        public void PlayButton_OnClick()
        {
            gm.SetGameState(GameState.PREGAME);
        }

        private void Gm_OnStateChange()
        {
            switch (gm.GameState)
            {
                case (GameState.MENU):
                    break;
                case (GameState.PREGAME):
                    break;
                case (GameState.QUIT):
                    Application.Quit();
                    break;
            }
        }
    }
}