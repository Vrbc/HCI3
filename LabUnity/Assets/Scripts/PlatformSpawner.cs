using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject startingPlatform;
    [SerializeField] private int maxActivePlatforms = 5;

    [SerializeField] private float platformDistanceZ = 9f;
    [SerializeField] private float minY = 0f;
    [SerializeField] private float maxY = 3f;
    [SerializeField] private float maxYDifference = 1f;
    [SerializeField] private float maxXOffset = 3f;

    private Queue<GameObject> activePlatforms;
    private Vector3 nextSpawnPosition;
    void Start()
    {
        activePlatforms = new Queue<GameObject>();
        activePlatforms.Enqueue(startingPlatform);

        nextSpawnPosition = startingPlatform.transform.position;

        for(int i = 0; i < 5; i++)
        {
            SpawnNextPlatform();
        }
    }

    public void SpawnNextPlatform()
    {
        Vector3 previousPosition = nextSpawnPosition;

        float rndX = Random.Range(-maxXOffset, maxXOffset);
        
        float rndYchange = Random.Range(-maxYDifference, maxYDifference);
        float newY =  previousPosition.y + rndYchange;

        if(newY > maxY) newY = maxY;
        if(newY < minY) newY = minY;

        nextSpawnPosition = new Vector3(rndX, newY, previousPosition.z + platformDistanceZ);

        GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPosition, Quaternion.identity);

        activePlatforms.Enqueue(newPlatform);

        if(activePlatforms.Count > maxActivePlatforms)
        {
            GameObject oldest = activePlatforms.Dequeue();

            if(oldest != null)
            {
                Destroy(oldest);
            }
        }
    }
}
