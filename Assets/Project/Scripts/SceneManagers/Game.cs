using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Scenes
{
    public class Game : MonoBehaviour
    {
        #region Private Variables

        GameManager gm;
        PlayerManager pm;
        ObjectPooler op;

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
            gm.SetGameState(GameState.GAME);
            pm.SetPlayerState(PlayerState.EnteringMatch);
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
                case PlayerState.EnteringMatch:
                    OnEnterState_STARTING_MATCH();
                    break;
            }
        }

        private void OnEnterState_GAME()
        {
            Debug.Log("Handling gameState transition to GAME.");

            // FLIP THE COIN TO DETERMINE PLAYER 1

            // INSTANTIATE THE GAMEBOARD
            Board

            pm.SetPlayerState(PlayerState.InMatch);
        }

        private void OnEnterState_STARTING_MATCH()
        {
            Debug.Log("Preloading player's game objects...");

            PreloadObjectPools();            
        }

        private void PreloadObjectPools()
        {
            List<GameObject> prefabsToLoad = new List<GameObject>
            {
                pm.PlayerCollection.CurrentFlatstones.PrimaryPrefab,
                pm.PlayerCollection.CurrentFlatstones.SecondaryPrefab,
                pm.PlayerCollection.CurrentCapstones.PrimaryPrefab,
                pm.PlayerCollection.CurrentCapstones.SecondaryPrefab
            };

            foreach (GameObject prefab in prefabsToLoad)
            {
                op.AddObject(prefab);
            }
        }

        #endregion
    }
}