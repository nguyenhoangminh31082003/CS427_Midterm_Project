using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodShop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private long bloodPrice = 25;
    void Start()
    {
        
    }

    public void BuyBlood()
    {
        if (MainCharacter.Instance.GetCoinCount() - bloodPrice >= 0)
        {
            if (MainCharacter.Instance.IncreaseLiveCount())
            {
                MainCharacter.Instance.ChangeCoinCount(-bloodPrice);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
