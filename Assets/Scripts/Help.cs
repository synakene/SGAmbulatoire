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
        HelpSheet.GetComponentInChildren<Button>().enabled = false;
        iTween.ValueTo(HelpSheet.gameObject, iTween.Hash(
            "from", 1.0f,
            "to", 0f,
            "time", 0.5f,
            "onupdate", "Fade",
            "oncomplete", "Finish"
        ));
    }

    private void Fade(float fade)
    {
        HelpSheet.GetComponent<CanvasGroup>().alpha = fade;
    }

    private void Finish()
    {
        HelpSheet.GetComponent<CanvasGroup>().alpha = 1.0f;
        HelpSheet.SetActive(false);
        GameObject canvasDialogue = GameObject.Find("Dialogue Canvas");
        canvasDialogue.GetComponent<Canvas>().enabled = true;
        DialogueManager.ConversationView.OnConversationContinue();
    }
}
