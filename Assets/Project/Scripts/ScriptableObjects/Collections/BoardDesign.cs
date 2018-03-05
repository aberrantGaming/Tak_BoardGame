using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/Board Design")]
    public class BoardDesign : Collectable
    {
        public Texture Design;
        public List<Tile> Tiles;

        public BoardDesign()
        {
            Tiles = new List<Tile>(64);

            name = "board_name";
            desc = "board_description";
        }
    }
}

