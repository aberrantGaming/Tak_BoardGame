using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.SeeSameGames.Tak
{
    public class Intro : MonoBehaviour
    {
        #region Public Variables

        public float DelayTimer = 3f;

        #endregion

        #region Private Variables

        GameManager gm;

        #endregion

        #region MonoBehaviour Callbacks

        protected void Awake()
        {
            gm = GameManager.Instance;
            gm.OnStateChange += HandleOnStateChange;

            Debug.Log("State on Awake: " + gm.CurrentGameState);

            gm.SetGameState(GameState.INTRO);
        }

        protected void Start()
        {
            Debug.Log("State on Start: " + gm.CurrentGameState);
        }

        #endregion

        #region Private Methods

        protected void HandleOnStateChange()
        {
            Debug.Log("Handling state change to: " + gm.CurrentGameState);

            switch (gm.CurrentGameState)
            {
                case (GameState.INTRO):
                    Invoke("LoadLauncher", DelayTimer);
                    break;
            }
        }

        protected void LoadLauncher()
        {
            gm.SetGameState(GameState.IN_LAUNCHER);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        #endregion
    }
}