using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData
{
    public ItemType itemType;
    public Sprite sprite;
}

[CreateAssetMenu]
public class ItemIcons : ScriptableObject
{
    public ItemData[] itemDatas;

    public Sprite GetSprite(ItemType itemType)
    {
        for (int i = 0; i < itemDatas.Length; i++)
        {
            if (itemDatas[i].itemType == itemType)
            {
                return itemDatas[i].sprite;
            }
        }
        return null;
    }
}
