using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //NEAT variables
    public float[] vision = new float[4];
    public int fitness = 0;
    public int score = 0;
    public int lifespan = 0;
    public bool dead = false;

    //Unity variables
    public float jumpPower = 8;
    public float limit = 10;

    //construction variables
    public Genome brain;
    public Rigidbody2D rb;

    private void Start()
    {
        Debug.Log("test");
        brain = new Genome(4, 1, false);
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LimitVelocity();
    }

    public void Jump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }

    public void LimitVelocity()
    {
        if (rb.velocity.y > limit)
        {
            rb.velocity = new Vector2(0, limit);
        }
        else if (rb.velocity.y < -limit)
        {
            rb.velocity = new Vector2(0, -limit);
        }
    }


    public GameObject FindClosestPipe()
    {
        GameObject[] pipes = GameObject.FindGameObjectsWithTag("Obstacle");

        for (int i = 0; i < pipes.Length; i++)
        {
            pipes[i].GetComponent<>
        }
    }

    //calculate input values
    public void Look()
    {
        vision = new float[4];

        vision[0] = rb.velocity.y;
    }
}
