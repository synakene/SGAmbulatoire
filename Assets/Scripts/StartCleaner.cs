using UnityEngine;
using System.Collections;

public class StartCleaner : MonoBehaviour {

	void Awake () {
        GameObject DialogueManager = GameObject.FindGameObjectWithTag("DialogueManager");
        GameObject GameManager = GameObject.FindGameObjectWithTag("GameManager");
        //GameObject CameraAngles = GameObject.FindGameObjectWithTag("CameraAngles");
        if (DialogueManager != null)
        {
            Destroy(DialogueManager);
        }

        if (GameManager != null)
        {
            Destroy(GameManager);
        }

        //if (CameraAngles != null)
        //{
        //    Destroy(CameraAngles);
        //}

    }
}
