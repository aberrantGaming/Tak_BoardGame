using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.SeeSameGames.Tak
{
    public class UiController : UiAnimator
    {
        #region Public Variables

        public GameObject SystemUiCanvas;
        public GameObject SettingsUiCanvas;

        #endregion

        #region Private Variables

        protected GameManager gm;
        protected GameState ResumeToGameState;

        #endregion

        #region MonoBehaviour Callbacks

        protected virtual void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += HandleOnStateChange;
        }

        #endregion

        #region Public Methods

        public virtual void Pause()
        {
            if (gm.CurrentGameState == GameState.INTRO)
                return;

            ResumeToGameState = gm.CurrentGameState;
            gm.SetGameState(GameState.PAUSED);
        }

        public virtual void Resume()
        {
            if (gm.CurrentGameState != GameState.PAUSED)
                return;

            gm.SetGameState(ResumeToGameState);

            OnResume();
        }

        public virtual void OpenSettings()
        {
            if (gm.CurrentGameState != GameState.PAUSED)
                return;

            OnSettings();            
        }

        public virtual void Quit()
        {
            gm.SetGameState(GameState.QUIT);
        }

        /// <summary>
        /// Toggle a UI Element On/Off
        /// </summary>
        /// <param name="uiElement"></param>
        /// <param name="display"></param>
        public virtual void ToggleUiElement(GameObject uiElement, bool? display = null)
        {
            uiElement.SetActive(display == null ? !uiElement.activeSelf : display ?? false);
        }        

        #endregion

        #region Private Methods

        protected virtual void HandleOnStateChange()
        {
            Debug.Log(gm.CurrentGameState);
            switch (gm.CurrentGameState)
            {
                case (GameState.PAUSED):
                    OnPaused();
                    break;

                case (GameState.QUIT):
                    OnQuit();
                    break;
            }
        }

        protected virtual void OnPaused()
        {
            isPaused = true;

            ToggleUiElement(SystemUiCanvas, true);
            ToggleUiElement(SettingsUiCanvas, false);
        }

        protected virtual void OnResume()
        {
            isPaused = false;

            ToggleUiElement(SystemUiCanvas, false);
            ToggleUiElement(SettingsUiCanvas, false);
        }            

        protected virtual void OnQuit()
        {
            Debug.Log("Quitting...");
            Application.Quit();
        }

        protected virtual void OnSettings()
        {
            ToggleUiElement(SystemUiCanvas, false);
            ToggleUiElement(SettingsUiCanvas, true);
        }

        #endregion

    }
}