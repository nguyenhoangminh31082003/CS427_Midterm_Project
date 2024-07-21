using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopCoin : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI dialogueArea;
    private long currenGold = 0;
    void Start()
    {
        currenGold = MainCharacter.Instance.GetCoinCount();
        dialogueArea.SetText("$ " + currenGold.ToString());
    }

    public void UpdateCoin()
    {
        currenGold = MainCharacter.Instance.GetCoinCount();
        dialogueArea.SetText("$ " + currenGold.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
