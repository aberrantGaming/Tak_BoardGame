using System;
using System.Collections;
using System.Collections.Generic;
using Com.aberrantGames.Tak.GameEngine;
using UnityEngine;

namespace Com.aberrantGames.Tak.Scenes
{
    public class DevGameScene : MonoBehaviour
    {
        public GameMode currentGamemode;

        #region Private Variables

        GameManager gm;
        PlayerManager pm;
        ObjectPooler op;

        Position game;
        
        #endregion

        #region MonoBehaviour Callbacks

        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;

            pm = PlayerManager.Instance;
            pm.OnStateChange += Pm_OnStateChange;

            op = ObjectPooler.Instance;            
        }

        private void Start()
        {
            pm.SetPlayerState(PlayerState.PREMATCH);
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

        private void Pm_OnStateChange()
        {
            switch (pm.PlayerState)
            {
                case PlayerState.PREMATCH:
                    OnEnterState_STARTING_MATCH();
                    break;
            }
        }

        private void OnEnterState_GAME()
        {
            Debug.Log("Handling game state transition to GAME...");

            game = MakeGame(currentGamemode);

            Gameboard gameboard = Gameboard.MakeGameboard(game, pm.PlayerCollection.CurrentBoardDesign);
            
            gameboard.DrawGameBoard();
            
        }

        private void OnEnterState_STARTING_MATCH()
        {
            Debug.Log("Handling player state transition to PREMATCH...");

            PreloadObjectPools();
            currentGamemode = pm.selectedGamemode;

            gm.SetGameState(GameState.GAME);
        }

        private Position MakeGame(GameMode gameMode)
        {
            Debug.Log("Creating new game; Size : (" + gameMode.Config.Size + ")...");

            return Game.New(gameMode.Config);
        }

        private void PreloadObjectPools()
        {
            Debug.Log("Preloading player's game objects...");

            List<GameObject> prefabsToLoad = new List<GameObject>
            {
                pm.PlayerCollection.CurrentFlatstones.PrimaryPrefab,
                pm.PlayerCollection.CurrentFlatstones.SecondaryPrefab,
                pm.PlayerCollection.CurrentCapstones.PrimaryPrefab,
                pm.PlayerCollection.CurrentCapstones.SecondaryPrefab,                
            };

            foreach (GameObject prefab in prefabsToLoad)
            {
                op.AddObject(prefab);
            }
        }

        #endregion
    }
}