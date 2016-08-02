using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

    /*
    * Incr�mente les points des objectifs en parametre + leurs totaux
    * Ex : GoodResponse(2,0,-3) -> incr�mente de 2 points l'objectif 1, de 0 points l'objectif 2, de -3 points l'objectif 3 + le total pour chacun
    */
    public class SequencerCommandGoodResponse : SequencerCommand
    {
        public void Start()
        {
            if (GetParameter(2) == null)
            {
                int score1 = int.Parse(GetParameter(0));
                int score2 = int.Parse(GetParameter(1));

                Data.curScoreObj1 += score1;
                if (Data.curScoreObj1 < 0) { Data.curScoreObj1 = 0; }
                GameObject.Find("count1").GetComponent<Text>().text = (Data.curScoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();

                Data.curScoreObj2 += score2;
                if (Data.curScoreObj2 < 0) { Data.curScoreObj2 = 0; }
                GameObject.Find("count2").GetComponent<Text>().text = (Data.curScoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();

                GameObject ProfileButton = GameObject.Find("ProfileButton");
                iTween.PunchScale(ProfileButton.gameObject, new Vector3(0.3f, 0.3f), 1.2f);
            }

            Stop();
        }
    }
}
