using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollecterPhysicsCallbacks : MonoBehaviour
{
    [SerializeField] private BallCollecterPlatform ballCollecterPlatform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            
            other.gameObject.tag = "Untagged";
            ballCollecterPlatform.CollactNewBall(other.gameObject);
            SoundManager.Instance.PlayPopSound();
        }
    }
}
