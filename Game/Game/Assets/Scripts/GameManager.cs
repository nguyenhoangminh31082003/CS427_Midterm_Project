using System;
using System.Collections;
using System.Collections.Generic;
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
    public void CollisionHandler(string collidedObjectName, string collidedObjectTag,
                                 string attackedObjectName, string attackedObjectTag)
    {
        //For debug purposes
        CollisionLog(collidedObjectName);
        CollisionLog(collidedObjectTag);
        CollisionLog(attackedObjectName);
        CollisionLog(attackedObjectTag);

        if (collidedObjectTag == "Player" && attackedObjectTag == "Clone")
        {
            GameObject collidedObject = RetrieveObject(collidedObjectName);
            GameObject attackedObject = RetrieveObject(attackedObjectName);

            //Call specific logic here
            //PlayerMovement contact = attackedObject.GetComponent<PlayerMovement>();
            //contact.knockBack(-15.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
