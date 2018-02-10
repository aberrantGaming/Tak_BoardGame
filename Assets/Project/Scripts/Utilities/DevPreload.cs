using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class DevPreload : MonoBehaviour
    {
        protected void Awake()
        {
            GameObject check = GameObject.Find("__app");

            if (check == null)
                UnityEngine.SceneManagement.SceneManager.LoadScene("_preload");
        }
    }
}