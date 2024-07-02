using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Start()
    {

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
    public void CollisionHandler(string collidedObjectTag, string collidedObjectName,
                                 string attackedObjectTag, string attackedObjectName)
    {
        //For debug purposes
        CollisionLog(collidedObjectName);
        CollisionLog(collidedObjectTag);
        CollisionLog(attackedObjectName);
        CollisionLog(attackedObjectTag);

        if (collidedObjectTag == "Monster" && attackedObjectTag == "Sword")
        {
            GameObject collidedObject = RetrieveObject(collidedObjectName);

            EnemyHealth enemyHealth = collidedObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(1);
        }

        if (collidedObjectTag == "Player" && attackedObjectTag == "Monster")
        {
            GameObject collidedObject = RetrieveObject(collidedObjectName);
            GameObject attackedObject = RetrieveObject(attackedObjectName);
            
            MainCharacter mainCharacter = collidedObject.GetComponent<MainCharacter>();

            if (attackedObjectName.Contains("Skull"))
            {
                ChaseMovement chaseMovement = attackedObject.GetComponent<ChaseMovement>();
                double countDown = MainCharacter.NUMBER_OF_MILLISECONDS_OF_INVINCIBILITY_PERIOD;
                chaseMovement.StopMoving();
                while (countDown > 0)
                {
                    //Debug.Log(countDown);
                    countDown -= Time.deltaTime;
                    
                }
                //chaseMovement.Roaming();
            }

            mainCharacter.DecreaseLiveCount();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
