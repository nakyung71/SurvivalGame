using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip catNPC;
    public AudioClip horseNPC;
    public AudioClip sheepNPC;

    public void Start()
    {
        audioSource = AudioManager.Instance.transform.GetChild(0).GetComponent<AudioSource>();
    }

    public void Cat()
    {
        audioSource.PlayOneShot(catNPC);
    }

    public void Horse()
    {
        audioSource.PlayOneShot(horseNPC);
    }

    public void Sheep()
    {
        audioSource.PlayOneShot(sheepNPC);
    }
}
