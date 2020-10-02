using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DialogueChanging : MonoBehaviour
{

    public YarnProgram curProgram;

    public GameObject[] curSpeaker;

    public GameObject DialogueRunner;

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
        UI.MarkLineComplete();
    }
}
