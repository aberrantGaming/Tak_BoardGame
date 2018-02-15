using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class SystemUiInput : UiInput
    {
        #region Public Variables

        public KeyCode SystemMenuInput = KeyCode.Escape;

        #endregion

        #region Public Methods

        public virtual void Resume_OnButtonPress()
        {
            uc.Resume();
        }

        public virtual void Quit_OnButtonPress()
        {
            uc.Quit();
        }

        #endregion

        #region Private Methods

        protected override void InputHandle()
        {
            if (Input.GetKeyDown(SystemMenuInput))
            {
                {
                    if (Input.GetKeyDown(SystemMenuInput))
                    {
                        if (!uc.isPaused)
                            uc.Pause();
                        else
                            uc.Resume();
                    }
                }
            }
        }
        
        #endregion
    }
}