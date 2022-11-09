using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action gameStartedEvent;
    public static event Action gameFinishedEvent;
    public static event Action gameSuccessedEvent;
    public static event Action gameFailedEvent;

    public void StartGame()
    {
        gameStartedEvent?.Invoke();
    }
    public void GameSuccessed()
    {
        
        gameSuccessedEvent?.Invoke();
        gameFinishedEvent?.Invoke();  
    }
    public void GameFailed()
    {

        gameFailedEvent?.Invoke();
        gameFinishedEvent?.Invoke();
    }
    private void OnEnable()
    {
        BallCollecterPlatform.collecterFailedEvent += GameFailed;
        PickerMovement.movedToNextStartEvent += GameSuccessed;
    }
    private void OnDisable()
    {
        BallCollecterPlatform.collecterFailedEvent -= GameFailed;
        PickerMovement.movedToNextStartEvent -= GameSuccessed;
    }
}
