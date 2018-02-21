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
        PlayerManager pm;

        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;

            pm = PlayerManager.Instance;
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
            
            IDictionary<string, Transform> prefabDictionary = new Dictionary<string, Transform>
            {
                { "BoardFoundation", pm.PlayerCollection.Board.BoardFoundation.transform },
                { "TileLight", pm.PlayerCollection.Board.TileLight.transform },
                { "TileDark", pm.PlayerCollection.Board.TileDark.transform }
            };

            activeGame = new Board(selectedGamemode, prefabDictionary);

            Debug.Log("Gamemode Name : " + activeGame.config.GamemodeName + " ; " +
                      "Board Size : " + activeGame.config.BoardSize);

            // Transition to next game phase:
            // gm.SetGameState();
        }

        #endregion
    }
}