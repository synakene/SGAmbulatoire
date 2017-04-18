using Scorm;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Example : MonoBehaviour {

    [SerializeField]
    private Transform buttonsContainer;

    [SerializeField]
    private Button initButton;

    [SerializeField]
    private Button getLearnerIdButton;

    [SerializeField]
    private Button getLearnerNameButton;

    [SerializeField]
    private Button getLessonLocationButton;

    [SerializeField]
    private Button setLessonLocationButton;

    [SerializeField]
    private Button getCreditButton;

    [SerializeField]
    private Button getLessonStatusButton;

    [SerializeField]
    private Button setLessonStatusButton;

    [SerializeField]
    private Button getEntryButton;

    [SerializeField]
    private Button getRawScoreButton;

    [SerializeField]
    private Button setRawScoreButton;

    [SerializeField]
    private Button getMaxScoreButton;

    [SerializeField]
    private Button setMaxScoreButton;

    [SerializeField]
    private Button getMinScoreButton;

    [SerializeField]
    private Button setMinScoreButton;

    [SerializeField]
    private Button getTotalTimeButton;

    [SerializeField]
    private Button getLessonModeButton;

    [SerializeField]
    private Button setSessionTimeButton;

    [SerializeField]
    private Button getCommentsButton;

    [SerializeField]
    private Button getCommentsFromLmsButton;

    [SerializeField]
    private Button getLanguageButton;

    [SerializeField]
    private Button getSuspendDataButton;

    [SerializeField]
    private Button setSuspendDataButton;

    [SerializeField]
    private Button getObjectivesButton;

    [SerializeField]
    private Button setObjectiveButton;

    private List<Button> buttons;

    private float startTime;

    void Awake() {
        initButton.interactable = true;

        buttons = buttonsContainer.GetComponentsInChildren<Button>(true).ToList();

        ToggleInitialized(false);

        initButton.onClick.AddListener(() => {
            ScormAPI.Instance.Init();
        });
    }

    void Start() {
        startTime = Time.time;

        ScormAPI.Instance.OnInitialized += () => {
            ScormAPI.Instance.LogInfo("On initialized");

            initButton.interactable = false;

            ToggleInitialized(true);

            SetListeners();
        };

        ScormAPI.Instance.OnUserClosed += () => {
            ScormAPI.Instance.LogInfo("User closed");
        };

        ScormAPI.Instance.OnLearnerIdRetrieved += (learnerId) => {
            ScormAPI.Instance.LogInfo("LearnerId: " + learnerId);
        };

        ScormAPI.Instance.OnLearnerNameRetrieved += (learnerName) => {
            ScormAPI.Instance.LogInfo("LearnerName: " + learnerName);
        };

        ScormAPI.Instance.OnLessonLocationRetrieved += (lessonLocation) => {
            ScormAPI.Instance.LogInfo("Lesson location: " + lessonLocation);
        };

        ScormAPI.Instance.OnCreditRetrieved += (credit) => {
            ScormAPI.Instance.LogInfo("Credit: " + credit);
        };

        ScormAPI.Instance.OnLessonStatusRetrieved += (lessonStatus) => {
            ScormAPI.Instance.LogInfo("LessonStatus: " + lessonStatus);
        };

        ScormAPI.Instance.OnEntryRetrieved += (entry) => {
            ScormAPI.Instance.LogInfo("Entry: " + entry);
        };

        ScormAPI.Instance.OnRawScoreRetrieved += (rawScore) => {
            ScormAPI.Instance.LogInfo("RawScore: " + rawScore);
        };

        ScormAPI.Instance.OnMaxScoreRetrieved += (maxScore) => {
            ScormAPI.Instance.LogInfo("MaxScore: " + maxScore);
        };

        ScormAPI.Instance.OnMinScoreRetrieved += (minScore) => {
            ScormAPI.Instance.LogInfo("MinScore: " + minScore);
        };

        ScormAPI.Instance.OnTotalTimeRetrieved += (totalTime) => {
            ScormAPI.Instance.LogInfo("TotalTime: " + totalTime);
        };

        ScormAPI.Instance.OnLessonModeRetrieved += (lessonMode) => {
            ScormAPI.Instance.LogInfo("LessonMode: " + lessonMode);
        };

        ScormAPI.Instance.OnCommentsRetrieved += (comments) => {
            ScormAPI.Instance.LogInfo("Comments: " + comments);
        };

        ScormAPI.Instance.OnCommentsFromLMSRetrieved += (commentsFromLMS) => {
            ScormAPI.Instance.LogInfo("CommentsFromLMS: " + commentsFromLMS);
        };

        ScormAPI.Instance.OnLanguageRetrieved += (language) => {
            ScormAPI.Instance.LogInfo("Language: " + language);
        };

        ScormAPI.Instance.OnSuspendDataRetrieved += (data) => {
            ScormAPI.Instance.LogInfo("SuspendData: " + data);
        };

        ScormAPI.Instance.OnObjectivesRetrieved += (objectives) => {
            ScormAPI.Instance.LogInfo("Objective count: " + objectives.Count());

            objectives.ToList().ForEach(o => {
                ScormAPI.Instance.LogInfo("Objective: " + o.ToString());
            });
        };
    }

    void SetListeners() {
        getLearnerIdButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetLearnerId();
        });

        getLearnerNameButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetLearnerName();
        });

        getLessonLocationButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetLessonLocation();
        });

        setLessonLocationButton.onClick.AddListener(() => {
            ScormAPI.Instance.SetLessonLocation(string.Format("custom_location_{0}", Guid.NewGuid()));
            ScormAPI.Instance.Commit();
        });

        getCreditButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetCredit();
        });

        getLessonStatusButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetLessonStatus();
        });

        setLessonStatusButton.onClick.AddListener(() => {
            ScormAPI.Instance.SetLessonStatus(ScormAPI.LessonStatus.Incomplete);
            ScormAPI.Instance.Commit();
        });

        getEntryButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetEntry();
        });

        getRawScoreButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetRawScore();
        });

        setRawScoreButton.onClick.AddListener(() => {
            ScormAPI.Instance.SetRawScore(3.5f);
            ScormAPI.Instance.Commit();
        });

        getMaxScoreButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetMaxScore();
        });

        setMaxScoreButton.onClick.AddListener(() => {
            ScormAPI.Instance.SetMaxScore(10f);
            ScormAPI.Instance.Commit();
        });

        getMinScoreButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetMinScore();
        });

        setMinScoreButton.onClick.AddListener(() => {
            ScormAPI.Instance.SetMinScore(0.1f);
            ScormAPI.Instance.Commit();
        });

        getTotalTimeButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetTotalTime();
        });

        getLessonModeButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetLessonMode();
        });

        setSessionTimeButton.onClick.AddListener(() => {
            int sessionTime = (int)((Time.time - startTime) * 1000);

            ScormAPI.Instance.SetSessionTime(sessionTime);
        });

        getCommentsButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetComments();
        });

        getCommentsFromLmsButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetCommentsFromLMS();
        });

        getLanguageButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetLanguage();
        });

        getSuspendDataButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetSuspendData();
        });

        setSuspendDataButton.onClick.AddListener(() => {
            ScormAPI.Instance.SetSuspendData("{\\\"firstName\\\":\\\"John\\\", \\\"lastName\\\":\\\"Doe\\\"}");
        });

        getObjectivesButton.onClick.AddListener(() => {
            ScormAPI.Instance.GetObjectives();
        });

        setObjectiveButton.onClick.AddListener(() => {
            string id = RandomString(15);
            float minScore = UnityEngine.Random.Range(0, 10);
            float maxScore = UnityEngine.Random.Range(0, 10);
            float rawScore = UnityEngine.Random.Range(0, 10);
            float scaledScore = UnityEngine.Random.Range(-1, 1);
            ScormAPI.LessonStatus successStatus = ScormAPI.LessonStatus.Passed;
            ScormAPI.LessonStatus completionStatus = ScormAPI.LessonStatus.Completed;
            float progressMeasure = UnityEngine.Random.Range(0, 1);
            string description = RandomString(15);

            ScormAPI.Instance.SetObjective(id, minScore, maxScore, rawScore, scaledScore, successStatus, completionStatus, progressMeasure, description);
        });
    }

    void ToggleInitialized(bool status) {
        buttons.ForEach(b => {
            if (b != initButton) {
                b.interactable = status;
            }
        });
    }

    private static System.Random random = new System.Random();

    public static string RandomString(int length) {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}