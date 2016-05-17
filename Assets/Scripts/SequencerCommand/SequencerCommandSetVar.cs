using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

    /*
    * Incrémente les totaux des objectifs en parametre
    * BadResponse(3,-2,0) -> incrémente le total de l'objectif 1 de 3, le total de l'objectif 2 de 2 !!, le total de l'objectif 3 de 0
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
