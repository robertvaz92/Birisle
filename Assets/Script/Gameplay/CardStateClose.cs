using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardStateClose : StateBase
{
    CardUpdater m_updater;
    public CardStateClose(CardUpdater updater)
    {
        m_updater = updater;
    }
}
