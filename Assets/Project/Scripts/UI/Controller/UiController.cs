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
            ResumeToGameState = gm.CurrentGameState;
            gm.SetGameState(GameState.PAUSED);
        }

        public virtual void Resume()
        {
            gm.SetGameState(ResumeToGameState);
        }

        public virtual void QuitToDesktop()
        {
            Application.Quit();
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
                    isPaused = true;
                    ToggleUiElement(SystemUiCanvas, true);
                    break;

                default:
                    isPaused = false;
                    Resume();
                    break;
            }
        }

        #endregion

    }
}