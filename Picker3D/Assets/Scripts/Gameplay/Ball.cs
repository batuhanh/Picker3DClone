using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private GameObject explosionEfectPrefab;
    [SerializeField] private Renderer myRenderer;
    public void Explode(Material platformMat)
    {
        GameObject spawnedEffectObj = Instantiate(explosionEfectPrefab, transform.position, Quaternion.identity);
        ParticleSystemRenderer psr = spawnedEffectObj.GetComponent<ParticleSystemRenderer>();
        psr.material = platformMat;
        spawnedEffectObj.GetComponent<ParticleSystem>().Play();
        Destroy(gameObject);
    }
}
