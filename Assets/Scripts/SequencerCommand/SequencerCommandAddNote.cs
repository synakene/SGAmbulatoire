using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandAddNote : SequencerCommand {

		public void Start() {
            GameObject noteBookPanel;
            GameObject notes;
            noteBookPanel = GameObject.Find("BlockNotePanel");
            notes = noteBookPanel.gameObject.transform.FindChild("Notes").gameObject;

            Text textUI = notes.GetComponent<Text>();

            //Question
            string question = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.DialogueText;
            //Reponse
            int idConv = DialogueManager.ConversationModel.GetConversationID(DialogueManager.CurrentConversationState);
            int idLink = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.outgoingLinks[0].destinationDialogueID;
            DialogueEntry entry = DialogueManager.Instance.DatabaseManager.MasterDatabase.GetDialogueEntry(idConv, idLink);
            string reponse = entry.SubtitleText;

            textUI.text += question;
            textUI.text += "\n\t- " + reponse + "\n";

            GameObject BlockNoteButton = GameObject.Find("BlockNoteButton");
            iTween.PunchScale(BlockNoteButton.gameObject, new Vector3(0.3f, 0.3f), 1.2f);

            Stop();
		}
	}
}
