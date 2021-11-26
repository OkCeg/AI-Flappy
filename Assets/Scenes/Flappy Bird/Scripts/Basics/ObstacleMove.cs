using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class ObstacleMove : MonoBehaviour
{
    public static float Speed = 30;

    private void FixedUpdate()
    {
        transform.position -= new Vector3(Speed / 1000, 0);
    }
}
