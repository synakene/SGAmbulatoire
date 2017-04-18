using System;
using System.Collections.Generic;
using UnityEngine;

namespace Scorm {

    public class ScormAPI : MonoBehaviour {

        #region Singleton
        private static ScormAPI instance;

        public static ScormAPI Instance {
            get { return instance; }
        }

        public delegate void LogHandler(string text);
        public event LogHandler OnMessageLogged;

        void Awake() {
            instance = this;
        }

        void OnDestroy() {
            instance = null;
        }
        #endregion

        #region LearnerId
        public delegate void LearnerIdHandler(string learnerId);
        public event LearnerIdHandler OnLearnerIdRetrieved;

        /// <summary>
        /// Identifies the learner on behalf of whom the SCO was launched.
        /// </summary>
        public void GetLearnerId() {
            CallUnityMethod("CallLearnerId", "getLearnerId");
        }

        private void CallLearnerId(string learnerId) {
            if (OnLearnerIdRetrieved != null) {
                OnLearnerIdRetrieved(learnerId);
            }
        }
        #endregion

        #region LearnerName
        public delegate void LearnerNameHandler(string learnerName);
        public event LearnerNameHandler OnLearnerNameRetrieved;

        /// <summary>
        /// Name provided for the learner by the LMS.
        /// </summary>
        public void GetLearnerName() {
            CallUnityMethod("CallLearnerName", "getLearnerName");
        }

        private void CallLearnerName(string learnerName) {
            if (OnLearnerNameRetrieved != null) {
                OnLearnerNameRetrieved(learnerName);
            }
        }
        #endregion

        #region LessonLocation
        public delegate void LessonLocationHandler(string lessonLocation);
        public event LessonLocationHandler OnLessonLocationRetrieved;

        /// <summary>
        /// The learner’s current location in the SCO.
        /// </summary>
        public void GetLessonLocation() {
            CallUnityMethod("CallLessonLocation", "getLessonLocation");
        }

        /// <summary>
        /// The learner’s current location in the SCO.
        /// </summary>
        /// <param name="lessonLocation">The learner’s current location in the SCO.</param>
        public void SetLessonLocation(string lessonLocation) {
            CallJavascriptMethod("setLessonLocation", lessonLocation);
        }

        private void CallLessonLocation(string lessonLocation) {
            if (OnLessonLocationRetrieved != null) {
                OnLessonLocationRetrieved(lessonLocation);
            }
        }
        #endregion

        #region Credit
        public enum CreditType {
            Credit = 0,
            NoCredit = 1
        }

        public delegate void CreditHandler(CreditType credit);
        public event CreditHandler OnCreditRetrieved;

        /// <summary>
        /// Indicates whether the learner will be credited for performance in the SCO.
        /// </summary>
        public void GetCredit() {
            CallUnityMethod("CallCredit", "getCredit");
        }

        private void CallCredit(string credit) {
            if (OnCreditRetrieved != null) {
                CreditType creditType = CreditType.NoCredit;

                switch (credit) {
                    case "credit":
                        creditType = CreditType.Credit;
                        break;
                    case "no-credit":
                        creditType = CreditType.NoCredit;
                        break;
                    default:
                        throw new Exception("[SCORM] No valid credit value: '" + credit + "'");
                }

                OnCreditRetrieved(creditType);
            }
        }
        #endregion

        #region LessonStatus
        public enum LessonStatus {
            Passed = 0, // 1.2
            Completed = 1, // 1.2 / 2004
            Failed = 2, // 1.2
            Incomplete = 3, // 1.2 / 2004
            Browsed = 4, // 1.2
            NotAttempted = 5, // 1.2 / 2004
            Unknown = 6 // 2004
        }

        public delegate void LessonStatusHandler(LessonStatus lessonStatus);
        public event LessonStatusHandler OnLessonStatusRetrieved;

