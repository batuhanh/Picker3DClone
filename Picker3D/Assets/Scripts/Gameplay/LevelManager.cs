using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelPrefabs;
    private int currentLevelIndex;
    private int nextLevelIndex;

    public static event Action levelLoadedEvent;
    private void Awake()
    {
        LoadCurrentLevel();
    }
    private void LoadCurrentLevel()
    {
        currentLevelIndex = PlayerPrefs.GetInt("Level", 0);
        nextLevelIndex = PlayerPrefs.GetInt("NextLevelIndex", 0);

        if (currentLevelIndex < levelPrefabs.Length - 1) //get both from levels
        {
            nextLevelIndex = currentLevelIndex + 1;
            PlayerPrefs.SetInt("NextLevelIndex", nextLevelIndex);
        }
        else if (currentLevelIndex == levelPrefabs.Length - 1) //get next level random 
        {
            if (nextLevelIndex != -1)//-1 means we can pick random and assign it
            {
                nextLevelIndex = currentLevelIndex;
                while (currentLevelIndex == nextLevelIndex)
                {
                    nextLevelIndex = UnityEngine.Random.Range(0, levelPrefabs.Length);
                }
                PlayerPrefs.SetInt("NextLevelIndex", nextLevelIndex);
            }
        }
        else // get current from last random and next from random
        {
            if (nextLevelIndex != -1)
            {
                int lastLevelIndex = PlayerPrefs.GetInt("NextLevelIndex", 0);
                currentLevelIndex = lastLevelIndex;

                nextLevelIndex = currentLevelIndex;
                while (currentLevelIndex == nextLevelIndex)
                {
                    nextLevelIndex = UnityEngine.Random.Range(0, levelPrefabs.Length);
                }
                PlayerPrefs.SetInt("CurrentLevelIndex", currentLevelIndex);
                PlayerPrefs.SetInt("NextLevelIndex", nextLevelIndex);
            }
        }


        GameObject currentLevel = Instantiate(levelPrefabs[currentLevelIndex], transform.position,
            Quaternion.identity, transform);

        Vector3 nextLevelSpawnPos = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + GetCurrentLevelLength(currentLevel));
        GameObject nextLevel = Instantiate(levelPrefabs[nextLevelIndex], nextLevelSpawnPos,
            Quaternion.identity, transform);

        levelLoadedEvent?.Invoke();
    }
    private float GetCurrentLevelLength(GameObject curLevelObj)
    {
        float curLevelLength = 0f;
        LevelInfoManager levelInfoManager = curLevelObj.gameObject.GetComponent<LevelInfoManager>();
        curLevelLength = levelInfoManager.GetLevelLength() + 20;
        return curLevelLength;
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void ResetNextLevelIndex()
    {
        PlayerPrefs.SetInt("NextLevelIndex", -1);
    }
    private void OnEnable()
    {
        GameManager.gameSuccessedEvent += ResetNextLevelIndex;
    }
    private void OnDisable()
    {
        GameManager.gameSuccessedEvent -= ResetNextLevelIndex;
    }
}
