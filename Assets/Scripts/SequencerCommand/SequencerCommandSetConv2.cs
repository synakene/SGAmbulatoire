using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandSetConv2 : SequencerCommand {

		public void Start() {
            //Cam move
            GameObject Cam = GameObject.Find("Main Camera");

            Vector3 NewCamPos = new Vector3(8f, 1.22f, -0.56f);
            Quaternion NewCamRot = Quaternion.Euler(7.1f, 300f, 0f);

            Cam.transform.position = NewCamPos;
            Cam.transform.rotation = NewCamRot;

            //NPC setting
            GameObject NPC = GameObject.Find("NPC");
            Transform conv1 = NPC.transform.Find("Conv1");
            Transform conv2 = NPC.transform.Find("Conv2");
            conv1.gameObject.SetActive(false);
            conv2.gameObject.SetActive(true);

            //NPC anim
            //NPC.GetComponent<Animation>().Play("R_A_SitSeat");

            //Player anim
            //GameObject Player = GameObject.Find("Player");
            //Player.GetComponent<Animation>().Stop();
            //Player.GetComponent<Animation>().Play("W_A_SitSeat");

            Stop();
		}


	
	}

}
