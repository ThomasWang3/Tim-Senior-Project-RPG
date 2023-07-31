using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFunctions : MonoBehaviour
{

    // Disables (closes) the UI gameObject and resumes time
    public void Close()
    {
        //print("Pause Menu closed");
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    // loads the MainMenu Scene
    public void MainMenu()
    {
        //print("MainMenu button pressed");
        SceneManager.LoadScene("Main Menu");
    }


}
