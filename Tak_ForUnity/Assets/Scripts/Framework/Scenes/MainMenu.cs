using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aberrantGames.Tak.Scenes
{
    public class Scene_MainMenu : MonoBehaviour
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
                    ShowPregameMenu();
                    break;
                case (GameState.QUIT):
                    Application.Quit();
                    break;
            }
        }

        private void ShowMainMenu()
        {
            MainMenuPrefab.gameObject.SetActive(true);

            if (PregameMenuPrefab.gameObject.activeSelf)
                PregameMenuPrefab.gameObject.SetActive(false);
        }

        private void ShowPregameMenu()
        {
            //PregameMenuPrefab.gameObject.SetActive(true);

            //if (MainMenuPrefab.gameObject.activeSelf)
            //    MainMenuPrefab.gameObject.SetActive(false);
        }
    }
}