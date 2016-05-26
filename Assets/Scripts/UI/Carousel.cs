using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class Carousel : MonoBehaviour {

    public List<Image> images;
	public bool contextRemainder;
	private GameObject headerCanvas;
	private GameObject canvasDialogue;

    private Vector3 moveNext;
    private Vector3 movePrevious;

    void Awake()
    {
        int index = 1;
        Vector3 posInit = new Vector3(0f, 0f, 0f);
        foreach (Image im in images)
        {
            // Position
            posInit.x = (index * (Screen.width));
            im.transform.localPosition = posInit;
            index++;
        }
		if (contextRemainder)
		{
			headerCanvas = GameObject.Find("HeaderCanvas");
			canvasDialogue = GameObject.Find("Dialogue Canvas");
		}
    }

    void Start () {

        moveNext = new Vector3(-(Screen.width), 0f);
        movePrevious = new Vector3((Screen.width), 0f);

        iTween.MoveBy(gameObject, iTween.Hash("amount", moveNext,
            "time", 1,
            "islocal", true,
            "easetype", "easeInOutSine",
            "ignoretimescale", true));
    }

	public void Demarrer()
	{
		if (!contextRemainder)
		{
			SceneManager.LoadScene(1);
		}

		else
		{
			ReinitPosition();
			gameObject.SetActive(false);
			Time.timeScale = 1;
			DialogueManager.Unpause();
			GameObject headerPanel = headerCanvas.transform.FindChild ("HeaderPanel").gameObject;
			headerPanel.SetActive (true);
			Button[] buttons = headerPanel.GetComponentsInChildren<Button>();
			foreach (Button but in buttons)
				but.enabled = true;

			if (canvasDialogue)
			{
				canvasDialogue.GetComponent<Canvas>().enabled = true;
			}
		}
	}

    public void NextImage()
    {
        iTween.MoveBy(gameObject, iTween.Hash("amount", moveNext,
            "time", 1,
            "islocal", true,
            "easetype", "easeInOutSine",
            "ignoretimescale", true));
    }

    public void Previous()
    {
        iTween.MoveBy(gameObject, iTween.Hash("amount", movePrevious,
            "time", 1,
            "islocal", true,
            "easetype", "easeInOutSine",
            "ignoretimescale", true));
    }

    public void ReinitPosition()
    {
        Awake();
    }
}