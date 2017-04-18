using UnityEngine;
using System.Collections;
using Scorm;

public class ScormControler : MonoBehaviour {

	// Use this for initialization
	void Start () {
        InitScorm();
	}

    public void ScormCommit()
    {
        float score = Data.getAverageScore();
        if (Data.ScormScoreRetrived < score)
        {
            ScormAPI.Instance.SetRawScore(score);
            Data.ScormScoreRetrived = score;
        }
        ScormAPI.Instance.SetLessonStatus(ScormAPI.LessonStatus.Completed);
        ScormAPI.Instance.Commit();
    }

    public void InitScorm()
    {
        if (!ScormAPI.Instance.IsInitialized)
        {
            ScormAPI.Instance.OnRawScoreRetrieved += (data) =>
            {
                Data.ScormScoreRetrived = data;
            };

            ScormAPI.Instance.OnInitialized += () =>
            {
                ScormAPI.Instance.SetMinScore(0f);
                ScormAPI.Instance.SetMinScore(100f);
                ScormAPI.Instance.GetRawScore();
                ScormAPI.Instance.Commit();
            };

            ScormAPI.Instance.Init();
        }

    }
}
