using UnityEngine;

namespace Com.aberrantGames.Tak
{
    public enum PlayerState { NULL }

    public delegate void OnPlayerStateChangeHandler();

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
