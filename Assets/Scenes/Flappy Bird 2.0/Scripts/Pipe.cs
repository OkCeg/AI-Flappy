using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public bool passedPlayer;
    public GameObject[] players;

    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Character");
    }

    private void Update()
    {
        if (players[0].transform.position.x > transform.position.x && !passedPlayer)
        {
            passedPlayer = true;

            //update score for all alive players
            for (int i = 0; i < players.Length; i++)
            {
                if (!players[i].GetComponent<Player>().dead)
                {
                    players[i].GetComponent<Player>().score++;
                }
            }
        }
    }
}
