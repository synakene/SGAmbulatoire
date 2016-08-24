using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CalculScore : MonoBehaviour {

    private Text score1;
    private Text score2;
    private Text score3;

    // Use this for initialization
    void Start () {
        score1 = GameObject.Find("score1").GetComponent<Text>();
        score2 = GameObject.Find("score2").GetComponent<Text>();
        score3 = GameObject.Find("score3").GetComponent<Text>();

        if ((Data.MaxScoreObj1 != 0 && Data.MaxScoreObj2 != 0 && Data.MaxScoreObj3 != 0))
        {
            score1.text = ((Data.curScoreObj1 * 100) / Data.MaxScoreObj1).ToString() + " %";
            score2.text = ((Data.curScoreObj2 * 100) / Data.MaxScoreObj2).ToString() + " %";
            score3.text = ((Data.curScoreObj3 * 100) / Data.MaxScoreObj3).ToString() + " %";
        }
    }
}
