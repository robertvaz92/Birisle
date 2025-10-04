using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SavedData 
{
    public int Rows;
    public int Columns;
    public int Score;
    public int AttemptCount;
    public int Progress;

    public List<SavedCardInfo> CardIds;
}


[Serializable]
public class SavedCardInfo
{
    public int Id;
    public bool IsActive;
}