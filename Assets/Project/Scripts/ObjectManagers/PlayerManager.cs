﻿using UnityEngine;
using Com.aberrantGames.Tak.Collectables;

namespace Com.aberrantGames.Tak
{
    public enum PlayerState { NULL }

    public delegate void OnPlayerStateChangeHandler();

    [System.Serializable]
    public struct Collection
    {
        [SerializeField] private BoardCollectable board;
        public BoardCollectable Board { get { return board; } private set { } }

        [SerializeField] private TableCollectable table;
        public TableCollectable Table { get { return table; } private set { } }

        [SerializeField] private StonesCollectable stones;
        public StonesCollectable Stones { get { return stones; } private set { } }
    }

    public class PlayerManager : MonoBehaviour
    {
        #region Singleton Pattern

        /// <summary>
        /// Ensures that our class has only one instance and provides a global point of access to it.
        /// </summary>
        protected PlayerManager() { }
        public event OnPlayerStateChangeHandler OnStateChange;

        public static PlayerManager Instance { get; private set; }

        protected void Awake()
        {
            if (PlayerManager.Instance == null)
                Instance = this;
            else
                DestroyObject(this);
        }

        #endregion

        #region Public Variables
        
        public Collection PlayerCollection;

        #endregion

        #region Private Variables

        public PlayerState PlayerState { get; private set; }

        #endregion

        #region MonoBehaviour Callbacks

        protected void OnApplicationQuit()
        {
            PlayerManager.Instance = null;
        }

        #endregion

        #region Public Methods

        public void SetGameState(PlayerState state)
        {
            this.PlayerState = state;
            OnStateChange();
        }

        #endregion
    }
}