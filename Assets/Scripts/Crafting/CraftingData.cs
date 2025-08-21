using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="CraftingData", menuName ="CraftingSO")]
public class CraftingData : ScriptableObject
{
    public NeededItem[] neededItems;
}


[System.Serializable]
public class NeededItem
{
    public ItemData itemData;
    public int neededQuantity;
}

