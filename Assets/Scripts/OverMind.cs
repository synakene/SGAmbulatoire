using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class OverMind : MonoBehaviour {

    public GameObject canvasDialogue;
    public GameObject headerCanvas;

    // QCM GameObjects
    public GameObject QCMCanvas;
    public GameObject FeuilleQCM;
    public GameObject FeedbackQCM;

    // Views
    public ScoreView ScoreView;
    public BlockNoteView blockNoteView;

    // Models
    public BlockNote blockNote;

	private float startTime;

	void Awake () {
		startTime = Time.time;
        // Feedback
        Data.CompleteFeedback = new CompleteFeedback();
        Parser.ParseFeedback();
    }

    void Start()
    {
        canvasDialogue = GameObject.Find("Dialogue Canvas");
        headerCanvas = GameObject.Find("HeaderCanvas");
        QCMCanvas = GameObject.Find("QCMCanvas");
        ScoreView = new ScoreView();
    }

	private void OnDestroy()
	{
		float time = Time.time - startTime;
		Data.min = (int) time / 60;
		Data.sec = (int) time % 60;
		print(Data.min + " min " + Data.sec);
	}

	public void QCM (string num)
    {
        canvasDialogue.GetComponent<Canvas>().enabled = false;
        headerCanvas.GetComponent<Canvas>().enabled = false;

        if (num == "1")
        {
            FeuilleQCM = QCMCanvas.gameObject.transform.FindChild("FeuilleQCM1").gameObject;
            FeedbackQCM = QCMCanvas.gameObject.transform.FindChild("FeedbackQCM1").gameObject;
        }

        else if (num == "2")
        {
            FeuilleQCM = QCMCanvas.gameObject.transform.FindChild("FeuilleQCM2").gameObject;
            FeedbackQCM = QCMCanvas.gameObject.transform.FindChild("FeedbackQCM2").gameObject;
        }

        else if (num == "3")
        {
            FeuilleQCM = QCMCanvas.gameObject.transform.FindChild("FeuilleQCM3").gameObject;
            FeedbackQCM = QCMCanvas.gameObject.transform.FindChild("FeedbackQCM3").gameObject;
        }

        else
        {
            Debug.Log("You need to specify which QCM to run");
        }

        FeuilleQCM.SetActive(true);
        FeedbackQCM.SetActive(false);

        FeuilleQCM.GetComponent<QCM>().ReinitVal();

        iTween.MoveFrom(FeuilleQCM.gameObject, iTween.Hash("y", -(Screen.height),
            "time", 1.5,
            "islocal", true
        ));
    }

    // Score
    public void AddPoint(int op1, int op2, int op3)
    {
        Data.curScoreObj1 += op1;
        if (Data.curScoreObj1 < 0) { Data.curScoreObj1 = 0; }

        Data.curScoreObj2 += op2;
        if (Data.curScoreObj2 < 0) { Data.curScoreObj2 = 0; }

        Data.curScoreObj3 += op3;
        if (Data.curScoreObj3 < 0) { Data.curScoreObj3 = 0; }

        ScoreView.ScorePanelAnim(op1, op2, op3);
        ScoreView.Update();
    }

    public void InitBlockNote()
    {
        blockNote = new BlockNote();
        blockNoteView = new BlockNoteView();
    }

    public void AddNote()
    {
        blockNote.AddNote();
        blockNoteView.Update(blockNote.notes);
    }

    public void AddNote2()
    {
        blockNote.AddNote2();
        blockNoteView.Update(blockNote.notes);
    }

    public void ReinitBlockNote()
    {
        if (blockNote == null || blockNoteView == null)
            InitBlockNote();
        blockNote.Reinit();
        blockNoteView.ButtonAnim();
    }
}
