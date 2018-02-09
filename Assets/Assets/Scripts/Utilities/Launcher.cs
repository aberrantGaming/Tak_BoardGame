﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class Launcher : Photon.PunBehaviour
    {
        #region Public Variables

        /// <summary>
        /// The PUN logLevel.
        /// </summary>
        public PhotonLogLevel Loglevel = PhotonLogLevel.Informational;

        /// <summary>
        /// The maximum number of players per room. When a room is full, it can't be joined by new players, and so a new room will be created.
        /// </summary>
        [Tooltip("The maximum number of players per room. When a room is full, it can't be joined by new players, and so a new room will be created.")]
        public byte MaxPlayersPerRoom = 2;

        [Tooltip("The UI Panel for the main menu")]
        public GameObject controlPanel;
        [Tooltip("The UI Label to inform the user that the connection is in progress")]
        public GameObject progressLabel;

        #endregion

        #region Private Variables

        /// <summary>
        /// This Client's version number. Users are seperated from each other by gameversion (which allows you to make breaking changes).
        /// </summary>
        string _gameVersion = "1";

        #endregion

        #region MonoBehaviour Callbacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during early initialization phase.
        /// </summary>
        private void Awake()
        {
            // #NotImportant
            // Force LogLevel.
            PhotonNetwork.logLevel = Loglevel;

            // #Critical 
            // we don't join the lobby. There is no need to join a lobby to get the list of rooms.
            PhotonNetwork.autoJoinLobby = false;

            // #Critical
            // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically.
            PhotonNetwork.automaticallySyncScene = true;
        }

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        private void Start()
        {
            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Start the connection process.
        /// - If already connected, we attempt joining a random room.
        /// - If not already connected, connect this application instance to Photon Cloud Network.
        /// </summary>
        public void Connect()
        {
            progressLabel.SetActive(true);
            controlPanel.SetActive(false);

            // we check if we are connected or not, we join if we are, else we initiate the connection to the server.
            if (PhotonNetwork.connected)
            {
                // #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
                PhotonNetwork.JoinRandomRoom();
            }
            else
            {
                // #Critical, we must first and foremost connect to Photon Online Server.
                PhotonNetwork.ConnectUsingSettings(_gameVersion);
            }
        }

        #endregion

        #region Photon.PunBehaviour CallBacks

        public override void OnConnectedToMaster()
        {
            Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN");

            // #Critical: The first thing we try to do is to join a potential existing room. If there is, good, else we'll be called back with OnPhotonRandomJoinFailed()
            PhotonNetwork.JoinRandomRoom();
        }

        public override void OnDisconnectedFromPhoton()
        {
            Debug.LogWarning("DemoAnimator/Launcher: OnDisconnectedFromPhoton() was called by PUN");

            progressLabel.SetActive(false);
            controlPanel.SetActive(true);
        }

        public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
        {
            Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN.");
            // #Critical: we failed to join a room, maybe none exists or they are all full. No worries, we create a new room.
            PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = MaxPlayersPerRoom }, null);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log("DemoAnimator/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
    }
}