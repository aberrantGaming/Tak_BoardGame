using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class Allocate
    {
        #region Variables

        struct Prealloc
        {
            #region Variables

            public Position Position;

            public uint[] Height;
            public ulong[] Stacks, Groups;

            #endregion

            #region Constructors

            public Prealloc(Position _p, int _s = 0)
            {
                Position = _p;

                Height = new uint[_s * _s];
                Stacks = new ulong[_s * _s];
                Groups = new ulong[2 * _s];
            }

            #endregion
        }

        #endregion

        #region Public Methods

        public static Position Alloc(Position _position = null)
        {
            Prealloc _alloc;

            if ((_position.Size >= 3) && (_position.Size <= 8))
            {
                _alloc = new Prealloc(_position, _position.Size);
                _alloc.Position.Height = _alloc.Height;
                _alloc.Position.Stacks = _alloc.Stacks;
                _alloc.Position.Analysis.SetGroups(_alloc.Groups);

                _alloc.Height = _position.Height;
                _alloc.Stacks = _position.Stacks;

                return _alloc.Position;
            }
            else
                Debug.LogError("illegal size : " + _position.Size);

            return null;
        }

        #endregion
    }
}
