using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using UnityEngine.SceneManagement;

public class DialogueChanging : MonoBehaviour
{

    public GameObject DialogueRunner;

    public DialogueRunner runner;

    public GameObject RedText;

    public Sprite PinkImage;

    public Sprite GreenImage;

    public Sprite RedImage;

    public Sprite BlueImage;

    public Sprite OrangeImage; 

    public Text curText;

    public YarnProgram[] programs;

    private bool canGo = true;

    private DialogueUI UI;

    private bool isEnglish = true;



    // Start is called before the first frame update
    void Start()
    {

        UI = DialogueRunner.GetComponent<DialogueUI>();
        isEnglish = GameObject.Find("Main Camera").GetComponent<SceneChanger>().isEnglish;

        Debug.Log("HOw could it be" + isEnglish);

        if (isEnglish)
        {
            runner.yarnScripts[0] = programs[0];
        }
        else if (!isEnglish)
        {
            runner.yarnScripts[0] = programs[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            UI.MarkLineComplete();
        }
    }

    public void startText()
    {
        StartCoroutine("TextGoing");
    }

    public IEnumerator TextGoing()
    {
        yield return new WaitForSeconds(2f);
        
        UI.MarkLineComplete();
        changText();
    }

    public IEnumerator KeepGoing()
    {
        yield return new WaitForSeconds(5f);
    }

    public void OnEnd()
    {
        StartCoroutine(End());
    }

    IEnumerator End()
    {

        SceneManager.LoadScene("End", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(1);

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

        yield return new WaitForSecondsRealtime(10f);
   
        SceneManager.UnloadSceneAsync(3);
        
        SceneManager.UnloadSceneAsync(2);

    }

    public void changText()
    {

        Debug.Log(runner.variableStorage.GetValue("$bubble").AsString);
        Debug.Log("Current stuff" + runner.variableStorage.GetValue("pause").AsNumber);
        if (runner.variableStorage.GetValue("$pause").AsNumber != 1)
        {
            switch (runner.variableStorage.GetValue("$bubble").AsString)
            {
                case "PINK":
                    curText.transform.localPosition = new Vector3(600f, 325f, 0);
                    RedText.GetComponent<Image>().overrideSprite = OrangeImage;


                    break;
                case "BLUE":
                    curText.transform.localPosition = new Vector3(-575f, 322.5385f, 0);
                    RedText.GetComponent<Image>().overrideSprite = BlueImage;


                    break;
                case "GREEN":
                    curText.transform.localPosition = new Vector3(-575f, 322.5385f, 0);
                    RedText.GetComponent<Image>().overrideSprite = GreenImage;


                    break;
                case "ORANGE":

                    curText.transform.localPosition = new Vector3(600f, 325f, 0);
                    RedText.GetComponent<Image>().overrideSprite = PinkImage;



                    break;
                case "RED":
                    curText.transform.localPosition = new Vector3(5.1f, -228.9f, 0);

                    RedText.GetComponent<Image>().overrideSprite = RedImage;

                    break;
                default:
                    break;

            }
        }
        else if (runner.variableStorage.GetValue("$pause").AsNumber == 0)
        {
            Debug.Log("Reached");
            StartCoroutine("KeepGoing"); }
       
    }
}
