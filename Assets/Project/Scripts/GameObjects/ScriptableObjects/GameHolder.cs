using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class GameHolder : ScriptableObject
    {
        #region Exposed Auto-Properties

        [SerializeField] private string gamemodeName;
        public string GamemodeName { get { return gamemodeName; } private set { } }

        [SerializeField] private int boardSize;
        public int BoardSize { get { return boardSize; } private set { } }

        [SerializeField] private byte playerCount;
        public byte PlayerCount { get { return playerCount; } private set { } }

        [SerializeField] private bool blackWinsTies;
        public bool BlackWinsTies { get { return blackWinsTies; } private set { } }

        [SerializeField] private int stonesCount;
        public int StonesCount { get { return stonesCount; } private set { } }

        [SerializeField] private int capstonesCount;
        public int CapstonesCount { get { return capstonesCount; } private set { } }
        
        #endregion

        #region Private Variables

        List<Tile> BoardTiles;

        #endregion

        #region Constructor

        public GameHolder()
        {
            // Apply default cfg

            this.GamemodeName = "Default";

            this.boardSize = 3;
            this.playerCount = 1;
            this.blackWinsTies = true;
            this.stonesCount = 0;
            this.capstonesCount = 0;

            this.BoardTiles = new List<Tile>();
        }

        #endregion

        #region Public Methods

        public void SetStonesCount(int c)
        {
            stonesCount = c;
        }

        public void SetCapstonesCount(int c)
        {
            capstonesCount = c;
        }

        #endregion

        // TODO: setup (object): Function that returns the initial value of G.

        // TODO: moves (object): The keys are move names, and the values are pure functions that return the new value of G once the move has been processed.
        // Something like IDictionary<Move, void> MoveCallbacks;

        // TODO: flow (object): Arguments to customize the flow of the game.

        // TODO: flow.phases(array): Optional list of game phases.
    }
}