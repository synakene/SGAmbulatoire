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

        DialogueLua.SetVariable("param1", false);
        DialogueLua.SetVariable("param2", false);
        DialogueLua.SetVariable("param3", false);
        DialogueLua.SetVariable("param4", false);
        DialogueLua.SetVariable("param5", false);
        DialogueLua.SetVariable("diag1", false);
        DialogueLua.SetVariable("diag2", false);
        DialogueLua.SetVariable("diag3", false);
        DialogueLua.SetVariable("diag4", false);
        DialogueLua.SetVariable("diag5", false);
        DialogueLua.SetVariable("diag6", false);
        DialogueLua.SetVariable("diag7", false);
        DialogueLua.SetVariable("diag8", false);
        DialogueLua.SetVariable("diag9", false);
    }

    //Feedback
    public static List<Feedback> allFeedback;
    public static CompleteFeedback CompleteFeedback;
    public static string playerName = "";

    //time :
    public static int min = 0;
    public static int sec = 0;

    //Score :
    public static int scoreObj1 = 0;
    public static int scoreObj2 = 0;

    public static int MaxScoreObj1 = 0;
    public static int MaxScoreObj2 = 0;

    public static float MaxTotal = 23f;

    public static void reinitFeedback()
    {
        CompleteFeedback = new CompleteFeedback();
    }

    public static void reinitScore()
    {
        scoreObj1 = 0;
        scoreObj2 = 0;

        MaxScoreObj1 = 0;
        MaxScoreObj2 = 0;
    }
}
