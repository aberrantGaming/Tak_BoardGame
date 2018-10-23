using UnityEngine;
using aberrantGames.Tak.Collectables;

namespace aberrantGames.Tak
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
