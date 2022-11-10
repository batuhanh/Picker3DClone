using System.Collections;
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
            other.gameObject.tag = "Untagged";
            BallCollecterPlatform ballCollecterPlatform = other.gameObject.GetComponentInParent<BallCollecterPlatform>();
            ballCollecterPlatform.CheckCollecterStatus();
            other.gameObject.SetActive(false);
            hittedBallCollecterEvent?.Invoke();
        }
        if (other.gameObject.CompareTag("LevelEnd"))
        {
            other.gameObject.tag = "Untagged";
            hittedLevelEndEvent?.Invoke();
        }
        if (other.gameObject.CompareTag("Ball"))
        {
            SoundManager.Instance.PlayPopSound();
            other.gameObject.GetComponent<Ball>().SetStatus(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            other.gameObject.GetComponent<Ball>().SetStatus(false);
        }
    }
}
