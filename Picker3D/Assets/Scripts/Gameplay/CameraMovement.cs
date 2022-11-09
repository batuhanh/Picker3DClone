using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float followSpeed = 2f;
    private float zOffset = 0f;
    private float targetZPos = 0f;
    private bool isOffsetCalculated = false;
    private bool canFollow = false;

    private void LateUpdate()
    {
        if (canFollow)
        {
           targetZPos = Mathf.Lerp(targetZPos, targetTransform.position.z + zOffset, Time.deltaTime * followSpeed);
            transform.position = new Vector3(transform.position.x, transform.position.y,
                targetZPos);
        }
    }
    private void CalculateZOffset()
    {
        zOffset = transform.position.z - targetTransform.position.z;
        targetZPos = transform.position.z;
    }
    private void StartFollowing()
    {
        if (!isOffsetCalculated)
        {
            CalculateZOffset();
            isOffsetCalculated = true;
        }
        canFollow = true;
    }
    private void StopFollowing()
    {
        canFollow = false;
    }
    private void OnEnable()
    {
        GameManager.gameStartedEvent += StartFollowing;
        GameManager.gameFinishedEvent += StopFollowing;
    }
    private void OnDisable()
    {
        GameManager.gameStartedEvent -= StartFollowing;
        GameManager.gameFinishedEvent -= StopFollowing;
    }
}
