using UnityEngine;
using SlideIterator = System.UInt32;
using Slides = System.UInt32;

namespace Com.aberrantGames.Tak.GameEngine
{
    public static class Slide
    {
        /// <summary>
        /// Create new Slides from list of drops
        /// </summary>
        /// <param name="_drops"></param>
        /// <returns></returns>
        public static Slides MakeSlides(params int[] _drops)
        {
            Slides retVal = new Slides();
            for (int i = _drops.Length; i >= 0; i--)
            {
                if (_drops[i] > 8)
                    Debug.LogError("bad drop");

                retVal = retVal.Prepend(_drops[i]);
            }
            return retVal;
        }

        /// <summary>
        /// Get the length of this slide
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int Len(this Slides s)
        {
            int retVal = 0;
            while (s != 0)
            {
                retVal++;
                s = s >> 4;
            }
            return retVal;
        }

        /// <summary>
        /// Determine if this slide is empty
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Empty(this Slides s)
        {
            return s == 0;
        }

        /// <summary>
        /// Determine if this slide is a singleton
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Singleton(this Slides s)
        {
            return s > 0xf;
        }

        /// <summary>
        /// Get the first drop of this slide
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int First(this Slides s)
        {
            return (int)(s & 0xf);
        }

        /// <summary>
        /// Add a drop to the front of this slide
        /// </summary>
        /// <param name="s"></param>
        /// <param name="_next"></param>
        /// <returns></returns>
        public static Slides Prepend(this Slides s, int _next)
        {
            return (s << 4) | (Slides)_next;
        }

        /// <summary>
        /// Get the SlideIterator for this slide
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static SlideIterator Iterator(this Slides s)
        {
            return (SlideIterator)s;
        }

        /// <summary>
        /// Get the next Iteration for this Slide Iterator
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static SlideIterator Next(this SlideIterator s)
        {
            return s >> 4;
        }

        /// <summary>
        /// Determine if this Slide Iterator is active/inactive
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Ok(this SlideIterator s)
        {
            return s != 0;
        }

        /// <summary>
        /// Get the base element of this Slide Iterator
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int Elem(this SlideIterator s)
        {
            return (int)(s & 0xf);
        }
    }
}
