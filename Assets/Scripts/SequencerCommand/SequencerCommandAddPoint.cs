using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

    /*
    */
    public class SequencerCommandAddPoint : SequencerCommand
    {
        public void Start()
        {
            if (GetParameter(3) == null)
            {
                int score1 = int.Parse(GetParameter(0));
                int score2 = int.Parse(GetParameter(1));
                int score3 = int.Parse(GetParameter(2));

                Data.curScoreObj1 += score1;
                if (Data.curScoreObj1 < 0) { Data.curScoreObj1 = 0; }
                GameObject.Find("count1").GetComponent<Text>().text = (Data.curScoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();

                Data.curScoreObj2 += score2;
                if (Data.curScoreObj2 < 0) { Data.curScoreObj2 = 0; }
                GameObject.Find("count2").GetComponent<Text>().text = (Data.curScoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();

                Data.curScoreObj3 += score3;
                if (Data.curScoreObj3 < 0) { Data.curScoreObj3 = 0; }
                GameObject.Find("count3").GetComponent<Text>().text = (Data.curScoreObj3).ToString() + "/" + (Data.MaxScoreObj3).ToString();

                GameObject ProfileButton = GameObject.Find("ProfileButton");
                iTween.PunchScale(ProfileButton.gameObject, new Vector3(0.3f, 0.3f), 1.2f);
            }

            Stop();
        }
    }
}
