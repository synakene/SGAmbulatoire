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
            Data.scoreObj3 = Data.curScoreObj3;
            Stop();
        }
    }
}
