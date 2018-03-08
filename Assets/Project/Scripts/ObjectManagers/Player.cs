using UnityEngine;
using Com.aberrantGames.Tak.Collectables;

namespace Com.aberrantGames.Tak
{
    public enum PlayerState { IDLE, PREMATCH, MATCH, POSTMATCH }
    public enum CollectionType { Flatstones, Capstones, BoardDesign }
   
    [System.Serializable]
    public struct Collection
    {
        [SerializeField] private Flatstones currFlatstones;
        [SerializeField] private Capstones currCapstones;
        [SerializeField] private BoardDesign currBoardDesign;

        public Flatstones CurrentFlatstones {
            get
            {
                return currFlatstones;
            }
            private set { }
        }
        public Capstones CurrentCapstones
        {
            get
            {
                return currCapstones;
            }
            private set { }
        }
        public BoardDesign CurrentBoardDesign
        {
            get
            {
                return currBoardDesign;
            }
            private set { }
        }

        public void SetCollectable(CollectionType collectionType, ICollectable collectable)
        {
            switch (collectionType)
            {
                case (CollectionType.Flatstones): SetFlatstones((Flatstones)collectable); break;
                case (CollectionType.Capstones): SetCapstones((Capstones)collectable); break;
                case (CollectionType.BoardDesign): SetBoardDesign((BoardDesign)collectable); break;
            }
        }

        private void SetFlatstones(Flatstones newFlatstones)
        {
            currFlatstones = newFlatstones;
        }

        private void SetCapstones(Capstones newCapstones)
        {
            currCapstones = newCapstones;
        }

        private void SetBoardDesign(BoardDesign newBoardDesign)
        {
            currBoardDesign = newBoardDesign;
        }
    }

    public class Player : MonoBehaviour
    {
        #region Public Variables
        
        public PlayerState PlayerState { get; private set; }

        public Collection PlayerCollection;

        public int matchScore;

        #endregion

        #region Private Variables

        private GameManager gm;

        #endregion
    }
}
