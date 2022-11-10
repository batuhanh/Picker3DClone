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
    [SerializeField] private Material platformMat;
    public void SetPlatformColor(Color newColor)
    {
        var tempMaterial = new Material(platformMat);
        tempMaterial.color = newColor;
        platformRenderer.sharedMaterial = tempMaterial;
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

        if (amount == ballsParent.childCount && ballsParent.childCount > 0 && ballsParent.GetChild(0).position != new Vector3(0, ballsParent.position.y, myRoadTransform.position.z))
        {
            //means balls position setted in a deafult way
            return;
        }
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
                Vector3 spawnPos = new Vector3(0, ballsParent.position.y,
                    myRoadTransform.position.z + i);
                ballsParent.GetChild(i).transform.position = spawnPos;
            }
            else
            {
                Vector3 spawnPos = new Vector3(0f, ballsParent.position.y,
                    myRoadTransform.position.z + i);
                GameObject spawnedBall = (GameObject)PrefabUtility.InstantiatePrefab(ballPrefab);
                spawnedBall.transform.SetParent(ballsParent);
                spawnedBall.transform.position = spawnPos;
            }

        }


    }
}
