using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using System.Collections;

public class HeaderUI : MonoBehaviour
{
    public GameObject helpPanel;
    public Button helpButton;

    public GameObject quitPanel;
    public Button quitButton;

    public GameObject profilePanel;
    public Button profileButton;
    public Animation anim;

    public GameObject returnToHomePanel;
    public Button homeButton;

    public GameObject contextPanel;

    private GameObject canvasDialogue;

    public GameObject headerPanel;

    public Text time;
    private float cpt;
    private int min = 0;
    private int sec = 0;

    void Awake()
    {
        canvasDialogue = GameObject.Find("Dialogue Canvas");
    }

    void Start()
    {
        helpPanel.SetActive(false);
        quitPanel.SetActive(false);
        returnToHomePanel.SetActive(false);
        contextPanel.SetActive(false);

        min = Data.min;
        sec = Data.sec;

        GameObject.Find("count1").GetComponent<Text>().text = (Data.scoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();
        GameObject.Find("count2").GetComponent<Text>().text = (Data.scoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();
    }

    void OnDestroy()
    {
        Data.min = min;
        Data.sec = sec;
    }

    void Update()
    {
        CalculateTime();
    }

    public void PointerEnterProfile()
    {
        if (profileButton.enabled)
            anim.Play("MoveIn");
    }

    public void PointerExitProfile()
    {
        if (profileButton.enabled)
            anim.Play("MoveOut");
    }

    public void CalculateTime()
    {
        cpt += Time.deltaTime * Time.timeScale;
        if (cpt >= 1)
        {
            sec += 1;
            cpt = 0;
        }
        if (sec >= 60)
        {
            min += 1;
            sec = 0;
            cpt = 0;
        }
        string formatedTime = string.Format("{00:00}:{01:00}", min, sec);
        time.text = formatedTime;
    }

    public void HelpPress()
    {
        if (helpPanel.activeSelf)
        {
            Pause(false);
            quitButton.enabled = true;
            helpButton.enabled = true;
            profileButton.enabled = true;
            homeButton.enabled = true;
            headerPanel.SetActive(true);
            helpPanel.SetActive(false);

            if (canvasDialogue)
            {
                canvasDialogue.GetComponent<Canvas>().enabled = true;
            }

        }
        else
        {
            quitButton.enabled = false;
            profileButton.enabled = false;
            homeButton.enabled = false;
            helpPanel.SetActive(true);

            if (canvasDialogue)
            {
                canvasDialogue.GetComponent<Canvas>().enabled = false;
            }
            Pause(true);
        }

    }

    public void ExitPress()
    {
        quitPanel.SetActive(true);
        quitButton.enabled = false;
        helpButton.enabled = false;
        profileButton.enabled = false;
        homeButton.enabled = false;
        Pause(true);
    }

    public void HomePress()
    {
        returnToHomePanel.SetActive(true);
        quitButton.enabled = false;
        helpButton.enabled = false;
        profileButton.enabled = false;
        homeButton.enabled = false;
        Pause(true);
    }

    public void ShowContext()
    {
        Pause(true);
        contextPanel.SetActive(true);
        returnToHomePanel.SetActive(false);

        if (canvasDialogue)
        {
            canvasDialogue.GetComponent<Canvas>().enabled = false;
        }

        headerPanel.SetActive(false);
    }

    public void ConfirmExit()
    {
        if (Application.isWebPlayer)
        {
            Time.timeScale = 1;
            Data.min = 0;
            Data.sec = 0;
            DialogueManager.Unpause();
            Data.reinitScore();
            Data.ReinitLuaVar();
            SceneManager.LoadScene(0);
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Time.timeScale = 1;
            Data.min = 0;
            Data.sec = 0;
            DialogueManager.Unpause();
            Data.reinitScore();
            Application.Quit();
        }

        else
        {
            Time.timeScale = 1;
            Data.min = 0;
            Data.sec = 0;
            DialogueManager.Unpause();
            Data.reinitScore();
            Data.ReinitLuaVar();
            SceneManager.LoadScene(0);
        }
    }

    public void ConfirmReturnToHome()
    {
        Time.timeScale = 1;
        Data.min = 0;
        Data.sec = 0;
        DialogueManager.Unpause();
        DialogueManager.StopConversation();
        Data.ReinitLuaVar();
        Data.reinitScore();
        SceneManager.LoadScene(0);
    }

    public void NoPress()
    {
        Pause(false);
        quitPanel.SetActive(false);
        returnToHomePanel.SetActive(false);
        quitButton.enabled = true;
        helpButton.enabled = true;
        profileButton.enabled = true;
        homeButton.enabled = true;
    }


    public void CloseWindow()
    {
        StartCarousel sc = GameObject.Find("ContextPanel").GetComponent<StartCarousel>();
        sc.ReinitPosition();
        contextPanel.SetActive(false);
        Pause(false);

        quitButton.enabled = true;
        helpButton.enabled = true;
        profileButton.enabled = true;
        homeButton.enabled = true;
        headerPanel.SetActive(true);

        if (canvasDialogue)
        {
            canvasDialogue.GetComponent<Canvas>().enabled = true;
        }
    }

    private void Pause(bool pause)
    {
        if (pause)
        {
            Time.timeScale = 0;
            DialogueManager.Pause();
        }
        else
        {
            Time.timeScale = 1;
            DialogueManager.Unpause();
        }
    }
}
