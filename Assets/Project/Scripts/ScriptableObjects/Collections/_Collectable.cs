using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    public enum Rarity { Common = 0, Uncommon, Rare }

    public interface ICollectable
    {
        int ID { get; }
        int Rarity { get; }
        string Name { get; }
        string Description { get; }

        void AddCollectable(int _id);
    }

    public class Collectable : ScriptableObject, ICollectable
    {
        #region Variables

        [SerializeField] protected int id;
        [SerializeField] protected Rarity rarity;
        [SerializeField] protected new string name;
        [SerializeField] protected string desc;

        #endregion

        #region Interface Members

        public int ID
        {
            get
            {
                return this.id;
            }
            private set { }
        }

        public int Rarity
        {
            get
            {
                return (int)this.rarity;
            }
            private set { }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            private set { }
        }

        public string Description
        {
            get
            {
                return desc;
            }
            private set { }
        }

        public void AddCollectable(int _id) { }

        #endregion
    }
}
