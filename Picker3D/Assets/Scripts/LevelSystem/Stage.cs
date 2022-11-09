using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] private int stageIndex=0;
    [SerializeField] private int spawnedBallCount=10;
    [SerializeField] private int targetBallCount=10;
    [SerializeField] private Color platformColor=Color.white;
    [SerializeField] private float platformLength=1;

    [SerializeField] private BallCollecterPlatform ballCollecterPlatform;
    [SerializeField] private RoadPlatform roadPlatform;
    private void Start()
    {
        ballCollecterPlatform.SetUpperCubeColor(platformColor);
        roadPlatform.SetPlatformColor(platformColor);
        ballCollecterPlatform.SetCollectLimit(targetBallCount);
    }

    public void SetupStage()
    {
        roadPlatform.CalcPlatformLength(platformLength);
        ballCollecterPlatform.SetPosition(platformLength);
        ballCollecterPlatform.SetUpperCubeColor(platformColor);
        roadPlatform.SetPlatformColor(platformColor);
        roadPlatform.SpawnBalls(spawnedBallCount);
        ballCollecterPlatform.SetCollectedText("0 / " + targetBallCount.ToString());
        
    }
    public void SetStagePosZ(float localStartPosZ)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y
            , localStartPosZ);
    }
    public int SpawnedBallCount
    {
        get { return spawnedBallCount; }
        set { spawnedBallCount = value; }
    }
    public int StageIndex
    {
        get { return stageIndex; }
        set { stageIndex = value; }
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
