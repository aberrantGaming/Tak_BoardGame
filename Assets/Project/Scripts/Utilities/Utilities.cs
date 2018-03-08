using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Utilities
{
    public struct CoordPair
    {
        public int X, Y;
    }

    public static class Defaults
    {
        private const string gamemodesPath = "Gamemodes";
        private const string collectablesPath = "Collectables";

        private const string defaultGamemodeName = "OfflineHotseat";
        private const string defaultTileName = "plain_tile_00";
        private const string defaultBoardName = "plain_board_00";
        private const string defaultFlatstoneName = "plain_flatstones_00";
        private const string defaultCapstoneName = "plain_capstones_00";

        public static GameEngine.Gamemode GetDefaultGamemode()
        {
            object o = Resources.Load(gamemodesPath + defaultGamemodeName);
            GameEngine.Gamemode ret = (GameEngine.Gamemode)o;
            return ret;
        }

        public static Collectables.Tile GetDefaultTile()
        {
            object o = Resources.Load(collectablesPath + defaultTileName);
            Collectables.Tile ret = (Collectables.Tile)o;
            return ret;
        }

        public static Collectables.Flatstones GetDefaultFlatstones()
        {
            object o = Resources.Load(collectablesPath + defaultFlatstoneName);
            Collectables.Flatstones ret = (Collectables.Flatstones)o;
            return ret;
        }

        public static Collectables.Capstones GetDefaultCapstones()
        {
            object o = Resources.Load(collectablesPath + defaultCapstoneName);
            Collectables.Capstones ret = (Collectables.Capstones)o;
            return ret;
        }
    }
}