using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class CardGridManager : MonoBehaviour
{
    public CardsDataSo m_cardIconData;
    public RectTransform m_container;
    public Card m_cardPrefab;
    
    public Vector2 m_spacing = new Vector2(50, 50);
    public Vector2 m_padding = new Vector2(20, 20);

    public int m_initialCardPoolSize = 50;

    public Card[] m_cards;

    private ObjectPool<Card> m_cardPool;
    private int m_rows;
    private int m_columns;
    private bool m_isInitialized = false;
    public int m_totalActiveCards { get; private set; }
    public int m_totalFlipTries { get; private set; }
    public int m_totalTimeTaken { get; private set; }
    public int m_totalMatches { get; private set; }


    public CardsScoreManager m_scoreManager { get; private set; }

    public Action<Card> OnCardSelect;
    public Action<Card, Card> OnCardMatchSuccess;
    public Action<Card, Card> OnCardMatchFail;
    public Action OnGameOver;

    private DateTime m_startTime;
    public void Initialize()
    {
        m_scoreManager = new CardsScoreManager(this);
        m_totalActiveCards = 0;
        m_totalFlipTries = 0;
        m_totalMatches = 0;
        if (m_cardPool == null)
        {
            m_cardPool = new ObjectPool<Card>(m_cardPrefab, m_initialCardPoolSize, m_container, "Card");
        }

        m_rows = GameManager.Instance.m_rows;
        m_columns = GameManager.Instance.m_columns;
        CreateCardsWithPairs();
        DisplayCards();
        m_startTime = DateTime.Now;
        m_isInitialized = true;
    }

    public void OnExit()
    {
        for (int i = 0; i < m_cards.Length; i++)
        {
            ReturnObject(m_cards[i]);
        }
        m_scoreManager?.CleanUp();
        m_isInitialized = false;
    }

    private void CreateCardsWithPairs()
    {
        m_totalActiveCards = m_rows * m_columns;
        m_cards = new Card[m_totalActiveCards];

        bool isOdd = m_cards.Length % 2 != 0;
        int cardLen = m_cards.Length;
        if (isOdd)
        {
            cardLen = cardLen - 1;

            m_cards[cardLen] = GetNewCard();
            m_cards[cardLen].Initialize(-1, this);
            m_totalActiveCards--;
        }

        for (int i = 0, j = 0; i < cardLen; i += 2, j++)
        {
            int id = j % m_cardIconData.m_icons.Count;
            Sprite sprite = m_cardIconData.m_icons[id];

            m_cards[i] = GetNewCard();
            m_cards[i].Initialize(id, this);

            m_cards[i + 1] = GetNewCard();
            m_cards[i + 1].Initialize(id, this);
        }

        //Shuffling
        for (int i = m_cards.Length - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);
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

    public void OnMachSuccess(Card c1, Card c2)
    {
        m_totalFlipTries++;
        m_totalMatches++;
        m_totalActiveCards -= 2;
        OnCardMatchSuccess?.Invoke(c1, c2);

        if (m_totalActiveCards <= 0)
        {
            OnAllMatchesFinished();
        }
    }

    public void OnMatchFailed(Card c1, Card c2)
    {
        m_totalFlipTries++;
        OnCardMatchFail?.Invoke(c1, c2);
    }

    public void OnAllMatchesFinished()
    {
        DateTime endTime = DateTime.Now;
        m_totalTimeTaken = (int)(endTime - m_startTime).TotalSeconds;
        OnGameOver?.Invoke();
    }

    private void Update()
    {
        if (m_isInitialized)
        {
            for (int i = 0; i < m_cards.Length; i++)
            {
                m_cards[i].CustomUpdate();
            }
        }
    }
}