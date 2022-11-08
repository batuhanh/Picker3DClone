using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class BallCollecterPlatform : MonoBehaviour
{
    [SerializeField] private TMP_Text collecedStatusText;
    [SerializeField] private Renderer upperCubeRenderer;
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
}
