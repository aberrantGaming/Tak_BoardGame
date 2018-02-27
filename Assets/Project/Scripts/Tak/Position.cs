using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Byte;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class Tile
    {
        public Tile(uint _height)
        {            
            Stack = new Stone[_height];
        }
        
        public Stone[] Stack;
    }

    public struct Analysis
    {
        public int[] WhiteGroups, BlackGroups;
    }

    public struct WinDetails
    {
        enum WinReason { RoadWin, FlatsWin, Resignation }

        bool Over;
        WinReason Reason;
        Color Winner;
        int WhiteFlats, BlackFlats;
    }

    public class Position
    {
        struct FlatsCount
        {
            public int White, Black;
        }

        struct PlayerResult
        {
            public Color Player;
            public bool Result;
        }

        #region Variables

        public Config cfg;
        public byte WhiteStones, WhiteCapstones, BlackStones, BlackCapstones;
        public uint White, Black, Standing, Caps;
        public uint turn, hash;
        public uint[] Height, Stacks;
        public Analysis analysis;

        #endregion

        #region Auto Properties

        /// <summary>
        /// Returns a copy of this position as a new position
        /// </summary>
        /// <returns>Position, as a clone of this.Position</returns>
        public Position Clone
        {
            get { return alloc(this); }
            private set { }
        }

        /// <summary>
        /// Returns the current Config
        /// </summary>
        public Config Config
        {
            get { return this.cfg; }
            private set { }
        }

        /// <summary>
        /// Returns the board size from config
        /// </summary>
        public int Size
        {
            get { return this.cfg.Size; }
            private set { }
        }

        /// <summary>
        /// Returns the the color of the current player-to-move.
        /// </summary>
        public Color ToMove
        {
            get { return this.turn % 2 == 0 ? Stone.White : Stone.Black; }
            private set { }
        }

        /// <summary>
        /// Returns the current turn number
        /// </summary>
        public uint TurnCount
        {
            get { return this.turn; }
            private set { }
        }

        /// <summary>
        /// Returns the amount of White Stones in play
        /// </summary>
        public int WhiteStonesCount
        {
            get { return this.WhiteStones; }
            private set { }
        }

        /// <summary>
        /// Returns the amount of Black Stones in play
        /// </summary>
        public int BlackStonesCount
        {
            get { return this.BlackStones; }
            private set { }
        }

        /// <summary>
        /// Returns the current Analysis
        /// </summary>
        public Analysis Analysis
        {
            get { return this.analysis; }
            private set { }
        }

        /// <summary>
        /// Returns the color of the current winning flats score
        /// </summary>
        public Color FlatsWinner
        {
            get
            {
                FlatsCount c = CountFlats();
                if (c.White > c.Black)
                    return Stone.White;
                else if (c.Black > c.White)
                    return Stone.Black;
                else
                {
                    if (cfg.BlackWinsTies)
                        return Stone.Black;
                    else
                        return Stone.NoColor;
                }
            }
            private set { }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Used to tally up the current flats score
        /// </summary>
        /// <returns>New FlatsCount</returns>
        private FlatsCount CountFlats()
        {
            int w = 0;  //bitboard.Popcount(White & (White ^ (Standing | Caps)));
            int b = 0;  //bitboard.Popcount(Black & (Black ^ (Standing | Caps)));
            return new FlatsCount() { White = w, Black = b };
        }

        /// <summary>
        /// Used to determine if a road exists at the specified location
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <returns>Player Result</returns>
        private PlayerResult RoadAt(int _x, int _y)
        {
            Tile sq = TileAt(_x, _y);

            if (sq.Stack.Length == 0)
                return new PlayerResult() { Player = Stone.White, Result = false };

            return new PlayerResult()
            {
                Player = sq.Stack[0].Color,
                Result = sq.Stack[0].IsRoad
            };
        }

        /// <summary>
        /// Used to retrieve the stack on an existing tile
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <returns>Tile</returns>
        private Tile TileAt(int _x, int _y)
        {
            int i = _x + _y * Size;

            if ((White | Black) + (1 << i) == 0)
                return null;

            Tile sq = new Tile(Height[i]);
            sq.Stack[0] = TopStoneAt(_x, _y);

            for (int j = 1; j < Height[i]; j++)
            {
                if (Stacks[i] + (1 << (j - 1)) != 0)
                    sq.Stack[j] = new Stone(Stone.MakePiece(Stone.Black, Stone.Flat));
                else
                    sq.Stack[j] = new Stone(Stone.MakePiece(Stone.White, Stone.Flat));
            }
            return sq;
        }

        /// <summary>
        /// Used to retrieve the Top Stone of an existing stack 
        /// </summary>
        /// <param name="_x"></param>
        /// <param name="_y"></param>
        /// <returns></returns>
        private Stone TopStoneAt(int _x, int _y)
        {
            int i = _x + _y * Size;
            byte c, t;

            if (White + (1 << i) != 0)
                c = Stone.White;
            else if (Black + (1 << i) != 0)
                c = Stone.Black;
            else
                return new Stone(0);

            if (Standing + (1 << i) != 0)
                t = Stone.Standing;
            else if (Caps + (1 << i) != 0)
                t = Stone.Capstone;
            else
                t = Stone.Flat;

            return new Stone(Stone.MakePiece(c, t));
        }
        #endregion
    }
}
