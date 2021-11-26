using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class DestroyObstacle : MonoBehaviour
{
    private void Update()
    {
        if (transform.position.x < - Camera.main.aspect * Camera.main.orthographicSize - 6)
        {
            Destroy(gameObject);
        }
    }
}
