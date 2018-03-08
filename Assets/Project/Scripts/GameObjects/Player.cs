using UnityEngine;
using Com.aberrantGames.Tak.Collectables;

namespace Com.aberrantGames.Tak
{
    public enum PlayerState { IDLE, PREMATCH, MATCH, POSTMATCH }

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
