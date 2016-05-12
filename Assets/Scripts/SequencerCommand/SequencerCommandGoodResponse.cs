using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

    /*
    * Incrémente les points des objectifs en parametre + leurs totaux
    * Ex : GoodResponse(2,0,-3) -> incrémente de 2 points l'objectif 1, de 0 points l'objectif 2, de -3 points l'objectif 3 + le total pour chacun
    */
    public class SequencerCommandGoodResponse : SequencerCommand
    {
        public void Start()
        {
            int score1 = int.Parse(GetParameter(0));
            int score2 = int.Parse(GetParameter(1));
            int score3 = int.Parse(GetParameter(2));

            Data.scoreObj1 += score1;
            if (Data.scoreObj1 < 0) { Data.scoreObj1 = 0; }
            Data.MaxScoreObj1 += Mathf.Abs(score1);
            GameObject.Find("HeaderPanel").GetComponent<Animation>().Play("AddScore");
            GameObject.Find("count1").GetComponent<Text>().text = (Data.scoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();                

            Data.scoreObj2 += score2;
            if (Data.scoreObj2 < 0) { Data.scoreObj2 = 0; }
            Data.MaxScoreObj2 += Mathf.Abs(score2);
            GameObject.Find("HeaderPanel").GetComponent<Animation>().Play("AddScore");
            GameObject.Find("count2").GetComponent<Text>().text = (Data.scoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();

            Data.scoreObj3 += score3;
            if (Data.scoreObj3 < 0) { Data.scoreObj3 = 0; }
            Data.MaxScoreObj3 += Mathf.Abs(score3);
            GameObject.Find("HeaderPanel").GetComponent<Animation>().Play("AddScore");
            GameObject.Find("count3").GetComponent<Text>().text = (Data.scoreObj3).ToString() + "/" + (Data.MaxScoreObj3).ToString();
        }
    }
}
