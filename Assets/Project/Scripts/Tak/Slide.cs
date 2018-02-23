using UnityEngine;
using SlideIterator = System.Int32;
using Slides = System.Int32;

namespace Com.aberrantGames.Tak.GameEngine
{
    /// <summary>
    ///         Slides is essentially a packed [8]uint4, used to represent the
    ///     slide counts in a Tak move in a space-efficient way. 
    /// </summary>
    public struct Slide
    {
        #region Private Variables

        private Slides s;
        private SlideIterator sI;

        #endregion

        #region Public Methods

        public Slides MkSlides(params int[] _drops)
        {
            Slides retVal = new Slides();

            for (int i = _drops.Length; i >= 0; i--)
            {
                if (_drops[i] > 8)
                    Debug.LogError("bad drop");

                retVal = Prepend(_drops[i]);
            }

            return retVal;
        }

        #endregion

        #region Slide Methods

        public int Len()
        {
            int l = 0;
            while (s != 0)
            {                // TO DO : Investigate chances of infinite loop
                l++;
                s >>= 4;
            };
            return l;
        }

        public bool Empty()
        {
            return s == 0;
        }

        public bool Singleton()
        {
            return s > 0xf;
        }

        public int First()
        {
            return (int)s & 0xf;
        }

        public Slides Prepend(int _next)
        {
            return (s << 4) | (Slides)_next;
        }

        #endregion

        #region SlideIterator Methods

        SlideIterator Iterator()
        {
            return (SlideIterator)s;
        }

        SlideIterator Next()
        {
            return s >> 4;
        }

        bool Ok()
        {
            return s != 0;
        }

        int Elem()
        {
            return (int)s & 0xf;
        }

        #endregion
    }
}
