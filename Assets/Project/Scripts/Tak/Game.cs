﻿using Com.aberrantGames.Tak.Bits;
using System.Collections.Generic;
using UnityEngine;
using Color = System.Byte;
using Piece = System.Byte;
using Square = Com.aberrantGames.Tak.GameEngine.Tile;

namespace Com.aberrantGames.Tak.GameEngine
{
    public enum WinReason
    {
        RoadWin,
        FlatsWin,
        Resignation
    }

    public struct Config
    {
        public int Size, Pieces, Capstones;
        public bool BlackWinsTies;
        public Constants c;
    }

    public class Position
    {
        public Config cfg;
        public byte WhiteStones, WhiteCapstones, BlackStones, BlackCapstones;
        public int turn;
        public ulong White, Black, Standing, Caps;
        public uint[] Height;
        public ulong[] Stacks;
        public Analysis analysis;
        
        public ulong hash;
    }

    public struct Analysis
    {
        public List<ulong> WhiteGroups, BlackGroups;
    }

    public class Tile
    {
        public Piece[] Stack;

        public Tile(uint size)
        {
            Stack = new Piece[size];
        }
    }

    public struct FlatsCount
    {
        public int White, Black;
    }

    public struct WinDetails
    {
        public bool Over;
        public WinReason Reason;
        public Color Winner;
        public int WhiteFlats, BlackFlats;
    }

    public struct PlayerResult
    {
        public Color Player;
        public bool Result;

        public PlayerResult(bool _result, Color _player)
        {
            Result = _result;
            Player = _player;
        }
    }

