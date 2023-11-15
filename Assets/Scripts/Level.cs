using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Task
{
    public ItemType itemType;
    public int number;
}

public class Level : MonoBehaviour
{
    public Task[] tasks; 
}
