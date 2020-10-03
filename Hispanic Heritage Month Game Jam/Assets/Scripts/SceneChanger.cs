using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public GameObject credits, main;
    public GameObject languageButton; 
    public bool isStarting;
    public bool isEnglish = true; 

    public void Awake(){
        isStarting = false;
    }

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
        if(!isStarting) {
            isStarting = true;
            SceneManager.LoadScene("CamScene", LoadSceneMode.Additive);
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        }
        

        //SceneManager.UnloadSceneAsync(2);

    }

    public void toggleIsEnglish()
    {
        isEnglish = !isEnglish;
        toggleLanguage();
    }

    public void toggleLanguage()
    {
        if (isEnglish)
        {
            languageButton.GetComponentInChildren<Text>().text = "English";
        }
        else if (!isEnglish)
        {
            languageButton.GetComponentInChildren<Text>().text = "Español";
        }

    }
}
