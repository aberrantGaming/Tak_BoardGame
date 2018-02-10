using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public enum GameState
    {
        INTRO,
        MAIN_MENU,
        SYSTEM_MENU,
        PLAYING
    }

    public delegate void OnStateChangeHandler();

    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Ensures that our class has only one instance and provides a global point of access to it.
        /// </summary>
        #region Singleton Pattern

        protected GameManager() { }
        private static GameManager instance = null;
        public event OnStateChangeHandler OnStateChange;

        public static GameManager Instance
        {
            get
            {
                if (GameManager.instance == null)
                {
                    DontDestroyOnLoad(GameManager.instance);
                    GameManager.instance = new GameManager();
                }

                return GameManager.instance;
            }
        }

        #endregion

        #region Public Variables

        public GameState CurrentGameState { get; private set; }
        public GameState PreviousGameState { get; private set; }

        #endregion
        
        #region MonoBehaviour Callbacks

        protected void OnApplicationQuit()
        {
            GameManager.instance = null;
        }

        #endregion

        #region Public Methods

        public void SetGameState(GameState state)
        {
            this.PreviousGameState = this.CurrentGameState;
            this.CurrentGameState = state;
            OnStateChange();
        }

        #endregion
        
    }
}