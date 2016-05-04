using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Starter : MonoBehaviour {

    public GameObject StartPanel;
    public GameObject TutoPanel;

	void Start () {
        StartPanel.gameObject.SetActive(true);
        TutoPanel.gameObject.SetActive(false);
	}
	
    public void StartButton()
    {
        StartPanel.gameObject.SetActive(false);
        TutoPanel.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}
