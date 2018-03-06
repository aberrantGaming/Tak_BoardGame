using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/Tile")]
    public class Tile : Collectable
    {
        public GameObject Prefab;
        public Material Material;

        public Tile()
        {
            name = "tile_name";
            desc = "tile_description";
        }

        public static Tile MakeDefaultTile()
        {
            return Utilities.Defaults.GetDefaultTile();
        }
    }
}
