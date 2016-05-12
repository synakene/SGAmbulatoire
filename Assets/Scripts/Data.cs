using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public static class Data {

    public static void ReinitLuaVar()
    {
        DialogueLua.SetVariable("choix1", true);
        DialogueLua.SetVariable("choix2", true);
        DialogueLua.SetVariable("choix3", true);
    }

    //Feedback
    //public static List<Feedback> allFeedback;
    //public static CompleteFeedback CompleteFeedback;
    //public static string playerName = "";

    //time :
    public static int min = 0;
    public static int sec = 0;

    //Score :
    //public static int scoreObj1 = 0;
    //public static int scoreObj2 = 0;
    //public static int scoreObj3 = 0;
    //public static int scoreObj4 = 0;

    //public static int MaxScoreObj1 = 0;
    //public static int MaxScoreObj2 = 0;
    //public static int MaxScoreObj3 = 0;
    //public static int MaxScoreObj4 = 0;

    //public static int MaxTotal = 23;
    //public static float jaugeValue = 0;

    //public static void reinitFeedback()
    //{
    //    CompleteFeedback = new CompleteFeedback();
    //}

    //public static void reinitScore()
    //{
    //    scoreObj1 = 0;
    //    scoreObj2 = 0;
    //    scoreObj3 = 0;
    //    scoreObj4 = 0;

    //    MaxScoreObj1 = 0;
    //    MaxScoreObj2 = 0;
    //    MaxScoreObj3 = 0;
    //    MaxScoreObj4 = 0;
    //    jaugeValue = 0;
    //}
}
