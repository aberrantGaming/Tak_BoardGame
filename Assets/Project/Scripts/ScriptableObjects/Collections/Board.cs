using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/Board")]
    public class Board : Collectable
    {
        public GameObject Prefab;
        public Material Material;

        List<Tile> Tiles;

        public Board()
        {
            Tiles = new List<Tile>(64);

            name = "board_name";
            desc = "board_description";
        }
    }
}

