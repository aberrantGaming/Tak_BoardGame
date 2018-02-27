using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    /// <summary>
    ///         Slides is essentially a packed [8]uint4, used to represent the
    ///     slide counts in a Tak move in a space-efficient way. 
    /// </summary>
    public class Slide
    {
        #region Properties

        public int Slides
        {
            get;
            private set;
        }

        public SlideIterator Iterator
        {
            get { return new SlideIterator(Slides); }
            private set { }
        }

        public int Len
        {
            get
            {
                int l = 0;
                while (Slides != 0)
                {                // TO DO : Investigate chances of infinite loop
                    l++;
                    Slides = Slides >> 4;
                };
                return l;
            }
        }

        public bool Empty
        {
            get
            {
                return Slides == 0;
            }
        }

        public bool Singleton
        {
            get
            {
                return Slides > 0xf;
            }
        }

        public int First
        {
            get
            {
                return (int)Slides & 0xf;
            }
        }

        #endregion

        #region Public Methods

        public int MkSlides(params int[] _drops)
        {
            int retVal = new int();

            for (int i = _drops.Length; i >= 0; i--)
            {
                if (_drops[i] > 8)
                    Debug.LogError("bad drop");

                retVal = Prepend(_drops[i]);
            }

            return retVal;
        }

        #endregion

        #region Private Methods

        public int Prepend(int _next)
        {
            return (Slides << 4) | _next;
        }

        #endregion
    }

    public class SlideIterator
    {
        #region Properties

        public int Iterations
        {
            get;
            private set;
        }

        SlideIterator Next
        {
            get
            {
                return new SlideIterator(Iterations >> 4);
            }
        }

        bool Ok
        {
            get
            {
                return Iterations != 0;
            }
        }

        int Elem
        {
            get
            {
                return Iterations & 0xf;
            }
        }

        #endregion

        #region Constructor

        public SlideIterator(int _slides)
        {
            Iterations = _slides;
        }

        #endregion    
    }
}