        /// <summary>
        /// Indicates whether the learner has completed and satisfied the requirements for the SCO.
        /// </summary>
        public void GetLessonStatus() {
            CallUnityMethod("CallLessonStatus", "getLessonStatus");
        }

        private void CallLessonStatus(string lessonStatus) {
            if (OnLessonStatusRetrieved != null) {
                LessonStatus lessonStatusType = StringToLessonStatus(lessonStatus);

                OnLessonStatusRetrieved(lessonStatusType);
            }
        }

        /// <summary>
        /// Indicates whether the learner has completed and satisfied the requirements for the SCO.
        /// </summary>
        /// <param name="lessonStatus">The learner’s lesson status.</param>
        public void SetLessonStatus(LessonStatus lessonStatus) {
            string status = LessonStatusToString(lessonStatus);

            CallJavascriptMethod("setLessonStatus", status);
        }

        private static LessonStatus StringToLessonStatus(string lessonStatus) {
            LessonStatus lessonStatusType = LessonStatus.NotAttempted;

            if (lessonStatus.Equals("passed")) {
                lessonStatusType = LessonStatus.Passed;
            } else if (lessonStatus.Equals("completed")) {
                lessonStatusType = LessonStatus.Completed;
            } else if (lessonStatus.Equals("failed")) {
                lessonStatusType = LessonStatus.Failed;
            } else if (lessonStatus.Equals("incomplete")) {
                lessonStatusType = LessonStatus.Incomplete;
            } else if (lessonStatus.Equals("browsed")) {
                lessonStatusType = LessonStatus.Browsed;
            } else if (lessonStatus.Equals("not attempted")) {
                lessonStatusType = LessonStatus.NotAttempted;
            } else if (lessonStatus.Equals("unknown")) {
                lessonStatusType = LessonStatus.Unknown;
            } else {
                throw new Exception("[SCORM] No valid lessonStatus value: '" + lessonStatus + "'");
            }

            return lessonStatusType;
        }

        private static string LessonStatusToString(LessonStatus lessonStatus) {
            string status = "";

            if (lessonStatus == LessonStatus.Passed) {
                status = "passed";
            } else if (lessonStatus == LessonStatus.Completed) {
                status = "completed";
            } else if (lessonStatus == LessonStatus.Failed) {
                status = "failed";
            } else if (lessonStatus == LessonStatus.Incomplete) {
                status = "incomplete";
            } else if (lessonStatus == LessonStatus.Browsed) {
                status = "browsed";
            } else if (lessonStatus == LessonStatus.NotAttempted) {
                status = "not attempted";
            } else if (lessonStatus == LessonStatus.Unknown) {
                status = "unknown";
            } else {
                throw new Exception("[SCORM] No valid lessonStatus value: '" + lessonStatus + "'");
            }

            return status;
        }
        #endregion

        #region Entry
        public enum EntryType {
            AbInitio = 0,
            Resume = 1,
            Empty = -1
        }

        public delegate void EntryHandler(EntryType entry);
        public event EntryHandler OnEntryRetrieved;

        /// <summary>
        /// Asserts whether the learner has previously accessed the SCO.
        /// </summary>
        public void GetEntry() {
            CallUnityMethod("CallEntry", "getEntry");
        }

        private void CallEntry(string entry) {
            if (OnEntryRetrieved != null) {
                EntryType entryType = EntryType.AbInitio;

                switch (entry) {
                    case "ab-initio":
                        entryType = EntryType.AbInitio;
                        break;
                    case "resume":
                        entryType = EntryType.Resume;
                        break;
                    case "":
                        entryType = EntryType.Empty;
                        break;
                    default:
                        throw new Exception("[ScormAPI] No valid entry value: '" + entry + "'");
                }

                OnEntryRetrieved(entryType);
            }
        }
        #endregion

        #region RawScore
        public delegate void RawScoreHandler(float rawScore);
        public event RawScoreHandler OnRawScoreRetrieved;

