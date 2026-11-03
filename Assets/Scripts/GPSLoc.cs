using UnityEngine;

public class GPSLoc
{
    public double lon;//longitude
    public double lat;//lattitude

    public GPSLoc()
    {
        lon = 0;
        lat = 0;

    }
    public GPSLoc(double lon, double lat)
    {
        this.lon = lon;
        this.lat = lat;

    }
    public string getLocData()
    {
        return "Lat: " + lat + "\nLon: " + lon;
   
    }
}
