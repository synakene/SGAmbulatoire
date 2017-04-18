using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;

public static class Data {

    public static void ReinitLuaVar()
    {
		DialogueLua.SetVariable("choix1", false);
		DialogueLua.SetVariable("choix2", false);
		DialogueLua.SetVariable("choix3", false);

        DialogueLua.SetVariable("param1", false);
        DialogueLua.SetVariable("param2", false);
        DialogueLua.SetVariable("param3", false);
        DialogueLua.SetVariable("param4", false);
        DialogueLua.SetVariable("param5", false);

        DialogueLua.SetVariable("q1", false);
        DialogueLua.SetVariable("q2", false);
        DialogueLua.SetVariable("q3", false);
        DialogueLua.SetVariable("q4", false);
        DialogueLua.SetVariable("q5", false);
        DialogueLua.SetVariable("q6", false);
        DialogueLua.SetVariable("q7", false);
        DialogueLua.SetVariable("q8", false);

        DialogueLua.SetVariable("terminer1", false);
        DialogueLua.SetVariable("terminer2", false);
        DialogueLua.SetVariable("terminer3", false);

        DialogueLua.SetVariable("Fdiag2", false);
		DialogueLua.SetVariable("Fdiag3", false);
		DialogueLua.SetVariable("Fdiag4", false);

		DialogueLua.SetVariable("diag1", false);
		DialogueLua.SetVariable("diag2", false);
        DialogueLua.SetVariable("diag3", false);

        DialogueLua.SetVariable("diagS5_1", false);
        DialogueLua.SetVariable("diagS5_2", false);
        DialogueLua.SetVariable("NbParam", 0);
    }

    //Feedback
    public static List<Feedback> allFeedback;
    public static CompleteFeedback CompleteFeedback;
    public static string playerName = "";

    // Description scenes (for feedbacks)
    public static string S1 = "Présentation du patient";
    public static string S2 = "Identito-vigilance";
    public static string S3 = "Examen somatique";
    public static string S4 = "Prise de paramètres vitaux";
    public static string S5 = "Mesures d'hygiene";

    //time :
    public static int min = 0;
    public static int sec = 0;

    //Score :
    public static int scoreObj1 = 0;
    public static int scoreObj2 = 0;
    public static int scoreObj3 = 0;

    public static int curScoreObj1 = 0;
    public static int curScoreObj2 = 0;
    public static int curScoreObj3 = 0;

    public static int MaxScoreObj1 = 9;
    public static int MaxScoreObj2 = 17;
    public static int MaxScoreObj3 = 15;

    public static float MaxTotal = (float)(MaxScoreObj1 + MaxScoreObj2 + MaxScoreObj3);

    public static float ScormScoreRetrived = 0;

    public static int getAverageScore()
    {
        return Mathf.RoundToInt(((curScoreObj1 + curScoreObj2 + curScoreObj3) / MaxTotal) * 100);
    }

    public static void reinitFeedback()
    {
        CompleteFeedback = new CompleteFeedback();
    }

    public static void reinitScore()
    {
        scoreObj1 = 0;
        scoreObj2 = 0;
        scoreObj3 = 0;

        curScoreObj1 = 0;
        curScoreObj2 = 0;
        curScoreObj3 = 0;
    }
}
