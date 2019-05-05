using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject platform;
    public int nrOfPlatforms;
    public float minRangeX;
    public float maxRangeX;
    public float minRangeY;
    public float maxRangeY;
    private float startY;

    void Start()
    {
        startY = transform.position.y;
        float x = transform.position.x;
        float y;
        float z = transform.position.z;
        for (int i = 0; i < nrOfPlatforms; i++)
        {
            x += Random.Range(minRangeX, maxRangeX);
            y = startY + Random.Range(minRangeY, maxRangeY);
            platform.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
            Instantiate(platform, new Vector3(x, y, z), Quaternion.identity);
        }
    }

}
