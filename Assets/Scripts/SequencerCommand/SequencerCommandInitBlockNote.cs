using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandInitBlockNote : SequencerCommand {

		public void Start() {
            Transform BlockNoteButton = GameObject.Find("HeaderPanel").transform.FindChild("BlockNoteButton");
            BlockNoteButton.gameObject.SetActive(true);
            iTween.PunchScale(BlockNoteButton.gameObject, new Vector3(0.3f, 0.3f), 1.2f);
            Stop();
		}
	}
}
