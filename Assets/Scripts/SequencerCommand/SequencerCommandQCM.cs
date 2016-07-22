using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandQCM : SequencerCommand {

		public void Start() {

            Debug.Log("Use SendMessage to OverMind");

            //string num = GetParameter(0);

            //GameObject canvasDialogue = GameObject.Find("Dialogue Canvas");
            //canvasDialogue.GetComponent<Canvas>().enabled = false;
            //GameObject.Find("HeaderCanvas").GetComponent<Canvas>().enabled = false;

            //GameObject FeuilleQCM = null;
            //GameObject FeedbackQCM = null;

            //GameObject QCMCanvas = GameObject.Find("QCMCanvas");
            //if (num == "1")
            //{
            //    FeuilleQCM = QCMCanvas.gameObject.transform.FindChild("FeuilleQCM1").gameObject;
            //    FeedbackQCM = QCMCanvas.gameObject.transform.FindChild("FeedbackQCM1").gameObject;
            //}

            //else if (num == "2")
            //{
            //    FeuilleQCM = QCMCanvas.gameObject.transform.FindChild("FeuilleQCM2").gameObject;
            //    FeedbackQCM = QCMCanvas.gameObject.transform.FindChild("FeedbackQCM2").gameObject;
            //}

            //else
            //{
            //    Debug.Log("You need to specify which QCM to run");
            //}

            //FeuilleQCM.SetActive(true);
            //FeedbackQCM.SetActive(false);

            //iTween.MoveFrom(FeuilleQCM.gameObject, iTween.Hash("y", -(Screen.height),
            //    "time", 1.5,
            //    "islocal", true
            //));
            Stop();
		}
	}
}
