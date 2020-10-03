using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

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

    private bool canGo = true;

    private DialogueUI UI;


    // Start is called before the first frame update
    void Start()
    {
        UI = DialogueRunner.GetComponent<DialogueUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void startText()
    {
        StartCoroutine("TextGoing");
    }

    public IEnumerator TextGoing()
    {
        yield return new WaitForSeconds(2f);
        
        changText();
        UI.MarkLineComplete();
    }

    public void changText()
    {
        Debug.Log(runner.variableStorage.GetValue("$bubble").AsString);
        switch (runner.variableStorage.GetValue("$bubble").AsString)
        {
            case "PINK":
                curText.transform.localPosition = new Vector3(600f, 325f, 0);
                RedText.GetComponent<Image>().overrideSprite = PinkImage; 

                
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
                RedText.GetComponent<Image>().overrideSprite = OrangeImage;


                break;
            case "RED":
                curText.transform.localPosition = new Vector3(5.1f, -228.9f, 0);

                RedText.GetComponent<Image>().overrideSprite = RedImage;

                break;
            default:
                break;

        }
        Debug.Log(RedText.GetComponent<Image>().sprite.name);
        

    }
}
