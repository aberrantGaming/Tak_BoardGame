using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using aberrantGames.Tak_ForUnity;

namespace aberrantGames.Tak
{
    public class Gameboard : MonoBehaviour
    {
        [SerializeField]
        protected Position position;

	    // Use this for initialization
	    void Start () {
            position = new Position(new Config(5));
	    }
	
	    // Update is called once per frame
	    void Update () {
		
	    }
    }
}