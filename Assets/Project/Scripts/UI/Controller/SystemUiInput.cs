using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class SystemUiInput : MonoBehaviour
    {
        #region Public Variables

        public KeyCode SystemMenuInput = KeyCode.Escape;
                
        #endregion

        #region Private Variables

        protected UiController uc;      // access to the UiController component
        protected GameManager gm;       // access to Game State
        
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
            //uc.UpdateAnimator();
        }

        #endregion

        #region Public Methods

        public virtual void SystemResumeBtn_OnPress()
        {
            uc.ToggleUiElement(gameObject, false);
        }

        public virtual void SystemQuitBtn_OnPress()
        {
            uc.QuitToDesktop();
        }

        public virtual void SystemSettingsBtn_OnPress()
        {
            //uc.LaunchSettingsUi();
        }        

        #endregion

        #region Private Methods

        protected virtual void UiInitilization()
        {
            uc = GetComponent<UiController>();

            if (uc != null)
            {
                uc.Initialize();
                uc.ToggleUiElement(gameObject, false);
            }
        }

        protected virtual void InputHandle()
        {
            if (Input.GetKeyDown(SystemMenuInput))
            {
                    //uc.Resume();
            }
        }
        
        #endregion
    }
}