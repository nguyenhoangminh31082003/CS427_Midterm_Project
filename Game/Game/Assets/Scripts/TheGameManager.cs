using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class TheGameManager : MonoBehaviour
{
    public static TheGameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            //MainCharacter.Instance.LoadDataFromPlayerPrefs();
        }
    }

    private void CollisionLog(string msg)
    {
        Debug.Log(msg);
    }

    private GameObject RetrieveObject(string objectName)
    {
        return GameObject.Find(objectName);
    }

    private void CollisionHandlerBetweenPlayerAndItem(string collidedObjectName, string attackedObjectName)
    {
        GameObject collidedObject = RetrieveObject(collidedObjectName);
        GameObject attackedObject = RetrieveObject(attackedObjectName);

        if (collidedObjectName.ToLower().IndexOf("First", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            TheFirstPlayer player = collidedObject.GetComponent<TheFirstPlayer>();
            ThePickup pickup = attackedObject.GetComponent<ThePickup>();
            ThePickUpWeapon pickupWeapon = attackedObject.GetComponent<ThePickUpWeapon>();

            if (pickupWeapon != null)
            {
                if (pickupWeapon.GetPickUpType() == ThePickUpWeapon.ItemWeaponType.Sword)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    player.IncreaseWeaponCount(Sword.GetWeaponName(), 1);

                }

                if (pickupWeapon.GetPickUpType() == ThePickUpWeapon.ItemWeaponType.Bow)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    player.IncreaseWeaponCount(Bow.GetWeaponName(), 1);
                }
                Destroy(attackedObject);
            }

            if (pickup != null)
            {
                if (pickup.GetPickUpType() == ThePickup.PickUpType.GoldCoin)
                {
                    AudioManager.Instance.PlaySFX("coin_pick_up");
                    player.ChangeCoinCount(1);
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.HealthGlobe)
                {
                    AudioManager.Instance.PlaySFX("health_pick_up");
                    player.IncreaseLiveCount();
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.SilverKey)
                {
                    AudioManager.Instance.PlaySFX("key_pick_up");
                    KeyManager.Instance.AddItem(KeyManager.KeyItem.SilverKey);
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.GoldKey)
                {
                    AudioManager.Instance.PlaySFX("key_pick_up");
                    KeyManager.Instance.AddItem(KeyManager.KeyItem.GoldKey);
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.Arrow)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    player.IncreaseWeaponCount(Arrow.GetWeaponName(), 1);
                }
            }

            Destroy(attackedObject);
        }

        if (collidedObjectName.ToLower().IndexOf("Second", StringComparison.OrdinalIgnoreCase) >= 0)
        {
            TheSecondPlayer player = collidedObject.GetComponent<TheSecondPlayer>();
            ThePickup pickup = attackedObject.GetComponent<ThePickup>();
            ThePickUpWeapon pickupWeapon = attackedObject.GetComponent<ThePickUpWeapon>();

            if (pickupWeapon != null)
            {
                if (pickupWeapon.GetPickUpType() == ThePickUpWeapon.ItemWeaponType.Sword)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    player.IncreaseWeaponCount(Sword.GetWeaponName(), 1);

                }

                if (pickupWeapon.GetPickUpType() == ThePickUpWeapon.ItemWeaponType.Bow)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    player.IncreaseWeaponCount(Bow.GetWeaponName(), 1);
                }
                Destroy(attackedObject);
            }

            if (pickup != null)
            {
                if (pickup.GetPickUpType() == ThePickup.PickUpType.GoldCoin)
                {
                    AudioManager.Instance.PlaySFX("coin_pick_up");
                    player.ChangeCoinCount(1);
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.HealthGlobe)
                {
                    AudioManager.Instance.PlaySFX("health_pick_up");
                    player.IncreaseLiveCount();
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.SilverKey)
                {
                    AudioManager.Instance.PlaySFX("key_pick_up");
                    KeyManager.Instance.AddItem(KeyManager.KeyItem.SilverKey);
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.GoldKey)
                {
                    AudioManager.Instance.PlaySFX("key_pick_up");
                    KeyManager.Instance.AddItem(KeyManager.KeyItem.GoldKey);
                }
                else if (pickup.GetPickUpType() == ThePickup.PickUpType.Arrow)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    player.IncreaseWeaponCount(Arrow.GetWeaponName(), 1);
                }
            }

            Destroy(attackedObject);
        }
    }

    private void CollisionHandlerBetweenPlayerAndTrap(string collidedObjectName, string attackedObjectName)
    {
        GameObject collidedObject = RetrieveObject(collidedObjectName);
        GameObject attackedObject = RetrieveObject(attackedObjectName);

        MainCharacter mainCharacter = collidedObject.GetComponent<MainCharacter>();

        if (!mainCharacter.IsInvincible())
        {
            mainCharacter.GetKnockBack(attackedObject.transform);
        }
        mainCharacter.DecreaseLiveCount();
    }

    private void CollisionHandlerBetweenPlayerAndMonster(string collidedObjectName, string attackedObjectName)
    {
        GameObject collidedObject = RetrieveObject(collidedObjectName);
        GameObject attackedObject = RetrieveObject(attackedObjectName);

        MainCharacter mainCharacter = collidedObject.GetComponent<MainCharacter>();

        if (!mainCharacter.IsInvincible())
        {
            mainCharacter.GetKnockBack(attackedObject.transform);
        }
        mainCharacter.DecreaseLiveCount();
    }

    private void CollisionHandlerBetweenMonsterAndSword(string  collidedObjectName, string attackedObjectName)
    {
        GameObject collidedObject = RetrieveObject(collidedObjectName);
        GameObject attackedObject = RetrieveObject(attackedObjectName);

        Sword sword = attackedObject.GetComponent<Sword>();
        EnemyHealth enemyHealth = collidedObject.GetComponent<EnemyHealth>();
        enemyHealth.TakeDamage(attackedObject, (int)Math.Round(sword.GetAmountDamageThatCanBeCaused()));
    }

    private void CollisionHandlerBetweenBoxAndSword(string collidedObjectName, string attackedObjectName)
    {
        GameObject collidedObject = RetrieveObject(collidedObjectName);

        Destructible destructible = collidedObject?.GetComponent<Destructible>();
        destructible.Destruct();
    }

    private void CollisionHandlerMonsterAndArrow(string collidedObjectName, string attackedObjectName)
    {
        GameObject collidedObject = RetrieveObject(collidedObjectName);
        GameObject attackedObject = RetrieveObject(attackedObjectName);

        Arrow arrow = attackedObject.GetComponent<Arrow>();
        EnemyHealth enemyHealth = collidedObject.GetComponent<EnemyHealth>();
        enemyHealth.TakeDamage(attackedObject, (int)Math.Round(arrow.GetAmountDamageThatCanBeCaused()));
        Destroy(attackedObject);
    }

    private void CollisionHandlerBoxAndArrow(string collidedObjectName, string attackedObjectName)
    {
        GameObject collidedObject = RetrieveObject(collidedObjectName);
        GameObject attackedObject = RetrieveObject(attackedObjectName);

        Destructible destructible = collidedObject?.GetComponent<Destructible>();
        destructible.Destruct();

        Destroy(attackedObject);
    }

    //Name is needed to retrieve object from the hierachy 
    //Tag is needed to identify the object
    public void CollisionHandler(string collidedObjectTag, 
                                 string collidedObjectName,
                                 string attackedObjectTag, 
                                 string attackedObjectName)
    {

        if (collidedObjectTag == "Player" && attackedObjectTag == "Item") {
            CollisionHandlerBetweenPlayerAndItem(collidedObjectName, attackedObjectName);
        }

        if (collidedObjectTag == "Monster" && attackedObjectTag == "Sword")
        {
            CollisionHandlerBetweenMonsterAndSword(collidedObjectName, attackedObjectName);
        }

        if (collidedObjectTag == "Box" && attackedObjectTag == "Sword")
        {
            CollisionHandlerBetweenBoxAndSword(collidedObjectName, attackedObjectName);
        }

        if (collidedObjectTag == "Box" && attackedObjectTag == "Arrow")
        {
            CollisionHandlerBoxAndArrow(collidedObjectName, attackedObjectName);
        }

        if (collidedObjectTag == "Monster" && attackedObjectTag == "Arrow")
        {
            CollisionHandlerMonsterAndArrow(collidedObjectName, attackedObjectName);
        }

        if (collidedObjectTag == "Player" && attackedObjectTag == "Monster")
        {
            CollisionHandlerBetweenPlayerAndMonster(collidedObjectName, attackedObjectName);
        }

        if (collidedObjectTag == "Player" && attackedObjectTag == "Trap")
        {
            CollisionHandlerBetweenPlayerAndTrap(collidedObjectName, attackedObjectName);
        }
    }

    void Update()
    {

    }
}
