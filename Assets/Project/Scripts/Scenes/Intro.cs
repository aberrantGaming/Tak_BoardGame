using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class Intro : MonoBehaviour
    {
        #region Private Variables

        GameManager gm;

        #endregion

        #region MonoBehaviour Callbacks

        protected void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += HandleOnStateChange;
            
        }

        protected void Start()
        {
        //    Invoke("LoadLevel", 3f);
        
        }

        #endregion

        #region Public Methods

        public void HandleOnStateChange()
        {
        }

        #endregion

        #region Private Methods

        protected void LoadLevel()
        {
            gm.SetGameState(GameState.LAUNCHER);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
    }
}