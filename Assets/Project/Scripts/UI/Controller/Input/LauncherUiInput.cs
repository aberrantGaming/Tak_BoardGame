using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class LauncherUiInput : MonoBehaviour
    {
        public void LauncherPlayBtn_OnPress()
        {
            Debug.Log("PlayGame");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        
    }
}