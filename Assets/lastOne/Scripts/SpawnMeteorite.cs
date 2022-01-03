using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMeteorite : MonoBehaviour
{
    [SerializeField]
    private int meteoSpawnIncreaseRate;
    [SerializeField]
    private GameObject meteoriteGameObjectList;
    [SerializeField]
    private GameObject sky;
    private float nextSpawnFrame;
    private int nextSpawnCounter;

    void Start()
    {
        nextSpawnFrame = 40;
        nextSpawnCounter = 0;
    }

    void FixedUpdate()
    {
        nextSpawnCounter++;
        if (nextSpawnCounter < nextSpawnFrame || Time.timeScale == 0)
            return;
        SpawnMeteo(GetRandomMeteoGameObject());
        nextSpawnCounter = 0;
        if (nextSpawnFrame > 7)
            nextSpawnFrame = nextSpawnFrame - meteoSpawnIncreaseRate;
    }

    private void SpawnMeteo(GameObject meteo)
    {
        GameObject tmpGameObject = Instantiate(meteo);
        tmpGameObject.transform.position = GetRandomVectorPositionOnSky();
        tmpGameObject.SetActive(true);
    }

    private Vector3 GetRandomVectorPositionOnSky()
    {
        Vector3 skyStart = sky.GetComponent<Renderer>().bounds.min;
        Vector3 skyEnd = sky.GetComponent<Renderer>().bounds.max;
        Vector3 tmpVector3 = new Vector3(Random.Range(skyStart.x,skyEnd.x),skyStart.y,0);
        return tmpVector3;
    }

    private GameObject GetRandomMeteoGameObject()
    {
        int randomInt = Random.Range(0, meteoriteGameObjectList.transform.childCount-1);
        return meteoriteGameObjectList.transform.GetChild(randomInt).gameObject;
    }
}