        /// <summary>
        /// Number that reflects the performance of the learner relative to the range bounded by the values of min and max.
        /// </summary>
        public void GetRawScore() {
            CallUnityMethod("CallRawScore", "getRawScore");
        }

        private void CallRawScore(string rawScore) {
            if (OnRawScoreRetrieved != null) {
                try {
                    if (string.IsNullOrEmpty(rawScore)) {
                        LogWarning("No rawScore value defined.");

                        OnRawScoreRetrieved(-1);
                    } else {
                        OnRawScoreRetrieved(float.Parse(rawScore));
                    }
                } catch (Exception) {
                    LogError("No valid rawScore format: '" + rawScore + "'");
                }
            }
        }

        /// <summary>
        /// Number that reflects the performance of the learner relative to the range bounded by the values of min and max.
        /// </summary>
        /// <param name="rawScore">The learner's raw score.</param>
        public void SetRawScore(float rawScore) {
            CallJavascriptMethod("setRawScore", rawScore);
        }
        #endregion

        #region MaxScore
        public delegate void MaxScoreHandler(float maxScore);
        public event MaxScoreHandler OnMaxScoreRetrieved;

        /// <summary>
        /// Maximum value in the range for the raw score.
        /// </summary>
        public void GetMaxScore() {
            CallUnityMethod("CallMaxScore", "getMaxScore");
        }

        private void CallMaxScore(string maxScore) {
            if (OnMaxScoreRetrieved != null) {
                try {
                    if (string.IsNullOrEmpty(maxScore)) {
                        LogWarning("No maxScore value defined.");

                        OnMaxScoreRetrieved(-1);
                    } else {
                        OnMaxScoreRetrieved(float.Parse(maxScore));
                    }
                } catch (Exception) {
                    LogError("No valid maxScore format: '" + maxScore + "'");
                }
            }
        }

        /// <summary>
        /// Maximum value in the range for the raw score.
        /// </summary>
        /// <param name="maxScore">The learner's max score.</param>
        public void SetMaxScore(float maxScore) {
            CallJavascriptMethod("setMaxScore", maxScore);
        }
        #endregion

        #region MinScore
        public delegate void MinScoreHandler(float minScore);
        public event MinScoreHandler OnMinScoreRetrieved;

        /// <summary>
        /// Minimum value in the range for the raw score.
        /// </summary>
        public void GetMinScore() {
            CallUnityMethod("CallMinScore", "getMinScore");
        }

        private void CallMinScore(string minScore) {
            if (OnMinScoreRetrieved != null) {
                try {
                    if (string.IsNullOrEmpty(minScore)) {
                        LogWarning("No minScore value defined.");

                        OnMinScoreRetrieved(-1);
                    } else {
                        OnMinScoreRetrieved(float.Parse(minScore));
                    }
                } catch (Exception) {
                    LogError("No valid minScore format: '" + minScore + "'");
                }
            }
        }

        /// <summary>
        /// Minimum value in the range for the raw score.
        /// </summary>
        /// <param name="minScore">The learner's min score.</param>
        public void SetMinScore(float minScore) {
            CallJavascriptMethod("setMinScore", minScore);
        }
        #endregion

        #region TotalTime
        public delegate void TotalTimeHandler(float totalTimeInMiliseconds);
        public event TotalTimeHandler OnTotalTimeRetrieved;

        /// <summary>
        /// Sum of all of the learner’s session times accumulated in the current learner attempt.
        /// </summary>
        public void GetTotalTime() {
            CallUnityMethod("CallTotalTime", "getTotalTime");
        }

        private void CallTotalTime(string totalTimeInCentiseconds) {
            if (OnTotalTimeRetrieved != null) {
                try {
                    if (string.IsNullOrEmpty(totalTimeInCentiseconds)) {
                        LogWarning("No totalTime value defined.");

                        OnTotalTimeRetrieved(-1);
                    } else {
                        OnTotalTimeRetrieved(float.Parse(totalTimeInCentiseconds) * 10); // To Miliseconds
                    }
                } catch (Exception) {
                    LogError("No valid total time format: '" + totalTimeInCentiseconds + "'");
                }
            }
        }
        #endregion

