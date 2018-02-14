using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 namespace Com.SeeSameGames.Tak
{
    public class UiInput : MonoBehaviour
    {

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
            {
                uc.Initialize();
            }
        }

        protected virtual void InputHandle()
        {

        }

        #endregion
    }
}