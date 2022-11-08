using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameEndMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject failMenu;

    private void OpenGameUI()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(true);
    }
    private void CloseGameUI()
    {
        gameMenu.SetActive(false);
        gameEndMenu.SetActive(true);
    }
    private void OnEnable()
    {
        GameManager.gameStartedEvent += OpenGameUI;
        GameManager.gameFinishedEvent += CloseGameUI;
    }
    private void OnDisable()
    {
        GameManager.gameStartedEvent -= OpenGameUI;
        GameManager.gameFinishedEvent -= CloseGameUI;
    }
}
