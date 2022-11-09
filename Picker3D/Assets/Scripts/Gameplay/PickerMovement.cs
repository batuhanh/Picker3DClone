using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using DG.Tweening;

public class PickerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody myRb;
    [SerializeField] private float horiztontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    private bool canMove = false;
    private bool canRun = false;
    private float horizontal;
    private Vector3 mousePosition;

    public static event Action movedToNextStartEvent;
    private void Update()
    {
        if (canMove)
        {
            //Horizontal Calc
            if (Input.GetMouseButtonDown(0))
            {
                mousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {

                horizontal = (Input.mousePosition.x - mousePosition.x) / Screen.width * 2.5f;
                mousePosition = Input.mousePosition;
            }
            else
            {
                horizontal = 0;
            }   
        }
    }
    private void FixedUpdate()
    {
        if (canMove)
        {
            //Vertical Calc
            float verticalActualSpeed = (verticalSpeed * Time.fixedDeltaTime);
            if (!canRun)
            {
                verticalActualSpeed = 0f;
            }

            //applying speeds to transform
            myRb.MovePosition(new Vector3(Mathf.Clamp(transform.position.x + (horizontal * horiztontalSpeed * Time.fixedDeltaTime), -1.5f, 1.5f),
                 transform.position.y, transform.position.z + verticalActualSpeed));
        }
    }
    private void EnableMovement()
    {
        canMove = true;
        canRun = true;
    }
    private void DisableMovement()
    {
        canMove = false;
        canRun = false;
    }
    private void DisableVerticalMovement()
    {
        canRun = false;
    }
    private void EnableVerticalMovement()
    {
        canRun = true;
    }
    private void MoveToNextLevelStartPos()
    {
        DisableMovement();
        float curLevellength = LevelManager.Instance.GetCurrentLevelLength(LevelManager.Instance.GetCurrentLevel());
        Vector3 targetPos = new Vector3(0, transform.position.y, curLevellength-10f);
        transform.DOMove(targetPos, 2f).OnComplete(() =>
        {
            movedToNextStartEvent?.Invoke();
        });
    }
    private void OnEnable()
    {
        GameManager.gameStartedEvent += EnableMovement;
        GameManager.gameFinishedEvent += DisableMovement;
        PickerPhysicsCallbacks.hittedBallCollecterEvent += DisableVerticalMovement;
        PickerPhysicsCallbacks.hittedLevelEndEvent += MoveToNextLevelStartPos;
        BallCollecterPlatform.collecterSuccessEvent += EnableVerticalMovement;
    }
    private void OnDisable()
    {
        GameManager.gameStartedEvent -= EnableMovement;
        GameManager.gameFinishedEvent -= DisableMovement;
        PickerPhysicsCallbacks.hittedBallCollecterEvent -= DisableVerticalMovement;
        PickerPhysicsCallbacks.hittedLevelEndEvent -= MoveToNextLevelStartPos;
        BallCollecterPlatform.collecterSuccessEvent -= EnableVerticalMovement;
    }
}
