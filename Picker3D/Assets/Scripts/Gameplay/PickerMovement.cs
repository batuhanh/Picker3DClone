using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerMovement : MonoBehaviour
{
    [SerializeField] private float horiztontalSpeed = 10f;
    [SerializeField] private float verticalSpeed = 10f;
    private bool canMove = false; 
    private bool canRun = false; 
    private float horizontal;
    private Vector3 mousePosition;

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

            //Vertical Calc
            float verticalActualSpeed = (verticalSpeed * Time.deltaTime);
            if (!canRun)
            {
                verticalActualSpeed = 0f;
            }

            //applying speeds to transform
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + (horizontal * horiztontalSpeed * Time.deltaTime), -3f, 3f),
                transform.position.y, transform.position.z + verticalActualSpeed);
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
    private void OnEnable()
    {
        GameManager.gameStartedEvent += EnableMovement;
        GameManager.gameFinishedEvent += DisableMovement;
    }
    private void OnDisable()
    {
        GameManager.gameStartedEvent -= EnableMovement;
        GameManager.gameFinishedEvent -= DisableMovement;
    }
}
