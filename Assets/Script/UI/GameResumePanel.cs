using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class GameResumePanel : MonoBehaviour
{
    public RectTransform m_content;

    public TextMeshProUGUI m_grid;
    public TextMeshProUGUI m_score;
    public TextMeshProUGUI m_progress;

    private MainMenu m_mainMenu;

    public void Initialize(MainMenu mainMenu)
    {
        m_mainMenu = mainMenu;
        m_content.localScale = Vector3.zero;
        m_content.DOScale(1, 1).SetEase(Ease.OutBounce);

        m_grid.text = $"{GameManager.Instance.m_loadedGame.Rows} x {GameManager.Instance.m_loadedGame.Columns}";
        m_score.text = GameManager.Instance.m_loadedGame.Score.ToString();
        m_progress.text = $"{GameManager.Instance.m_loadedGame.Progress} %";
    }

    public void OnClickClose()
    {
        gameObject.SetActive(false);
    }

    public void OnClickContinue()
    {
        m_mainMenu.OnClickConfirm();
        OnClickClose();
    }
}