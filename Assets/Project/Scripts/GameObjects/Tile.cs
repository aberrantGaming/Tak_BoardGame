using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.GameEngine
{
    public class Tile
    {
        public string tileID;
        public List<Stone> Stack;
        public Transform tilePrefab;
        public Tile(Transform _prefab)
        {
            tilePrefab = _prefab;
        }
    }

    
}
