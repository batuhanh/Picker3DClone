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
        nextLevelIndex = PlayerPrefs.GetInt("NextLevel", 0);

        if (currentLevelIndex < levelPrefabs.Length) //get both from levels
        {
            nextLevelIndex = currentLevelIndex + 1;
        }
        else if (currentLevelIndex == levelPrefabs.Length) //get next level random 
        {
            if (nextLevelIndex != -1)//-1 means we can pick random and assign it
            {
                nextLevelIndex = currentLevelIndex;
                while (currentLevelIndex == nextLevelIndex)
                {
                    nextLevelIndex = UnityEngine.Random.Range(0, levelPrefabs.Length);
                }
                PlayerPrefs.SetInt("NextLevel", nextLevelIndex);
            }
        }
        else // get both random
        {
            if (nextLevelIndex != -1)
            {

            }
        }

        
        GameObject currentLevel = Instantiate(levelPrefabs[currentLevelIndex], transform.position,
            Quaternion.identity, transform);

        levelLoadedEvent?.Invoke();
    }
    private float GetCurrentLevelLength()
    {
        return 0f;
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void ResetNextLevelIndex()
    {
        PlayerPrefs.SetInt("NextLevel", -1);
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
