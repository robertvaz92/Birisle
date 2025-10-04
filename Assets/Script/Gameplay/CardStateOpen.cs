using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateOpen : StateBase
{
    CardUpdater m_updater;
    public CardStateOpen(CardUpdater updater)
    {
        m_updater = updater;
    }
}
