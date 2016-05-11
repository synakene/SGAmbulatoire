using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

public class StartCarousel : MonoBehaviour {

    public List<Image> images;

    public bool contextRemainder;
    private GameObject headerPanel;
    private GameObject canvasDialogue;
    private Vector3 initPos;

    void Awake()
    {
        initPos = images[0].transform.localPosition;
        initPos.x = (Screen.width);

        if (contextRemainder)
        {
            headerPanel = GameObject.Find("HeaderPanel");
            canvasDialogue = GameObject.Find("Dialogue Canvas");
        }
    }

    void Start()
    {
        gameObject.SetActive(true);
        foreach (Image im in images)
        {
            im.transform.localPosition = initPos;
            im.gameObject.SetActive(false);

            Transform nextButton = im.gameObject.transform.FindChild("NextButton");
            Transform previousButton = im.gameObject.transform.FindChild("PreviousButton");
            Transform quitButton = im.gameObject.transform.FindChild("QuitButton");
            if (nextButton)
                nextButton.gameObject.GetComponent<Button>().enabled = false;
            if (previousButton)
                previousButton.gameObject.GetComponent<Button>().enabled = false;
            if (quitButton)
                quitButton.gameObject.GetComponent<Button>().enabled = false;

        }
        images[0].gameObject.SetActive(true);
        iTween.MoveTo(images[0].gameObject, iTween.Hash("x", 0,
            "time", 1.5,
            "islocal", true,
            "oncompletetarget", gameObject,
            "oncompleteparams", images[0].gameObject,
            "oncomplete", "EnableButton",
            "ignoretimescale", true));
    }

    public void ReinitPosition()
    {
        foreach (Image im in images)
        {
            im.transform.localPosition = initPos;
            im.gameObject.SetActive(false);
        }
        images[0].gameObject.SetActive(true);
        Transform nextButton = images[0].gameObject.transform.FindChild("NextButton");
        Transform previousButton = images[0].gameObject.transform.FindChild("PreviousButton");
        Transform quitButton = images[0].gameObject.transform.FindChild("QuitButton");
        if (nextButton)
            nextButton.gameObject.GetComponent<Button>().enabled = true;
        if (previousButton)
            previousButton.gameObject.GetComponent<Button>().enabled = true;
        if (quitButton)
            quitButton.gameObject.GetComponent<Button>().enabled = true;

        images[0].transform.localPosition = new Vector3(0, 0, 0);
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
            headerPanel.SetActive(true);
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
        int i = 0;
        foreach (Image im in images)
        {
            if (im.gameObject.activeSelf == true)
            {
                if (i < images.Count -1)
                {
                    Transform nextButton = im.gameObject.transform.FindChild("NextButton");
                    Transform previousButton = im.gameObject.transform.FindChild("PreviousButton");
                    Transform quitButton = im.gameObject.transform.FindChild("QuitButton");
                    
                    if (nextButton != null)
                    {
                        nextButton.gameObject.GetComponent<Button>().enabled = false;
                    }

                    if (previousButton != null)
                    {
                        previousButton.gameObject.GetComponent<Button>().enabled = false;
                    }

                    if (quitButton != null)
                    {
                        quitButton.gameObject.GetComponent<Button>().enabled = false;
                    }

                    iTween.MoveTo(im.gameObject, iTween.Hash("x", -(Screen.width),
                        "time", 1.5,
                        "ignoretimescale", true,
                        "oncompletetarget", gameObject,
                        "oncompleteparams", im.gameObject,
                        "islocal", true,
                        "oncomplete", "Deactive"));


                    images[i + 1].gameObject.SetActive(true);
                    iTween.MoveTo(images[i + 1].gameObject, iTween.Hash("x", 0,
                        "time", 1.5,
                        "oncompletetarget", gameObject,
                        "oncompleteparams", images[i + 1].gameObject,
                        "oncomplete", "EnableButton",
                        "islocal", true,
                        "ignoretimescale", true));

                    break;
                }
            }
            i++;
        }
    }

    public void Previous()
    {
        int i = 0;
        foreach (Image im in images)
        {
            if (im.gameObject.activeSelf == true)
            {
                if (i > 0)
                {
                    Transform nextButton = im.gameObject.transform.FindChild("NextButton");
                    Transform previousButton = im.gameObject.transform.FindChild("PreviousButton");
                    Transform quitButton = im.gameObject.transform.FindChild("QuitButton");

                    if (nextButton != null)
                    {
                        nextButton.gameObject.GetComponent<Button>().enabled = false;
                    }

                    if (previousButton != null)
                    {
                        previousButton.gameObject.GetComponent<Button>().enabled = false;
                    }

                    if (quitButton != null)
                    {
                        quitButton.gameObject.GetComponent<Button>().enabled = false;
                    }

                    iTween.MoveTo(im.gameObject, iTween.Hash("x", (Screen.width),
                        "time", 1.5,
                        "ignoretimescale", true,
                        "oncompletetarget", gameObject,
                        "oncompleteparams", im.gameObject,
                        "islocal", true,
                        "oncomplete", "Deactive"));


                    images[i - 1].gameObject.SetActive(true);
                    iTween.MoveTo(images[i - 1].gameObject, iTween.Hash("x", 0,
                        "time", 1.5,
                        "oncompletetarget", gameObject,
                        "oncompleteparams", images[i - 1].gameObject,
                        "oncomplete", "EnableButton",
                        "islocal", true,
                        "ignoretimescale", true));

                    break;
                }
            }
            i++;
        }
    }

    private void Deactive(GameObject o)
    {
        o.gameObject.SetActive(false);
    }

    private void EnableButton(GameObject o)
    {
        Transform nextButton = o.transform.FindChild("NextButton");
        Transform previousButton = o.transform.FindChild("PreviousButton");
        Transform quitButton = o.transform.FindChild("QuitButton");

        if (nextButton != null)
        {
            nextButton.gameObject.GetComponent<Button>().enabled = true;
        }

        if (previousButton != null)
        {
            previousButton.gameObject.GetComponent<Button>().enabled = true;
        }

        if (quitButton != null)
        {
            quitButton.gameObject.GetComponent<Button>().enabled = true;
        }
    }

}
