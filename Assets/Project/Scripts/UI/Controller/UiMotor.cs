using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class UiMotor : MonoBehaviour
    {
        #region PUblic Variables

        #region Booleans

        [HideInInspector]
        public bool 
            isPaused;

        [HideInInspector]
        public bool
            inSystemMenu,
            inSettingsMenu;

        #endregion

        #region Components

        [HideInInspector]
        public Animator animator;

        #endregion
        
        #endregion

        public void Initialize()
        {
            // access components
            animator = GetComponent<Animator>();
        }
    }
}