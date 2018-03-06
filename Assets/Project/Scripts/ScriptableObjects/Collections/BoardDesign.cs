using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/Board Design")]
    public class BoardDesign : Collectable
    {
        public Texture Design;
        public IDictionary<int, List<Tile>> Boards
        {
            get
            {
                return new Dictionary<int, List<Tile>>
                {
                    { 3, board3 },
                    { 4, board4 },
                    { 5, board5 },
                    { 6, board6 },
                    { 7, board7 },
                    { 8, board8 },
                };
            }
            private set { }
        }

        public List<Tile> board3, board4, board5, board6, board7, board8;
    }
}

