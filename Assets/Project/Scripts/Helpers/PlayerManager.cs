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

        public Queue<Stone> Hand { get; private set; }      // This object represents what the player is "holding" in hand


        //public bool PlayersTurn { get { return (gm.CurrentGameState == GameState.PLAYERS_TURN); } private set { } }
        public bool PlayersTurn = true;


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
        CommandManager cm = new CommandManager();

        int CarryLimit = 5;
        StoneColor PlayerColor;

        #endregion

        #region MonoBehaviour Callbacks

        protected void Update()
        {
            if (PlayersTurn)
                HandleInput();
        }

        protected void FixedUpdate()
        {
            cm.ProcessPendingCommands();
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

        #region Public Methods

        public void StackPickup(Queue<Stone> _stack)
        {
            Hand = _stack;
        }

        #endregion

        #region Private Methods

        protected void HandleOnStateChange()
        {
            switch (gm.CurrentGameState)
            {
                case (GameState.STARTING_MATCH): OnMatchStarting(); break;
            }
        }

        protected void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, 100.0f))
                {
                    GameObject selectedTransform = hit.transform.gameObject;

                    Stone selectedStone = selectedTransform.GetComponent<Stone>();
                    if (selectedStone != null)
                        RequestStonePickup(selectedStone);

                    Tile selectedTile = selectedTransform.GetComponent<Tile>();
                    if (selectedTile != null)
                        SelectTile(selectedTile);
                    
                    StonesBag selectedStonesBag = selectedTransform.GetComponent<StonesBag>();
                    if (selectedStonesBag != null)
                        SelectStonesBag(selectedStonesBag);                  
                }
            }
        }

        /// <summary>
        ///     Request to pick up a stack from the tile.
        /// </summary>
        private void RequestStackPickup(Tile _source)
        {
            Debug.Log("Requesting Pickup of Stack at : " + _source.tileID);

            PickupStack pickupCommand = new PickupStack(_source, PlayerColor, CarryLimit);
            cm.AddCommand(pickupCommand);
        }

        private void RequestStonePickup(Stone _stone)
        {
            Debug.Log("Requesting Pickup of : " + _stone.StoneType.ToString() + " : " + _stone.name.ToString());
        }

        private void SelectTile(Tile _tile)
        {
            Debug.Log("Tile Selected: " + _tile.name);
        }

        private void SelectStonesBag(StonesBag _stonesBag)
        {
            Debug.Log("Stones Bag Selected: " +_stonesBag.name);
        }

        private void OnMatchStarting()
        {
            CarryLimit = 5;                     // TODO: REPLACE with dynamic callback to the match manager's board size
            PlayerColor = StoneColor.LIGHT;     // TODO: REPLACE with dynamic callback to the match manager's assigned colors
        }

        #endregion

    }
}
