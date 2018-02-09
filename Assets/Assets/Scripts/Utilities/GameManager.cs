using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class GameManager : MonoBehaviour
    {
        #region Photon Messages

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

        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
        }

        #endregion
        
    }
}