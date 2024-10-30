using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Helper,
    Consumable
}

public enum ConsumableType
{
    Speed,
    Health
}

[System.Serializable]
public class ItemDataCounsumable
{
    public ConsumableType type;
    public float value;
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public ItemType itemType;

    [Header("Consumable")] 
    public ItemDataCounsumable[] consumables;
}
