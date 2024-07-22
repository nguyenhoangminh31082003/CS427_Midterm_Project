using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheKeyManager : MonoBehaviour
{
    public static TheKeyManager Instance;

    public Dictionary<KeyItem, int> _inventoryItems = new Dictionary<KeyItem, int>();
    public enum KeyItem {
        SilverKey,
        GoldKey
    }
    void Awake() {
        Instance = this;
    }

    public void AddItem(KeyItem item)
    {
        if (_inventoryItems.ContainsKey(item))
        {
            _inventoryItems[item]++;
        }
        else
        {
            _inventoryItems[item] = 1;
        }
    }

    public void RemoveItem(KeyItem item)
    {
        if (_inventoryItems.ContainsKey(item))
        {
            _inventoryItems[item]--;
            if (_inventoryItems[item] <= 0)
            {
                _inventoryItems.Remove(item);
            }
        }
    }

    public bool CheckRequiredItem(KeyItem item)
    {
        return _inventoryItems.ContainsKey(item) && _inventoryItems[item] > 0;
    }

    public List<KeyItem> GetInventoryKey()
    {
        return new List<KeyItem>(_inventoryItems.Keys);
    }

    public int CountItem(KeyItem item)
    {
        return _inventoryItems.ContainsKey(item) ? _inventoryItems[item] : 0;
    }

}
