using UnityEngine;
using PixelCrushers.DialogueSystem;
using System.Collections;

public class ResetDatabase : MonoBehaviour {

	void Awake () {
        PersistentDataManager.Reset(DatabaseResetOptions.RevertToDefault);
    }

}
