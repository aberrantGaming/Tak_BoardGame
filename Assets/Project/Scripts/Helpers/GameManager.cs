using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public enum GameState { NULL, INTRO, PAUSED, IN_LAUNCHER, STARTING_MATCH, ENDING_MATCH, EXITING_APP }

    public delegate void OnStateChangeHandler();

    public class GameManager : Photon.PunBehaviour
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

        #region PunBehaviour Callbacks

        public override void OnPhotonPlayerConnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("OnPhotonPlayerConnected() " + otherPlayer.NickName);

            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient);

                LoadArena();
            }
        }

        public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
        {
            Debug.Log("OnPhotonPlayerDisconnected() " + otherPlayer.NickName);

            if (PhotonNetwork.isMasterClient)
            {
                Debug.Log("OnPhotonPlayerDisonnected isMasterClient " + PhotonNetwork.isMasterClient);
                
                LoadArena();
            }
        }

        #endregion

        #endregion

        #region Public Methods

        public void SetGameState(GameState state)
        {
            this.CurrentGameState = state;
            OnStateChange();
        }

        /// <summary>
        /// Called when the local player leaves the room.
        /// </summary>
        public override void OnLeftRoom()
        {
            // Load launcher scene
            SceneManager.LoadScene(0);      // TODO: Consider a refactor to load previous scene;
        }

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion

        #region Private Methods

        protected void LoadArena()
        {
            if (!PhotonNetwork.isMasterClient)
                Debug.LogError("PhotonNetwork : Trying to load a level but we are not the master client");

            Debug.Log("PhotonNetwork : Loading Level. ");
            PhotonNetwork.LoadLevel("Arena");
        }

        

        #endregion
    }
}