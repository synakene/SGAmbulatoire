using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandSaveChoice : SequencerCommand {

		public void Start() {
            int id = DialogueManager.CurrentConversationState.subtitle.dialogueEntry.id;
            int idConv = DialogueManager.ConversationModel.GetConversationID(DialogueManager.CurrentConversationState);
            string feedback = Data.CompleteFeedback.GetFeedback(idConv, id);
            Data.CompleteFeedback.SetChoice(id, feedback);
            Stop();
        }
	}
}
