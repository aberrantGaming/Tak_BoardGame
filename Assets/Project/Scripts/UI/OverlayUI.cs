using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class OverlayUI : MonoBehaviour
    {
        #region Private Variables

        private GameManager gm;

        #endregion

        #region MonoBehaviour Callbacks

        protected void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += HandleOnStateChange;
        }

        #endregion

        #region Public Methods
        
        public void HandleOnStateChange()
        {
            Debug.Log("Handling state change to: " + gm.CurrentGameState);
        }

        public void StartGame()
        {
            gm.SetGameState(GameState.PLAYING);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion

    }
}