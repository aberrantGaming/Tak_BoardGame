using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class UiController : UiAnimator
    {
        #region Public Variables

        public GameObject SystemUI;

        #endregion

        public virtual void ToggleSystemUI()
        {
            SystemUI.SetActive(!SystemUI.activeSelf);
        }

    }
}