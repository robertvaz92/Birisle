using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CardsScoreManager
{
    private int m_scorePerMatch;
    private int m_streakCount = 0;
    private CardGridManager m_manager;

    public Action<int, int> OnScoreUpdate;

    private int m_totalCards;

    public int m_baseScore { get; private set; }
    public int m_timeBonus { get; private set; }
    public int m_attemptBonus { get; private set; }
    public int m_finalScore { get; private set; }


    public CardsScoreManager(CardGridManager manager)
    {
        m_manager = manager;
        m_scorePerMatch = 10;
        m_baseScore = 0;
        m_streakCount = 0;

        m_manager.OnCardMatchSuccess -= OnCardMatchSuccess;
        m_manager.OnCardMatchSuccess += OnCardMatchSuccess;

        m_manager.OnCardMatchFail -= OnCardMatchFail;
        m_manager.OnCardMatchFail += OnCardMatchFail;

        m_manager.OnGameOver -= OnGameOver;
        m_manager.OnGameOver += OnGameOver;
    }

    public void CleanUp()
    {
        m_manager.OnCardMatchSuccess -= OnCardMatchSuccess;
        m_manager.OnCardMatchFail -= OnCardMatchFail;
        m_manager.OnGameOver -= OnGameOver;
    }

    public void OnCardMatchSuccess(Card c1, Card c2)
    {
        m_streakCount += m_manager.m_cardIconData.StreakMultiplier;
        m_baseScore += m_streakCount * m_manager.m_cardIconData.BaseScorePerMatch;
        OnScoreUpdate?.Invoke(m_baseScore, m_streakCount);
    }

    public void OnCardMatchFail(Card c1, Card c2)
    {
        m_streakCount = 0;
        OnScoreUpdate?.Invoke(m_baseScore, m_streakCount);
    }

    private void OnGameOver()
    {
        m_totalCards = GameManager.Instance.m_rows * GameManager.Instance.m_columns;

        m_attemptBonus = CalculateBonus(m_totalCards, m_manager.m_totalFlipTries, m_manager.m_cardIconData.TriesBonusMultiplier);
        m_timeBonus = CalculateBonus(m_totalCards, m_manager.m_totalTimeTaken, m_manager.m_cardIconData.TimeBonusMultiplier);
        m_finalScore = m_baseScore + m_attemptBonus + m_timeBonus;
    }

    private int CalculateBonus(float count, float completedValue, float multiplier)
    {
        return (int)((count / completedValue) * multiplier);
    }
}
