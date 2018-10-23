using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aberrantGames.Tak.Collectables
{
    public enum StoneType { Flatstone = 0, Capstone }

    [CreateAssetMenu(menuName = "Collectable/Flatstones")]
    public class Flatstones : Collectable
    {
        public GameObject PrimaryPrefab, SecondaryPrefab;
        public Material MaterialDark, MaterialLight;

        protected StoneType stoneType;

        public Flatstones()
        {
            stoneType = StoneType.Flatstone;

            name = "flatstones_name";
            desc = "flatstones_description";
        }
    }
}