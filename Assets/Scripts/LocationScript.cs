using UnityEngine;
using TMPro;
using System;

public class LocationScript : MonoBehaviour
{

    [SerializeField]
    private char unit = 'K';

    public TMP_Text debugTxt;
    public bool gps_ok = false;
    float PI = Mathf.PI;

    GPSLoc startLoc = new GPSLoc();
    GPSLoc currLoc = new GPSLoc();

    bool measureDistance = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}


public class GPSLoc
{
    public float lon;//longitude
    public float lat;//lattitude

    public GPSLoc()
    {
        lon = 0;
        lat = 0;

    }
    public GPSLoc(float lon, float lat)
    {
        this.lon = lon;
        this.lat = lat;

    }
    public string getLocData()
    {
        return "Lat: " + lat + "\nLon: " + lon;
   
    }
}