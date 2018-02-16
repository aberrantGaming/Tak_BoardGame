using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.SeeSameGames.Tak.Utilities
{
    public class DDOL : MonoBehaviour
    {
        public void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("DDOL " + gameObject.name);
        }
    }
}