        #region LessonMode
        public enum LessonMode {
            Browse = 0,
            Normal = 1,
            Review = 2
        }

        public delegate void LessonModeHandler(LessonMode lessonMode);
        public event LessonModeHandler OnLessonModeRetrieved;

        /// <summary>
        /// Identifies one of three possible modes in which the SCO may be presented to the learner.
        /// </summary>
        public void GetLessonMode() {
            CallUnityMethod("CallLessonMode", "getLessonMode");
        }

        private void CallLessonMode(string lessonMode) {
            if (OnLessonModeRetrieved != null) {
                LessonMode lessonModeType = LessonMode.Browse;

                switch (lessonMode) {
                    case "browse":
                        lessonModeType = LessonMode.Browse;
                        break;
                    case "normal":
                        lessonModeType = LessonMode.Normal;
                        break;
                    case "review":
                        lessonModeType = LessonMode.Review;
                        break;
                    default:
                        throw new Exception("[ScormAPI] No valid lessonMode value: '" + lessonMode + "'");
                }

                OnLessonModeRetrieved(lessonModeType);
            }
        }
        #endregion

        #region SessionTime
        /// <summary>
        /// Amount of time that the learner has spent in the current learner session for this SCO.
        /// </summary>
        /// <param name="milliseconds">Time in milliseconds.</param>
        public void SetSessionTime(int milliseconds) {
            CallJavascriptMethod("setSessionTime", milliseconds / 10f); // To Centiseconds
        }
        #endregion

        #region Comments
        public delegate void CommentsHandler(string comments);
        public event CommentsHandler OnCommentsRetrieved;

        /// <summary>
        /// Textual input from the learner about the SCO.
        /// </summary>
        public void GetComments() {
            CallUnityMethod("CallComments", "getComments");
        }

        private void CallComments(string comments) {
            if (OnCommentsRetrieved != null) {
                OnCommentsRetrieved(comments);
            }
        }
        #endregion

        #region CommentsFromLMS
        public delegate void CommentsFromLMSHandler(string commentsFromLMS);
        public event CommentsFromLMSHandler OnCommentsFromLMSRetrieved;

        /// <summary>
        /// Comments or annotations associated with a SCO.
        /// </summary>
        public void GetCommentsFromLMS() {
            CallUnityMethod("CallCommentsFromLMS", "getCommentsFromLMS");
        }

        private void CallCommentsFromLMS(string commentsFromLMS) {
            if (OnCommentsFromLMSRetrieved != null) {
                OnCommentsFromLMSRetrieved(commentsFromLMS);
            }
        }
        #endregion

        #region Objectives
        [Serializable]
        public class ObjectivesData {

            [SerializeField]
            private List<ObjectiveData> objectives;

            public List<ObjectiveData> Objectives {
                get { return objectives; }
            }
        }

        [Serializable]
        public class ObjectiveData {

            [SerializeField]
            private string id;

            public string Id {
                get { return id; }
            }

            [SerializeField]
            private float minScore;

            public float MinScore {
                get { return minScore; }
            }

            [SerializeField]
            private float maxScore;

            public float MaxScore {
                get { return maxScore; }
            }

            [SerializeField]
            private float rawScore;

            public float RawScore {
                get { return rawScore; }
            }

            [SerializeField]
            private float scaledScore;

            public float ScaledScore {
                get { return scaledScore; }
            }

            [SerializeField]
            private string successStatus;

            public LessonStatus SuccessStatus {
                get { return StringToLessonStatus(successStatus); }
            }

            [SerializeField]
            private string completionStatus;

            public LessonStatus CompletionStatus {
                get { return StringToLessonStatus(completionStatus); }
            }

            [SerializeField]
            private float progressMeasure;

            public float ProgressMeasure {
                get { return progressMeasure; }
            }

