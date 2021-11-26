using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class DestroyCharacter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            Trigger();
        }
    }

    private bool AllDeadCheck()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < characters.Length; i++)
        {
            if (!characters[i].GetComponent<Brain>().dead)
            {
                return false;
            }
        }
        return true;
    }

    public void Trigger()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = null;
        gameObject.GetComponent<Brain>().dead = true;
        if (AllDeadCheck())
        {
            GetComponentInParent<Reset>().NextGeneration();
        }
    }
}
