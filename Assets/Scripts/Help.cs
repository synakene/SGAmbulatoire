using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;
using System.Collections.Generic;

public class Help : MonoBehaviour {

    GameObject HelpCanvas;
    GameObject HelpSheet;

    void Start () {
        HelpCanvas = GameObject.Find("HelpCanvas");
        HelpSheet = HelpCanvas.gameObject.transform.FindChild("HelpSheet").gameObject;
    }

    public void Continue()
    {
        iTween.MoveTo(HelpSheet.gameObject, iTween.Hash("y", (Screen.height),
            "time", 1.5,
            "islocal", true,
            "oncompletetarget", gameObject,
            "oncomplete", "Finish"
        ));
    }

    private void Finish()
    {
        HelpSheet.SetActive(false);
        GameObject canvasDialogue = GameObject.Find("Dialogue Canvas");
        canvasDialogue.GetComponent<Canvas>().enabled = true;
        DialogueManager.ConversationView.OnConversationContinue();

        GameObject.Find("HeaderCanvas").GetComponent<Canvas>().enabled = true;
    }
}
