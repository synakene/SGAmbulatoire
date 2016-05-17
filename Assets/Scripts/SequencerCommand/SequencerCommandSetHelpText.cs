using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {


    public class SequencerCommandSetHelpText : SequencerCommand
    {
        public void Start()
        {
            int num = int.Parse(GetParameter(0));
            Transform helpTransform = GameObject.Find("HelpCanvas").transform;
            Text[] helpTextTab = helpTransform.gameObject.GetComponentsInChildren<Text>(true);

            Text helpText = helpTextTab[0];
            foreach (Text t in helpTextTab)
            {
                if (t.name == "Text")
                    helpText = t;
            }

            switch (num)
            {
                case 0:
                    helpText.text = "Attention à votre ergonomie, rester debout pendant tout l’entretien va vous fatiguer. De plus, vous n’êtes pas à la même hauteur que le patient pour communiquer.";
                    break;
                case 1:
                    helpText.text = "Ce sont les affaires du patient !";
                    break;
                case 2:
                    helpText.text = "Attention à l’hygiène, ce n’est pas votre lit ! De plus, le patient peut ressentir votre geste comme une intrusion dans son espace personnel.";
                    break;
            }

            Stop();
        }
    }
}
