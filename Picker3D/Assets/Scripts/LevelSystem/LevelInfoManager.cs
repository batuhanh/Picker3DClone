using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInfoManager : MonoBehaviour
{
    [SerializeField] private Transform stagesParent;
    [SerializeField] private List<GameObject> stages;
    [SerializeField] private GameObject stagePrefab;
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
            UpdateStagesInfo();
        }

    }
    public void UpdateStagesInfo()
    {

    }
    public Stage GetStage(int index)
    {
        Debug.Log(stages[index]);
        return stages[index].gameObject.GetComponent<Stage>();
    }
}
