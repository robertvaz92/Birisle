using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameMenu : UIMenuBase
{
    public CardGridManager m_cardManager;

    public TextMeshProUGUI m_score;
    public TextMeshProUGUI m_streak;
    public TextMeshProUGUI m_remainingPairs;
    public TextMeshProUGUI m_totalFlipTries;

    public GameOverPanel m_gameOverPanel;
    public override void OnEnter()
    {
        base.OnEnter();
        m_gameOverPanel.gameObject.SetActive(false);
        m_cardManager.Initialize();
        OnScoreUpdate(0, 0);

        m_cardManager.m_scoreManager.OnScoreUpdate -= OnScoreUpdate;
        m_cardManager.m_scoreManager.OnScoreUpdate += OnScoreUpdate;

        m_cardManager.OnGameOver -= OnGameOver;
        m_cardManager.OnGameOver += OnGameOver;
    }

    public override void OnExit()
    {
        if (m_cardManager.m_scoreManager != null)
        {
            m_cardManager.m_scoreManager.OnScoreUpdate -= OnScoreUpdate;
        }

        m_cardManager.OnGameOver -= OnGameOver;

        m_cardManager.OnExit();
        base.OnExit();
    }

    public void OnScoreUpdate(int score, int streak)
    {
        m_score.text = score.ToString();
        m_streak.text = streak.ToString();
        m_remainingPairs.text = (m_cardManager.m_totalActiveCards / 2).ToString();
        m_totalFlipTries.text = m_cardManager.m_totalFlipTries.ToString();
    }

    public void OnGameOver()
    {
        m_cardManager.OnGameOver -= OnGameOver;
        StartCoroutine(DelayedGameOver());
    }

    IEnumerator DelayedGameOver()
    {
        yield return new WaitForSeconds(2);
        GameManager.Instance.m_audioManager.PlaySFX(GameManager.Instance.m_audioManager.m_data.m_gameOverSfx);
        m_gameOverPanel.gameObject.SetActive(true);
        m_gameOverPanel.Initialize(this);
    }

    public void OnClickClose()
    {
        GameManager.Instance.SwitchMenu(MenuScreenTypes.MAIN_MENU_SCREEN);
    }
}
