using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {

        SceneManager.LoadScene("CamScene", LoadSceneMode.Additive);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Additive);

        //SceneManager.UnloadSceneAsync(2);

    }
}
