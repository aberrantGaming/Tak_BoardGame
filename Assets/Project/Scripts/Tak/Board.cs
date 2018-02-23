using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Square = Com.aberrantGames.Tak.GameEngine.Tile;


namespace Com.aberrantGames.Tak.GameEngine
{
    enum WinReason {
        RoadWin,
        FlatsWin,
        Resignation
    }

    public struct Config
    {
        int Size, Pieces, Capstones;
        bool BlackWinsTies;
        // c bitboard.Constants
    }

    public struct Position { }

    public struct Analysis { }

    public struct WinDetails { }

    public class Board
    {
        #region Variables

        static int[] defualtPieces = new int[] { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        static int[] defaultCapstones = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        Position p;

        #endregion

        #region Properties

        public int Size() { }
        public Config Config() { }
        public Color ToMove() { }
        public int MoveNumber() { }
        public int WhiteStones() { }
        public int BlackStones() { }

        #endregion

        #region Public Methods

        public Position? New(Config _game) { }
        public Position? Clone() { }
        public Position? FromSquares() { }

        public Square At(int x, int y) { }
        public Stone Top(int x, int y) { }
        public Color FlatsWinner() { }
        public bool HasRoadAt(Color _c, int _x, int _y) { }        // accomodate lack of System.Tuple

        public void Set(Position? _p, int _x, int _y, Square _s) { }
        public void Analyze() { }

        #endregion

        #region Private Methods

        private void GameOver(bool _over, Color _winner) { }

        private Analysis? Analysis() { }

        private int CountFlats(Color _c) { }                        // accomodate lack of System.Tuple
        
        private WinDetails WinDetails() { }

        #endregion
    }
}
