﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickerPhysicsCallbacks : MonoBehaviour
{
    public static event Action hittedBallCollecterEvent;
    public static event Action hittedLevelEndEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BallCollecter"))
        {
            BallCollecterPlatform ballCollecterPlatform = other.gameObject.GetComponentInParent<BallCollecterPlatform>();
            ballCollecterPlatform.CheckCollecterStatus();
            other.gameObject.SetActive(false);
            hittedBallCollecterEvent?.Invoke();
        }
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            hittedLevelEndEvent?.Invoke();
        }
    }
}
