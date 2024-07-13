using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;


public class FinishPoint : Interactable
{
    [SerializeField] KeyManager.KeyItem _requiredKey;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Interact()
    {
        if (KeyManager.Instance.CheckRequiredItem(_requiredKey))
        {
            KeyManager.Instance.RemoveItem(_requiredKey);
            SceneController.Instance.NextLevel();
        }
    }
}
