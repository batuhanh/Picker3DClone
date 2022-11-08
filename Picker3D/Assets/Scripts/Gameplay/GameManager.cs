using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static event Action gameStartedEvent;
    public static event Action gameFinishedEvent;

    public void StartGame()
    {
        gameStartedEvent?.Invoke();
    }
}
