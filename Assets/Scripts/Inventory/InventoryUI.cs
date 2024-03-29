using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour{
    public static InventoryUI Instance { get; private set; }

    [SerializeField] Inventory targetInventory;
    [SerializeField] List<InventoryUIItem> allItems;
    [SerializeField] private Image currentItemDisplay;
    [SerializeField] GameObject removeButton;

    private Dictionary<string, Image> itemImagesMap;
    private InventoryItem selectedItem;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        } 
        else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        targetInventory.OnItemAdded += OnItemAdded;
        targetInventory.OnItemRemoved += OnItemRemoved;

        foreach (var itemUI in allItems) itemUI.OnItemSelected += UpdateCurrentItemDisplay;

        itemImagesMap = new Dictionary<string, Image>();
        foreach (Transform child in currentItemDisplay.transform) {
            Image image = child.GetComponent<Image>();
            if (image != null) {
                itemImagesMap[child.gameObject.name] = image;
                image.gameObject.SetActive(false);
            }
        }
    }

    void OnItemAdded(Item i) => Redraw();
    void OnItemRemoved(Item i) => Redraw();

    public void Redraw() {
        removeButton.SetActive(false);
        foreach (var itemUI in allItems) 
            itemUI.Clear();

        foreach (Transform child in currentItemDisplay.transform)
            child.gameObject.SetActive(false);

        for (var i = 0; i < targetInventory.inventoryItems.Count; i++) {
            InventoryItem inventoryItem = targetInventory.inventoryItems[i];
            allItems[i].ActivateImage(inventoryItem);
        }
    }


    private void UpdateCurrentItemDisplay(string itemName) {
        foreach (var image in itemImagesMap.Values) 
            image.gameObject.SetActive(false);
        
        if (itemImagesMap.TryGetValue(itemName, out Image selectedImage)) 
            selectedImage.gameObject.SetActive(true);               
    }

    public void SetSelectedItem(InventoryItem item) {
        removeButton.SetActive(true);
        removeButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = item.item.itemName;
        selectedItem = item;
    }

    public void OnRemoveButtonClick() {
        if (selectedItem != null) 
            targetInventory.RemoveItem(selectedItem.item);      
    }

    private void OnDestroy() {
        targetInventory.OnItemAdded -= OnItemAdded;
        targetInventory.OnItemRemoved -= OnItemRemoved;
    }
}
