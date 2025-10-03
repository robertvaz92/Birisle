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

    private bool m_interactable;
    public void Initialize(int id, Sprite icon)
    {
        m_id = id;
        m_iconSprite = icon;
        m_canvasGroup.alpha = 1;
        m_interactable = true;

        m_icon.sprite = m_iconSprite;
    }

    public void Initialize(bool nonInteractable)
    {
        m_canvasGroup.alpha = 0;
        m_interactable = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_interactable)
        {
            m_highlight.SetActive(!m_highlight.activeInHierarchy);
        }
    }


}
