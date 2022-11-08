using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class RoadPlatform : MonoBehaviour
{
    [SerializeField] private Transform myRoadTransform;
    [SerializeField] private Renderer platformRenderer;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform ballsParent;
    public void SetPlatformColor(Color newColor)
    {
        Material newMat = platformRenderer.material;
        newMat.color = newColor;
        platformRenderer.material= newMat;
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
        for (int i = 0; i < ballsParent.childCount; i++)
        {
            ballsParent.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < amount; i++)
        {
            if (i < ballsParent.childCount)
            {
                ballsParent.GetChild(i).gameObject.SetActive(true);
                Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-2.1f, 2.1f),
                ballsParent.position.y, myRoadTransform.position.z + UnityEngine.Random.Range(-3f, 3f));
                ballsParent.GetChild(i).transform.position = spawnPos;
            }
            else
            {
                Vector3 spawnPos = new Vector3(UnityEngine.Random.Range(-2.1f, 2.1f),
                ballsParent.position.y, myRoadTransform.position.z + UnityEngine.Random.Range(-3f, 3f));
                GameObject spawnedBall = (GameObject)PrefabUtility.InstantiatePrefab(ballPrefab);
                spawnedBall.transform.SetParent(ballsParent);
                spawnedBall.transform.position = spawnPos;
            }

        }

    }
}
