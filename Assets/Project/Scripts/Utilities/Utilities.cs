using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Com.aberrantGames.Tak.GameEngine;

namespace Com.aberrantGames.Tak.Utilities
{
    public struct CoordPair
    {
        public int X, Y;
    }

    public static class Utilities
    {
        private const string _gamemodesPath = "Gamemodes";
        private const string _defautGamemodeName = "_default";

        public static GameHolder RetrieveDefaultGamemode()
        {
            object o = Resources.Load(_gamemodesPath + _defautGamemodeName);
            GameHolder retrievedGamemode = (GameHolder)o;
            return retrievedGamemode;
        }
    }
}