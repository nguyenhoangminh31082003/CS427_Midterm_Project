using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class TheKeyManager : MonoBehaviour
{
    public static TheKeyManager Instance;

    public Dictionary<KeyItem, int>[] _inventoryItems = 
    {
        new Dictionary<KeyItem, int>(),
        new Dictionary<KeyItem, int>()
    };
    public enum KeyItem {
        SilverKey,
        GoldKey
    }
    void Awake() {
        Debug.Log("HELLO WESTEROS!!!!!!!!!!!!!!!!!!!");
        Instance = this;
    }

    public void AddItem(string whichPlayer, KeyItem item)
    {
        int id = -1;

        if (whichPlayer.ToLower().IndexOf("First", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 0;
        else if (whichPlayer.ToLower().IndexOf("Second", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 1;

        if (id >= 0)
        {
            if (_inventoryItems[id].ContainsKey(item))
            {
                _inventoryItems[id][item]++;
            }
            else
            {
                _inventoryItems[id][item] = 1;
            }
        }
        
    }

    public void RemoveItem(string whichPlayer, KeyItem item)
    {
        int id = -1;

        if (whichPlayer.ToLower().IndexOf("First", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 0;
        else if (whichPlayer.ToLower().IndexOf("Second", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 1;

        if (id >= 0)
        {
            if (_inventoryItems[id].ContainsKey(item))
            {
                _inventoryItems[id][item]--;
                if (_inventoryItems[id][item] <= 0)
                {
                    _inventoryItems[id].Remove(item);
                }
            }
        }

    }

    public bool CheckRequiredItem(string whichPlayer, KeyItem item)
    {
        int id = -1;

        if (whichPlayer.ToLower().IndexOf("First", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 0;
        else if (whichPlayer.ToLower().IndexOf("Second", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 1;

        if (id < 0)
            return false;

        return _inventoryItems[id].ContainsKey(item) && _inventoryItems[id][item] > 0;
    }

    public List<KeyItem> GetInventoryKey(string whichPlayer)
    {
        int id = -1;

        if (whichPlayer.ToLower().IndexOf("First", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 0;
        else if (whichPlayer.ToLower().IndexOf("Second", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 1;

        if (id < 0)
            return new List<KeyItem>();

        return new List<KeyItem>(_inventoryItems[id].Keys);
    }

    public int CountItem(string whichPlayer, KeyItem item)
    {
        int id = -1;

        if (whichPlayer.ToLower().IndexOf("First", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 0;
        else if (whichPlayer.ToLower().IndexOf("Second", StringComparison.OrdinalIgnoreCase) >= 0)
            id = 1;

        if (id < 0)
            return 0;

        return _inventoryItems[id].ContainsKey(item) ? _inventoryItems[id][item] : 0;
    }

}
