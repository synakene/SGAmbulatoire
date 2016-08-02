using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreView {

    GameObject ProfilPanel;
    GameObject ProfileButton;

    public ScoreView()
    {
        ProfilPanel = GameObject.Find("HeaderCanvas/ProfilPanel");
        ProfileButton = GameObject.Find("ProfileButton");
    }

    public void Update()
    {
        if (ProfilPanel == null)
        {
            ProfilPanel = GameObject.Find("HeaderCanvas/ProfilPanel");
        }
        ProfilPanel.transform.FindDeepChild("GoalPanel/Goal1/count1").GetComponent<Text>().text = (Data.curScoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();
        ProfilPanel.transform.FindDeepChild("GoalPanel/Goal2/count2").GetComponent<Text>().text = (Data.curScoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();

        if (ProfileButton == null)
        {
            ProfileButton = GameObject.Find("ProfileButton");
        }
        iTween.PunchScale(ProfileButton.gameObject, new Vector3(0.3f, 0.3f), 1.2f);
    }
}
