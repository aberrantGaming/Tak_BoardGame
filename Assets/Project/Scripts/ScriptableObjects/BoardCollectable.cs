using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Com.aberrantGames.Tak.Collectables
{
    [CreateAssetMenu(menuName = "Collectable/BoardCollectable")]
    public class BoardCollectable : ScriptableObject
    {
        public string BoardName;
        public string BoardDesc;

        public GameObject BoardFoundation;

        public GameObject TileDark;
        public GameObject TileLight;

        public BoardCollectable()
        {
            this.BoardName = "Development Board";
            this.BoardDesc = "Run-time board for development use.";

            this.BoardFoundation =
                GameObject.CreatePrimitive(PrimitiveType.Cube);
            BoardFoundation.transform.localScale = new Vector3(1.0f, 0.01f, 1.0f);
            BoardFoundation.transform.position = new Vector3(0, 0, 0);

            this.TileDark =
                GameObject.CreatePrimitive(PrimitiveType.Cube);
            TileDark.transform.localScale = new Vector3(0.95f, 0.1f, 0.95f);
            TileDark.transform.position = new Vector3(0, 0.1f, 0);

            this.TileLight =
                GameObject.CreatePrimitive(PrimitiveType.Cube);
            TileLight.transform.localScale = new Vector3(0.95f, 0.1f, 0.95f);
            TileLight.transform.position = new Vector3(0, 0.1f, 0);
        }

    }
}