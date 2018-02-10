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

            Debug.Log("Current game state when Awakes: " + gm.CurrentGameState);
        }

        protected void Start()
        {
            Invoke("LoadLevel", 3f);

            Debug.Log("Current game state when Starts: " + gm.CurrentGameState);
        }

        #endregion

        #region Public Methods

        public void HandleOnStateChange()
        {
            Debug.Log("Handling state change to: " + gm.CurrentGameState);
        }

        #endregion

        #region Private Methods

        protected void LoadLevel()
        {
            gm.SetGameState(GameState.MAIN_MENU);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
    }
}