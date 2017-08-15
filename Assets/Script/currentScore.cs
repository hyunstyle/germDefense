using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class currentScore : MonoBehaviour
{

    private int curScore;
    public Text yourScore;

    // Use this for initialization
    void Start()
    {
        curScore = lifeController.score;
        yourScore.text = curScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}