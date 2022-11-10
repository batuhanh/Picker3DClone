using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameEndMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject failMenu;
    [SerializeField] private Text levelText;

    private void OpenGameUI()
    {
        UpdateLevelText();
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
    }
    private void CloseGameUI()
    {
        gameMenu.SetActive(false);
        gameEndMenu.SetActive(true);
    }
    private void OpenFailUI()
    {
        gameEndMenu.SetActive(true);
        failMenu.SetActive(true);
    }
    private void OpenWinUI()
    {
        gameEndMenu.SetActive(true);
        winMenu.SetActive(true);
    }
    private void UpdateLevelText()
    {
        levelText.text = "Level " + (PlayerPrefs.GetInt("Level",0)+1).ToString();
    }
    private void OnEnable()
    {
        GameManager.gameStartedEvent += OpenGameUI;
        GameManager.gameFinishedEvent += CloseGameUI;
        GameManager.gameFailedEvent += OpenFailUI;
        GameManager.gameSuccessedEvent += OpenWinUI;
    }
    private void OnDisable()
    {
        GameManager.gameStartedEvent -= OpenGameUI;
        GameManager.gameFinishedEvent -= CloseGameUI;
        GameManager.gameFailedEvent -= OpenFailUI;
        GameManager.gameSuccessedEvent -= OpenWinUI;
    }
}
