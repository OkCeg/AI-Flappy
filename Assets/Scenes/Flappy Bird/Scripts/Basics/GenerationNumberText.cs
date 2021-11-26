using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//complete
public class GenerationNumberText : MonoBehaviour
{
    private Text text;

    private void Start()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        text.text = "Generation " + Reset.gen;
    }
}
