using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndCarousel : MonoBehaviour {

    public List<Image> images;
    public GameObject endPanel;
    private Vector3 initPos;

    void Awake()
    {
        initPos = images[0].transform.localPosition;
        initPos.x = (Screen.width);

        Text text = GameObject.Find("TextResultat").GetComponent<Text>();
        if (Application.isWebPlayer)
        {
            text.text = "Vos résultats détaillés sont visibles en cliquant sur le bouton\nsitué en haut de la fenêtre du jeu";
        }

        endPanel.gameObject.SetActive(false);
        endPanel.transform.localPosition = initPos;
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

    void Start()
    {

    }

    public void Demarrer()
    {
        Data.ReinitLuaVar();
        SceneManager.LoadScene(0);
    }

    public void ConfirmExit()
    {
        if (Application.isWebPlayer)
        {
            Time.timeScale = 1;
            Data.ReinitLuaVar();
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
            SceneManager.LoadScene(0);
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
                } else
                {
                    iTween.MoveTo(im.gameObject, iTween.Hash("x", -(Screen.width),
                        "time", 1.5,
                        "ignoretimescale", true,
                        "oncompletetarget", gameObject,
                        "oncompleteparams", im.gameObject,
                        "islocal", true,
                        "oncomplete", "Deactive"));
                    endPanel.SetActive(true);

                    iTween.MoveTo(endPanel, iTween.Hash("x", 0,
                        "time", 1.5,
                        "oncompletetarget", gameObject,
                        "oncompleteparams", endPanel,
                        "oncomplete", "EnableButton",
                        "islocal", true,
                        "ignoretimescale", true));
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
