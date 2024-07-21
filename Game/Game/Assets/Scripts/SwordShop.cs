using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordShop : MonoBehaviour
{
    [SerializeField] private long swordPrice = 50;
    void Start()
    {

    }

    public void BuySword()
    {
        if (MainCharacter.Instance.GetCoinCount() - swordPrice >= 0)
        {
            if (MainCharacter.Instance.IncreaseAmountDamageThatCanBeCausedByWeapon(Sword.GetWeaponName(), 1))
            {
                MainCharacter.Instance.ChangeCoinCount(-swordPrice);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
