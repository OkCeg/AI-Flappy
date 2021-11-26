using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//complete
public class NewObstacles : MonoBehaviour
{
    public static float TimeInterval = 1.5f;

    public static float NextTime;

    public static float[] heights;

    public int index = 1;

    //inspector
    public GameObject player;
    public GameObject obstacle;

    private void Start()
    {
        TimeInterval = (Camera.main.orthographicSize * Camera.main.aspect - player.transform.position.x) * 10 / ObstacleMove.Speed;

        if (Brain.start)
        {
            heights = new float[250];
            for (int i = 0; i < heights.Length; i++)
            {
                heights[i] = Random.Range(-2.5f, 2.5f);
            }
        }

        NextTime = Time.time + TimeInterval;
        Instantiate(obstacle, new Vector2(Camera.main.aspect * Camera.main.orthographicSize, heights[0]), Quaternion.identity);
    }

    private void Update()
    {
        if (Time.time > NextTime)
        {
            Instantiate(obstacle, new Vector2(Camera.main.aspect * Camera.main.orthographicSize, heights[index]), Quaternion.identity);
            index++;
            NextTime = Time.time + TimeInterval;
        }
    }
}
