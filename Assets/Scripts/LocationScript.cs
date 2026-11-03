using UnityEngine;
using TMPro;
using System;
using System.Collections;
using Unity.VisualScripting;

public class LocationScript : MonoBehaviour
{

    [SerializeField]
    private char unit = 'K';//kilometers

    public TMP_Text debugTxt;
    public bool gps_ok = false;
    float PI = Mathf.PI;

    GPSLoc startLoc = new GPSLoc();
    GPSLoc currLoc = new GPSLoc();

    bool measureDistance = false;

    IEnumerator Start()
    {
        //check if location service is enabled
        if(!Input.location.isEnabledByUser)
        {
            Debug.Log("location not enabled on device or app does not have permission to use location");
            debugTxt.text = "location not enabled on device or app does not have permission to use location";
        }
        //start location service
        Input.location.Start();

        //wait until location service initializes
        int maxWait = 20;//how long until give up connecting
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        //if the service didnt intitalize in 20 seconds. cancel locations ervice use e.g timed out
        if (maxWait < 1)
        {
            Debug.Log("Timed out");
            debugTxt.text += "\nTimed Out";
            yield break;
        }

        // if the connection failed this cancels locations service use
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device locatrion");
            debugTxt.text += ("\nUnable to determine device location");
            yield break;
        }
        else
        {
            //if the connection succeeded this retrives the devices current location and 
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude);
            debugTxt.text
                = "\nLocation: \nLat: " + Input.location.lastData.latitude
                 + " \nLon: " + Input.location.lastData.longitude
                 + " \nAlt: " + Input.location.lastData.altitude
                 + " \nH_Acc: " + Input.location.lastData.horizontalAccuracy
                 + " \nTime: " + Input.location.lastData.timestamp;

            gps_ok = true;
        }
    }

    
    void Update()
    {
        if (gps_ok)
        {
            debugTxt.text = "GPS:...";

            debugTxt.text
                = "\nLocation: \nLat: " + Input.location.lastData.latitude
                + " \nLon: " + Input.location.lastData.longitude
                + " \nH_Acc: " + Input.location.lastData.horizontalAccuracy;

            
            currLoc.lat = Input.location.lastData.latitude;
            currLoc.lon = Input.location.lastData.longitude;


            debugTxt.text += "\nStored: " + startLoc.getLocData();//should be zero when on screen if right

            if (measureDistance == true)
            {
                double distanceBetween = distance(currLoc.lat, currLoc.lon, startLoc.lat, startLoc.lon, 'k'); 
                debugTxt.text += "\nDistance: " + distanceBetween;
            
            }

        }
    }
    public void StopGPS()
    {
        Input.location.Stop();

    }

    public void StoreCurrentGPS()
    {
        startLoc = new GPSLoc(currLoc.lon, currLoc.lat);
        measureDistance = true;
    }

    
    private double distance(double lat1, double lon1, double lat2, double lon2, char unit)
    {
        if ((lat1 == lat2) && (lon1 == lon2))
        {
            return 0;
        }
        else
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts decimal degrees to radians             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double deg2rad(double deg)
    {
        return (deg * Math.PI / 180.0);
    }

    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    //::  This function converts radians to decimal degrees             :::
    //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
    private double rad2deg(double rad)
    {
        return (rad / Math.PI * 180.0);
    }


    public void Distance()
    {
        
    }
}