    public static class Game
    {
        static int[] defualtPieces = new int[] { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        static int[] defaultCapstones = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        #region Public Methods

        /// <summary>
        /// Initializes a new game based on the provided Configuration
        /// </summary>
        /// <param name="g"></param>
        /// <returns></returns>
        public static Position New(Config g)
        {
            if (g.Pieces == 0)
                g.Pieces = defualtPieces[g.Size];
            if (g.Capstones == 0)
                g.Capstones = defualtPieces[g.Size];

            g.c = Bitboard.Precompute((uint)g.Size);

            Position p = new Position()
            {
                cfg = g,
                WhiteStones = (byte)g.Pieces,
                WhiteCapstones = (byte)g.Capstones,
                BlackStones = (byte)g.Pieces,
                BlackCapstones = (byte)g.Capstones,
                turn = 0,

                //hash = fnvBasis
            };
            return p;
        }

        /// <summary>
        /// Returns a copy of this position as a new position
        /// </summary>
        /// <returns></returns>
        public static Position Clone(this Position p)
        {
            return Allocation.Alloc(p);
        }

        /// <summary>
        /// Initializes a position with the specified tiles and move number.
        /// </summary>
        /// <returns></returns>
        public static Position FromSquares(Config cfg, Square[][] board, int _move)
        {
            Position p = New(cfg);
            p.turn = _move;

            for (int y = 0; y < p.Size(); y++)
            {
                for (int x = 0; x < p.Size(); x++)
                {
                    Square sq = board[y][x];
                    if (sq.Stack.Length == 0)
                        continue;
                    int i = x + y * p.Size();
                    switch (sq.Stack[0].Color())
                    {
                        case Stone.White: p.White |= (uint)(1 << i); break;
                        case Stone.Black: p.Black |= (uint)(1 << i); break;
                    }
                    switch (sq.Stack[0].Type())
                    {
                        case Stone.Capstone: p.Caps |= (uint)(1 << i); break;
                        case Stone.Standing: p.Standing |= (uint)(1 << i); break;
                    }

                    int j = -1;                    
                    foreach (Piece piece in sq.Stack)
                    {
                        j++;

                        if (piece == Stone.MakePiece(Stone.White, Stone.Capstone))
                            p.WhiteCapstones--;
                        else if (piece == Stone.MakePiece(Stone.Black, Stone.Capstone))
                            p.BlackCapstones--;
                        else if (piece == Stone.MakePiece(Stone.White, Stone.Flat)
                            || piece == Stone.MakePiece(Stone.White, Stone.Standing))
                            p.WhiteStones--;
                        else if (piece == Stone.MakePiece(Stone.Black, Stone.Flat)
                            || piece == Stone.MakePiece(Stone.Black, Stone.Standing))
                            p.BlackStones--;
                        else
                        {
                            Debug.LogError("bad stone");
                            return null;
                        }

                        if (j == 0)
                            continue;

                        if (piece.Color() == Stone.Black)
                        {
                            p.Stacks[i] |= (uint)(1 << (j - 1));
                        }
                    }
                    p.Height[i] = (uint)sq.Stack.Length;
                    //p.hash ^= p.hashAt(i);
                }
            }

            p.analyze();
            return p;
        }

        #endregion

        #region Position Extension Methods

        public static int Size(this Position p)
        {
            return p.cfg.Size;
        }

        public static Config Config(this Position p)
        {
            return p.cfg;
        }

        public static Square At(this Position p, int x, int y)
        {
            int i = x + y * p.Size();

            if (((p.White | p.Black) & (uint)(1 << i)) == 0)
                return null;

            Square sq = new Square(p.Height[i]);
            sq.Stack[0] = p.Top(x, y);

            for (int j = 1; j < p.Height[i]; j++)
            {
                if ((p.Stacks[i] & (ulong)1 << (j - 1)) != 0)
                    sq.Stack[j] = Stone.MakePiece(Stone.Black, Stone.Flat);
                else
                    sq.Stack[j] = Stone.MakePiece(Stone.White, Stone.Flat);
            }

            return sq;
        }

        public static Piece Top(this Position p, int x, int y)
        {
            uint i = (uint)(x + y * p.Size());
            byte c, t;

            if ((p.White & (uint)(1 << (int)i)) != 0)
                c = Stone.White;
            else if ((p.Black & (uint)(1 << (int)i)) != 0)
                c = Stone.Black;
            else
                return 0;

            if ((p.Standing & (uint)(1 << (int)i)) != 0)
                t = Stone.Standing;
            else if (p.Caps + (uint)(1 << (int)i) != 0)
                t = Stone.Capstone;
            else
                t = Stone.Flat;

            return Stone.MakePiece(c, t);
        }

        public static void Set(this Position p, int x, int y, Square s)
        {
            int i = (y * p.cfg.Size + x);
            p.White &= p.White ^ (uint)(1 << i);
            p.Black &= p.Black ^ (uint)(1 << i);
            p.Standing &= p.Standing ^ (uint)(1 << i);
            p.Caps &= p.Caps ^ (uint)(1 << i);
            if (s.Stack.Length == 0)
            {
                p.Height[i] = 0;
                return;
            }

            switch (s.Stack[0].Color())
            {
                case (Stone.White): p.White |= (uint)(1 << i); break;
                case (Stone.Black): p.Black |= (uint)(1 << i); break;
            }

            switch (s.Stack[0].Type())
            {
                case (Stone.Standing): p.Standing |= (uint)(1 << i); break;
                case (Stone.Capstone): p.Caps |= (uint)(1 << i); break;
            }

            //p.hash = ~p.hashAt(i);

            p.Height[i] = (uint)s.Stack.Length;
            p.Stacks[i] = 0;
            for (int j = 0; j <= s.Stack.Length; j++)
            {
                if (s.Stack[j].Color() == Stone.Black)
                    p.Stacks[i] |= (uint)(1 << j);
            }
            //p.hash = ~p.hashAt(i);
        }

        public static Color ToMove(this Position p)
        {
            if ((p.turn % 2) == 0)
            {
                return Stone.White;
            }
            return Stone.Black;
        }

        public static int TurnNumber(this Position p)
        {
            return p.turn;
        }

        public static int WhiteStones(this Position p)
        {
            return p.WhiteStones;
        }

        public static int BlackStones(this Position p)
        {
            return p.BlackStones;
        }

        public static PlayerResult GameOver(this Position p)
        {
            if (p.HasRoad().Result == true)
                return new PlayerResult(true, p.HasRoad().Player);

            if ((p.WhiteStones + p.WhiteCapstones) != 0 &&
                    (p.BlackStones + p.BlackCapstones) != 0 &&
                    (p.White | p.Black) != p.cfg.c.Mask)
            {
                return new PlayerResult(false, Stone.NoColor);
            }

            return new PlayerResult(true, p.FlatsWinner());
        }

        public static PlayerResult RoadAt(this Position p, int x, int y)
        {
            Square sq = p.At(x, y);

            if (sq.Stack.Length == 0)
                return new PlayerResult() { Player = Stone.White, Result = false };

            return new PlayerResult()
            {
                Player = sq.Stack[0].Color(),
                Result = sq.Stack[0].IsRoad()
            };
        }

        public static PlayerResult HasRoad(this Position p)
        {
            PlayerResult white = new PlayerResult() { Player = Stone.White, Result = false };
            PlayerResult black = new PlayerResult() { Player = Stone.Black, Result = false };

            foreach (ulong g in p.analysis.WhiteGroups)
            {
                if (((g & p.cfg.c.T) != 0 && (g & p.cfg.c.B) != 0) ||
                        ((g & p.cfg.c.L) != 0 && (g & p.cfg.c.R) != 0))
                    white.Result = true;
                break;
            }

            foreach (ulong g in p.analysis.BlackGroups)
            {
                if (((g & p.cfg.c.T) != 0 && (g & p.cfg.c.B) != 0) ||
                        ((g & p.cfg.c.L) != 0 && (g & p.cfg.c.R) != 0))
                    black.Result = true;
                break;
            }

            if (white.Result && black.Result)
            {
                if (p.ToMove() == Stone.White)
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

        public static Analysis Analysis(this Position p)
        {
            return p.analysis;
        }

        public static void analyze(this Position p)
        {
            ulong wr = p.White & ~p.Standing;
            ulong br = p.Black & ~p.Standing;
            List<ulong> alloc = p.analysis.WhiteGroups;

            p.analysis.WhiteGroups = Bits.Bitboard.FloodGroups(p.cfg.c, wr, alloc);
            alloc = p.analysis.WhiteGroups;
            alloc = new List<ulong>(alloc.Capacity);
            p.analysis.BlackGroups = Bits.Bitboard.FloodGroups(p.cfg.c, br, alloc);
        }

        public static FlatsCount CountFlats(this Position p)
        {
            FlatsCount retVal = new FlatsCount
            {
                White = (int)Bits.Bitboard.Popcount(p.White & ~(p.Standing | p.Caps)),
                Black = (int)Bits.Bitboard.Popcount(p.Black & ~(p.Standing | p.Caps))
            };

            return retVal;
        }

        public static Color FlatsWinner(this Position p)
        {
            FlatsCount c = p.CountFlats();
            if (c.White > c.Black)
                return Stone.White;
            else if (c.Black > c.White)
                return Stone.Black;
            else
            {
                if (p.cfg.BlackWinsTies)
                    return Stone.Black;
                else
                    return Stone.NoColor;
            }
        }

        public static WinDetails WinDetails(this Position p)
        {
            PlayerResult check = p.GameOver();

            WinDetails retVal;

            retVal.Over = check.Result;
            retVal.Winner = check.Player;
            retVal.WhiteFlats = p.CountFlats().White;
            retVal.BlackFlats = p.CountFlats().Black;

            if (p.HasRoad().Result == true)
                retVal.Reason = WinReason.RoadWin;
            else
                retVal.Reason = WinReason.FlatsWin;

            return retVal;
        }

        #endregion
    }

}