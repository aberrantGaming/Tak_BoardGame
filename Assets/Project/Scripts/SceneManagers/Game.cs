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

            // Apply default if no Gamemode is available.
            if (selectedGamemode == null)   
                selectedGamemode = ScriptableObject.CreateInstance<GameHolder>();

            Debug.Log("Selected Gamemode : " + selectedGamemode.GamemodeName);

            Board g = new Board(selectedGamemode);

            Debug.Log("Board Size : " + g.cfg.BoardSize);

        }

        #endregion
    }
}