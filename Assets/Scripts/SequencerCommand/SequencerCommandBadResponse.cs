using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

    /*
    * Incrémente les totaux des objectifs en parametre
    * BadResponse(3,-2,0) -> incrémente le total de l'objectif 1 de 3, le total de l'objectif 2 de 2 !!, le total de l'objectif 3 de 0
    */
    public class SequencerCommandBadResponse : SequencerCommand
    {
        public void Start()
        {
            int score1 = int.Parse(GetParameter(0));
            int score2 = int.Parse(GetParameter(1));

            Data.MaxScoreObj1 += Mathf.Abs(score1);
            GameObject.Find("count1").GetComponent<Text>().text = (Data.scoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();                

            Data.MaxScoreObj2 += Mathf.Abs(score2);
            GameObject.Find("count2").GetComponent<Text>().text = (Data.scoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();

            Stop();
        }
    }
}
