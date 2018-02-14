using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class LauncherUiInput : UiInput
    {
        public void Quickmatch_OnButtonPress()
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void PracticeMatch_OnButtonPress()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        
    }
}