using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public RectTransform m_rectTransform;
    public CanvasGroup m_canvasGroup;
    public GameObject m_highlight;
    public Image m_icon;

    public int m_id { get; private set; }
    public Sprite m_iconSprite { get; private set; }
    public Sprite m_bgSprite { get; private set; }

    public CardUpdater m_updater { get; private set; }

    public void Initialize(int id, Sprite icon, Sprite bgSprite, bool isValidCard)
    {
        m_updater = new CardUpdater(this);
        m_id = id;
        m_iconSprite = icon;
        m_bgSprite = bgSprite;
        if (isValidCard)
        {
            m_canvasGroup.alpha = 1;
            m_icon.sprite = m_bgSprite;

            m_updater.SwitchState(STATE_TYPE.CLOSE);
        }
        else
        {
            m_updater.SwitchState(STATE_TYPE.STALE);
        }
    }

    public void CustomUpdate()
    {
        m_updater.CustomUpdate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        m_updater.OnCardClick();
        //m_highlight.SetActive(!m_highlight.activeInHierarchy);
    }
}