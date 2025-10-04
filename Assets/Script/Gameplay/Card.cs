using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerClickHandler
{
    public RectTransform m_rectTransform;
    public RectTransform m_holder;
    public CanvasGroup m_canvasGroup;
    public GameObject m_highlight;
    public Image m_icon;

    public int m_id { get; private set; }
    public Sprite m_iconSprite { get; private set; }
    public Sprite m_bgSprite { get; private set; }

    public CardGridManager m_manager { get; private set; }
    public CardUpdater m_updater { get; private set; }

    public Card m_compareCard { get; private set; }
    public bool m_isMatched { get; private set; }


    public void Initialize(int id, CardGridManager manager)
    {
        m_manager = manager;
        m_updater = new CardUpdater(this);
        m_id = id;
        m_bgSprite = m_manager.m_cardIconData.m_bgIcon;

        if (m_id == -1)
        {
            m_updater.ForceSwitchState(STATE_TYPE.STALE);
        }
        else
        {
            m_iconSprite = m_manager.m_cardIconData.m_icons[m_id];
            m_canvasGroup.alpha = 1;
            m_icon.sprite = m_bgSprite;

            m_updater.ForceSwitchState(STATE_TYPE.CLOSE);
        }

        m_compareCard = null;
        m_isMatched = false;
        m_holder.localScale = Vector3.one;
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

    public void SetCompareCard(Card c)
    {
        m_compareCard = c;
    }

    public void SwitchToCloseState()
    {
        m_updater.SwitchState(STATE_TYPE.CLOSE);
    }

    public void SetMatchedFlag()
    {
        m_isMatched = true;
    }
}