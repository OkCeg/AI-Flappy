using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class LimitHeight : MonoBehaviour
{
    private void Update()
    {
        //to ensure no crazy high or low y-values
        if (transform.position.y > Camera.main.orthographicSize)
        {
            GetComponent<DestroyCharacter>().Trigger();
            transform.position = new Vector2(transform.position.x, Camera.main.orthographicSize);
        }
        else if (transform.position.y < -Camera.main.orthographicSize)
        {
            transform.position = new Vector2(transform.position.x, -Camera.main.orthographicSize);
        }
    }
}
