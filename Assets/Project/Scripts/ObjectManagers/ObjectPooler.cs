using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak
{
    public class ObjectPooler : MonoBehaviour
    {
        #region Singleton

        protected ObjectPooler() { }
        public static ObjectPooler Instance { get; private set; }

        protected void Awake()
        {
            if (ObjectPooler.Instance == null)
                Instance = this;
            else
                DestroyObject(this);

            Initialize();
        }

        #endregion

        #region Variables

        public List<ObjectPoolItem> ItemsToPool;
        public List<List<GameObject>> PooledObjectsList;
        public List<GameObject> PooledObjects;

        private List<int> positions;

        #endregion

        #region Public Methods        

        public GameObject GetPooledObject(int index)
        {
            int currSize = PooledObjectsList[index].Count;
            for (int i = positions[index]; i < positions[index] + PooledObjectsList[index].Count; i++)
            {
                if (!PooledObjectsList[index][i % currSize].activeInHierarchy)
                {
                    positions[index] = i % currSize;
                    return PooledObjectsList[index][i % currSize];
                }
            }

            if (ItemsToPool[index].shouldExpand)
            {
                GameObject obj = (GameObject)Instantiate(ItemsToPool[index].objectToPool);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                PooledObjectsList[index].Add(obj);
                return obj;
            }

            return null;
        }

        public List<GameObject> GetAllPooledObjects(int index)
        {
            return PooledObjectsList[index];
        }

        public int AddObject(GameObject GO, int amt = 1, bool exp = true)
        {
            ObjectPoolItem item = new ObjectPoolItem(GO, amt, exp);
            int currLen = ItemsToPool.Count;
            ItemsToPool.Add(item);
            ObjectPoolItemToPooledObject(currLen);
            return currLen;
        }

        #endregion

        #region Private Methods

        protected void Initialize()
        {
            PooledObjectsList = new List<List<GameObject>>();
            PooledObjects = new List<GameObject>();
            positions = new List<int>();

            for (int i = 0; i < ItemsToPool.Count; i++)
            {
                ObjectPoolItemToPooledObject(i);
            }
        }

        internal protected void ObjectPoolItemToPooledObject(int index)
        {
            ObjectPoolItem item = ItemsToPool[index];

            PooledObjects = new List<GameObject>();
            for (int i = 0; i < item.AmountToPool; i++)
            {
                GameObject obj = (GameObject)Instantiate(item.objectToPool);
                obj.SetActive(false);
                obj.transform.parent = this.transform;
                PooledObjects.Add(obj);
            }
            PooledObjectsList.Add(PooledObjects);
            positions.Add(0);
        }

        #endregion
    }

    [System.Serializable]
    public class ObjectPoolItem
    {
        public GameObject objectToPool;
        public int AmountToPool;
        public bool shouldExpand = true;

        public ObjectPoolItem(GameObject obj, int amt, bool exp = true)
        {
            objectToPool = obj;
            AmountToPool = Mathf.Max(amt, 2);
            shouldExpand = exp;
        }
    }

}