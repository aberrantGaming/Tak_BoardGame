using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class SystemUiInput : MonoBehaviour
    {
        #region Public Variables

        public KeyCode SystemMenuInput = KeyCode.Escape;

        public GameObject SystemUiCanvas;
        public GameObject SettingsUiCanvas;

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
            // UpdateCamera();
        }

        protected virtual void Update()
        {
            uc.UpdateAnimator();
        }

        #endregion

        #region Public Methods

        public virtual void SystemResumeBtn_OnPress()
        {
            uc.ToggleUiElement(SystemUiCanvas, false);
            // gm.SetGameState(ReturnToGameState);
        }

        public virtual void SystemQuitBtn_OnPress()
        {
            uc.QuitToDesktop();
        }

        public virtual void SystemSettingsBtn_OnPress()
        {
            uc.ToggleUiElement(SystemUiCanvas, false);
            uc.ToggleUiElement(SettingsUiCanvas, true);
        }

        public virtual void CloseSystemUi()
        {
            uc.ToggleUiElement(SystemUiCanvas, false);
            uc.ToggleUiElement(SettingsUiCanvas, false);
        }

        #endregion

        #region Private Methods

        protected virtual void UiInitilization()
        {
            uc = GetComponent<UiController>();

            if (uc != null)
            {
                uc.Initialize();
                uc.ToggleUiElement(SystemUiCanvas, false);
            }
        }

        protected virtual void InputHandle()
        {
            if (Input.GetKeyDown(SystemMenuInput))
                uc.ToggleUiElement(SystemUiCanvas);
        }
        
        #endregion
    }
}