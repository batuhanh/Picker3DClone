using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelCreatorObject : MonoBehaviour
{
    [SerializeField] private GameObject exampleLvlPrefab;
    [SerializeField] private Transform levelsParent;
    private string levelsPath;

    public void CreateNewLevel()
    {
        levelsPath = "Assets/Prefabs/Levels/SavedLevels/Level" + (levelsParent.childCount + 1).ToString() + ".prefab";

        GameObject newLevel = Instantiate(exampleLvlPrefab, levelsParent.transform.position
            , Quaternion.identity, levelsParent);
        newLevel.name = "Level" + (levelsParent.childCount).ToString();

        PrefabUtility.SaveAsPrefabAssetAndConnect(newLevel, levelsPath,InteractionMode.AutomatedAction);
    }
}
