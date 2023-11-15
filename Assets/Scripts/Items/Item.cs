using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    None,
    Ball,
    Stone,
    Barrel,
    Box,
    Dynamit
}

public class Item : MonoBehaviour
{
    public ItemType ItemType;
}
