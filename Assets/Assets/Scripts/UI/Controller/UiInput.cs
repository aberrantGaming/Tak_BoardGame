using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class UiInput : MonoBehaviour
    {
        #region Public Variables

        public KeyCode SystemMenuInput = KeyCode.Escape;

        #endregion

        #region Private Variables

        protected UiController uc;      // access to the UiController component

        #endregion

        #region MonoBehaviour Callbacks

        protected virtual void Start()
        {
            UiInitilization();
        }

        protected virtual void LateUpdate()
        {
            InputHandle();
            // UpdateUiStates();
        }

        protected virtual void FixedUpdate()
        {

        }

        protected virtual void Update()
        {
            uc.UpdateAnimator();
        }

        #endregion

        #region Private Methods

        protected virtual void UiInitilization()
        {
            uc = GetComponent<UiController>();

            if (uc != null)
                uc.Initialize();
        }

        protected virtual void InputHandle()
        {
            SystemInput();
        }

        protected virtual void SystemInput()
        {
            if (Input.GetKeyDown(SystemMenuInput))
                uc.ToggleSystemUI();
        }

        #endregion
    }
}