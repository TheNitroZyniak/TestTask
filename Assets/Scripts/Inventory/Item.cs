using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "ScriptableObjects/Item")]
public class Item : ScriptableObject {
    public string itemName;
    public Sprite icon;
}

[System.Serializable]
public class InventoryItem {
    public Item item;
    public int quantity;
}

[System.Serializable]
public class InventoryData {
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
}
