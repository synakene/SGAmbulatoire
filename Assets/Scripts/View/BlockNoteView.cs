using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockNoteView {

    GameObject BlockNotePanel;
    GameObject BlockNote;
    GameObject notes;
    //GameObject BlockNoteButton;
    Button BlockNoteButton;
    bool inScreen;

    public BlockNoteView()
    {
        // Initialize GameObjects
        BlockNotePanel = GameObject.Find("BlockNotePanel");
        BlockNote = BlockNotePanel.transform.FindChild("BlockNote").gameObject;
        notes = BlockNote.gameObject.transform.FindDeepChild("Notes").gameObject;
        notes.GetComponent<Text>().text = "";

        // Set the position and activate the block-note
        RectTransform blockNoteRT = BlockNote.gameObject.GetComponent<RectTransform>();
        BlockNote.gameObject.SetActive(true);
        Vector3 posOut = new Vector3(-blockNoteRT.rect.width - (Screen.width / 2), blockNoteRT.localPosition.y);
        blockNoteRT.localPosition = posOut;

        // Block-note button initialization 
        BlockNoteButton = GameObject.Find("HeaderPanel").transform.FindChild("BlockNoteButton").GetComponent<Button>();

        // Add an onClick listener on the button to call BlockNotePress()
        BlockNoteButton.onClick.AddListener(() => { BlockNotePress();});

        // Active the GameObject of the button
        BlockNoteButton.gameObject.SetActive(true);

        inScreen = false;
        ButtonAnim();
    }

    public void Reinit()
    {
        notes.GetComponent<Text>().text = "";
        ButtonAnim();
    }


    public void BlockNotePress()
    {
        RectTransform noteBookRT = BlockNote.gameObject.GetComponent<RectTransform>();
        Vector3 posIn = new Vector3(-323f - (Screen.width / 2), noteBookRT.localPosition.y);
        Vector3 posOut = new Vector3(-noteBookRT.rect.width - (Screen.width / 2), noteBookRT.localPosition.y);

        if (inScreen)
        {
            inScreen = false;
            iTween.MoveTo(BlockNote, iTween.Hash(
                "x", posOut.x,
                "time", 1f,
                "islocal", true
            ));
        }
        else
        {
            inScreen = true;
            iTween.MoveTo(BlockNote, iTween.Hash(
                "x", posIn.x,
                "time", 1f,
                "islocal", true
            ));
        }
    }

    public void Update(string note)
    {
        Text textUI = notes.GetComponent<Text>();
        textUI.text = note;
        ButtonAnim();
    }

    public void ButtonAnim()
    {
        iTween.PunchScale(BlockNoteButton.gameObject, new Vector3(0.3f, 0.3f), 1.2f);
    }

}
