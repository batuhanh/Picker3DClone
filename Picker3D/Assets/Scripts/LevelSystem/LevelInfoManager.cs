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
    [SerializeField] private Renderer endCubeRenderer;
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
                //unpack prefab and delete prefab from folder
                string prefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromOriginalSource(gameObject));
                PrefabUtility.UnpackPrefabInstance(gameObject, PrefabUnpackMode.OutermostRoot, InteractionMode.AutomatedAction);
                FileUtil.DeleteFileOrDirectory(prefabPath);

                for (int i = 0; i < stageCount - newStageCount; i++)
                {
                    GameObject stageWillRemove = stages[stages.Count - 1];
                    stages.Remove(stageWillRemove);
                    DestroyImmediate(stageWillRemove);
                }
                //create new prefab with same level name to prefab folders
                string levelsPath = "Assets/Prefabs/Levels/SavedLevels/" + gameObject.name + ".prefab";
                PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, levelsPath, InteractionMode.AutomatedAction);
                AssetDatabase.Refresh();
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
        startCubeRenderer.material.color = stages[0].gameObject.GetComponent<Stage>().PlatformColor;
        endCubeRenderer.material.color = stages[stages.Count-1].gameObject.GetComponent<Stage>().PlatformColor;
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
