using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.SceneManagement;

namespace PixelCrushers.DialogueSystem.SequencerCommands {
	
	public class SequencerCommandLoadLevel : SequencerCommand {
		
		public void Start() {
			string levelName = GetParameter(0);
			if (!string.IsNullOrEmpty(levelName)) {
				PersistentDataManager.Record();
                SceneManager.LoadScene(levelName);
			}
			Stop();
		}
	}
}
