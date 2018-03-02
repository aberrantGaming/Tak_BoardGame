using System;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public static class Allocation
    {
        struct Prealloc
        {
            #region Variables

            public Position Position;

            public uint[] Height;
            public ulong[] Stacks;
            public List<ulong> Groups;

            #endregion

            #region Constructors

            public Prealloc(Position _p, int _s = 0)
            {
                Position = _p;

                Height = new uint[_s * _s];
                Stacks = new ulong[_s * _s];
                Groups = new List<ulong>(2 * _s);
            }

            #endregion
        }
        
        #region Public Methods

        public static Position Alloc(Position _position = null)
        {
            Prealloc _alloc;

            if ((_position.Size() >= 3) && (_position.Size() <= 8))
            {
                _alloc = new Prealloc(_position, _position.Size());
                _alloc.Position.Height = _alloc.Height;
                _alloc.Position.Stacks = _alloc.Stacks;
                _alloc.Position.analysis.WhiteGroups = _alloc.Groups;

                _alloc.Height = _position.Height;
                _alloc.Stacks = _position.Stacks;

                return _alloc.Position;
            }
            else
                Debug.LogError("illegal size : " + _position.Size());

            return null;
        }

        public static void CopyPosition(Position _p, Position _out)
        {
            uint[] h = _out.Height;
            ulong[] s = _out.Stacks;
            List<ulong> g = _out.analysis.WhiteGroups;

            _out = _p;
            _out.Height = h;
            _out.Stacks = s;
            _out.analysis.WhiteGroups = g;

            Array.Copy(_p.Height, _out.Height, _p.Height.Length);
            Array.Copy(_p.Stacks, _out.Stacks, _p.Stacks.Length);
        }

        public static Position Alloc(int size)
        {
            Position p = new Position() { cfg = new Config() { Size = size } };
            return Alloc(p);
        }

        #endregion
    }
}
