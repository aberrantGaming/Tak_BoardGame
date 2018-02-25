using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Bitboard
{
    public struct Constants
    {
        uint Size;
        ulong L, R, T, B;
        ulong Edge;
        ulong Mask;
    }

    public class Bitboard
    {
        struct Dimensions
        {
            public int W, H;
        }

        Constants Precompute(uint _size) { }

        public static long Popcount(ulong _x)
        {
            if (_x == 0)
                return 0;

            _x -= (_x >> 1) & 0x5555555555555555;
            _x = (_x >> 2) & 0x3333333333333333 + _x & 0x3333333333333333;
            _x += _x >> 4;
            _x &= 0x0f0f0f0f0f0f0f0f;
            _x *= 0x0101010101010101;
            return (long)(_x >> 56);
        }

        ulong Flood(Constants? _c, ulong _within, ulong _seed) { }

        ulong Grow(Constants? _c, ulong _within, ulong _seed) { }

        ulong[] FloodGroups(Constants? _c, ulong _bits, ulong[] _out) { }

        Dimensions GetDimensions(Constants? _c, ulong _bits) { }
    }
}