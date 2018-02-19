﻿using UnityEngine;

namespace Com.aberrantGames.Tak
{
    public enum GameState { NULL, INTRO, PAUSED, LAUNCHER, GAME, END_GAME, QUIT }

    public delegate void OnGameStateChangeHandler();

    public class GameManager : Photon.PunBehaviour
    {
        #region Singleton Pattern

        /// <summary>
        /// Ensures that our class has only one instance and provides a global point of access to it.
        /// </summary>
        protected GameManager() { }
        public event OnGameStateChangeHandler OnStateChange;

        public static GameManager Instance { get; private set; }

        protected void Awake()
        {
            if (GameManager.Instance == null)
                Instance = this;
            else
                DestroyObject(this);
        }

        #endregion

        #region Private Variables

        public GameState GameState { get; private set; }

        #endregion

        #region MonoBehaviour Callbacks

        protected void OnApplicationQuit()
        {
            GameManager.Instance = null;
        }

        #endregion

        #region Public Methods

        public void SetGameState(GameState state)
        {
            this.GameState = state;
            OnStateChange();
        }

        #endregion
    }
}