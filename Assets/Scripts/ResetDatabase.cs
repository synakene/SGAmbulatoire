using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Collections;

public class ResetDatabase : MonoBehaviour {

	void Awake () {
        Debug.Log("coucou");
        
        PersistentDataManager.Reset(DatabaseResetOptions.RevertToDefault);
    }

}
