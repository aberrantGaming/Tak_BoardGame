using UnityEngine;
using Com.aberrantGames.Tak.Collectables;

namespace Com.aberrantGames.Tak
{
    public enum PlayerState { Idle, EnteringMatch, InMatch, LeavingMatch}
    public enum CollectionType { Flatstones, Capstones, BoardDesign }

    public delegate void OnPlayerStateChangeHandler();

    [System.Serializable]
    public struct Collection
    {
        [SerializeField] private Flatstones currFlatstones;
        [SerializeField] private Capstones currCapstones;
        [SerializeField] private BoardDesign currBoardDesign;

        public Flatstones CurrentFlatstones { get { return currFlatstones; } private set { } }
        public Capstones CurrentCapstones { get { return currCapstones; } private set { } }
        public BoardDesign CurrentBoardDesign { get { return currBoardDesign; } private set { } }

        public void SetCollectable(CollectionType collectionType, ICollectable collectable)
        {
            switch (collectionType)
            {
                case (CollectionType.Flatstones): SetFlatstones((Flatstones)collectable); break;
                case (CollectionType.Capstones): SetCapstones((Capstones)collectable); break;
                case (CollectionType.BoardDesign): SetBoardDesign((BoardDesign)collectable); break;
            }
        }

        private void SetFlatstones(Flatstones newFlatstones)
        {
            currFlatstones = newFlatstones;
        }

        private void SetCapstones(Capstones newCapstones)
        {
            currCapstones = newCapstones;
        }

        private void SetBoardDesign(BoardDesign newBoardDesign)
        {
            currBoardDesign = newBoardDesign;
        }
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

            Initialize();
        }

        #endregion

        #region Public Variables
        
        public Collection PlayerCollection;

        public PlayerState PlayerState { get; private set; }

        #endregion

        #region Private Variables

        private GameManager gm;
        
        #endregion

        #region MonoBehaviour Callbacks

        protected void OnApplicationQuit()
        {
            PlayerManager.Instance = null;
        }

        #endregion

        #region Public Methods

        public void SetPlayerState(PlayerState state)
        {
            this.PlayerState = state;
            OnStateChange();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Called from Awake() after establishing the Singleton pattern
        /// </summary>
        internal protected void Initialize()
        {
            if (PlayerCollection.CurrentFlatstones == null)
                PlayerCollection.SetCollectable(CollectionType.Flatstones, ScriptableObject.CreateInstance<Flatstones>());

            if (PlayerCollection.CurrentCapstones == null)
                PlayerCollection.SetCollectable(CollectionType.Capstones, ScriptableObject.CreateInstance<Capstones>());
        }

        #endregion
    }
}
