using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//complete
public class ScoreText : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        int high = FindHighestScore();

        text.text = "Score: " + high;

        if (high == 250)
        {
            Debug.Log("SUCCESS!!!");
            Debug.Log(Reset.gen);
        }
    }

    public int FindHighestScore()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        int max = 0;
        foreach (GameObject character in players)
        {
            int charScore = character.GetComponent<Brain>().score;
            if (charScore > max)
            {
                max = charScore;
            }
        }
        return max;
    }
}
