using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak
{
    public class Gamemode : ScriptableObject
    {
        #region Properties

        // |    General Properties  | \\
        public string GameModeName { get { return gamemodeName; } private set { } }
        public string GamemodeDescription { get { return gamemodeDesc; } private set { } }

        // |    Match Properties    | \\
        public int ScoreToWin {  get { return scoreToWin; } private set { } }
        public bool LightPlaysFirst { get { return lightPlaysFirst; } private set { } }
        public bool DarkWinsTies { get { return darkWinsTies; } private set { } }

        // |    Config Properties   | \\
        public int BoardSize { get { return boardSize; } private set { } }
        public int StonesCount { get { return flatstonesCount; } private set { } }
        public int CapstonesCount { get { return capstonesCount; } private set { } }
        
        public GameEngine.Config Config
        {
            get
            {
                GameEngine.Config retVal = new GameEngine.Config
                {
                    Size = boardSize,
                    Pieces = flatstonesCount,
                    Capstones = capstonesCount,
                    BlackWinsTies = darkWinsTies
                };

                return retVal;
            }
        }

        #endregion

        #region Private Variables

        [SerializeField] private string gamemodeName = "Default";               // Gamemode name
        [SerializeField] private string gamemodeDesc = "Default Gamemode";      // Gamemode description
        [SerializeField] private int scoreToWin = 1;                            // Score required to win a match
        [SerializeField] private bool lightPlaysFirst = false;                  // First-to-play is decided by coin if false
        [SerializeField] private bool darkWinsTies = true;                      // Ties result in no score being awarded for a round if false;
        [SerializeField] private int boardSize = 5;                             // Dimensions of the playable gameboard
        [SerializeField] private int flatstonesCount = 0;                       // Number of flatstones that each player starts with
        [SerializeField] private int capstonesCount = 0;                        // Number of capstones that each player starts with
        
        #endregion
    }
}