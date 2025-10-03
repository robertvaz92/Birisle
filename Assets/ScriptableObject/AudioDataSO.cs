using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Audio Data", menuName = "ScriptableObjects/Audio Data", order = 1)]
public class AudioDataSO : ScriptableObject
{
    public AudioClip m_bgMusic;

    public AudioClip m_matchSfx;
    public AudioClip m_mismatchSfx;
    public AudioClip m_cardFlipSfx;
    public AudioClip m_gameOverSfx;

}
