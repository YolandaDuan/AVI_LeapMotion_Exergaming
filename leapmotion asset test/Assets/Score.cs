using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private GameObject player;
    private float startX;
    private float score;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        startX = player.transform.position.x;
        score = 0f;
    }   
    // Update is called once per frame
    void Update()
    {
        score = player.transform.position.x - startX;
        text.text = "Distance: " + (int)score + "m";
    }
}
