using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Utilities
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
