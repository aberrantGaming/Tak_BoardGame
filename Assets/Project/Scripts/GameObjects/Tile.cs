using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak
{
    public class Tile
    {
        public string tileID;
        public Transform tilePrefab;
        public Tile(Transform _prefab)
        {
            tilePrefab = _prefab;
        }
    }

    
}
