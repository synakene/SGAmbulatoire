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

                OverMind overmind = GameObject.Find("GameManager").GetComponent<OverMind>();
                overmind.AddPoint(score1, score2, score3);
            }

            Stop();
        }
    }
}
