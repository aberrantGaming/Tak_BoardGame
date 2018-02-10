using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class UiController : UiAnimator
    {
        #region Public Variables

        public GameObject SystemUI;
        public GameObject SystemMenuGO;
        public GameObject SettingsMenuGO;

        #endregion

        #region MonoBehaviour Callbacks

        protected virtual void Start()
        {
            ResetSystemUiElements();
        }

        #endregion

        #region Public Methods

        public virtual void ToggleSystemUI(bool value = new bool())
        {
            Debug.Log(value);
            SystemUI.SetActive(value ? value : !SystemUI.activeSelf);
        }

        public virtual void ToggleSystemMenu(bool value = new bool())
        {
            SystemMenuGO.SetActive(value ? value : !SystemMenuGO.activeSelf);
        }

        public virtual void ToggleSettingsMenu(bool value = new bool())
        {
            SettingsMenuGO.SetActive(value ? value : !SettingsMenuGO.activeSelf);
        }

        public virtual void QuitToDesktop()
        {
            Application.Quit();
        }

        public virtual void ResetSystemUiElements()
        {
            ToggleSystemUI(false);
            ToggleSystemMenu(true);
            ToggleSettingsMenu(false);
        }



        #endregion
    }
}