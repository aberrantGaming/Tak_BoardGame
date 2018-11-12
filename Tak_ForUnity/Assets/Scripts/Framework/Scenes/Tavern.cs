using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aberrantGames.Tak.Scenes
{
    public class Tavern : MonoBehaviour
    {
        public Vector3 boardSpawnPoint;

        GameManager gm;
        
        private void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += Gm_OnStateChange;

            gm.SetGameState(GameState.GAME);

            Init();
        }

        private void Init()
        {
            if (gm.GameboardPrefab)
            {
                if (!gm.GameboardPrefab.gameObject.scene.IsValid())
                { 
                    Instantiate(gm.GameboardPrefab, boardSpawnPoint, Quaternion.identity);
                    gm.GameboardPrefab.transform.parent = GameObject.Find("_dynamic").transform;
                }
            }
        }

        private void Gm_OnStateChange()
        {
            switch (gm.GameState)
            {
                case (GameState.GAME): break;
                case (GameState.POSTGAME): break;
            }
        }
    }
}
