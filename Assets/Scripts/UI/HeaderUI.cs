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
    //private GameObject jaugeCanvas;

    public GameObject headerPanel;

    public Text time;
    private float cpt;
    private int min = 0;
    private int sec = 0;

    //private List<AudioSource> audiosList;

    void Awake()
    {
        //audiosList = new List<AudioSource>();
        //GameObject npc = GameObject.FindGameObjectWithTag("Louis");
        //GameObject player = GameObject.FindGameObjectWithTag("Margaux");

        //AudioSource[] audios = npc.transform.GetComponentsInChildren<AudioSource>(true);
        //foreach (AudioSource audio in audios)
        //{
        //    audiosList.Add(audio);
        //}
        //audiosList.Add(player.GetComponent<AudioSource>());

        canvasDialogue = GameObject.Find("Dialogue Canvas");
        //jaugeCanvas = GameObject.Find("JaugeCanvas");
    }

    void Start()
    {
        helpPanel.SetActive(false);
        quitPanel.SetActive(false);
        returnToHomePanel.SetActive(false);
        contextPanel.SetActive(false);
        min = Data.min;
        sec = Data.sec;

        //GameObject.Find("count1").GetComponent<Text>().text = (Data.scoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();
        //GameObject.Find("count2").GetComponent<Text>().text = (Data.scoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();
        //GameObject.Find("count3").GetComponent<Text>().text = (Data.scoreObj3).ToString() + "/" + (Data.MaxScoreObj3).ToString();
        //GameObject.Find("count4").GetComponent<Text>().text = (Data.scoreObj4).ToString() + "/" + (Data.MaxScoreObj4).ToString();

        //jaugeCanvas.GetComponentInChildren<Slider>().value = Data.jaugeValue;
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
        //jaugeCanvas.SetActive(false);
        Pause(true);
    }

    public void HomePress()
    {
        returnToHomePanel.SetActive(true);
        quitButton.enabled = false;
        helpButton.enabled = false;
        profileButton.enabled = false;
        homeButton.enabled = false;
        //jaugeCanvas.SetActive(false);
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

        //quitButton.enabled = false;
        //helpButton.enabled = false;
        //profileButton.enabled = false;
        //homeButton.enabled = false;

        headerPanel.SetActive(false);
        //jaugeCanvas.SetActive(false);
    }

    public void ConfirmExit()
    {
        if (Application.isWebPlayer)
        {
            Time.timeScale = 1;
            DialogueManager.Unpause();
            //Data.reinitScore();
            SceneManager.LoadScene(0);
        }

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Time.timeScale = 1;
            DialogueManager.Unpause();
            //Data.reinitScore();
            Application.Quit();
        }

        else
        {
            Time.timeScale = 1;
            DialogueManager.Unpause();
            //Data.reinitScore();
            SceneManager.LoadScene(0);
        }
    }

    public void ConfirmReturnToHome()
    {
        Time.timeScale = 1;
        DialogueManager.Unpause();
        DialogueManager.StopConversation();
        //Data.reinitScore();
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
        //jaugeCanvas.SetActive(true);
    }


    public void CloseWindow()
    {
        StartCarousel sc = GameObject.Find("ContextPanel").GetComponent<StartCarousel>();
        sc.ReinitPosition();
        contextPanel.SetActive(false);
        Pause(false);
        //if (DialogueManager.IsConversationActive)
        //{
        //    if (DialogueManager.CurrentConversationState.HasNPCResponse)
        //    {
        //        GameObject player = GameObject.FindGameObjectWithTag("Margaux");
        //        player.GetComponent<AudioSource>().Play();
        //    }
        //    else
        //    {
        //        GameObject npc = GameObject.FindGameObjectWithTag("Louis");
        //        npc.GetComponentInChildren<AudioSource>().Play();
        //    }
        //}
        quitButton.enabled = true;
        helpButton.enabled = true;
        profileButton.enabled = true;
        homeButton.enabled = true;
        //jaugeCanvas.SetActive(true);
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
            //foreach (AudioSource audio in audiosList)
            //{
            //    audio.Pause();
            //}

            Time.timeScale = 0;
            DialogueManager.Pause();
        }
        else
        {
            Time.timeScale = 1;
            DialogueManager.Unpause();
            //foreach (AudioSource audio in audiosList)
            //{
            //    audio.UnPause();
            //}
            //if (DialogueManager.IsConversationActive && GameObject.Find("Dialogue Manager/Canvas v2/Dialogue UI/Dialogue Panel/NPC Subtitle Reminder Panel"))
            //{
            //    if (!GameObject.Find("Dialogue Manager/Canvas v2/Dialogue UI/Dialogue Panel/NPC Subtitle Reminder Panel").activeSelf)
            //    {
            //        GameObject npc = GameObject.FindGameObjectWithTag("NPC");
            //        GameObject player = GameObject.FindGameObjectWithTag("Player");
            //        if (DialogueManager.CurrentConversationState.subtitle.speakerInfo.IsPlayer)
            //        {
            //            string seq = "SALSA(Audio/" + player.GetComponent<Salsa3D>().audioClip.name + ");";
            //            DialogueManager.PlaySequence(seq, player.transform, npc.transform);
            //            StartCoroutine(PlaySeq(player.GetComponent<Salsa3D>().audioSrc));
            //        }
            //        else
            //        {
            //            string seq = "SALSA(Audio/" + npc.GetComponentInChildren<Salsa3D>().audioClip.name + ");";
            //            DialogueManager.PlaySequence(seq, npc.GetComponentInChildren<Salsa3D>().transform, player.transform);
            //            StartCoroutine(PlaySeq(npc.GetComponentInChildren<Salsa3D>().audioSrc));
            //        }
            //    }
            //}
        }
    }

    private IEnumerator PlaySeq(AudioSource clip)
    {
        bool isPlaying = true;

        while (isPlaying && !(clip.time == clip.clip.length))
        {
            //foreach (AudioSource source in audiosList)
            //{
            //    if (source.name != clip.name && source.isPlaying)
            //    {
            //        clip.Stop();
            //        isPlaying = false;
            //        break;
            //    }
            //}
            yield return null;
        }
    }
}
