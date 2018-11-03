using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aberrantGames.Tak_ForUnity
{
    public enum StoneColor { Primary, Secondary }
    public enum StoneType { Flat, Standing, Capstone }
    
    public class Stone
    {
        public StoneColor Color;
        public StoneType Type;
    }

    public struct Stack
    {
        public List<Stone> Stones;
    }

    /// <summary>
    /// This object holds the gameboard meta data
    /// </summary>
    [System.Serializable]
    public struct Config
    {
        public int boardSize, stonesCount, capstonesCount;
        public bool pushTiesToSecondary;

        // Default counts for stones and capstones
        private static readonly int[] defualtStoneCount = new int[] { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        private static readonly int[] defaultCapstoneCount = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        public Config(int _boardSize)
        {           
            boardSize = Mathf.Clamp(_boardSize, 3, 8);  

            stonesCount = defualtStoneCount[_boardSize];
            capstonesCount = defaultCapstoneCount[_boardSize];

            pushTiesToSecondary = true;  // i.e. Black wins in a tie by default
        }
    }

    /// <summary>
    /// This object holds a snapshot of the gameboard's position for a single turn
    /// </summary>
    [System.Serializable]
    public class Position
    {
        protected int Turn;
        private Config Config;
        protected List<Stack> Gameboard;

        public Position(Config _c)
        {
            Config = _c;
            Gameboard = new List<Stack> { Capacity = (Config.boardSize * Config.boardSize) };
        }

        #region Exposure Methods

        public int Size
        {
            get { return Config.boardSize; }
            private set { }
        }

        public Config Cfg
        {
            get { return Config; }
            private set { }
        }

        public int TurnNumber
        {
            get { return Turn; }
            private set { }
        }

        public StoneColor ToMove
        {
            get { return ((Turn % 2) == 0) ? StoneColor.Primary : StoneColor.Secondary; }
            private set { }
        }

        #endregion
    }
}
