using Scorm;
using System;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleView : MonoBehaviour {

    private static ConsoleView instance;
    
    public static ConsoleView Instance {
        get { return instance; }
    }

    [SerializeField]
    private Button clearButton;

    [SerializeField]
    private ScrollRect scrollRect;

    [SerializeField]
    private Text text;

    void Awake() {
        instance = this;

        this.text.text = "";

        clearButton.onClick.AddListener(Clear);
    }

    void Start() {
        ScormAPI.Instance.OnMessageLogged += (message) => {
            Append(message);
        };
    }

    void Append(string text) {        
        this.text.text += text + Environment.NewLine;

        scrollRect.verticalNormalizedPosition = 0;
    }

    void Clear() {
        this.text.text = "";

        scrollRect.verticalNormalizedPosition = 1;
    }
}
