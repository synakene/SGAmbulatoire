using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandContinue : SequencerCommand {

		public void Start() {
            DialogueManager.ConversationView.OnConversationContinue();
            Stop();
		}
	}
}
