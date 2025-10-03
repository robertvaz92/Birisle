using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGridManager : MonoBehaviour
{
    public CardsIconDataSo m_cardIconData;
    public RectTransform m_container;
    public Card m_cardPrefab;
    
    public Vector2 m_spacing = new Vector2(50, 50);
    public Vector2 m_padding = new Vector2(20, 20);

    public int m_initialCardPoolSize = 50;

    public Card[] m_cards;

    private ObjectPool<Card> m_cardPool;
    private int m_rows;
    private int m_columns;


    public void Initialize()
    {
        if (m_cardPool == null)
        {
            m_cardPool = new ObjectPool<Card>(m_cardPrefab, m_initialCardPoolSize, m_container, "Card");
        }

        m_rows = GameManager.Instance.m_rows;
        m_columns = GameManager.Instance.m_columns;
        CreateCardsWithPairs();
        DisplayCards();
    }

    public void OnExit()
    {
        for (int i = 0; i < m_cards.Length; i++)
        {
            ReturnObject(m_cards[i]);
        }
    }

    private void CreateCardsWithPairs()
    {
        m_cards = new Card[m_rows * m_columns];

        bool isOdd = m_cards.Length % 2 != 0;
        int cardLen = m_cards.Length;
        if (isOdd)
        {
            cardLen = cardLen - 1;

            m_cards[cardLen] = GetNewCard();
            m_cards[cardLen].Initialize(false);
        }

        for (int i = 0, j = 0; i < cardLen; i += 2, j++)
        {
            int id = j % m_cardIconData.m_icons.Count;
            Sprite sprite = m_cardIconData.m_icons[id];

            m_cards[i] = GetNewCard();
            m_cards[i].Initialize(id, sprite);

            m_cards[i + 1] = GetNewCard();
            m_cards[i + 1].Initialize(id, sprite);
        }

        //Shuffling
        for (int i = m_cards.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            // Swap
            Card temp = m_cards[i];
            m_cards[i] = m_cards[j];
            m_cards[j] = temp;
        }
    }

    private void DisplayCards()
    {
        Vector3[] containerCorners = new Vector3[4];

        m_container.GetLocalCorners(containerCorners);

        float containerWidth = m_container.rect.width - (m_spacing.x * (m_columns - 1)) - (m_padding.x * 2);
        float containerHeight = m_container.rect.height - (m_spacing.y * (m_rows - 1)) - (m_padding.y * 2);

        float cardWidth = containerWidth / m_columns;
        float cardHeight = containerHeight / m_rows;


        for (int i = 0; i < m_cards.Length; i++)
        {
            RectTransform cardRect = m_cards[i].m_rectTransform;
            int row = i / m_columns;
            int col = i % m_columns;

            float x = m_padding.x + col * (cardWidth + m_spacing.x) + containerCorners[0].x;
            float y = m_padding.y + row * (cardHeight + m_spacing.y) + containerCorners[0].y;

            cardRect.sizeDelta = new Vector2(cardWidth, cardHeight);
            cardRect.anchoredPosition = new Vector2(x, y);
        }
    }

    private Card GetNewCard()
    {
        Card card = m_cardPool.Get();
        //Make any initialisation here if needed
        return card;
    }


   



    void ReturnObject(Card card)
    {
        m_cardPool.ReturnToPool(card);
    }
}