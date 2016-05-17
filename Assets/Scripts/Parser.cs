using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.Collections.Generic;
using System.Text;


public class Parser {

    public static void ParseFeedback()
    {
        Data.allFeedback = new List<Feedback>();
        TextAsset jsonFile = Resources.Load<TextAsset>("Feedback");
        JSONNode data = JSON.Parse(jsonFile.ToString());
        for (var i = 0; i < data["feedback"].Count; i++)
        {
            Feedback feedback;
            int idConv = data["feedback"][i]["idConv"].AsInt;
            int idNode = data["feedback"][i]["idNode"].AsInt;
            int idGoodAnswer = data["feedback"][i]["idGoodAnswer"].AsInt;
            string text = data["feedback"][i]["text"];

            feedback = new Feedback(idConv, idNode, idGoodAnswer, text);
            Data.allFeedback.Add(feedback);
        }
    }
}
