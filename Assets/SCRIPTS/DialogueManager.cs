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
        currLine = 0;
    }
	

    public void ShowBox(string dialogue)
    {
        dText.text = dialogueLines[currLine];
        dialogueActive = true;
        dBox.SetActive(true);

        ++currLine;

        if (currLine >= dialogueLines.Length) {
            dBox.SetActive(false);
            dialogueActive = false;
            currLine = 0;
        }
    }

    public void ResetDialouge() {
        dialogueActive = false;
        dBox.SetActive(false);
        currLine = 0;
    }

}
