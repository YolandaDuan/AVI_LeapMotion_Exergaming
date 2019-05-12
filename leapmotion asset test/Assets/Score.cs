using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameObject player;
    private float startX;
    private float score;
    private float minusScore;
    private float highScore;
    public Text text;
    public float CurrentScore { get { return score; } }
    public float HighScore { get { return highScore; } }
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startX = player.transform.position.x;
        score = 0f;
        minusScore = 0f;
        highScore = 0f;
    }   

    public void resetScore()
    {
        minusScore = player.transform.position.x - startX;
        score = player.transform.position.x - startX - minusScore;
    }
    // Update is called once per frame
    void Update()
    {
        score = player.transform.position.x - startX - minusScore;
        if (score > highScore)
        {
            highScore = score;
        }
        text.text = "Time: " + SecondsToMinutesAndSeconds(Time.timeSinceLevelLoad) + "\n" +
                    "High Score: " + (int)highScore + " m\n" +
                    "Score: " + (int)score + " m\n";
    }

    private string SecondsToMinutesAndSeconds(float seconds) {
        var roundedSeconds = (int)Mathf.Round(seconds);
        return $"{Mathf.Floor(roundedSeconds/60)} min, {roundedSeconds % 60} sec";
    }                            
}
