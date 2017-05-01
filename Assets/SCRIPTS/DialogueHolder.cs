using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueHolder : MonoBehaviour {
    public string dialogue;
    private DialogueManager dMan;
    private bool playerCollide = false;
    private bool requestInput = false;
    public string[] dialogueLines;
    public bool firstTime = true;


	void Start () {
        dMan = FindObjectOfType<DialogueManager>();
    }

    void Update () {
        if ((Input.GetKeyDown(KeyCode.Tab) && playerCollide) || (playerCollide && firstTime))
        {
            firstTime = false;

            dMan.dialogueLines = dialogueLines;
            dMan.ShowBox(dialogue);

        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {

        if(other.gameObject.name == "Player")  
        {
            playerCollide = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerCollide = false;
            dMan.ResetDialouge();
        }
    }


}
