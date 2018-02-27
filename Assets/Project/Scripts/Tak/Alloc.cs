using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    struct Prealloc
    {
        public Position Position;
        
        public uint[] Height;
        public ulong[] Stacks, Groups;
        
        public Prealloc(Position _p, int _s = 0)
        {
            Position = _p;
            
            Height = new uint[_s * _s];
            Stacks = new ulong[_s * _s];
            Groups = new ulong[2 * _s];
        }
    }
    
    public class Allocate
    {
        public static Position Alloc(Position _position = null)
        {
            Prealloc _alloc;

            if ((_position.Size >= 3) && (_position.Size <= 8))
            {
                _alloc = new Prealloc(_position, _position.Size);
                _alloc.Position.Height = _alloc.Height;
                _alloc.Position.Stacks = _alloc.Stacks;
                _alloc.Position.analysis.WhiteGroups = _alloc.Groups;

                _alloc.Height = _position.Height;
                _alloc.Stacks = _position.Stacks;

                return _alloc.Position;                
            }
            else
                Debug.LogError("illegal size : " + _position.Size);
            
            return null;
        }
    }
}
