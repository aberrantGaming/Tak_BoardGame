using UnityEngine;

namespace aberrantGames.Tak
{
    public enum GameState { NULL, INTRO, MENU, PAUSED, PREGAME, GAME, POSTGAME, QUIT }

    public delegate void OnGameStateChangeHandler();

    public class GameManager : MonoBehaviour
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
                Object.Destroy(this);
        }

        #endregion

        #region Properties

        public GameState GameState  { get; private set; }

        #endregion

        #region Public Variables
        
        #endregion

        #region Private Variables

        private const bool DEBUG_ENABLED = true;
        
        #endregion

        #region MonoBehaviour Callbacks

        protected void Start()
        {
        }

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

        #region private methods


        #endregion
    }
}