using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource bgmSource;

    private void start()
    {
        // 싱글턴 처리
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // AudioSource 가져오기
        bgmSource = GetComponent<AudioSource>();
        if (bgmSource == null)
        {
            Debug.LogWarning("[AudioManager] AudioSource가 없습니다.");
        }
    }

    //// 음악 켜기/끄기 토글
    //public void ToggleBGM()
    //{
    //    if (bgmSource == null) return;

    //    if (bgmSource.isPlaying)
    //    {
    //        bgmSource.Pause(); // Stop()이 아니라 Pause() 쓰면 다시 켰을 때 이어서 재생
    //    }
    //    else
    //    {
    //        bgmSource.Play();
    //    }
    //}

    //// 음악 강제로 끄기
    //public void StopBGM()
    //{
    //    if (bgmSource != null)
    //        bgmSource.Stop();
    //}

    //// 음악 강제로 켜기
    //public void PlayBGM()
    //{
    //    if (bgmSource != null && !bgmSource.isPlaying)
    //        bgmSource.Play();
    //}
}