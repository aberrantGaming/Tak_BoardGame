using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Square = Com.aberrantGames.Tak.GameEngine.Tile;

namespace Com.aberrantGames.Tak.GameEngine
{
    public struct Analysis
    {
        int[] WhiteGroups;
        int[] BlackGroups;
    }
    public class Position
    {
        enum WinReason
        {
            RoadWin,
            FlatsWin,
            Resignation
        }

        struct WinDetails
        {
            bool Over;
            WinReason Reason;
            Color Winner;
            int WhiteFlats, BlackFlats;
        }

        #region Variables

        public Config cfg;

        public byte WhiteStones, WhiteCapstones, BlackStones, BlackCapstones;

        public int White, Black, Standing, Caps;
        public int move, hash;

        public int[] Height, Stacks;

        public Analysis analysis;

        #endregion

        /// <summary>
        ///     Create a new Position as a copy of the current board position
        /// </summary>
        /// <returns>Position, as a clone of this.Position</returns>
        public Position? Clone() { }

        public Config Config() { }

        public int Size() { }

        public Color ToMove() { }

        public int MoveNumber() { }

        public int cntWhiteStones() { }

        public int cntBlackStones() { }

        public Square At(int x, int y) { }

        public Stone Top(int x, int y) { }

        public bool HasRoadAt(Color _c, int _x, int _y) { }        // accomodate lack of System.Tuple

        private void GameOver(bool _over, Color _winner) { }

        private int CountFlats(Color _c) { }                        // accomodate lack of System.Tuple        

        public Color FlatsWinner() { }

        private Analysis? Analysis() { }

        public void Analyze() { }

        private WinDetails WinDetails() { }

        public Position? Move(MoveDetail _move)
        {
            return MovePreallocated(_move, null);
        }

        public Position? MovePreallocated(MoveDetail _move, Position? _next)
        {

        }
    }

}
