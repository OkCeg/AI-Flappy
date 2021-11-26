using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class PassedPlayer : MonoBehaviour
{
    private GameObject player;

    //for the top point to check for point adding
    private void Update()
    {
        player = GameObject.FindWithTag("Character");
        if (player != null && transform.position.x < player.transform.position.x)
        {
            Destroy(gameObject);
        }
    }
}
