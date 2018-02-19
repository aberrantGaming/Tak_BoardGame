using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    public class BoardCollectable : ScriptableObject
    {
        public string BoardName;
        public string BoardDesc;

        public GameObject BoardFoundation;

        public GameObject TileDark;
        public GameObject TileLight;
    }
}