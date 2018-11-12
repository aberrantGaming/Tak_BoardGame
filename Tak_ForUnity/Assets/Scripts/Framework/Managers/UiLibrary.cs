using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aberrantGames.Tak
{
    public class UiLibrary : MonoBehaviour
    {
        GameManager gm;

        private void Awake()
        {
            gm = GameManager.Instance;
        }

        public void PlayButton()
        {
            gm.SetGameState(GameState.PREGAME);
        }
    }
}