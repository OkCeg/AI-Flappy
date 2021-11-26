using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adjust how to save data?
public class Population : MonoBehaviour
{
    public static int weightLength = 20;

    public static float[] first = new float[weightLength];
    public static float[] second = new float[weightLength];

    //set in inspector
    public GameObject character;

    private void Awake()
    {
        for (int i = 0; i < 50; i++)
        {
            Instantiate(character, character.transform.position, Quaternion.identity, transform);
        }
    }

    //for debugging
    private void Start()
    {
        GameObject[] characters = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].name = "Character " + i;
        }
    }
}
