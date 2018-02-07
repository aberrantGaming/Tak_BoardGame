using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        TMPro.TMP_Dropdown Selection = gameObject.GetComponentInChildren<TMPro.TMP_Dropdown>();

        if (Selection.value == 0)
            Utils.SelectGameboard("Beginner");
        else if (Selection.value == 2)
            Utils.SelectGameboard("Master");

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
