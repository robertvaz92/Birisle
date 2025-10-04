using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    public RectTransform m_content;

    public TextMeshProUGUI m_score;
    public TextMeshProUGUI m_attemptBonus;
    public TextMeshProUGUI m_timeBonus;
    public TextMeshProUGUI m_finalScore;

    private GameMenu m_gameMenu;

    public void Initialize(GameMenu gameMenu)
    {
        m_gameMenu = gameMenu;
        m_content.localScale = Vector3.zero;
        m_content.DOScale(1, 1).SetEase(Ease.OutBounce);

        m_score.text = gameMenu.m_cardManager.m_scoreManager.m_baseScore.ToString();
        m_attemptBonus.text = gameMenu.m_cardManager.m_scoreManager.m_attemptBonus.ToString();
        m_timeBonus.text = gameMenu.m_cardManager.m_scoreManager.m_timeBonus.ToString();
        m_finalScore.text = gameMenu.m_cardManager.m_scoreManager.m_finalScore.ToString();
    }

    public void OnClickContiue()
    {
        m_gameMenu.OnClickClose();
    }
}
