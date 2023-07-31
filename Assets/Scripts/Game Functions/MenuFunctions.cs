using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuFunctions : MonoBehaviour
{

    private void OnEnable()
    {
        // set to 1 to resume time if the player access the main menu from a pause menu
        Time.timeScale = 1;
    }

    // loads the StartingArea Scene
    public void StartingArea()
    {
        //print("StartingArea button pressed.");
        SceneManager.LoadScene("Starting Area");
    }

    // loads the Level1 Scene
    public void Level1()
    {
        //print("Level1 button pressed.");
        SceneManager.LoadScene("Lvl 1");
    }

    // loads the Level2 Scene
    public void Level2()
    {
        //print("Level2 button pressed.");
        SceneManager.LoadScene("Lvl 2");
    }

    // loads the FinalBoss Scene
    public void FinalBoss()
    {
        //print("FinalBoss button pressed.");
        SceneManager.LoadScene("Final Boss Room");
    }

    // loads the Credits Scene
    public void Credits()
    {
        //print("Credits button pressed.");
        SceneManager.LoadScene("Credits");
    }

    // exits the game
    public void ExitGame()
    {
        //print("ExitGame button pressed.");
        Application.Quit();
    }

}
