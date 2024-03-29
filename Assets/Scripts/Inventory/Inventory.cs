using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class Inventory : MonoBehaviour {

    public Action<Item> OnItemAdded, OnItemRemoved;
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private void Start() {
        LoadInventory();
        InventoryUI.Instance.Redraw();
    }

    public void AddItem(Item item) {
        InventoryItem existingItem = inventoryItems.Find(i => i.item == item);
        if (existingItem != null) {
            existingItem.quantity++;
        } else {
            inventoryItems.Add(new InventoryItem { item = item, quantity = 1 });
        }

        OnItemAdded?.Invoke(item);
        SaveInventory();
    }

    
    public void RemoveItem(Item item) {
        InventoryItem foundItem = inventoryItems.Find(i => i.item == item);
        if (foundItem != null) {
            foundItem.quantity--;
            if (foundItem.quantity <= 0) {
                inventoryItems.Remove(foundItem);
            }
            OnItemRemoved?.Invoke(item);
            SaveInventory();
        }
    }

    public void SaveInventory() {
        InventoryData data = new InventoryData { inventoryItems = inventoryItems };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
    }

    public void LoadInventory() {
        string filePath = Application.persistentDataPath + "/inventory.json";
        if (File.Exists(filePath)) {
            string json = File.ReadAllText(filePath);
            InventoryData data = JsonUtility.FromJson<InventoryData>(json);
            inventoryItems = data.inventoryItems;
        }
    }
}
