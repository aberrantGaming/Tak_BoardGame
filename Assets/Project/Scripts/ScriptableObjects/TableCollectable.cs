using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/New Table")]
    public class TableCollectable : ScriptableObject
    {
        public string TableName = "New Table";
        public string TableDesc = "New table description.";

        public GameObject TablePrefab;
    }
}