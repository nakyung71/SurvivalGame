using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip swingClip;
    public AudioClip hitClip;

    public void Start()
    {
        audioSource = AudioManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
    }
    public void Swing()
    {
        audioSource.PlayOneShot(swingClip);
    }

    public void Hit()
    {
        audioSource.PlayOneShot(hitClip);
    }
}
