using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;
using System.Collections.Generic;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandHelp : SequencerCommand {

		public void Start() {
            GameObject canvasDialogue = GameObject.Find("Dialogue Canvas");
            canvasDialogue.GetComponent<Canvas>().enabled = false;

            GameObject HelpCanvas = GameObject.Find("HelpCanvas");
            GameObject HelpSheet = HelpCanvas.gameObject.transform.FindChild("HelpSheet").gameObject;
            HelpSheet.GetComponentInChildren<Button>().enabled = true;
            HelpSheet.SetActive(true);

            iTween.PunchScale(HelpSheet.gameObject, new Vector3(0.3f, 0.3f), 1.2f);
            
            Stop();
		}
	}
}
