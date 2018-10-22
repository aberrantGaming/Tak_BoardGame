using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.aberrantGames.Tak.GameEngine;

namespace Com.aberrantGames.Tak
{
    public enum GameState { NULL, INTRO, PAUSED, LAUNCHER, IN_MATCH, FINISHING_MATCH, QUIT }

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
                DestroyObject(this);
        }

        #endregion

        #region Properties

        public GameState GameState
        {
            get;
            private set;
        }

        public MatchSettings Gamemode
        {
            get
            {
                return gamemode;
            }
            private set { }
        }

        private Player MatchWinner
        {
            get
            {
                foreach (byte player in matchPlayers.Keys)
                {
                    if (matchPlayers[player].matchScore == Gamemode.ScoreToWin)
                        return matchPlayers[player];
                }

                return null;
            }
        }

        #endregion

        #region Public Variables

        public float StartDelay = 3f;
        public float FinalDelay = 3f;

        #endregion

        #region Private Variables

        private const bool DEBUG_ENABLED = true;

        [SerializeField] MatchSettings gamemode;

        private int roundNumber;
        private byte firstToPlay;
        private WaitForSeconds startWait, finalWait;
        private Player roundWinner, matchWinner;
        private List<Player> turnOrder;
        private IDictionary<byte, Player> matchPlayers;
        private IDictionary<Player, GameObject> playerPrefabs;
        
        protected Gameboard gameboard;

         #endregion

        #region MonoBehaviour Callbacks

        protected void Start()
        {
            startWait = new WaitForSeconds(StartDelay);
            finalWait = new WaitForSeconds(FinalDelay);            
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

        public void StartMatch()
        {
            matchPlayers = new Dictionary<byte, Player>(2)
            {
                { Stone.White, GetComponent<Player>()},
                { Stone.Black, GetComponent<Player>()}
            };

            firstToPlay = Stone.White;
            //    gamemode.LightPlaysFirst
            //    ? Stone.White
            //    : (Random.value > 0.5f ? Stone.Black : Stone.White);
            
            StartCoroutine(MatchFlow());
        }

        #region Private Methods

        private IEnumerator MatchFlow()
        {
            yield return StartCoroutine(RoundStarting());
            yield return StartCoroutine(RoundPlaying());
            yield return StartCoroutine(RoundEnding());

            if (matchWinner != null)
            {
                SetGameState(GameState.FINISHING_MATCH);
            }
            else
            {
                if (DEBUG_ENABLED) Debug.Log("restarting MatchFlow()");
                StartCoroutine(MatchFlow());
            }
        }

        private IEnumerator RoundStarting()
        {
            if (DEBUG_ENABLED) Debug.Log("RoundStarting..");

            // As soon as the round starts, reset board position and disable player control
            gameboard = Gameboard.MakeGameboard(
                Game.New(gamemode.Config),
                matchPlayers[firstToPlay].PlayerCollection.CurrentBoardDesign);

            // TO DO : implement player input
            if (DEBUG_ENABLED) Debug.Log("Player Input Passed.");

            // Increment the round number
            roundNumber++;

            // determine turn order
            if (roundNumber%2 == 0)
            {
                if (firstToPlay != Stone.White)
                    turnOrder = new List<Player>(2)
                    {
                        { matchPlayers[Stone.White] },
                        { matchPlayers[Stone.Black] }
                    };
                else
                    turnOrder = new List<Player>(2)
                    {
                        { matchPlayers[Stone.Black] },
                        { matchPlayers[Stone.White] }
                    };
            }
            else
            {
                if (firstToPlay == Stone.White)
                    turnOrder = new List<Player>(2)
                    {
                        { matchPlayers[Stone.White] },
                        { matchPlayers[Stone.Black] }
                    };
                else
                    turnOrder = new List<Player>(2)
                    {
                        { matchPlayers[Stone.Black] },
                        { matchPlayers[Stone.White] }
                    };
            }
                        
            // Draw the gameboard
            gameboard.DrawGameBoard();

            // Snap the camera's zoom and position to something appropriate
            
            // wait for the specified length of time until yeilding control back to the game loop
            yield return startWait;     
        }

        private IEnumerator RoundPlaying()
        {
            if (DEBUG_ENABLED) Debug.Log("RoundStarting..");

            // enable player control
            // TO DO : implement player input

            // while there is not a winner return on the next frame
            while (!GameEngine.Game.WinDetails(gameboard.Position).Over)
                yield return null;
        }

        private IEnumerator RoundEnding()
        {
            // disable player control
            // TO DO : implement player input

            // clear the winner from the previous round
            roundWinner = null;

            // see if there is a winner from this round
            roundWinner = matchPlayers[Game.WinDetails(gameboard.Position).Winner];

            // if there is a winner, increment their score
            if (roundWinner != null)
                roundWinner.matchScore++;

            // see if someone has won the match
            matchWinner = MatchWinner;

            // Wait for the specified length of time until yielding control back to the game loop.
            yield return finalWait;     
        }



        #endregion
    }
}