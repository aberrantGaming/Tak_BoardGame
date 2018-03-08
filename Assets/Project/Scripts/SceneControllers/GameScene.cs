using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak
{
    public class GameScene : MonoBehaviour
    {
        GameManager gm;

        void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;
        }

        private void Gm_OnStateChange()
        {
            switch (gm.GameState)
            {
                case (GameState.IN_MATCH): gm.StartMatch(); break;
            }
        }

        // Use this for initialization
        void Start()
        {
            gm.SetGameState(GameState.IN_MATCH);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}