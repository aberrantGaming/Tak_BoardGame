using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class GameMode : ScriptableObject
    {
        #region Exposed Auto-Properties

        [SerializeField] private string gameModeName;
        public string GameModeName { get { return gameModeName; } private set { } }

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
        
        public GameMode()
        {
            this.gameModeName = "Default";
            
            this.boardSize = 3;
            this.playerCount = 1;
            this.blackWinsTies = true;
            this.stonesCount = 0;
            this.capstonesCount = 0;
        }

        public Config Config
        {
            get
            {
                Config retVal = new Config
                {
                    Size = boardSize,
                    Pieces = stonesCount,
                    Capstones = capstonesCount,
                    BlackWinsTies = blackWinsTies
                };

                return retVal;
            }
        }
    }
}