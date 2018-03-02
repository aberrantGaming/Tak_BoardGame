using System.Collections.Generic;

namespace Com.aberrantGames.Tak.Bits
{
    public struct Dimensions
    {
        public int W, H;

        public Dimensions(int _w = 0, int _h = 0)
        {
            W = _w;
            H = _h;
        }
    }

    public class Constants
    {
        public uint Size;
        public ulong L, R, T, B;
        public ulong Edge;
        public ulong Mask;
    }

    public static class Bitboard
    {
        /// <summary>
        /// Used to count the population of bits on a bitboard
        /// </summary>
        /// <param name="x"></param>
        /// <returns>uLong = population count</returns>
        public static long Popcount(ulong x)
        {
            if (x == 0)
                return 0;

            x -= (x >> 1) & 0x5555555555555555;
            x = (x >> 2) & 0x3333333333333333 + x & 0x3333333333333333;
            x += x >> 4;
            x &= 0x0f0f0f0f0f0f0f0f;
            x *= 0x0101010101010101;
            return (long)(x >> 56);
        }

        /// <summary>
        /// Used to get the list of flood groups from this board
        /// </summary>
        /// <param name="c">constants</param>
        /// <param name="bits">bitboard</param>
        /// <param name="_out"></param>
        /// <returns></returns>
        public static List<ulong> FloodGroups(Constants c, ulong bits, List<ulong> _out)    // TO DO : Verify conversion to list
        {
            ulong seen = new ulong();
            while (bits != 0)
            {
                ulong next = bits & (bits - 1);
                ulong bit = bits & ~next;

                if ((seen & bit) == 0)
                {
                    ulong g = Flood(c, bits, bit);
                    if (g != bit)
                        _out.Add(g);
                    seen |= g;
                }

                bits = next;
            }

            return _out;
        }
        
        /// <summary>
        /// Predetermines constants from provided size
        /// </summary>
        /// <param name="size">uint representing board size</param>
        /// <returns></returns>
        public static Constants Precompute(uint size)
        {
            Constants c = new Constants();
            for (int i = 0; i < size; i++)
            {
                c.R |= (ulong)1 << (int)(i * size);
            }

            c.Size = size;
            c.L = c.R << (int)(size - 1);
            c.T = (ulong)((1 << (int)size) - 1) << (int)(size * (size - 1));
            c.B = (ulong)(1 << (int)size) - 1;
            c.Mask = (ulong)(1 << (int)(size * size) - 1);
            c.Edge = c.L | c.R | c.B | c.T;

            return c;
        }

        /// <summary>
        /// Used to get the dimensions of this board
        /// </summary>
        /// <param name="c">constants</param>
        /// <param name="bits">bitboard</param>
        /// <returns></returns>
        public static Dimensions GetDimensions(Constants c, ulong bits)
        {
            Dimensions retVal = new Dimensions(0, 0);
            ulong b = new ulong();

            if (bits == 0)
                return retVal;

            b = c.L;
            while ((bits & b) == 0)
            {
                b = (b >> 1);
            }
            while ((b != 0) && ((bits & b) != 0))
            {
                b = (b >> 1);
                retVal.W++;
            }

            b = c.T;
            while ((bits & b) == 0)
            {
                b = b >> (int)c.Size;      // TO DO : Confirm use of type cast
            }
            while ((b != 0) && ((bits & b) != 0))
            {
                b = b >> (byte)c.Size;     // TO DO : Confirm use of type cast
                retVal.H++;
            }

            return retVal;
        }
        
        public static ulong Flood(Constants c, ulong within, ulong seed)
        {
            while (true)
            {
                ulong next = Grow(c, within, seed);
                if (next == seed)
                {
                    return next;
                }
                seed = next;
            }
        }

        public static ulong Grow(Constants c, ulong within, ulong seed)
        {
            ulong next = seed;
            next |= (seed << 1) & ~c.R;
            next |= (seed >> 1) & ~c.L;
            next |= (seed >> (byte)c.Size);       // TO DO : Confirm use of type cast
            next |= (seed << (byte)c.Size);       // TO DO : Confirm use of type cast
            return next & within;
        }
    }
}