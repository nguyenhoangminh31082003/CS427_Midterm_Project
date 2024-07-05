using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public static KeyManager Instance;

    public List<KeyItem> _inventoryItems = new List<KeyItem>();
    public enum KeyItem {
        SilverKey,
        GoldKey
    }
    void Awake() {
        Instance = this;
    }

    public void AddItem(KeyItem item) {
        if (!_inventoryItems.Contains(item)) {
            _inventoryItems.Add(item);
        }
    }

    public void RemoveItem(KeyItem item) {
        if (_inventoryItems.Contains(item)) {
            _inventoryItems.Remove(item);
        }
    }

    public bool CheckRequiredItem(KeyItem item) {
        return _inventoryItems.Contains(item);
    }

    public List<KeyItem> GetInventoryKey() {
        return _inventoryItems;
    }
}
