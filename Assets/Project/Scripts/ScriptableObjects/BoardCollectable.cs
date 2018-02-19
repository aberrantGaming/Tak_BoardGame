using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/BoardCollectable")]
    public class BoardCollectable : ScriptableObject
    {
        public string BoardName;
        public string BoardDesc;

        public GameObject BoardFoundation;

        public GameObject TileDark;
        public GameObject TileLight;

        public BoardCollectable()
        {
            this.BoardName = "Development Board";
            this.BoardDesc = "Run-time board for development use.";
        }

    }
}