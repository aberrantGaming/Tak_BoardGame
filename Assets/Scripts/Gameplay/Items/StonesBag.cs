using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/Stones Bag")]
    public class StonesBag : Collectable
    {
        public GameObject Prefab;
        public Material Material;
        
        public StonesBag()
        {
            name = "stones_bag_name";
            desc = "stones_bag_description";
        }
    }
}
