using UnityEngine;
using System.Collections;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands {

	public class SequencerCommandSetConv4 : SequencerCommand {

		public void Start() {

            //Cam move
            GameObject Cam = GameObject.Find("Main Camera");

            Vector3 NewCamPos = new Vector3(-3.1f, 1.78f, 0.1f);
            Quaternion NewCamRot = Quaternion.Euler(5.14f, 107.51f, 0f);

            Cam.transform.position = NewCamPos;
            Cam.transform.rotation = NewCamRot;


            //NPC setting
            GameObject NPC = GameObject.Find("NPC");
			Transform conv3 = NPC.transform.Find("Conv3");
			Transform conv4 = NPC.transform.Find("Conv4");
            conv3.gameObject.SetActive(false);
			conv4.gameObject.SetActive(true);

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
