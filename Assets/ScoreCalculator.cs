using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCalculator : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI scoreText;


    void Awake()
    {
        scoreText.text = "Score 0000";
    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = "Score" + ToString(player.Score);
    }
}
