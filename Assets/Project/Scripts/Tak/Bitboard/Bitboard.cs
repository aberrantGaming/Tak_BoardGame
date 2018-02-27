using System.Collections.Generic;

namespace Com.aberrantGames.Tak.Bits
{
    public class Constants
    {
        #region Variables

        public uint Size;
        public ulong L, R, T, B;
        public ulong Edge;
        public ulong Mask;

        #endregion
    }

    public class Bitboard
    {
        #region Variables

        // Struct
        struct Dimensions
        {
            public int W, H;

            public Dimensions(int _w = 0, int _h = 0)
            {
                W = _w;
                H = _h;
            }
        }

        // Private
        protected ulong next;

        #endregion

        #region Public Methods

        /// <summary>
        /// Used to count the population of bits on a bitboard
        /// </summary>
        /// <param name="_x"></param>
        /// <returns>uLong = population count</returns>
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

        /// <summary>
        /// Used to get the list of flood groups from this board
        /// </summary>
        /// <param name="_c">constants</param>
        /// <param name="_bits">bitboard</param>
        /// <param name="_out"></param>
        /// <returns></returns>
        public static List<ulong> FloodGroups(Constants _c, ulong _bits, List<ulong> _out)    // TO DO : Verify conversion to list
        {
            ulong seen = new ulong();
            while (_bits != 0)
            {
                ulong next = _bits & (_bits - 1);
                ulong bit = _bits & ~next;

                if ((seen & bit) == 0)
                {
                    ulong g = Flood(_c, _bits, bit);
                    if (g != bit)
                        _out.Add(g);
                    seen |= g;
                }

                _bits = next;
            }

            return _out;
        }

        #endregion

        #region Private 

        /// <summary>
        /// Predetermines constants from provided _size
        /// </summary>
        /// <param name="_size">uint representing board size</param>
        /// <returns></returns>
        Constants Precompute(uint _size)
        {
            Constants c = new Constants();
            for (int i = 0; i < _size; i++)
            {
                c.R |= (ulong)1 << (int)(i * _size);        // TO DO : Confirm use of type casts
            }

            c.Size = _size;
            c.L = c.R << (int)(_size - 1);                                          // TO DO : Confirm use of type casts
            c.T = (ulong)((1 << (int)_size) - 1) << (int)(_size * (_size - 1));     // TO DO : Confirm use of type casts
            c.B = (ulong)(1 << (int)_size) - 1;                                     // TO DO : Confirm use of type casts
            c.Mask = (ulong)(1 << (int)(_size * _size) - 1);                        // TO DO : Confirm use of type casts
            c.Edge = c.L | c.R | c.B | c.T;

            return c;
        }

        /// <summary>
        /// Used to get the dimensions of this board
        /// </summary>
        /// <param name="_c">constants</param>
        /// <param name="_bits">bitboard</param>
        /// <returns></returns>
        Dimensions GetDimensions(Constants _c, ulong _bits)
        {
            Dimensions retVal = new Dimensions(0, 0);
            ulong b = new ulong();

            if (_bits == 0)
                return retVal;

            b = _c.L;
            while ((_bits & b) == 0)
            {
                b = (b >> 1);
            }
            while ((b != 0) && ((_bits & b) != 0))
            {
                b = (b >> 1);
                retVal.W++;
            }

            b = _c.T;
            while ((_bits & b) == 0)
            {
                b = b >> (int)_c.Size;      // TO DO : Confirm use of type cast
            }
            while ((b != 0) && ((_bits & b) != 0))
            {
                b = b >> (byte)_c.Size;     // TO DO : Confirm use of type cast
                retVal.H++;
            }

            return retVal;
        }


        public static ulong Flood(Constants _c, ulong _within, ulong _seed)
        {
            while (true)
            {
                ulong next = Grow(_c, _within, _seed);
                if (next == _seed)
                {
                    return next;
                }
                _seed = next;
            }
        }

        public static ulong Grow(Constants _c, ulong _within, ulong _seed)
        {
            ulong next = _seed;
            next |= (_seed << 1) & ~_c.R;
            next |= (_seed >> 1) & ~_c.L;
            next |= (_seed >> (byte)_c.Size);       // TO DO : Confirm use of type cast
            next |= (_seed << (byte)_c.Size);       // TO DO : Confirm use of type cast
            return next & _within;
        }
    }
    #endregion
}