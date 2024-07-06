using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DroppedItem {
    public GameObject item;
    public int weight;
}

public class PickUpSpawner : MonoBehaviour
{
    // [SerializeField] private GameObject goldCoin, healthGlobe, staminaGlobe, scroll;

    [SerializeField] private DroppedItem[] droppedItemList;

    private GameObject RandomByWeight(Dictionary<GameObject, int> itemWeights) {
        int totalWeight = 0;

        // sum weight
        foreach (int weight in itemWeights.Values) {
            totalWeight += weight;
        }

        // random between [1, totalWeight]
        int random = Random.Range(1, totalWeight + 1);

        // find which area the random in
        int cursor = 0;
        foreach (KeyValuePair<GameObject, int> itemWeight in itemWeights) {
            cursor += itemWeight.Value;
            if (cursor >= random) {
                return itemWeight.Key;
            }
        }

        return null; // this should never happen
    }

    public void DropItems() {
        Dictionary<GameObject, int> itemWeights = new Dictionary<GameObject, int>();
        foreach (DroppedItem pair in droppedItemList) {
            itemWeights[pair.item] = pair.weight;
        }

        GameObject selectedItem = RandomByWeight(itemWeights);

        Instantiate(selectedItem, transform.position, Quaternion.identity);
    }
}
