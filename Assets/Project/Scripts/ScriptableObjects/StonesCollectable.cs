using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/New Stones")]
    public class StonesCollectable : ScriptableObject
    {
        public string StonesName = "New Stones";
        public string StonesDesc = "New stones description";

        public GameObject StonesDarkPrefab;
        public GameObject StonesLightPrefab;

        public GameObject CapstonesDarkPrefab;
        public GameObject CapstonesLightPreab;
    }
}