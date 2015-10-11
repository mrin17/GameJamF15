using UnityEngine;
using System.Collections;

public class getScoreScript : MonoBehaviour {

    float highScore = 0;
    float currentScore = 0;
    public const float X_LIFE_SCORE = 2000;

    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this);
    }

    public float getScore()
    {
        return currentScore;
    }
    public void addToScore()
    {
        currentScore += 100;
    }

    public void resetScore()
    {
        currentScore = 0;
    }

    public void calcHighScore()
    {
        if (currentScore > highScore)
            highScore = currentScore;
    }

    public float getHighScore()
    {
        return highScore;
    }
}
