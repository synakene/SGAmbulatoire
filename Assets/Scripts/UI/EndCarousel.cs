using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class EndCarousel : MonoBehaviour {

	public List<Image> images;
	public InputField nameInputField;

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

        Text text = GameObject.Find("TextResultat").GetComponent<Text>();
        if (Application.isWebPlayer || Application.platform == RuntimePlatform.WebGLPlayer)
        {
            text.text = "Vos résultats détaillés sont visibles en cliquant sur le bouton\nsitué en haut de la fenêtre du jeu";
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

    public void OnValueChange()
    {
        Button butt = GameObject.Find("ContinueButton").GetComponent<Button>();
        if (nameInputField.text.Length == 0)
        {
            butt.interactable = false;
        }
        else
        {
            butt.interactable = true;
        }
    }

    public void OnEndEdit()
    {
        Data.playerName = nameInputField.text;
    }

    public void Demarrer()
    {
        Data.ReinitLuaVar();
        Data.reinitScore();
        Data.min = 0;
        Data.sec = 0;
        SceneManager.LoadScene(0);
    }

    public void ConfirmExit()
    {
        if (Application.isWebPlayer)
        {
            Time.timeScale = 1;
            Data.ReinitLuaVar();
            Data.reinitScore();
            Data.min = 0;
            Data.sec = 0;
            SceneManager.LoadScene(0);
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Time.timeScale = 1;
            Application.Quit();
        }

        else
        {
            Time.timeScale = 1;
            Data.ReinitLuaVar();
            Data.reinitScore();
            Data.min = 0;
            Data.sec = 0;
            SceneManager.LoadScene(0);
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
