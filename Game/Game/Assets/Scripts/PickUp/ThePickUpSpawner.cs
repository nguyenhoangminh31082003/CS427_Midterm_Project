using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TheDroppedItem {
    public GameObject item;
    public int weight;
}

public class ThePickUpSpawner : MonoBehaviour
{
    // [SerializeField] private GameObject goldCoin, healthGlobe, staminaGlobe, scroll;

    [SerializeField] private TheDroppedItem[] droppedItemList;
    [SerializeField] private Dialogue dialogue;

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
        foreach (TheDroppedItem pair in droppedItemList) {
            itemWeights[pair.item] = pair.weight;
        }

        GameObject selectedItem = RandomByWeight(itemWeights);

        if (selectedItem)
        {
            StoryItem storyItem = selectedItem.GetComponent<StoryItem>();
            if (storyItem) {
                storyItem.SetDialogue(dialogue);
            }
            Instantiate(selectedItem, transform.position, Quaternion.identity);
        }
    }
}
