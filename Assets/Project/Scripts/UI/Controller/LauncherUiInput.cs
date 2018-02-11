using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    [CreateAssetMenu(menuName = "Collectables/Gamebox")]
    public class LauncherUiInput : UiInput
    {
        public void PlayGame()
        {
            Debug.Log("PlayGame");
        }
    }
}