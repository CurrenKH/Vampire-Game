using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float Countdown;
    [SerializeField] GameObject spawnObject;
    //[SerializeField] float objectSpawnQTY = 10;
    [SerializeField] Camera cam;
    [SerializeField] float timer = 2f;
    //[SerializeField] string direction = "top";
    float timerHolder;

    float width;
    float widhtRadius;
    float height;
    float heightRadius;
    //[SerializeField] float objectSpawnPer10Secs = 10;

    //float width = cam.orthographicSize * cam.aspect + 1;

    // Start is called before the first frame update
    void Start()
    {
        timerHolder = timer;

        width = cam.orthographicSize * cam.aspect + 3;
        widhtRadius = width / 2;
        height = cam.orthographicSize + 3;
        heightRadius = height / 2;

    }

    // Update is called once per frame
    void Update()
    {
        Countdown -= Time.deltaTime;

        if (Countdown <= 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                SpawnRandomObject();
                timer = timerHolder;
            }
            Debug.Log(GameObject.FindGameObjectWithTag("Player"));
            Debug.Log(width);
            Debug.Log(height);

        }
    }

    private void SpawnRandomObject()
    {
        float randomSide = Random.Range(1, 5);
        Debug.Log("randomSide "+randomSide);
        Vector3 position = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        if (randomSide == 1)
        {
            // bottom
            float randomX = Random.Range(-widhtRadius, widhtRadius);
            position.x += randomX;
            position.y -= height;
        }
        else if(randomSide == 2)
        {
            // top
            float randomX = Random.Range(-widhtRadius, widhtRadius);
            position.x += randomX;
            position.y += height;
        }
        else if (randomSide == 3)
        {
            // right
            float randomY = Random.Range(-heightRadius, heightRadius);
            position.x += width;
            position.y += randomY;
        }
        else
        {
            // left
            float randomY = Random.Range(-heightRadius, heightRadius);
            position.x -= width;
            position.y += randomY;
        }
        Instantiate(spawnObject, position, Quaternion.identity);

        //float randomX = Random.Range(0, width);
        //float randomY = Random.Range(0, height);
        //Random random = new Random();
    }
}
