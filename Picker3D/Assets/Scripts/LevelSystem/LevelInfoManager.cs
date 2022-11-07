using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class LevelInfoManager : MonoBehaviour
{
    [SerializeField] private Transform stagesParent;
    [SerializeField] private List<GameObject> stages;
    [SerializeField] private GameObject stagePrefab;
    [SerializeField] private Renderer startCubeRenderer;
    [SerializeField] private GameObject endCube;
    private int stageCount = 0;


    public int GetStagesCount()
    {
        stageCount = stagesParent.childCount;
        return stageCount;
    }
    public void SetStagesCount(int newStageCount)
    {
        if (newStageCount != GetStagesCount())
        {
            if (newStageCount < stageCount)
            {
                for (int i = 0; i < stageCount - newStageCount; i++)
                {
                    GameObject stageWillRemove = stages[stages.Count - 1];
                   
                    stages.Remove(stageWillRemove);
                    DestroyImmediate(stageWillRemove);
                }
            }
            else if (newStageCount > stageCount)
            {
                for (int i = 0; i < newStageCount - stageCount; i++)
                {
                    GameObject newStage = Instantiate(stagePrefab, stagesParent.transform.position,
                        Quaternion.identity, stagesParent);
                    stages.Add(newStage);
                }
            }
            stageCount = newStageCount;

        }

    }
    public void SetupStartEndObjects()
    {
        startCubeRenderer.sharedMaterial.color = stages[0].gameObject.GetComponent<Stage>().PlatformColor;
        float endCubeZPos = 2.5f;
        foreach (var stage in stages)
        {
            endCubeZPos += (stage.gameObject.GetComponent<Stage>().PlatformLength * 5f) + 5f;
        }
        endCube.transform.localPosition = new Vector3(endCube.transform.localPosition.x,
            endCube.transform.localPosition.y, endCubeZPos);
    }
    public void UpdateStagesInfo()
    {
        foreach (GameObject stage in stages)
        {
            stage.GetComponent<Stage>().SetupStage();
        }
        CalcStagesPositions();
    }
    private void CalcStagesPositions()
    {
        float lastPlatformLength = 0f;
        for (int i = 0; i < stages.Count; i++)
        {
            Stage curStage = stages[i].GetComponent<Stage>();
            if (i == 0)
            {
                curStage.SetStagePosZ(0f);
                lastPlatformLength = ((curStage.PlatformLength) * 5f) + 5f;
            }
            else
            {
                curStage.SetStagePosZ(lastPlatformLength);
                float localStartPosZ = lastPlatformLength + ((curStage.PlatformLength) * 5f) + 5f;
                lastPlatformLength = localStartPosZ;
            }
        }
    }
    public Stage GetStage(int index)
    {
        return stages[index].gameObject.GetComponent<Stage>();
    }
}
