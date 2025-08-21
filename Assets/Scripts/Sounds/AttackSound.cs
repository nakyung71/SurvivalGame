using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip swingClip;
    public AudioClip woodHitClip;
    public AudioClip animalHitClip;
    public AudioClip stoneHitClip;

    public void Start()
    {
        audioSource = AudioManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
    }
    public void Swing()
    {
        audioSource.PlayOneShot(swingClip);
    }

    public void WoodHit()
    {
        audioSource.PlayOneShot(woodHitClip);
    }

    public void AnimalHit()
    {
        audioSource.PlayOneShot(animalHitClip);
    }

    public void StoneHit()
    {
        audioSource.PlayOneShot(stoneHitClip);
    }
}
