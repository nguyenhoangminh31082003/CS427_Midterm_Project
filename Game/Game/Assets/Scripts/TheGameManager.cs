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
            MainCharacter.Instance.LoadDataFromPlayerPrefs();
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

    //Name is needed to retrieve object from the hierachy 
    //Tag is needed to identify the object
    public void CollisionHandler(string collidedObjectTag, 
                                 string collidedObjectName,
                                 string attackedObjectTag, 
                                 string attackedObjectName)
    {

        if (collidedObjectTag == "Player" && attackedObjectTag == "Item") {
            GameObject collidedObject = RetrieveObject(collidedObjectName);
            GameObject attackedObject = RetrieveObject(attackedObjectName);

            MainCharacter mainCharacter = collidedObject.GetComponent<MainCharacter>();
            Pickup pickup = attackedObject.GetComponent<Pickup>();
            PickUpWeapon pickupWeapon = attackedObject.GetComponent<PickUpWeapon>();
            
            if (pickupWeapon != null)
            {
                if (pickupWeapon.GetPickUpType() == PickUpWeapon.ItemWeaponType.Sword)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    mainCharacter.IncreaseWeaponCount(Sword.GetWeaponName(), 1);
                    
                }

                if (pickupWeapon.GetPickUpType() == PickUpWeapon.ItemWeaponType.Bow)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    mainCharacter.IncreaseWeaponCount(Bow.GetWeaponName(), 1);
                }
                Destroy(attackedObject);
            }

            if (pickup != null)
            {
                if (pickup.GetPickUpType() == Pickup.PickUpType.GoldCoin)
                {
                    AudioManager.Instance.PlaySFX("coin_pick_up");
                    mainCharacter.ChangeCoinCount(1);
                }
                else if (pickup.GetPickUpType() == Pickup.PickUpType.HealthGlobe)
                {
                    AudioManager.Instance.PlaySFX("health_pick_up");
                    mainCharacter.IncreaseLiveCount();
                }
                else if (pickup.GetPickUpType() == Pickup.PickUpType.SilverKey)
                {
                    AudioManager.Instance.PlaySFX("key_pick_up");
                    KeyManager.Instance.AddItem(KeyManager.KeyItem.SilverKey);
                }
                else if (pickup.GetPickUpType() == Pickup.PickUpType.GoldKey)
                {
                    AudioManager.Instance.PlaySFX("key_pick_up");
                    KeyManager.Instance.AddItem(KeyManager.KeyItem.GoldKey);
                }
                else if (pickup.GetPickUpType() == Pickup.PickUpType.Arrow)
                {
                    AudioManager.Instance.PlaySFX("item_pick_up");
                    mainCharacter.IncreaseWeaponCount(Arrow.GetWeaponName(), 1);
                }
            }

            Destroy(attackedObject);
        }

        if (collidedObjectTag == "Monster" && attackedObjectTag == "Sword")
        {
            GameObject collidedObject = RetrieveObject(collidedObjectName);
            GameObject attackedObject = RetrieveObject(attackedObjectName);  
            
            Sword sword = attackedObject.GetComponent<Sword>();
            EnemyHealth enemyHealth = collidedObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackedObject, (int)Math.Round(sword.GetAmountDamageThatCanBeCaused()));
        }

        if (collidedObjectTag == "Box" && attackedObjectTag == "Sword")
        {
            GameObject collidedObject = RetrieveObject(collidedObjectName);
            
            Destructible destructible = collidedObject?.GetComponent<Destructible>();
            destructible.Destruct();
        }

        if (collidedObjectTag == "Box" && attackedObjectTag == "Arrow")
        {
            GameObject collidedObject = RetrieveObject(collidedObjectName);
            GameObject attackedObject = RetrieveObject(attackedObjectName);

            Destructible destructible = collidedObject?.GetComponent<Destructible>();
            destructible.Destruct();

            Destroy(attackedObject);
        }

        if (collidedObjectTag == "Monster" && attackedObjectTag == "Arrow")
        {
            GameObject collidedObject = RetrieveObject(collidedObjectName);
            GameObject attackedObject = RetrieveObject(attackedObjectName);

            Arrow arrow = attackedObject.GetComponent<Arrow>();
            EnemyHealth enemyHealth = collidedObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(attackedObject, (int)Math.Round(arrow.GetAmountDamageThatCanBeCaused()));
            Destroy(attackedObject);
        }

        if (collidedObjectTag == "Player" && attackedObjectTag == "Monster")
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

        if (collidedObjectTag == "Player" && attackedObjectTag == "Trap")
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
    }

    void Update()
    {

    }
}
