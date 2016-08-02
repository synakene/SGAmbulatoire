using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BlockNoteButton : MonoBehaviour {

    // Ne pas utiliser

    public GameObject blockNote;
    bool inScreen;

    void Start () {
        RectTransform noteBookRT = blockNote.gameObject.GetComponent<RectTransform>();
        blockNote.gameObject.SetActive(true);
        
        Vector3 posOut = new Vector3(-noteBookRT.rect.width - (Screen.width / 2), noteBookRT.localPosition.y);

        noteBookRT.localPosition = posOut;
        blockNote.GetComponentInChildren<Text>().text = "";
        inScreen = false;
    }

    public void BlockNotePress()
    {
        RectTransform noteBookRT = blockNote.gameObject.GetComponent<RectTransform>();
        Vector3 posIn = new Vector3(-323f - (Screen.width / 2), noteBookRT.localPosition.y);
        Vector3 posOut = new Vector3(-noteBookRT.rect.width - (Screen.width / 2), noteBookRT.localPosition.y);

        if (inScreen)
        {
            inScreen = false;
            iTween.MoveTo(blockNote, iTween.Hash(
                "x", posOut.x,
                "time", 1f,
                "islocal", true
            ));
        } else
        {
            inScreen = true;
            iTween.MoveTo(blockNote, iTween.Hash(
                "x", posIn.x,
                "time", 1f,
                "islocal", true
            ));
        }
    }
}
