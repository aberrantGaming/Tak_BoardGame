using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/New Board")]
    public class BoardCollectable : ScriptableObject
    {
        public string BoardName = "New Board";
        public string BoardDesc = "New board description.";

        public GameObject BoardFoundation;

        public GameObject TileDark;
        public GameObject TileLight;
    }
}