using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class All : MonoBehaviour
{
    //for mutation marking in history
    public static int NextConnectionNum = 0;

    //save 2500 pipe heights
    public float[] randomPipeHeights = new float[2500];

    private void Start()
    {
        //initialize a population
        gameObject.AddComponent<Population>();

        //save the pipe heights
        for (int i = 0; i < randomPipeHeights.Length; i++)
        {
            randomPipeHeights[i] = Random.Range(-2.5f, 2.5f);
        }
    }
}
