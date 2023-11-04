using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    // Outlets
    AudioSource audioSource;
    public AudioClip missSound;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip levelUpSound;
    public AudioClip hurtSound;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundHit()
    {
        audioSource.PlayOneShot(hitSound);
    }

    public void PlaySoundMiss()
    {
        audioSource.PlayOneShot(missSound);
    }
    
    public void PlaySoundDie()
    {
        audioSource.PlayOneShot(dieSound);
    }

    public void PlaySoundLevelUp()
    {
        audioSource.PlayOneShot(levelUpSound);
    }

    public void PlaySoundhurt()
    {
        audioSource.PlayOneShot(hurtSound);
    }
}
