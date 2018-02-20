using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public struct Analysis
    {
        public int[] LightGroups;
        public int[] DarkGroups;
    }

    public struct Position
    {
        #region Public Variables

        public GameHolder cfg;
        public byte LightStones;
        public byte LightCapstones;
        public byte DarkStones;
        public byte DarkCapstones;

        public int move;

        public int Light;
        public int Dark;
        public int Standing;
        public int caps;

        public int[] Height;
        public int[] Stacks;

        #endregion

        #region Private Variables

        Analysis analysis;

        int hash;

        #endregion

        #region Public Methods 

        public Position Alloc(Position _tpl)
        {
            Position _retVal = new Position();

            switch (_tpl.cfg.BoardSize)
            {
                case 3: _retVal = Position3(_tpl); break;
                case 4: _retVal = Position4(_tpl); break;
                case 5: _retVal = Position5(_tpl); break;
                case 6: _retVal = Position6(_tpl); break;
                case 7: _retVal = Position7(_tpl); break;
                case 8: _retVal = Position8(_tpl); break;
            }

            return _retVal;
        }

        public void CopyPosition(Position _p, Position _out)
        {
            int[] h = _out.Height;
            int[] s = _out.Stacks;
            int[] g = _out.analysis.LightGroups;

            _out = _p;
            _out.Height = h;
            _out.Stacks = s;
            _out.analysis.LightGroups = g;                        

            _p.Height = _out.Height;
            _p.Stacks = _out.Stacks;
        }

        #endregion

        #region Private Methods

        Position Position3(Position _position)
        {
            Position alloc = new Position()
            {
                Height = new int[3 * 3],
                Stacks = new int[3 * 3]
            };

            alloc.analysis.LightGroups = new int[6];
            alloc.analysis.DarkGroups = new int[6];

            alloc.Height = _position.Height;
            alloc.Stacks = _position.Stacks;

            return alloc;
        }

        private Position Position4(Position _position)
        {
            Position alloc = new Position()
            {
                Height = new int[4 * 4],
                Stacks = new int[4 * 4]
            };

            alloc.analysis.LightGroups = new int[8];
            alloc.analysis.DarkGroups = new int[8];

            alloc.Height = _position.Height;
            alloc.Stacks = _position.Stacks;

            return alloc;
        }

        private Position Position5(Position _position)
        {
            Position alloc = new Position()
            {
                Height = new int[5 * 5],
                Stacks = new int[5 * 5]
            };

            alloc.analysis.LightGroups = new int[10];
            alloc.analysis.DarkGroups = new int[10];

            alloc.Height = _position.Height;
            alloc.Stacks = _position.Stacks;

            return alloc;
        }

        private Position Position6(Position _position)
        {
            Position alloc = new Position()
            {
                Height = new int[6 * 6],
                Stacks = new int[6 * 6]
            };

            alloc.analysis.LightGroups = new int[12];
            alloc.analysis.DarkGroups = new int[12];

            alloc.Height = _position.Height;
            alloc.Stacks = _position.Stacks;

            return alloc;
        }

        private Position Position7(Position _position)
        {
            Position alloc = new Position()
            {
                Height = new int[7 * 7],
                Stacks = new int[7 * 7]
            };

            alloc.analysis.LightGroups = new int[14];
            alloc.analysis.DarkGroups = new int[14];

            alloc.Height = _position.Height;
            alloc.Stacks = _position.Stacks;

            return alloc;
        }

        private Position Position8(Position _position)
        {
            Position alloc = new Position()
            {
                Height = new int[8 * 8],
                Stacks = new int[8 * 8]
            };

            alloc.analysis.LightGroups = new int[16];
            alloc.analysis.DarkGroups = new int[16];

            alloc.Height = _position.Height;
            alloc.Stacks = _position.Stacks;

            return alloc;
        }


        #endregion

    }
}