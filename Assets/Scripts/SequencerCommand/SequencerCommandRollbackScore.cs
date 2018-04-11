using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandRollbackScore : SequencerCommand
    {
        public void Start()
        {
            Data.curScoreObj1 = Data.scoreObj1;
            Data.curScoreObj2 = Data.scoreObj2;
            Data.curScoreObj3 = Data.scoreObj3;

            Data.CompleteFeedback.Restart(DialogueManager.ConversationModel.GetConversationID(DialogueManager.CurrentConversationState));

            GameObject.Find("count1").GetComponent<Text>().text = (Data.curScoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();
            GameObject.Find("count2").GetComponent<Text>().text = (Data.curScoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();
            GameObject.Find("count3").GetComponent<Text>().text = (Data.curScoreObj3).ToString() + "/" + (Data.MaxScoreObj3).ToString();

            Stop();
        }
    }
}
