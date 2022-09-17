using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject menu;
    private MusicManager m;
    [SerializeField]
    private AudioMixer mixer;

    void Start()
    {
        m = GameObject.Find("MusicManager").GetComponent<MusicManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseOpen(menu);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CloseOpen(GameObject obj)
    {
        obj.SetActive(!obj.activeSelf);
        CursorMode mode = CursorMode.Auto;
        //float xspot = hand.width / 4;
        //float yspot = hand.height / 4;
        //Vector2 hotSpot = new Vector2(xspot, yspot);
        Texture2D tex = new Texture2D(32, 32);
        Cursor.SetCursor(null, Vector2.zero, mode);

        Time.timeScale = (obj.activeSelf) ? 1 : 0;
    }

    public void MusicSlider(float sound)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10( sound) * 20);
    }

    public void MainMenu()
    {
        var sceneChanger = GameObject.Find("Main Camera").GetComponent<SceneChanger>();
        SceneManager.UnloadSceneAsync("CamScene");
        SceneManager.UnloadSceneAsync("SampleScene");
        sceneChanger.ResetGame();
    }
}
