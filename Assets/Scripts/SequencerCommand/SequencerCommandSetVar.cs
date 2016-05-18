using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

    /*
    *
    */
    public class SequencerCommandSetVar : SequencerCommand
    {
        public void Start()
        {
            string var = (GetParameter(0));
            int boolean = int.Parse(GetParameter(1));

            if (boolean == 0)
            {
                DialogueLua.SetVariable(var, false);
            } else
            {
                DialogueLua.SetVariable(var, true);
            }
            
            Stop();
        }
    }
}
