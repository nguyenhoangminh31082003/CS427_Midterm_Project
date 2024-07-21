using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShop : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private long arrowsPrice = 35;
    void Start()
    {
        
    }

    public void BuyArrows()
    {
        if (MainCharacter.Instance.GetCoinCount() - arrowsPrice >= 0)
        {
            if (MainCharacter.Instance.IncreaseWeaponCount(Arrow.GetWeaponName(), 4))
            {
                MainCharacter.Instance.ChangeCoinCount(-arrowsPrice);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
