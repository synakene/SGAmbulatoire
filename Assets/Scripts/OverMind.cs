using UnityEngine;
using System.Collections;

public class OverMind : MonoBehaviour {

	void Awake () {
        Data.CompleteFeedback = new CompleteFeedback();
        Parser.ParseFeedback();
    }
}
