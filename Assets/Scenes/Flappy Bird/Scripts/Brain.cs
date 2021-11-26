using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//finetune weights
public class Brain : MonoBehaviour
{
    public static bool start = true;
    public bool dead = false;

    public int fitness = 0;
    public int score = 0;

    private float[] weights = new float[Population.weightLength];

    private void Awake()
    {
        fitness = 0;
        InitializeWeights();
    }

    public void InitializeWeights()
    {
        if (start)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = Random.Range(-2f, 2f);
            }
        }
    }

    public void MutateWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            if (Random.value < 0.1f)
            {
                weights[i] = Random.Range(-2f, 2f);
            }
        }
    }

    public void SetWeights(float[] newWeights)
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = newWeights[i];
        }
    }

    public void SetWeightsIndex(int index, float[] newWeights)
    {
        weights[index] = newWeights[index];
    }

    public float[] CopyWeights()
    {
        float[] temp = new float[weights.Length];

        for (int i = 0; i < temp.Length; i++)
        {
            temp[i] = weights[i];
        }

        return temp;
    }

    private void Update()
    {
        if (!dead)
        {
            fitness++;
        }
    }
}
