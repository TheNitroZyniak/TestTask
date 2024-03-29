using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InventoryUIItem : MonoBehaviour{
    public Action<string> OnItemSelected;

    [SerializeField] List<GameObject> images;
    [SerializeField] TextMeshProUGUI amountOfItems;
    [SerializeField] private Dictionary<string, GameObject> itemImagesMap;
    InventoryItem inventoryI;

    private string itemName;

    private void Awake() {
        itemImagesMap = new Dictionary<string, GameObject>();
        foreach (var image in images) 
            itemImagesMap[image.name] = image;     
    }

    public void ActivateImage(InventoryItem inventoryItem) {
        string itemName = inventoryItem.item.itemName;
        int quantity = inventoryItem.quantity;

        foreach (var image in images) {
            image.SetActive(image.name == itemName); 
        }
        amountOfItems.text = quantity > 1 ? quantity.ToString() : "";
        this.itemName = itemName;
        inventoryI = inventoryItem;
    }

    public void DeactivateImage() {
        foreach (var image in images) 
            image.SetActive(false);
    }

    public void OnClick() {
        if (inventoryI != null) {
            OnItemSelected?.Invoke(itemName);
            InventoryUI.Instance.SetSelectedItem(inventoryI);
        }
    }

    public void Clear() {
        inventoryI = null;
        foreach (var image in images) image.SetActive(false);
        amountOfItems.text = "";
    }
}