            [SerializeField]
            private string description;

            public string Description {
                get { return description; }
            }

            public ObjectiveData(string id, float minScore, float maxScore, float rawScore, float scaledScore, LessonStatus successStatus, LessonStatus completionStatus, float progressMeasure, string description) {
                this.id = id;
                this.minScore = minScore;
                this.maxScore = maxScore;
                this.rawScore = rawScore;
                this.scaledScore = scaledScore;
                this.successStatus = LessonStatusToString(successStatus);
                this.completionStatus = LessonStatusToString(completionStatus);
                this.progressMeasure = progressMeasure;
                this.description = description;
            }

            public override string ToString() {
                return string.Format("Id: {0} MinScore: {1} MaxScore: {2} RawScore: {3} ScaledScore: {4} SuccessStatus: {5} CompletionStatus: {6} ProgressMeasure: {7} Description: {8}", id, minScore, maxScore, rawScore, scaledScore, successStatus, completionStatus, progressMeasure, description);
            }
        }

        public delegate void ObjectivesHandler(IEnumerable<ObjectiveData> objectives);
        public event ObjectivesHandler OnObjectivesRetrieved;

        /// <summary>
        /// Get the objectives.
        /// </summary>
        public void GetObjectives() {
            CallUnityMethod("CallObjectives", "getObjectives");
        }

        private void CallObjectives(string data) {
            if (OnObjectivesRetrieved != null) {
                try {
                    data = string.Format("{{ \"objectives\": {0} }}", data);

                    ObjectivesData objectivesData = JsonUtility.FromJson<ObjectivesData>(data);

                    OnObjectivesRetrieved(objectivesData.Objectives);
                } catch (Exception e) {
                    LogError("No valid objectives format: '" + data + "'" + " " + e.Message);
                }
            }
        }

        /// <summary>
        /// Set the objective.
        /// </summary>
        /// <param name="id">The objective id. Only allows alphanumeric characters.</param>
        /// <param name="minScore">The objective min score.</param>
        /// <param name="maxScore">The objective max score.</param>
        /// <param name="rawScore">The objective raw score.</param>
        /// <param name="scaledScore">The objective scaled score.</param>
        /// <param name="successStatus">The objective success status.</param>
        /// <param name="completionStatus">The objective completion status.</param>
        /// <param name="progressMeasure">The objective progress measure.</param>
        /// <param name="description">The objective description.</param>
        public void SetObjective(string id, float minScore, float maxScore, float rawScore, float scaledScore, LessonStatus successStatus, LessonStatus completionStatus, float progressMeasure, string description) {
            ObjectiveData objectiveData = new ObjectiveData(id, minScore, maxScore, rawScore, scaledScore, successStatus, completionStatus, progressMeasure, description);

            CallJavascriptMethod("setObjective", objectiveData);
        }
        #endregion

        #region Language
        public delegate void LanguageHandler(string language);
        public event LanguageHandler OnLanguageRetrieved;

        /// <summary>
        /// The learner’s preferred language for SCOs with multilingual capability.
        /// </summary>
        public void GetLanguage() {
            CallUnityMethod("CallLanguage", "getLanguage");
        }

        private void CallLanguage(string language) {
            if (OnLanguageRetrieved != null) {
                OnLanguageRetrieved(language);
            }
        }
        #endregion

        #region SuspendData
        public delegate void SuspendDataHandler(string data);
        public event SuspendDataHandler OnSuspendDataRetrieved;

        /// <summary>
        /// Provides space to store and retrieve data between learner session
        /// </summary>
        public void GetSuspendData() {
            CallUnityMethod("CallSuspendData", "getSuspendData");
        }

        private void CallSuspendData(string data) {
            if (OnSuspendDataRetrieved != null) {
                OnSuspendDataRetrieved(data);
            }
        }

        /// <summary>
        /// Provides space to store and retrieve data between learner sessions.
        /// </summary>
        /// <param name="data">Data to store.</param>
        public void SetSuspendData(string data) {
            CallJavascriptMethod("setSuspendData", data);
        }
        #endregion

