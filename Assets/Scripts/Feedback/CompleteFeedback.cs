using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CompleteFeedback {

    public List<Info> CompleteFeedbackList;

    public CompleteFeedback()
    {
        CompleteFeedbackList = new List<Info>();
    }

    public void AddNode(Info info)
    {
        CompleteFeedbackList.Add(info);
    }

    public void SetChoice(int _IdChoice, string _Feedback)
    {
        Info lastLine = CompleteFeedbackList[CompleteFeedbackList.Count - 1];
        lastLine.AddChoiceIdAndFeedback(_IdChoice, _Feedback);
    }

    public string GetFeedback(int _IdConv, int _IdChoice)
    {
        foreach (Feedback feedback in Data.allFeedback)
        {
            if (_IdConv == feedback.IdConv && _IdChoice == feedback.IdNode)
            {
                return feedback.Text;
            }
        }
        return "Feedback not found";
    }

    public override string ToString()
    {
        string res = "";
        foreach (Info line in CompleteFeedbackList)
        {
            res += line.ToString() + "\n";
        }
        return res;
    }

    public class Info
    {
        private string question;
        private Dictionary<int, string> reponses;
        private string feedback;
        private int idChoice;
        private string goodAnswer;
        private int idGoodAnswer;

        public Info(string _Question)
        {
            question = _Question;
            reponses = new Dictionary<int, string>();
        }

        public void AddChoice(int _IdChoice, string _Text)
        {
            reponses.Add(_IdChoice, _Text);
        }

        public void AddGoodAnswer(int idReponse, string reponse)
        {
            goodAnswer = reponse;
            idGoodAnswer = idReponse;
        }

        public void AddChoiceIdAndFeedback(int _IdChoice, string _Feedback)
        {
            idChoice = _IdChoice;
            feedback = _Feedback;
        }

        public override string ToString()
        {
            string res = "Question : " + question + "\n" + "Reponses :";
            foreach (KeyValuePair<int, string> entry in reponses)
            {
                res += " " + entry.Value + "\n";
            }
            res += "Feedback :" + feedback;
            return res;
        }

        public int IdGoodAnswer
        {
            get
            {
                return idGoodAnswer;
            }

            set
            {
                idGoodAnswer = value;
            }
        }

        public string GoodAnswer
        {
            get
            {
                return goodAnswer;
            }

            set
            {
                goodAnswer = value;
            }
        }

        public string Question
        {
            get
            {
                return question;
            }

            set
            {
                question = value;
            }
        }

        public Dictionary<int, string> Reponses
        {
            get
            {
                return reponses;
            }

            set
            {
                reponses = value;
            }
        }

        public string Feedback
        {
            get
            {
                return feedback;
            }

            set
            {
                feedback = value;
            }
        }

        public int IdChoice
        {
            get
            {
                return idChoice;
            }

            set
            {
                idChoice = value;
            }
        }

    }
}
