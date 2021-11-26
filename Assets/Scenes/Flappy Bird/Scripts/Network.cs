using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//finetune weights
public class Network : MonoBehaviour
{
    public float horizontalDistance;
    public float topDistance;
    public float bottomDistance;
    public float vertVel;

    private float prediction;

    private float[] weights;

    private void Start()
    {
        weights = GetComponent<Brain>().CopyWeights();
    }

    private float Sigmoid(float num)
    {
        return 1 / (1 + Mathf.Exp(-num));
    }

    private float CalculatePrediction()
    {
        float hidden1 = horizontalDistance * weights[0] + topDistance * weights[1] + bottomDistance * weights[2] + vertVel * weights[3];
        float hidden2 = horizontalDistance * weights[4] + topDistance * weights[5] + bottomDistance * weights[6] + vertVel * weights[7];
        float hidden3 = horizontalDistance * weights[8] + topDistance * weights[9] + bottomDistance * weights[10] + vertVel * weights[11];
        float hidden4 = horizontalDistance * weights[12] + topDistance * weights[13] + bottomDistance * weights[14] + vertVel * weights[15];


        prediction = hidden1 * weights[16] + hidden2 * weights[17] + hidden3 * weights[18] + hidden4 * weights[19];

        return prediction;
    }

    private void CalculateDistances()
    {
        GameObject top = GameObject.FindGameObjectWithTag("Top");
        GameObject bottom = GameObject.FindGameObjectWithTag("Bottom");

        //if top exists, bottom also exists
        if (top != null && !GetComponent<Brain>().dead)
        {
            topDistance = top.transform.position.y - transform.position.y;
            bottomDistance = bottom.transform.position.y - transform.position.y;
            horizontalDistance = top.transform.position.x - transform.position.x;
        }

        vertVel = GetComponent<Rigidbody2D>().velocity.y;
    }

    private void Update()
    {
        CalculateDistances();
        CalculatePrediction();

        if (Sigmoid(prediction) > 0.55f)
        {
            GetComponent<Jump>().CharacterJump();
        }
    }
}
