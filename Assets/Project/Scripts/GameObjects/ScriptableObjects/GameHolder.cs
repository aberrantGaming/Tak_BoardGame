using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class GameHolder : ScriptableObject
    {
        public string GamemodeName;
                
        [SerializeField] private int boardSize;
        public int BoardSize { get { return boardSize; } private set { } }

        [SerializeField] private int stonesCount;
        public int StonesCount { get { return stonesCount; } private set { } }

        [SerializeField] private int capstonesCount;
        public int CapstonesCount { get { return capstonesCount; } private set { } }

        List<Tile> BoardTiles;
        bool BlackWinsTies;

        List<int> defaultStones = new List<int> { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        List<int> defaultCapstones = new List<int> { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        #region Constructor

        /// <summary>
        ///     The GameHolder is the config passed in when a new GameObject is created
        /// </summary>
        public GameHolder()
        {
            // Apply default cfg

            this.GamemodeName = "Default";

            this.boardSize = 5;
            this.BlackWinsTies = true;
            this.stonesCount = defaultStones[boardSize];
            this.capstonesCount = defaultCapstones[boardSize];

            this.BoardTiles = new List<Tile>();

            int numberOfTiles = this.BoardSize * this.BoardSize;
            for (int i = 0; i < numberOfTiles; i++)
            {
                this.BoardTiles.Add(new Tile());
            }
        }
        
        #endregion
        // TODO: setup (object): Function that returns the initial value of G.

        // TODO: moves (object): The keys are move names, and the values are pure functions that return the new value of G once the move has been processed.

        // TODO: flow (object): Arguments to customize the flow of the game.

        // TODO: flow.phases(array): Optional list of game phases.
    }
}