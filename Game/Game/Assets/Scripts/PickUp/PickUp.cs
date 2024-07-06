using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public enum PickUpType
    {
        GoldCoin,
        StaminaGlobe,
        HealthGlobe,
        SilverKey,
        GoldKey
    }
    [SerializeField] private PickUpType pickUpType;
    [SerializeField] private float pickUpDistance = 5f;
    [SerializeField] private float accelartionRate = .6f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 1.5f;
    [SerializeField] private float popDuration = 0.5f;
    private Vector3 moveDir;
    private Rigidbody2D rb;

    private GameManager gameManager;

    private float mass;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        gameManager = GameManager.Instance;
        if (pickUpType == PickUpType.SilverKey || pickUpType == PickUpType.GoldKey) { return; }
        StartCoroutine(AnimCurveSpawnRoutine());
    }

    private void Update() {
        // Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 playerPos = MainCharacter.Instance.transform.position;

        if (Vector3.Distance(transform.position, playerPos) < pickUpDistance) {
            moveDir = (playerPos - transform.position).normalized;
            moveSpeed += accelartionRate;
        } else {
            moveDir = Vector3.zero;
            moveSpeed = 0;
        }
    }

    private void FixedUpdate() {
        rb.velocity = moveDir * moveSpeed * Time.fixedDeltaTime;
    }
    public PickUpType GetPickUpType()
    {
        return pickUpType;
    }

    private void OnTriggerStay2D(Collider2D other) {
        // if (other.gameObject.GetComponent<PlayerController>()) {
        //     Destroy(gameObject);
        // }
        if (other.transform.tag == "Player")
        {
            gameManager.CollisionHandler(other.transform.tag, other.transform.name, this.tag, this.name);
        }
        //DetectPickupType();
        //Destroy(gameObject);
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

    public float GetMass()
    {
        switch (pickUpType)
        {
            case PickUpType.GoldCoin:
                mass = 1f;
                break;
            case PickUpType.HealthGlobe:
                mass = 1f;
                break;
            case PickUpType.StaminaGlobe:
                mass = 1f;
                break;
            case PickUpType.SilverKey:
                // KeyManager.Instance.AddItem(KeyManager.KeyItem.SilverKey); <- handle nhat key
                mass = 1f;
                break;
            case PickUpType.GoldKey:
                // KeyManager.Instance.AddItem(KeyManager.KeyItem.GoldKey);
                mass = 1f;
                break;
        }
        return mass;
    }

    // private void DetectPickupType()
    // {
    //     switch (pickUpType)
    //     {
    //         case PickUpType.GoldCoin:
    //             // do goldcoin stuff
    //             Debug.Log("GoldCoin");
    //             break;
    //         case PickUpType.HealthGlobe:
    //             Debug.Log("HealthGlobe");
    //             break;
    //         case PickUpType.StaminaGlobe:
    //             // do stamina globe stuff
    //             Debug.Log("StaminaGlobe");
    //             break;
    //         case PickUpType.SilverKey:
    //             KeyManager.Instance.AddItem(KeyManager.KeyItem.SilverKey);
    //             Debug.Log("SilverKey");
    //             break;
    //         case PickUpType.GoldKey:
    //             Debug.Log("GoldKey");
    //             KeyManager.Instance.AddItem(KeyManager.KeyItem.GoldKey);
    //             break;
    //     }
    // }
}
