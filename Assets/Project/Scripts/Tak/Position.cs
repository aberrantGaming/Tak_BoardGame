using Com.aberrantGames.Tak.Bits;
using System.Collections.Generic;
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
        public List<ulong> WhiteGroups, BlackGroups;

        public void SetGroups(List<ulong> _groups)
        {
            WhiteGroups = _groups;
            BlackGroups = _groups;
        }
    }

    public class Position
    {
        #region Variables

        // Structs
        struct FlatsCount
        {
            public int White, Black;            
        }

        struct PlayerResult
        {
            public Color Player;
            public bool Result;

            public PlayerResult(bool _result, Color _player)
            {
                Result = _result;
                Player = _player;
            }
        }

        struct WinDetails
        {
            public enum WinReason { RoadWin, FlatsWin, Resignation }

            public bool Over;
            public WinReason Reason;
            public Color Winner;
            public int WhiteFlats, BlackFlats;
        }
        
        // Public Variables
        public Config cfg;

        public byte WhiteStones, WhiteCapstones, BlackStones, BlackCapstones;

        public int turn;

        public ulong White, Black, Standing, Caps;

        public uint[] Height;

        public ulong[] Stacks;

        public Analysis analysis;

        // Private Variables
        private ulong hash;

        #endregion

        #region Properties

        /// <summary>
        /// Returns a copy of this position as a new position
        /// </summary>
        /// <returns>Position, as a clone of this.Position</returns>
        public Position Clone
        {
            get { return Allocate.Alloc(this); }
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
        public int TurnCount
        {
            get { return this.turn; }
            private set { }
        }

        /// <summary>
        /// Used to tally up the current flats score
        /// </summary>
        /// <returns>New FlatsCount</returns>
        private FlatsCount CountFlats
        {
            get
            {
                int w = 0;  //bitboard.Popcount(White & (White ^ (Standing | Caps)));
                int b = 0;  //bitboard.Popcount(Black & (Black ^ (Standing | Caps)));
                return new FlatsCount() { White = w, Black = b };
            }
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
            private set { this.analysis = value; }
        }

        /// <summary>
        /// Returns the color of the current winning flats score
        /// </summary>
        public Color FlatsWinner
        {
            get
            {
                FlatsCount c = CountFlats;
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

        /// <summary>
        /// Used to determine if a player has sucessfully built a road
        /// </summary>
        /// <returns></returns>
        private PlayerResult HasRoad
        {
            get
            {
                PlayerResult white = new PlayerResult() { Player = Stone.White, Result = false };
                PlayerResult black = new PlayerResult() { Player = Stone.Black, Result = false };

                for (ulong g = 0; g < (ulong)Analysis.WhiteGroups.Count; g++)        // _, g := range p.analysis.WhiteGroups
                {
                    if (((g & cfg.c.T) != 0 && (g & cfg.c.B) != 0) ||
                            ((g & cfg.c.L) != 0 && (g & cfg.c.R) != 0))
                        white.Result = true;
                    break;
                }

                for (ulong g = 0; g < (ulong)Analysis.BlackGroups.Count; g++)
                {
                    if (((g & cfg.c.T) != 0 && (g & cfg.c.B) != 0) ||
                            ((g & cfg.c.L) != 0 && (g & cfg.c.R) != 0))
                        black.Result = true;
                    break;
                }

                if (white.Result && black.Result)
                {
                    if (ToMove == White)
                        return black;

                    return white;
                }
                else if (white.Result)
                    return white;
                else if (black.Result)
                    return black;
                else
                    return new PlayerResult() { Player = Stone.NoColor, Result = false };
            }
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

            if (((White | Black) & (uint)(1 << i)) == 0)
                return null;

            Tile sq = new Tile(Height[i]);
            sq.Stack[0] = TopStoneAt(_x, _y);

            for (int j = 1; j < Height[i]; j++)
            {
                if ((Stacks[i] & (ulong)1 << (j - 1)) != 0)
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

            if (White + (uint)(1 << i) != 0)
                c = Stone.White;
            else if (Black + (uint)(1 << i) != 0)
                c = Stone.Black;
            else
                return new Stone(0);

            if (Standing + (uint)(1 << i) != 0)
                t = Stone.Standing;
            else if (Caps + (uint)(1 << i) != 0)
                t = Stone.Capstone;
            else
                t = Stone.Flat;

            return new Stone(Stone.MakePiece(c, t));
        }

        /// <summary>
        /// Runs an analysis of this position
        /// </summary>
        public void Analyze()
        {
            ulong wr = White & ~Standing;
            ulong br = Black & ~Standing;
            List<ulong> alloc = Analysis.WhiteGroups;

            analysis.WhiteGroups = Bitboard.FloodGroups(cfg.c, wr, alloc);
            alloc = analysis.WhiteGroups;
            alloc = new List<ulong>(alloc.Capacity);
            analysis.BlackGroups = Bitboard.FloodGroups(cfg.c, br, alloc);
        }
        
        /// <summary>
        /// Determines if the game is over and returns a winner if so.
        /// </summary>
        /// <param name="_over"></param>
        /// <param name="_winner"></param>
        /// <returns>PlayerResult</returns>
        private PlayerResult GameOver
        {
            get
            {
                if (HasRoad.Result == true)
                    return new PlayerResult(true, HasRoad.Player);

                if ((WhiteStones + WhiteCapstones) != 0 &&
                        (BlackStones + BlackCapstones) != 0 &&
                        (White | Black) != cfg.c.Mask)
                {
                    return new PlayerResult(false, Stone.NoColor);
                }

                return new PlayerResult(true, FlatsWinner);
            }
        }

        /// <summary>
        /// Returns the match results.
        /// </summary>
        /// <returns></returns>
        private WinDetails WinResults
        {
            get
            {
                PlayerResult check = GameOver;

                WinDetails retVal;

                retVal.Over = check.Result;
                retVal.Winner = check.Player;
                retVal.WhiteFlats = CountFlats.White;
                retVal.BlackFlats = CountFlats.Black;

                if (HasRoad.Result == true)
                    retVal.Reason = WinDetails.WinReason.RoadWin;
                else
                    retVal.Reason = WinDetails.WinReason.FlatsWin;

                return retVal;
            }
        }

        #endregion
    }
}
