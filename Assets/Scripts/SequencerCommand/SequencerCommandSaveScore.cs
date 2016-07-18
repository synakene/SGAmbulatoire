using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandSaveScore : SequencerCommand
    {
        public void Start()
        {
            Data.scoreObj1 = Data.curScoreObj1;
            Data.scoreObj2 = Data.curScoreObj2;

            GameObject.Find("count1").GetComponent<Text>().text = (Data.curScoreObj1).ToString() + "/" + (Data.MaxScoreObj1).ToString();
            GameObject.Find("count2").GetComponent<Text>().text = (Data.curScoreObj2).ToString() + "/" + (Data.MaxScoreObj2).ToString();

            Stop();
        }
    }
}
