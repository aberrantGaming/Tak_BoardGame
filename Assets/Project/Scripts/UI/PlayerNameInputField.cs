using UnityEngine;
using UnityEngine.UI;

namespace Com.SeeSameGames.Tak
{
    /// <summary>
    /// Player name input field. Let the user input his name, will apear above the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {

        #region Private Variables

        static string playerNamePrefKey = "PlayerName";     // Store the PlayerPref Key to avoid typos

        #endregion

        #region MonoBehaviour Callbacks

        /// <summary>
        /// MonoBehaviour method called on GameObject by Unity during initialization phase.
        /// </summary>
        void Start()
        {
            string defaultName = "";
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.playerName = defaultName;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Sets the name of the player, and saves it in the PlayerPrefs for future sessions.
        /// </summary>
        /// <param name="value"></param>
        public void SetPlayerName(string value)
        {
            // #Important
            PhotonNetwork.playerName = value + " ";     // Force a trailing space string in case value is an empty string

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }

        #endregion

    }
}

