using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockNote : MonoBehaviour {

    public Button noteBookButton;
    public GameObject noteBookPanel;

    bool inScreen;

    void Start () {
        RectTransform noteBookRT = noteBookPanel.gameObject.GetComponent<RectTransform>();
        noteBookPanel.gameObject.SetActive(true);
        Vector3 posOut = new Vector3(-noteBookRT.sizeDelta.x - 512f, 242f);
        noteBookRT.localPosition = posOut;
        noteBookPanel.GetComponentInChildren<Text>().text = "";
        inScreen = false;
    }

    public void BlockNotePress()
    {
        RectTransform noteBookRT = noteBookPanel.gameObject.GetComponent<RectTransform>();
        Vector3 posIn = new Vector3(-330f, noteBookRT.position.y);
        Vector3 posOut = new Vector3(-noteBookRT.sizeDelta.x, noteBookRT.position.y);

        if (inScreen)
        {
            inScreen = false;
            iTween.MoveTo(noteBookPanel, iTween.Hash(
                "x", posOut.x,
                "time", 1f,
                "islocal", false
            ));
        } else
        {
            inScreen = true;
            iTween.MoveTo(noteBookPanel, iTween.Hash(
                "x", posIn.x,
                "time", 1f,
                "islocal", false
            ));
        }
    }
}
