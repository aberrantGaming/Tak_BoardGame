using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    public class PlayerManager : MonoBehaviour
    {
        #region Singleton Pattern

        /// <summary>
        /// Ensures that our class has only one instance and provides a global point of access to it.
        /// </summary>
        protected PlayerManager() { }
        public static PlayerManager Instance { get; private set; }

        protected void Awake()
        {
            if (PlayerManager.Instance == null)
                Instance = this;
            else {
                DestroyObject(this);
                return;
            }
        }

        #endregion

        #region  Properties

        public bool PlayersTurn { get { return (gm.CurrentGameState == GameState.PLAYERS_TURN); } private set { } }


        // Expose our Auto Properties to the Unity Inspector.
        [SerializeField]
        private Box _currentBox;
        public Box CurrentBox { get { return _currentBox; } private set {  } }

        [SerializeField]
        private Board _currentBoard;
        public Board CurrentBoard { get { return _currentBoard; } private set { } }

        [SerializeField]
        private Stones _currentStones;
        public Stones CurrentStones { get { return _currentStones; } private set { } }

        #endregion

        #region Private Variables

        GameManager gm;


        Queue<Stone> StonesBag = new Queue<Stone>();    // This object represents the "deck" that the player draws new stones from
        Queue<Stone> HeldStack = new Queue<Stone>();    // This object represents what the player is "holding" in hand

        #endregion

        #region MonoBehaviour Callbacks

        protected void Update()
        {
            if (PlayersTurn)
                HandleInput();
        }

        /// <summary>
        ///     This method is called from Awake() within our Singleton Pattern
        /// </summary>
        protected void Init()
        {
            Debug.Log("Player Manager Init()");

            gm = GameManager.Instance;
            gm.OnStateChange += HandleOnStateChange;
        }

        #endregion

        #region Private Methods

        protected void HandleOnStateChange()
        {
        }

        protected void HandleInput()
        {

        }

        /// <summary>
        ///     Request a new Stone from the StonesBag
        /// </summary>
        private void DrawStone()
        {
            // Clear the current selection and draw a new stone.
            HeldStack.Clear();
            HeldStack.Enqueue(StonesBag.Dequeue());
        }

        private void PlaceStone(Tile destination, Stone stone)
        {
            if (!destination.IsBlocked)
                destination.Stack.Enqueue(HeldStack.Dequeue());
        }

        #endregion

    }
}
