using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public GameObject credits, main;
    public GameObject languageToggle;
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
        if (!isStarting) {
            isStarting = true;
            SceneManager.LoadScene("CamScene", LoadSceneMode.Additive);
            SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);
        }
        

        //SceneManager.UnloadSceneAsync(2);
    }


    public void ResetGame()
    {
        isStarting = false;
    }

    public void toggleLanguage()
    {

  
        if (languageToggle.GetComponent<Toggle>().isOn)
        {
            isEnglish = true;
        }
        else if (!languageToggle.GetComponent<Toggle>().isOn)
        {
            isEnglish = false;
           
        }
        Debug.Log("what could it be" + isEnglish);


    }
}
