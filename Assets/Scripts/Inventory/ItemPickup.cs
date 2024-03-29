using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour{
    public Item item;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) {
            Inventory playerInventory = other.GetComponent<Inventory>();
            if (playerInventory != null) {
                playerInventory.AddItem(item);
                Destroy(gameObject);
            }
        }
    }
}
