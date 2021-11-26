using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class ScoreCounter : MonoBehaviour
{
    private GameObject[] players;

    public Transform top;

    private bool passedPlayer;

    void Start()
    {
        top = transform.GetChild(0).GetChild(0);
    }

    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Character");

        if (!passedPlayer && players.Length > 0 && top.transform.position.x < players[0].transform.position.x)
        {
            passedPlayer = true;

            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].GetComponent<Brain>().dead)
                {
                    players[i].GetComponent<Brain>().score++;
                }
            }
        }
    }
}
