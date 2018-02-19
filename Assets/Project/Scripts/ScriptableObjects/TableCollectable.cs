using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    public class TableCollectable : ScriptableObject
    {
        public string TableName;
        public string TableDesc;

        public GameObject TablePrefab;
    }
}