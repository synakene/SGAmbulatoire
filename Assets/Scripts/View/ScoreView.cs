using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreView {

    GameObject ProfilPanel;
    GameObject ProfileButton;
    GameObject ScorePanel;
    ScoreAnim scoreAnim;

    public ScoreView()
    {
        ProfilPanel = GameObject.Find("HeaderCanvas/ProfilPanel");
        ProfileButton = GameObject.Find("ProfileButton");
        ScorePanel = GameObject.Find("HeaderCanvas/ScorePanel");
        scoreAnim = ScorePanel.transform.FindChild("Back").GetComponent<ScoreAnim>();
    }

    public void Update()
    {
        // Update Profil panel
        if (ProfilPanel == null)
        {
            ProfilPanel = GameObject.Find("HeaderCanvas/ProfilPanel");
        }
        ProfilPanel.transform.FindDeepChild("GoalPanel/Goal1/count1").GetComponent<Text>().text = (Data.curScoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();
        ProfilPanel.transform.FindDeepChild("GoalPanel/Goal2/count2").GetComponent<Text>().text = (Data.curScoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();
        ProfilPanel.transform.FindDeepChild("GoalPanel/Goal3/count3").GetComponent<Text>().text = (Data.curScoreObj3).ToString() + "/" + (Data.MaxScoreObj3).ToString();

        if (ProfileButton == null)
        {
            ProfileButton = GameObject.Find("ProfileButton");
        }
        iTween.PunchScale(ProfileButton.gameObject, new Vector3(0.3f, 0.3f), 1.2f);
    }

    public void ScorePanelAnim (int pts1, int pts2, int pts3)
    {
        // Update Score panel
        RectTransform rtImage;
        if (ScorePanel == null)
        {
            ScorePanel = GameObject.Find("HeaderCanvas/ScorePanel");
        }
        GameObject background = ScorePanel.transform.FindChild("Back").gameObject;

        GameObject op1 = background.transform.FindChild("op1").gameObject;
        GameObject op2 = background.transform.FindChild("op2").gameObject;
        GameObject op3 = background.transform.FindChild("op3").gameObject;
        Text num1 = op1.transform.FindChild("num1").gameObject.GetComponent<Text>();
        Text num2 = op2.transform.FindChild("num2").gameObject.GetComponent<Text>();
        Text num3 = op3.transform.FindChild("num3").gameObject.GetComponent<Text>();
        rtImage = background.GetComponent<RectTransform>();

        // Init anchoredPosition and cancels coroutines
        scoreAnim.StopAllCoroutines();
        iTween.Stop(scoreAnim.gameObject);
        rtImage.anchoredPosition = new Vector2(245f, 130f);

        // Active and set num
        if (pts1 == 0) { op1.SetActive(false); } else { op1.SetActive(true); num1.text = pts1.ToString(); }
        if (pts2 == 0) { op2.SetActive(false); } else { op2.SetActive(true); num2.text = pts2.ToString(); }
        if (pts3 == 0) { op3.SetActive(false); } else { op3.SetActive(true); num3.text = pts3.ToString(); }

        // Sizes
        if (pts1 != 0 && pts2 == 0 && pts3 == 0 || pts1 == 0 && pts2 != 0 && pts3 == 0 || pts1 == 0 && pts2 == 0 && pts3 != 0)
        {
            rtImage.sizeDelta = new Vector2(100, 140);
        }

        if ((pts1 != 0 && pts2 != 0 && pts3 == 0) || (pts1 != 0 && pts3 != 0 && pts2 == 0) || (pts2 != 0 && pts3 != 0 && pts1 == 0))
        {
            rtImage.sizeDelta = new Vector2(180, 140);
        }

        if (pts1 != 0 && pts2 != 0 && pts3 != 0)
        {
            rtImage.sizeDelta = new Vector2(250, 140);
        }

        // Anim background
        scoreAnim.Move();
    }
}
