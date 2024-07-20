using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThePickUpWeapon : TheInteractable
{
    [SerializeField] private ItemWeaponType weaponType;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 0.5f;
    public enum ItemWeaponType {
        Sword,
        Bow,
    }

    protected override void Start()
    {
        base.Start();
        StartCoroutine(AnimCurveSpawnRoutine());

    }
    protected override void Interact(int whichPlayer)
    {
        Debug.Log("Picked " + weaponType.ToString());

        if (whichPlayer == 1)
            this.theGameManager.CollisionHandler(TheFirstPlayer.Instance.tag, TheFirstPlayer.Instance.name, "Item", this.transform.name);
        else if (whichPlayer == 2)
            this.theGameManager.CollisionHandler(TheSecondPlayer.Instance.tag, TheSecondPlayer.Instance.name, "Item", this.transform.name);
    }

    public ItemWeaponType GetPickUpType()
    {
        return this.weaponType;
    }

    private IEnumerator AnimCurveSpawnRoutine() {
        Vector2 startPoint = transform.position;
        float randomX = transform.position.x + Random.Range(-2f, 2f);
        float randomY = transform.position.y + Random.Range(-1f, 1f);

        Vector2 endPoint = new Vector2(randomX, randomY);

        float timePassed = 0f;

        while (timePassed < popDuration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / popDuration;
            float heightT = animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPoint, endPoint, linearT) + new Vector2(0f, height);
            yield return null;
        }
    }
}
