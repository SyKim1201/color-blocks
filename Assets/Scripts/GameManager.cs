using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score;
    public Text scoreText;
    public Text final;

    public bool gameOver = false;

    void Start()
    {
        instance = GetComponent<GameManager>();

        score = 0;
        scoreText.text = score.ToString();

    }

    public void IncreaseScore()
    {
        score++;
    }

    public void FinalScore()
    {
        final.text = score.ToString();
    }

    public void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

}
