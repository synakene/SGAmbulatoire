using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandTimer : SequencerCommand {

		public IEnumerator Start() {
            float duration = GetParameterAsFloat(0);
            int entryID = int.Parse(GetParameter(1));
            KeyValuePair<int, float> info = new KeyValuePair<int, float>(entryID, duration);

            StartCoroutine("Count", info);
            yield return null;
        }

        IEnumerator Count(KeyValuePair<int, float> info)
        {

            GameObject go = GameObject.Find("NPC Subtitle Reminder Panel");

            GameObject jauge = Instantiate(Resources.Load("Prefabs/UI/Jauge")) as GameObject;
            jauge.transform.SetParent(go.transform, false);

            Image im = jauge.GetComponent<Image>();

            int idConv = DialogueManager.ConversationModel.GetConversationID(DialogueManager.CurrentConversationState);
            ConversationState curState = DialogueManager.CurrentConversationState;
            DialogueEntry entry = null;

            List<Link> outgoingLinks = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.outgoingLinks;

            foreach(Link l in outgoingLinks)
            {
                if(l.destinationDialogueID == info.Key)
                {
                    entry = DialogueManager.Instance.DatabaseManager.MasterDatabase.GetDialogueEntry(idConv, l.destinationDialogueID);
                }
            }

            if (entry == null)
            {
                Debug.Log("Bad parameter[1]");
                yield return null;
            }

            float duration = info.Value;
            while ((duration > 0) && (jauge != null))
            {
                if (DialogueManager.CurrentConversationState != curState)
                {
                    Destroy(jauge);
                    yield return null;
                    break;
                }
                duration -= Time.deltaTime;
                float cutoff = duration/info.Value;
                im.fillAmount = cutoff;
                yield return null;
            }
            Destroy(jauge);

            ConversationState dest = DialogueManager.ConversationModel.GetState(entry);
            DialogueManager.ConversationController.GotoState(dest);

            Stop();
        }

    }
}