        #region Common
        private bool isInitialized;

        public bool IsInitialized {
            get { return isInitialized; }
        }

        public delegate void InitHandler();
        public event InitHandler OnInitialized;

        public void Init() {
            CallUnityMethod("CallInit", "initialize");
        }

        private void CallInit(string result) {
            isInitialized = true;

            if (OnInitialized != null) {
                OnInitialized();
            }
        }


        public delegate void CloseHandler();
        public event CloseHandler OnUserClosed;

        private void Close() {
            if (OnUserClosed != null) {
                OnUserClosed();
            }
        }

        public void Commit() {
            CallJavascriptMethod("commit");
        }

        public enum ExitReason {
            TimeOut = 0,
            Suspend = 1,
            Logout = 2
        }

        public void Exit(ExitReason reason) {
            string exit = "";

            switch (reason) {
                case ExitReason.TimeOut:
                    exit = "time-out";
                    break;
                case ExitReason.Suspend:
                    exit = "suspend";
                    break;
                case ExitReason.Logout:
                    exit = "logout";
                    break;
                default:
                    throw new Exception("[ScormAPI] No valid exit reason value: '" + reason + "'");
            }

            CallJavascriptMethod("exit", exit);
        }

        public void Finish() {
            CallJavascriptMethod("finish");
        }
        #endregion

        #region Helpers
        private static void CallUnityMethod(string unityMethodName, string javascriptMethodName) {
            string returnMethod = string.Format("scorm.{0}()", javascriptMethodName);

            string script = null;

#if UNITY_5_4_OR_NEWER
            script = string.Format("var auxVar = {0}; SendMessage(\"{1}\", \"{2}\", \"\" + auxVar + \"\");", returnMethod, Instance.name, unityMethodName);
#else
            if (Application.platform == RuntimePlatform.WindowsWebPlayer || Application.platform == RuntimePlatform.OSXWebPlayer) {
                script = string.Format("var auxVar = {0}; u.getUnity().SendMessage(\"{1}\", \"{2}\", \"\" + auxVar + \"\");", returnMethod, Instance.name, unityMethodName);
            } else {
                script = string.Format("var auxVar = {0}; SendMessage(\"{1}\", \"{2}\", \"\" + auxVar + \"\");", returnMethod, Instance.name, unityMethodName);
            }
#endif

            Application.ExternalEval(script);
        }

        private static void CallJavascriptMethod(string methodName) {
            string script = string.Format("scorm.{0}();", methodName);

            Application.ExternalEval(script);
        }

        private static void CallJavascriptMethod(string methodName, string arg) {
            string script = string.Format("scorm.{0}(\"{1}\");", methodName, arg);

            Application.ExternalEval(script);
        }

        private static void CallJavascriptMethod<T>(string methodName, T arg) {
            string script = string.Format("scorm.{0}({1});", methodName, JsonUtility.ToJson(arg));

            Application.ExternalEval(script);
        }

        private static void CallJavascriptMethod(string methodName, float arg) {
            string script = string.Format("scorm.{0}({1});", methodName, arg);

            Application.ExternalEval(script);
        }
        #endregion

        #region Log
        public void LogInfo(string text) {
            string message = string.Format("[ScormAPI] - {0}", text);

            Debug.Log(message);

            if (OnMessageLogged != null) {
                OnMessageLogged(message);
            }
        }

        public void LogWarning(string text) {
            string message = string.Format("[ScormAPI Warning] - {0}", text);

            Debug.LogWarning(message);

            if (OnMessageLogged != null) {
                OnMessageLogged(message);
            }
        }

        public void LogError(string text) {
            string message = string.Format("[ScormAPI Error] - {0}", text);

            Debug.LogError(message);

            if (OnMessageLogged != null) {
                OnMessageLogged(message);
            }
        }
        #endregion
    }
}