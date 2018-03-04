using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/Capstones")]
    public class Capstones : Flatstones
    {
        public Capstones()
        {
            stoneType = StoneType.Capstone;

            name = "capstone_name;";
            desc = "capstone_description";
        }
    }
}