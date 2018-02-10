using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class SystemUI : MonoBehaviour
    {
        #region Public Variables

        public KeyCode SystemMenuInput = KeyCode.Escape;
        
        public GameObject SystemMenuUI;
        public GameObject SettingsMenuUI;

        #endregion

        #region Private Variables

        private GameObject self;
        private GameManager gm;
        private GameState ReturnToGameState;

        #endregion

        #region MonoBehaviour Callbacks

        protected void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += HandleOnStateChange;

            self = this.gameObject;
        }

        protected void Start()
        {
            SystemMenuUI.SetActive(true);
            SettingsMenuUI.SetActive(false);
        }

        protected void LateUpdate()
        {
            HandleInput();
        }

        #endregion

        #region Public Methods

        public void HandleOnStateChange()
        {
            switch (gm.CurrentGameState)
            {
                case GameState.SYSTEM_MENU:
                    ReturnToGameState = gm.PreviousGameState;
                    self.SetActive(true);
                    ShowSystemMenu();
                    break;
                default:
                    self.SetActive(false);
                    break;
            }

            Debug.Log("Handling state change to: " + gm.CurrentGameState);
        }

        public void ResumeButton_OnPress()
        {
            HideSystemMenu();
            gm.SetGameState(ReturnToGameState);
        }

        public void QuitButton_OnPress()
        {
            QuitGame();
        }

        #endregion

        #region Private Methods

        protected virtual void HandleInput()
        {
            if (Input.GetKeyDown(SystemMenuInput))
            {
                if (gm.CurrentGameState == GameState.SYSTEM_MENU 
                    && SettingsMenuUI.activeSelf)
                {
                    HideSettingsMenu();
                    ShowSystemMenu();
                }
                else if (gm.CurrentGameState == GameState.SYSTEM_MENU)
                {
                    gm.SetGameState(ReturnToGameState);
                }
                else
                    gm.SetGameState(GameState.SYSTEM_MENU);
            }
        }        

        protected virtual void HideSystemMenu()
        {
            SystemMenuUI.SetActive(false);
        }

        protected virtual void ShowSystemMenu()
        {
            this.gameObject.SetActive(true);
            SystemMenuUI.SetActive(true);
        }

        protected virtual void HideSettingsMenu()
        {
            SettingsMenuUI.SetActive(false);
        }

        protected virtual void QuitGame()
        {
            Application.Quit();
        }
        
        #endregion
    }
}