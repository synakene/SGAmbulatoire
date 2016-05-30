using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandDeleteNode : SequencerCommand {

		public void Start() {
			Data.CompleteFeedback.DeleteLastNode ();
            Stop();
        }
	}
}
