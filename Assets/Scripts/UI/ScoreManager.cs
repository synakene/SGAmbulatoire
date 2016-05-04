using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour {
    public int score = 0;
    public Text scoreText;

    private void upView()
    {
        scoreText.text = "Score : " + score;
    }

    public void AddScore(double amount)
    {
        score += (int)amount;
        upView();
    }

    public void reinitScore()
    {
        score = 0;
        upView();
    }
}
