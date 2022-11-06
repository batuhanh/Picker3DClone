using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int spawnedBallCount=10;
    [SerializeField] private int targetBallCount=10;
    [SerializeField] private Color platformColor=Color.white;
    [SerializeField] private float platformLength=1;

    public void SetupStage()
    {
        //do platform color spawn balls set target ball and set length of platform
    }
    public int SpawnedBallCount
    {
        get { return spawnedBallCount; }
        set { spawnedBallCount = value; }
    }
    public int TargetBallCount
    {
        get { return targetBallCount; }
        set { targetBallCount = value; }
    }
    public Color PlatformColor
    {
        get { return platformColor; }
        set { platformColor = value; }
    }
    public float PlatformLength
    {
        get { return platformLength; }
        set { platformLength = value; }
    }
}
