using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandLoadConversation : SequencerCommand {

		public void Start() {
            string conversation = GetParameter(0);
            DialogueManager.StopConversation();
            DialogueManager.StartConversation(conversation);
            Stop();
		}
	}
}
