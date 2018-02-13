using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public enum GameState { INTRO, PAUSED, LAUNCHER, GAME, END_GAME, QUIT }

    public delegate void OnStateChangeHandler();

    public class GameManager : MonoBehaviour
    {
        #region Singleton Pattern

        /// <summary>
        /// Ensures that our class has only one instance and provides a global point of access to it.
        /// </summary>
        protected GameManager() { }
        public event OnStateChangeHandler OnStateChange;

        public static GameManager Instance { get; private set; }

        protected void Awake()
        {
            if (GameManager.Instance == null)
                Instance = this;
            else
                DestroyObject(this);
        }

        #endregion

        #region Public Variables

        [Header("|   PROFILE VARIABLES   |")]
        public Box CurrentBox;
        public Board CurrentBoard;
        public Stones CurrentStones;

        #endregion

        #region Private Variables

        public GameState CurrentGameState { get; private set; }

        #endregion

        #region MonoBehaviour Callbacks

        protected void OnApplicationQuit()
        {
            GameManager.Instance = null;
        }

        #endregion

        #region Photon Methods

        /// <summary>
        /// Called when the local player leaves the room.
        /// </summary>
        public void OnLeftRoom()
        {
            // Load launcher scene
            SceneManager.LoadScene(0);      // TODO: Consider a refactor to load previous scene;
        }

        #endregion

        #region Public Methods

        public void SetGameState(GameState state)
        {
            this.CurrentGameState = state;
            OnStateChange();
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion

    }
}