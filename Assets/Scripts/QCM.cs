using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

public class QCM : MonoBehaviour {

    bool r1 = false;
    bool r2 = false;
    bool r3 = false;
    bool r4 = false;
    bool r5 = false;
    bool r6 = false;

    OverMind OverMind;

    void Start () {
        GameObject GameManager = GameObject.Find("GameManager");
        OverMind = GameManager.GetComponent<OverMind>();
    }

    public void ReinitVal()
    {
        r1 = false;
        r2 = false;
        r3 = false;
        r4 = false;
        r5 = false;
        r6 = false;
    }

    public void ValideQCM(int num)
    {
        if (num == 1)
        {
            if (r1 && r2 && r3)
            {
                OverMind.AddPoint(0,2);
            }
        }

        if (num == 2)
        {
            if (r1 && r2 && !r3 && r4 && !r5 && r6)
            {
                OverMind.AddPoint(0,2);
            }
        }

        if (num == 3)
        {
            if (!r1 && r2 && !r3 && !r4 && r5)
            {
                OverMind.AddPoint(0, 2);
            }
        }

        iTween.MoveTo(OverMind.FeuilleQCM.gameObject, iTween.Hash("y", (Screen.height),
            "time", 1.5,
            "islocal", true
        ));

        OverMind.FeedbackQCM.SetActive(true);

        iTween.MoveFrom(OverMind.FeedbackQCM.gameObject, iTween.Hash("y", -(Screen.height),
            "time", 1.5,
            "islocal", true
        ));
    }

    public void ValideFeedback()
    {
        iTween.MoveTo(OverMind.FeedbackQCM.gameObject, iTween.Hash("y", (Screen.height),
            "time", 1.5,
            "islocal", true,
            "oncompletetarget", gameObject,
            "oncomplete", "Finish"
        ));
    }

    private void Finish()
    {
        OverMind.FeuilleQCM.SetActive(false);
        OverMind.FeedbackQCM.SetActive(false);
        OverMind.canvasDialogue.GetComponent<Canvas>().enabled = true;
        DialogueManager.ConversationView.OnConversationContinue();
        OverMind.headerCanvas.GetComponent<Canvas>().enabled = true;
        ReinitVal();
    }

    public void CheckQCM(int button)
    {
        switch (button)
        {
            case 1:
                GameObject rep1 = OverMind.QCMCanvas.transform.FindDeepChild(gameObject.name + "/Reponse1").gameObject;
                Image im1 = rep1.GetComponentInChildren<Image>();
                Button but1 = rep1.GetComponentInChildren<Button>();
                if (im1.sprite.name == "QCM_btn_down")
                {
                    r1 = false;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_up");
                    im1.sprite = sprite;

                    SpriteState state = new SpriteState();
                    state.highlightedSprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_over");
                    but1.spriteState = state;
                }
                else
                {
                    r1 = true;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_down");
                    im1.sprite = sprite;

                    SpriteState state = new SpriteState();
                    but1.spriteState = state;
                }
                break;

            case 2:
                GameObject rep2 = OverMind.QCMCanvas.transform.FindDeepChild(gameObject.name + "/Reponse2").gameObject;
                Image im2 = rep2.GetComponentInChildren<Image>();
                Button but2 = rep2.GetComponentInChildren<Button>();
                if (im2.sprite.name == "QCM_btn_down")
                {
                    r2 = false;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_up");
                    im2.sprite = sprite;

                    SpriteState state = new SpriteState();
                    state.highlightedSprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_over");
                    but2.spriteState = state;
                }
                else
                {
                    r2 = true;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_down");
                    im2.sprite = sprite;

                    SpriteState state = new SpriteState();
                    but2.spriteState = state;
                }
                break;

            case 3:
                GameObject rep3 = OverMind.QCMCanvas.transform.FindDeepChild(gameObject.name + "/Reponse3").gameObject;
                Image im3 = rep3.GetComponentInChildren<Image>();
                Button but3 = rep3.GetComponentInChildren<Button>();
                if (im3.sprite.name == "QCM_btn_down")
                {
                    r3 = false;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_up");
                    im3.sprite = sprite;

                    SpriteState state = new SpriteState();
                    state.highlightedSprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_over");
                    but3.spriteState = state;
                }
                else
                {
                    r3 = true;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_down");
                    im3.sprite = sprite;

                    SpriteState state = new SpriteState();
                    but3.spriteState = state;
                }
                break;

            case 4:
                GameObject rep4 = OverMind.QCMCanvas.transform.FindDeepChild(gameObject.name + "/Reponse4").gameObject;
                Image im4 = rep4.GetComponentInChildren<Image>();
                Button but4 = rep4.GetComponentInChildren<Button>();
                if (im4.sprite.name == "QCM_btn_down")
                {
                    r4 = false;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_up");
                    im4.sprite = sprite;

                    SpriteState state = new SpriteState();
                    state.highlightedSprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_over");
                    but4.spriteState = state;
                }
                else
                {
                    r4 = true;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_down");
                    im4.sprite = sprite;

                    SpriteState state = new SpriteState();
                    but4.spriteState = state;
                }
                break;

            case 5:
                GameObject rep5 = OverMind.QCMCanvas.transform.FindDeepChild(gameObject.name + "/Reponse5").gameObject;
                Image im5 = rep5.GetComponentInChildren<Image>();
                Button but5 = rep5.GetComponentInChildren<Button>();
                if (im5.sprite.name == "QCM_btn_down")
                {
                    r5 = false;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_up");
                    im5.sprite = sprite;

                    SpriteState state = new SpriteState();
                    state.highlightedSprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_over");
                    but5.spriteState = state;
                }
                else
                {
                    r5 = true;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_down");
                    im5.sprite = sprite;

                    SpriteState state = new SpriteState();
                    but5.spriteState = state;
                }
                break;

            case 6:
                GameObject rep6 = OverMind.QCMCanvas.transform.FindDeepChild(gameObject.name + "/Reponse6").gameObject;
                Image im6 = rep6.GetComponentInChildren<Image>();
                Button but6 = rep6.GetComponentInChildren<Button>();
                if (im6.sprite.name == "QCM_btn_down")
                {
                    r6 = false;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_up");
                    im6.sprite = sprite;

                    SpriteState state = new SpriteState();
                    state.highlightedSprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_over");
                    but6.spriteState = state;
                }
                else
                {
                    r6 = true;
                    Sprite sprite = Resources.Load<Sprite>("UI/Images/SG1/picto/QCM_btn_down");
                    im6.sprite = sprite;

                    SpriteState state = new SpriteState();
                    but6.spriteState = state;
                }
                break;
        }
    }
}
