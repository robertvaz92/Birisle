using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioDataSO m_data;

    [Header("Audio Sources")]
    public AudioSource m_bgmSource;
    public AudioSource m_sfxSource;

    private bool m_isInitialized = false;
    public void Initialize()
    {
        if (m_bgmSource == null)
            m_bgmSource = gameObject.AddComponent<AudioSource>();
        if (m_sfxSource == null)
            m_sfxSource = gameObject.AddComponent<AudioSource>();

        m_isInitialized = true;
    }

    public void PlayBGM(float volume = 1f)
    {
        if (!m_isInitialized)
        {
            return;
        }

        if (m_bgmSource.clip == m_data.m_bgMusic)
        {
            return;
        }
        m_bgmSource.clip = m_data.m_bgMusic;
        m_bgmSource.volume = volume;
        m_bgmSource.Play();
    }

    public void StopBGM()
    {
        if (!m_isInitialized)
        {
            return;
        }

        m_bgmSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (!m_isInitialized)
        {
            return;
        }

        m_sfxSource.PlayOneShot(clip, volume);
    }
}