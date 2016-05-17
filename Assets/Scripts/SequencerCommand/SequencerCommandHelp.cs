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
            GameObject.Find("HeaderCanvas").GetComponent<Canvas>().enabled = false;

            GameObject HelpCanvas = GameObject.Find("HelpCanvas");
            GameObject HelpSheet = HelpCanvas.gameObject.transform.FindChild("HelpSheet").gameObject;
            HelpSheet.SetActive(true);

            Vector3 posInit = new Vector3(0f, -(Screen.height), 0f);
            HelpSheet.gameObject.transform.localPosition = posInit;

            iTween.MoveTo(HelpSheet.gameObject, iTween.Hash("y", 0,
                "time", 1.5,
                "islocal", true
            ));
            Stop();
		}
	}
}
