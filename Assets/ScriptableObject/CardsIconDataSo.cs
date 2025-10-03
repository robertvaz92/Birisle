using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cards Icon Data", menuName = "ScriptableObjects/Cards Icon Data", order = 2)]

public class CardsIconDataSo : ScriptableObject
{
    public List<Sprite> m_icons;
}
