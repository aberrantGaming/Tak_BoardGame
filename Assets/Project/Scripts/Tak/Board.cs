using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Square = Com.aberrantGames.Tak.GameEngine.Tile;

namespace Com.aberrantGames.Tak.GameEngine
{    
    public struct Config                    // TODO : Replace with GameHolder ScriptableObject
    {
        public int Size, Pieces, Capstones;
        public bool BlackWinsTies;
        public Bitboard.Constants c;
    }
        
    public class Board
    {
        #region Variables

        static int[] defualtPieces = new int[] { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        static int[] defaultCapstones = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        #endregion

        #region Public Methods

        /// <summary>
        ///     Initializes a new board position on the provided Configuration
        /// </summary>
        /// <param name="_g"></param>
        /// <returns> Position? </returns>
        public Position New(Config _g)
        {
            if (_g.Pieces == 0)
                _g.Pieces = defualtPieces[_g.Size];
            if (_g.Capstones == 0)
                _g.Capstones = defualtPieces[_g.Size];

            Position p = new Position()
            {
                cfg = _g,
                WhiteStones = (byte)_g.Pieces,
                WhiteCapstones = (byte)_g.Capstones,
                BlackStones = (byte)_g.Pieces,
                BlackCapstones = (byte)_g.Capstones,
                turn = 0,

                //hash = fnvBasis           // TO DO : Implement hash struct
            };
            return p;
        }

        /// <summary>
        ///     Initializes a position with the specified tiles and move number.
        /// </summary>
        /// <returns></returns>
        public Position FromSquares(Config _cfg, Square[][] _board, int _move)
        {
            Position p = New(_cfg);
            p.turn = _move;

            for (int y = 0; y < p.Size(); y++)
            {
                for (int x = 0; x < p.Size(); x++)
                {
                    Square sq = _board[y][x];
                    if (sq.Stack.Length == 0)
                        continue;
                    int i = x + y * p.Size();
                    switch (sq.Stack[0].Color())
                    {
                        case Stone.White: p.White |= (1 << i); break;
                        case Stone.Black: p.Black |= (1 << i); break;
                    }
                    switch (sq.Stack[0].Type())
                    {
                        case Stone.Capstone: p.Caps |= (1 << i); break;
                        case Stone.Standing: p.Standing |= (1 << i); break;
                    }

                    for (int j = 0; j < sq.Stack.Length; j++)   //for j, piece := range sq {
                    {
                        //switch piece {
                            //case MakePiece(White, Capstone):
                            //p.whiteCaps--
            
                            //case MakePiece(Black, Capstone):
                            //p.blackCaps--
            
                            //case MakePiece(White, Flat), MakePiece(White, Standing):
                            //p.whiteStones--
            
                            //case MakePiece(Black, Flat), MakePiece(Black, Standing):
                            //p.blackStones--
            
                            //default:
                            //return nil, errors.New("bad stone")            
                        //}

                        //if (j == 0)
                            //continue

                        //if piece.Color() == Black {
                        //p.Stacks[i] |= 1 << uint(j - 1)
                    }

                    p.Height[i] = (int)sq.Stack.Length;
                    //p.hash ^= p.hashAt(i);
                }
            }

            p.Analyze();
            return p;
        }

        /// <summary>
        ///     Set's a piece to a tile at a specified location on the referenced position
        /// </summary>
        /// <param name="_p">Position</param>
        /// <param name="_x">Position.X</param>
        /// <param name="_x">Position.Y</param>
        /// <param name="_s">Tile to set</param>
        public void Set(Position _p, int _x, int _y, Square _s)
        {
            int i = (_y * _p.cfg.Size + _x);
            _p.White &= _p.White ^ (1 << i);
            _p.Black &= _p.Black ^ (1 << i);
            _p.Standing &= _p.Standing ^ (1 << i);
            _p.Caps &= _p.Caps ^ (1 << i);
            if (_s.Stack.Length == 0)
            {
                _p.Height[i] = 0;
                return;
            }

            switch (_s.Stack[0].Color())
            {
                case (Stone.White): _p.White |= (1 << i); break;
                case (Stone.Black): _p.Black |= (1 << i); break;
            }

            switch (_s.Stack[0].Type())
            {
                case (Stone.Standing): _p.Standing |= (1 << i); break;
                case (Stone.Capstone): _p.Caps |= (1 << i); break;
            }

            //p.hash ^= p.hashAt(i);

            _p.Height[i] = _s.Stack.Length;
            _p.Stacks[i] = 0;            
            for (int j = 0; j <= _s.Stack.Length; j++)                   //for j, piece := range s[1:]
            { 
                if (_s.Stack[j].Color() == Stone.Black)                  //  if (piece.Color() == Black)                    
                    _p.Stacks[i] |= (1 << j);                            //      p.Stacks[i] |= (1 << uint(j))
            }
            //p.hash ^= p.hashAt(i);
        }

        #endregion
    }
}
