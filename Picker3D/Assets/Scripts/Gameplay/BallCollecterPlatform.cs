using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class BallCollecterPlatform : MonoBehaviour
{
    [SerializeField] private TMP_Text collecedStatusText;
    [SerializeField] private Renderer upperCubeRenderer;
    [SerializeField] private Animator myAnim;
    private List<GameObject> collectedBalls = new List<GameObject>();
    private int collectedCount = 0;
    private int collectLimit = 0;
    public static event Action collecterSuccessEvent;
    public static event Action collecterFailedEvent;

    public void CollactNewBall(GameObject ballObj)
    {
        collectedBalls.Add(ballObj);
        collectedCount++;
        SetCollectedText(collectedCount.ToString() + " / " + collectLimit.ToString());
    }
    public void SetCollectLimit(int newLimit)
    {
        collectLimit = newLimit;
    }
    public void SetCollectedText(string text)
    {
        collecedStatusText.text = text;
    }
    public void SetPosition(float lengthOfRoad)
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y,
             ((lengthOfRoad) * 5f));
    }
    public void SetUpperCubeColor(Color newColor)
    {
        Material newMat = upperCubeRenderer.material;
        newMat.color = newColor;
        upperCubeRenderer.material = newMat;
    }
    public void CheckCollecterStatus()
    {
        StartCoroutine(AnimDelayCaroutine());
    }
    private IEnumerator AnimDelayCaroutine()
    {
        yield return new WaitForSeconds(2f);
        foreach (var b in collectedBalls)
        {
            b.gameObject.GetComponent<Ball>().Explode(upperCubeRenderer.material);
        }
        if (collectedCount >= collectLimit)
        {
            //pass thru
            yield return new WaitForSeconds(1f);
            myAnim.SetTrigger("Close");
            yield return new WaitForSeconds(2f);
            collecterSuccessEvent?.Invoke();
        }
        else
        {
            //fail
            collecterFailedEvent?.Invoke();
        }
    }

}
