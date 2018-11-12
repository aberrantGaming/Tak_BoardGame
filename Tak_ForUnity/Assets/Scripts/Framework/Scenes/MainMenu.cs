using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace aberrantGames.Tak.Scenes
{
    public class MainMenu : MonoBehaviour
    {
        public Transform MainMenuPrefab;
        public Transform PregameMenuPrefab;

        GameManager gm;

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;

            gm.SetGameState(GameState.MENU);
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
                    ShowMainMenu();
                    break;
                case (GameState.PREGAME):
                    RunPregame();
                    break;
                case (GameState.QUIT):
                    Application.Quit();
                    break;
            }
        }

        private void ShowMainMenu()
        {
            MainMenuPrefab.gameObject.SetActive(true);
        }

        private void RunPregame()        
        {
            // verify gamemode configuration


            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}