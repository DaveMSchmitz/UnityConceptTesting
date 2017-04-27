using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public GameObject dBox;
    public Text dText;

    public bool dialogueActive;

    public string[] dialogueLines;
    public int currLine;

	void Start () {
	}
	
	void Update () {
        if (!dialogueActive && Input.GetKeyDown(KeyCode.Tab))
        {
            currLine = 0;
        }
        else if (dialogueActive && Input.GetKeyDown(KeyCode.Tab))
        {
           
            currLine++;
        }

        if(currLine >= dialogueLines.Length)
        {
            dBox.SetActive(false);
            dialogueActive = false;
            currLine = 0;
        }

        dText.text = dialogueLines[currLine];

    }

    public void ShowBox(string dialogue)
    {
        dialogueActive = true;
        dBox.SetActive(true);
        dText.text = dialogue;
    }

    public void ShowDialogue()
    {
        dialogueActive = true;
        dBox.SetActive(true);
    }
}
