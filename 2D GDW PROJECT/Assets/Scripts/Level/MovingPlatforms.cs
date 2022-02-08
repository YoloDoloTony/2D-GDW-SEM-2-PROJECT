using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] GameObject[] waypoints;
    [SerializeField] int speed;

    PlayerController player;


    int num;

    private void Start()
    {
        player = GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(waypoints[num].transform.position, transform.position) == 0)
        {
            num++;
            if (num >= waypoints.Length)
            {
                num = 0;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, waypoints[num].transform.position, Time.deltaTime * speed);
    }

 
}
