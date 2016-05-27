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
            if (GetParameter(2) == null)
            {
                int score1 = int.Parse(GetParameter(0));
                int score2 = int.Parse(GetParameter(1));

                Data.scoreObj1 += score1;
                if (Data.scoreObj1 < 0) { Data.scoreObj1 = 0; }
                GameObject.Find("HeaderPanel").GetComponent<Animation>().Play("AddScore");
                GameObject.Find("count1").GetComponent<Text>().text = (Data.scoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();

                Data.scoreObj2 += score2;
                if (Data.scoreObj2 < 0) { Data.scoreObj2 = 0; }
                GameObject.Find("HeaderPanel").GetComponent<Animation>().Play("AddScore");
                GameObject.Find("count2").GetComponent<Text>().text = (Data.scoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();
            }

            Stop();
        }
    }
}
