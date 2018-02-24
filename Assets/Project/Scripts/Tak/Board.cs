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
    }
        
    public class Board
    {
        #region Variables

        static int[] defualtPieces = new int[] { 0, 0, 0, 10, 15, 21, 30, 40, 50 };
        static int[] defaultCapstones = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 2 };

        public Position P { get; private set; }

        #endregion
        
        #region Constructors

        /// <summary>
        ///     Initializes a new board based on the provided Configuration
        /// </summary>
        /// <param name="_g"></param>
        /// <returns> Position? </returns>
        public Board(Config _g)
        {
            if (_g.Pieces == 0)
                _g.Pieces = defualtPieces[_g.Size];
            if (_g.Capstones == 0)
                _g.Capstones = defualtPieces[_g.Size];

            P = new Position()
            {
                cfg = _g,
                WhiteStones = (byte)_g.Pieces,
                WhiteCapstones = (byte)_g.Capstones,
                BlackStones = (byte)_g.Pieces,
                BlackCapstones = (byte)_g.Capstones,                
                move = 0,

                //hash = fnvBasis           // TO DO : Implement hash struct
            };
        }
        #endregion

        #region Public Methods

        /// <summary>
        ///     Initializes a position with the specified tiles and move number.
        /// </summary>
        /// <returns></returns>
        public Position FromSquares(Config _cfg, Square[][] _board, int _move) { }

        /// <summary>
        ///     Set's a piece to the specified location
        /// </summary>
        /// <param name="p">Position</param>
        /// <param name="x">Position.X</param>
        /// <param name="x">Position.Y</param>
        /// <param name="_s">Tile to set</param>
        public void Set(Position p, int x, int y, Square s)
        {
            int i = (y * p.cfg.Size + x);
            p.White &= p.White ^ (1 << i);
            p.Black &= p.Black ^ (1 << i);
            p.Standing &= p.Standing ^ (1 << i);
            p.Caps &= p.Caps ^ (1 << i);
            if (s.Stack.Length == 0)
            {
                p.Height[i] = 0;
                return;
            }

            switch (s.Stack[0].Color())
            {
                case (Stone.White): p.White |= (1 << i); break;
                case (Stone.Black): p.Black |= (1 << i); break;
            }

            switch (s.Stack[0].Type())
            {
                case (Stone.Standing): p.Standing |= (1 << i); break;
                case (Stone.Capstone): p.Caps |= (1 << i); break;
            }

            //p.hash ^= p.hashAt(i);

            p.Height[i] = s.Stack.Length;
            p.Stacks[i] = 0;            
            for (int j = 0; j <= s.Stack.Length; j++)                   //for j, piece := range s[1:]
            { 
                if (s.Stack[j].Color() == Stone.Black)                  //  if (piece.Color() == Black)                    
                    p.Stacks[i] |= (1 << j);                            //      p.Stacks[i] |= (1 << uint(j))
            }
            //p.hash ^= p.hashAt(i);
        }

        #endregion
    }
}
