using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak
{
    [CreateAssetMenu(menuName = "Collectables/Gamebox")]
    public class Box : ScriptableObject
    {
        #region Public Variables

        public string BoxName = "_BoxName";
        public string BoardDesc = "_BoxDescription";

        public Sprite BoxSprite;
        public GameObject BoxPrefab;

        #endregion

    }
}