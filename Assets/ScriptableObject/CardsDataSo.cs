using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards Data", menuName = "ScriptableObjects/Cards Data", order = 2)]

public class CardsDataSo : ScriptableObject
{
    [Header("Card Parameters")]
    public float m_autoCloseDuration = 3f;
    public float m_flipDuration = 0.15f;

    [Header("Score Values")]
    public int TimeBonusMultiplier = 100;
    public int TriesBonusMultiplier = 100;
    public int StreakMultiplier = 10;
    public int BaseScorePerMatch = 10;

    [Header("Card Icons")]
    public Sprite m_bgIcon;
    public List<Sprite> m_icons;
}
