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
    public static LevelManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        //ResetNextLevelIndex();
        LoadCurrentLevel();
    }
    private void LoadCurrentLevel()
    {
        currentLevelIndex = PlayerPrefs.GetInt("Level", 0);
        nextLevelIndex = PlayerPrefs.GetInt("NextLevelIndex", 0);
        int isLevelsSelected = PlayerPrefs.GetInt("IsLevelsSelected", 1); //1 mean selected 0 mean not selected

        if (currentLevelIndex < levelPrefabs.Length - 1) //get both from levels
        {
            nextLevelIndex = currentLevelIndex + 1;
            PlayerPrefs.SetInt("NextLevelIndex", nextLevelIndex);
        }
        else if (currentLevelIndex == levelPrefabs.Length - 1) //get next level random 
        {
            if (isLevelsSelected == 0)//-1 means we can pick random and assign it
            {
                nextLevelIndex = currentLevelIndex;
                while (currentLevelIndex == nextLevelIndex)
                {
                    nextLevelIndex = UnityEngine.Random.Range(0, levelPrefabs.Length);
                }
                PlayerPrefs.SetInt("NextLevelIndex", nextLevelIndex);

                PlayerPrefs.SetInt("IsLevelsSelected", 1);
            }
        }
        else // get current from last random and next from random
        {
            if (isLevelsSelected == 0)
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

                PlayerPrefs.SetInt("IsLevelsSelected", 1);
            }
            else
            {

                currentLevelIndex = PlayerPrefs.GetInt("CurrentLevelIndex", 0);
                nextLevelIndex = PlayerPrefs.GetInt("NextLevelIndex", 0);
            }
        }

        //Debug.Log("Level: " + PlayerPrefs.GetInt("Level",0));
        //Debug.Log("Current Level Index: " + currentLevelIndex + " Next Level Index: " + nextLevelIndex);
        GameObject currentLevel = Instantiate(levelPrefabs[currentLevelIndex], transform.position,
            Quaternion.identity, transform);

        Vector3 nextLevelSpawnPos = new Vector3(transform.position.x, transform.position.y,
            transform.position.z + GetCurrentLevelLength(currentLevel));
        GameObject nextLevel = Instantiate(levelPrefabs[nextLevelIndex], nextLevelSpawnPos,
            Quaternion.identity, transform);

        levelLoadedEvent?.Invoke();
    }
    public float GetCurrentLevelLength(GameObject curLevelObj)
    {
        float curLevelLength = 0f;
        LevelInfoManager levelInfoManager = curLevelObj.gameObject.GetComponent<LevelInfoManager>();
        curLevelLength = levelInfoManager.GetLevelLength() + 20;
        return curLevelLength;
    }
    public GameObject GetCurrentLevel()
    {
        return transform.GetChild(0).gameObject;
    }
    public void ReloadLevel()
    {
        SceneManager.LoadScene(0);
    }
    private void ResetNextLevelIndex()
    {
        PlayerPrefs.SetInt("IsLevelsSelected", 0);
    }
    private void IncreaseLevel()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 0) + 1);
    }
    private void OnEnable()
    {
        GameManager.gameSuccessedEvent += ResetNextLevelIndex;
        GameManager.gameSuccessedEvent += IncreaseLevel;
    }
    private void OnDisable()
    {
        GameManager.gameSuccessedEvent -= ResetNextLevelIndex;
        GameManager.gameSuccessedEvent -= IncreaseLevel;
    }
}
