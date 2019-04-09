using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocationServicesController : MonoBehaviour
{
    public Vector2 coordinates;
    private float latitude;
    private float longitude;

    void Start()
    {
        latitude = 0;
        longitude = 0;
        StartCoroutine(LocationServiceUpdate());     
    }

    void Update()
    {
        coordinates = new Vector2(latitude, longitude);
    }

    IEnumerator LocationServiceUpdate()
    {
        Input.location.Start();

        int waitTime = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
        {
            yield return new WaitForSeconds(1);
            waitTime--;
        }

        if (waitTime <= 0)
        {
            latitude = 0;
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            latitude = 0;
            yield break;
        }
        else
        {
            latitude = Input.location.lastData.latitude;
            longitude = Input.location.lastData.longitude;
        }

        Input.location.Stop();
    }
}
