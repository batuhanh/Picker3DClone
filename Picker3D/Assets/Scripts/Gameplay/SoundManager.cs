using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource pop1AS;
    [SerializeField] private AudioSource pop2AS;
    [SerializeField] private AudioSource pop3AS;
    public static SoundManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void PlayPopSound()
    {
        if (!pop1AS.isPlaying)
        {
            pop1AS.Play();
        }
        else if (!pop2AS.isPlaying)
        {
            pop2AS.Play();
        }
        else
        {
            pop3AS.Stop();
            pop3AS.Play();
        }
    }
}
