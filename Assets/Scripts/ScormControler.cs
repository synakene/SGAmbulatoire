using UnityEngine;
using System.Collections;
using Scorm;

public class ScormControler : MonoBehaviour {

	private float startTime;

	// Use this for initialization
	void Start () {
        InitScorm();
		startTime = Time.time;
	}

    public void ScormCommit()
    {
		ScormAPI.Instance.SetSessionTime((Data.min * 60 + Data.sec) * 1000);

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

			ScormAPI.Instance.OnLearnerIdRetrieved += (id) =>
			{
				Data.learnerId = id;
			};

			ScormAPI.Instance.OnLearnerNameRetrieved += (name) =>
			{
				Data.learnerName = name;
			};

            ScormAPI.Instance.OnInitialized += () =>
            {
                ScormAPI.Instance.SetMinScore(0f);
                ScormAPI.Instance.SetMaxScore(100f);
                ScormAPI.Instance.GetRawScore();
				ScormAPI.Instance.GetLearnerName();
				ScormAPI.Instance.GetLearnerId();
				ScormAPI.Instance.Commit();
            };

            ScormAPI.Instance.Init();
        }

    }
}
