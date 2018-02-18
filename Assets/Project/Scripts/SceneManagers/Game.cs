using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.aberrantGames.Tak.GameEngine;

namespace Com.aberrantGames.Tak.Scenes
{
    public class Game : MonoBehaviour
    {
        #region Public Variables

        public GameHolder selectedGamemode;

        public Board activeGame;

        #endregion

        #region Private Variables

        GameManager gm;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;            
        }

        private void Start()
        {
            gm.SetGameState(GameState.GAME);
        }

        #endregion

        #region Private Methods
        
        private void Gm_OnStateChange()
        {
            switch (gm.GameState)
            {
                case GameState.GAME:
                    OnEnterState_GAME();
                    break;
            }
        }

        private void OnEnterState_GAME()
        {
            Debug.Log("Handling gameState transition to GAME.");
            
            activeGame = new Board(selectedGamemode);
            Debug.Log("Gamemode Name : " + activeGame.config.GamemodeName + " ; " +
                      "Board Size : " + activeGame.config.BoardSize);

            // Transition to next game phase:
            // gm.SetGameState();
        }

        #endregion
    }
}