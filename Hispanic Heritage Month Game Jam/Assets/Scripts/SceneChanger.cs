using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public GameObject credits, main;

    public void Exit()
    {
        Application.Quit();
    }

    public void Credits()
    {
        credits.SetActive(true);
        main.SetActive(false);
    }

    public void Main()
    {
        credits.SetActive(false);
        main.SetActive(true);
    }

    public void startGame()
    {

        SceneManager.LoadScene("CamScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);

        //SceneManager.UnloadSceneAsync(2);

    }
}
