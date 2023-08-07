using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private float delay;


    private void Start()
    {
        //image.canvasRenderer.SetAlpha(0.0f);
        //image.CrossFadeAlpha(1, delay / 2, false);
        //image.CrossFadeAlpha(0, delay / 2, false);
        StartCoroutine(DelayMenuLoad());
        //SceneManager.LoadScene("Main Menu");
    }

    IEnumerator DelayMenuLoad()
    {
        for (float i = 0; i < 1; i += (Time.deltaTime / 2)){
            image.canvasRenderer.SetAlpha(i);
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        
        for (float i = 1; i >= 0; i -= (Time.deltaTime / 2))
        {
            image.canvasRenderer.SetAlpha(i);
            yield return null;
        }
        //yie

        SceneManager.LoadScene("Main Menu");
        yield return null;
    }
}
