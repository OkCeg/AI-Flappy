using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//better crossbreed?
public class Reset : MonoBehaviour
{
    public static int gen = 1;

    private void Start()
    {
        if (!Brain.start)
        {
            AssignWeights();
        }
    }

    public void NextGeneration()
    {
        NewObstacles.NextTime = Time.time + NewObstacles.TimeInterval;
        gen++;
        Brain.start = false;

        Population.first = FindBest().GetComponent<Brain>().CopyWeights();
        Population.second = FindSecondBest().GetComponent<Brain>().CopyWeights();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        //bug fix
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<Brain>().dead = false;
        }
    }

    public GameObject FindBest()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        int index = 0;
        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].GetComponent<Brain>().fitness > players[index].GetComponent<Brain>().fitness)
            {
                index = i;
            }
            else if (players[i].GetComponent<Brain>().fitness == players[index].GetComponent<Brain>().fitness)
            {
                if (DistanceMultiplied(players[i]) < DistanceMultiplied(players[index]))
                {
                    index = i;
                }
            }
        }
        return players[index];
    }

    public GameObject FindSecondBest()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");
        int index = 0;
        for (int i = 1; i < players.Length; i++)
        {
            if (players[i] != FindBest())
            {
                if (players[i].GetComponent<Brain>().fitness > players[index].GetComponent<Brain>().fitness)
                {
                    index = i;
                }
                else if (players[i].GetComponent<Brain>().fitness == players[index].GetComponent<Brain>().fitness)
                {
                    if (DistanceMultiplied(players[i]) < DistanceMultiplied(players[index]))
                    {
                        index = i;
                    }
                }
            }
        }
        return players[index];
    }

    public void AssignWeights()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Character");

        players[0].GetComponent<Brain>().SetWeights(Population.first);
        players[1].GetComponent<Brain>().SetWeights(Population.second);

        for (int i = 2; i < players.Length; i++)
        {
            players[i].GetComponent<Brain>().SetWeights(SinglePointCrossover());
            players[i].GetComponent<Brain>().MutateWeights();
        }
    }

    public float DistanceMultiplied(GameObject player)
    {
        Network net = player.GetComponent<Network>();
        return net.topDistance * net.bottomDistance;
    }

    public float[] SinglePointCrossover()
    {
        float[] newWeights = new float[Population.weightLength];
        int random = Random.Range(0, Population.weightLength);
        int index = 0;

        if (Random.value < 0.5f)
        {
            for (int i = 0; i < random; i++)
            {
                newWeights[index] = Population.first[index];
                index++;
            }

            for (int i = random; i < Population.weightLength; i++)
            {
                newWeights[index] = Population.second[index];
                index++;
            }
        }
        else
        {
            for (int i = 0; i < random; i++)
            {
                newWeights[index] = Population.second[index];
                index++;
            }

            for (int i = random; i < Population.weightLength; i++)
            {
                newWeights[index] = Population.first[index];
                index++;
            }
        }

        return newWeights;
    }
}
