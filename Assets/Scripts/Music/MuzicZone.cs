using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class MuzicZone : MonoBehaviour
{
    public AudioSource audioSource; // 이 Zone에서 재생할 AudioSource
    public float fadeTime;
    public float maxVolume;
    private float targetVolume;

    void Start()
    {
        targetVolume = 0f; // 초기 볼륨은 0으로 설정
        audioSource.volume = targetVolume;
        audioSource.Play(); // Zone에 들어가면 음악이 시작되도록 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Approximately(audioSource.volume, targetVolume)) // 현재 볼륨이 목표 볼륨과 같으면 아무것도 하지 않음
        { 
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, targetVolume, (maxVolume / fadeTime) * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 Zone에 들어오면
        {
            targetVolume = maxVolume; // 목표 볼륨을 최대 볼륨으로 설정
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어가 Zone을 나가면
        {
            targetVolume = 0f; // 목표 볼륨을 0으로 설정
        }
    }
}