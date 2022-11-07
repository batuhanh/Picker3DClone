using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPlatform : MonoBehaviour
{
    [SerializeField] private Transform myRoadTransform;
    [SerializeField] private Renderer platformRenderer;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballsParent;
    public void SetPlatformColor(Color newColor)
    {
        platformRenderer.material.color = newColor;
    }
    public void CalcPlatformLength(float platformLength)
    {
        myRoadTransform.localScale = new Vector3(myRoadTransform.localScale.x,
            myRoadTransform.localScale.y, platformLength);
        myRoadTransform.localPosition = new Vector3(myRoadTransform.localPosition.x, myRoadTransform.localPosition.y,
             transform.localPosition.z + ((platformLength - 1f) * 2.5f));

    }
    public void SpawnBalls(int amount)
    {
        ballsParent.position = new Vector3(myRoadTransform.position.x,
            ballsParent.position.y, myRoadTransform.position.z);
        int oldBallsCount = ballsParent.childCount;
        for (int i = 0; i < oldBallsCount; i++)
        {
            DestroyImmediate(ballsParent.GetChild(0).gameObject);
        }
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-2.1f, 2.1f),
                ballsParent.position.y, myRoadTransform.position.z + UnityEngine.Random.Range(-5f, 5f));
            GameObject spawnedBall = Instantiate(ballPrefab, spawnPos, Quaternion.identity, ballsParent);
        }
    }
}
