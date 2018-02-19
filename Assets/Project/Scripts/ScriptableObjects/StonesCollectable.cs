using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    public class StonesCollectable : ScriptableObject
    {
        public string StonesName;
        public string StonesDesc;

        public GameObject StonesDarkPrefab;
        public GameObject StonesLightPrefab;

        public GameObject CapstonesDarkPrefab;
        public GameObject CapstonesLightPreab;
    }
}