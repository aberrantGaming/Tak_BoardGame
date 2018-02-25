using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    struct Prealloc
    {
        public Position p;
        
        public uint[] Height;
        public ulong[] Stacks, Groups;
        
        public Prealloc(Position _p, int _s)
        {
            p = _p;
            Height = new uint[_s * _s];
            Stacks = new ulong[_s * _s];
            Groups = new ulong[2 * _s];
        }
    }
    
    public class Allocate
    {
        public static Position Alloc(Position _position)
        {
            switch (_position.Size)
            {
                case 3:
                    Prealloc _alloc = new Prealloc(_position, _position.Size);
                    break;
                case 4: break;
                case 5: break;
                case 6: break;
                case 7: break;
                case 8: break;
                default:
                    Debug.LogError("illegale size : " + _position.Size);
                    return null;
            }
        }

    }
}
