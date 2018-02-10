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

        public virtual void ResetSystemUiElements()
        {
            ToggleSystemUI(false);
            ToggleSystemMenu(true);
            ToggleSettingsMenu(false);
        }

        public virtual void QuitToDesktop()
        {
            Application.Quit();
        }

        public virtual void ToggleSystemUI(bool? value = null)
        {
            SystemUI.SetActive(value == null ? !SystemUI.activeSelf : value ?? false);
        }

        public virtual void ToggleSystemMenu(bool? value = null)
        {
            SystemMenuGO.SetActive(value == null ? !SystemMenuGO.activeSelf : value ?? false);
        }

        public virtual void ToggleSettingsMenu(bool? value = null)
        {
            SettingsMenuGO.SetActive(value == null ? !SettingsMenuGO.activeSelf : value ?? false);
        }

        #endregion
    }
}