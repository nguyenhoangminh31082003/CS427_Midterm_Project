using TMPro;
using System;
using UnityEngine;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;

public class MainCharacter : MonoBehaviour
{
    public static MainCharacter Instance;

    [SerializeField] private TextMeshProUGUI liveCountText;
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private GameObject playerBag;

    private DialogueManager dialogueManager;
    public const double NUMBER_OF_MILLISECONDS_OF_INVINCIBILITY_PERIOD = 4000;
    public const double MAXIMUM_WEIGHT_LIMIT = 1E8;
    public const int MAXIMUM_LIVE_COUNT = 5;
    private GameManager gameManager;
    public const double DEFAULT_SPEED = 4;

    private Knockback knockback;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private double speedX, speedY;
    private long coinCount;
    [SerializeField] private int liveCount;
    private PlayerBag bag;
    private double weight;

    private bool invincible;
    private float lastDamageTime;

    public void SaveDataToPlayerPrefs()
    {
        PlayerPrefs.SetString("invincible", this.invincible.ToString());
        PlayerPrefs.SetFloat("lastDamageTime", this.lastDamageTime);
        PlayerPrefs.SetString("weight", this.weight.ToString());
        PlayerPrefs.SetString("coinCount", this.coinCount.ToString());
        PlayerPrefs.SetString("speedX", this.speedX.ToString());
        PlayerPrefs.SetString("speedY", this.speedY.ToString());
        PlayerPrefs.SetInt("liveCount", this.liveCount);
        Debug.Log("Data saved");    
    }
    
    private void LoadDataFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("invincible"))
            this.invincible = bool.Parse(PlayerPrefs.GetString("invincible"));
        if (PlayerPrefs.HasKey("lastDamageTime"))
            this.lastDamageTime = PlayerPrefs.GetFloat("lastDamageTime");
        if (PlayerPrefs.HasKey("weight"))
            this.weight = double.Parse(PlayerPrefs.GetString("weight"));
        if (PlayerPrefs.HasKey("coinCount"))
            this.coinCount = long.Parse(PlayerPrefs.GetString("coinCount"));
        if (PlayerPrefs.HasKey("speedX"))
            this.speedX = double.Parse(PlayerPrefs.GetString("speedX"));
        if (PlayerPrefs.HasKey("speedY"))
            this.speedY = double.Parse(PlayerPrefs.GetString("speedY"));
        if (PlayerPrefs.HasKey("liveCount"))
            this.liveCount = PlayerPrefs.GetInt("liveCount");
        Debug.Log("Data loaded");
    }

        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            this.LoadDataFromPlayerPrefs();
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        this.rigidBody2D = this.GetComponent<Rigidbody2D>();
        this.bag = this.playerBag.GetComponent<PlayerBag>();
        this.knockback = this.GetComponent<Knockback>();
        this.speedX = 0;
        this.speedY = 0;
        this.weight = 1;
        this.liveCount = 5;
        this.coinCount = 0;

        this.invincible = false;
        this.lastDamageTime = 0;

        this.gameManager = GameManager.Instance;
        this.dialogueManager  = DialogueManager.Instance;
    }

    public void GetKnockBack(Transform source)
    {
        this.knockback.GetKnockedBack(source, 10f);
    }

    public bool ChangeCoinCount(long change)
    {
        long newCoinCount = this.coinCount + change;
        if (newCoinCount < 0)
        {
            return false;
        }
        this.coinCount = newCoinCount;
        return true;
    }
    /*
    public void ModifySpeed(double speedX, double speedY) {
        this.speedX = speedX;
        this.speedY = speedY;
    }
    */
    
    public long GetCoinCount() {
        return this.coinCount;
    }

    public int GetLiveConut()
    {
        return this.liveCount;
    }

    public double GetWeight()
    {
        return this.weight;
    }

    public double GetTotalWeight()
    {
        return this.weight + this.bag.GetTotalWeight();
    }

    public Vector2 GetVelocityVector()
    {
        return new Vector2((float)this.speedX, (float)this.speedY);
    }

    public bool DecreaseLiveCount()
    {
        if (this.invincible)
            return false;
        if (this.liveCount > 0)
        {
            --this.liveCount;
            this.invincible = true;
            this.lastDamageTime = Time.time;
            AudioManager.Instance.PlaySFX("player_hurt");
            return true;
        }
        return false;
    }

    public bool IncreaseLiveCount()
    {
        if (this.liveCount < MAXIMUM_LIVE_COUNT)
        {
            ++this.liveCount;
            return true;
        }
        return false;
    }

    private void UpdateVelocity()
    {
        bool rightArrow = Input.GetKey(KeyCode.RightArrow)  || Input.GetKey(KeyCode.D),
             leftArrow  = Input.GetKey(KeyCode.LeftArrow)   || Input.GetKey(KeyCode.A),
             upArrow    = Input.GetKey(KeyCode.UpArrow)     || Input.GetKey(KeyCode.W),
             downArrow  = Input.GetKey(KeyCode.DownArrow)   || Input.GetKey(KeyCode.S);
        this.speedX = 0;
        this.speedY = 0;
        if (leftArrow)
            this.speedX -= DEFAULT_SPEED;
        if (rightArrow)
            this.speedX += DEFAULT_SPEED;
        if (upArrow)
            this.speedY += DEFAULT_SPEED;
        if (downArrow)
            this.speedY -= DEFAULT_SPEED;
        if (this.speedX > 0)
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (this.speedX < 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        double percentage = Math.Max(0, 1 - this.GetTotalWeight() / MAXIMUM_WEIGHT_LIMIT);
        this.speedX *= percentage;
        this.speedY *= percentage;
    }

    // Update is called once per frame
    void Update()
    {
        if (dialogueManager != null)
        {
            if (dialogueManager.isDialogueActive) 
            { 
                return; 
            }
        }
        this.UpdateCurrentlyUsedWeapon();
        this.UpdateVelocity();
        this.UpdateInvincibilityStatus();
        this.UpdateCanvasElement();
        this.UpdateAttack();
        this.UpdateKeyUsage();
    }

    private void UpdateKeyUsage()
    {
        if (Input.GetKey(KeyCode.E))
        {
            //WHERE IS THE MANAGER!!!
        }
    }

    public bool IncreaseGateKeyCount(int delta)
    {
        if (delta <= 0)
            return false;
        return this.bag.ChangeGateKeyCount(delta);
    }

    public bool DecreaseGateKeyCount(int delta)
    {
        if (delta <= 0)
            return false;
        return this.bag.ChangeGateKeyCount(-delta);
    }
    public bool IncreaseChestKeyCount(int delta)
    {
        if (delta <= 0)
            return false;
        return this.bag.ChangeChestKeyCount(delta);
    }

    public bool DecreaseChestKeyCount(int delta)
    {
        if (delta <= 0)
            return false;
        return this.bag.ChangeChestKeyCount(-delta);
    }

    private void UpdateCurrentlyUsedWeapon()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.bag.MoveToTheNextWeaponAsTheCurrentWeapon();
        }
    }

    private void UpdateAttack()
    {
        this.bag.UseCurrentWeaponToAttackWithConsideringKeyboard();   
    }

    private void UpdateCanvasElement()
    {
        this.liveCountText.text = this.liveCount.ToString();
        this.coinCountText.text = this.coinCount.ToString();
    }

    private void UpdateInvincibilityStatus()
    {
        if (this.invincible)
        {
            float currentTime = Time.time,
                  amountPassed = (currentTime - this.lastDamageTime) * 1000;
            if (amountPassed > NUMBER_OF_MILLISECONDS_OF_INVINCIBILITY_PERIOD)
            {
                this.invincible = false;
            }
            else
            {
                UnityEngine.Color color = new UnityEngine.Color(1f, 1f, 1f, (float)(Math.Sin(amountPassed) + 1) / 2);
                this.spriteRenderer.color = color;
                this.bag.ChangeColorRecursively(color);
            }
        }
        else
        {
            UnityEngine.Color color = new UnityEngine.Color(1f, 1f, 1f, 1f);
            this.spriteRenderer.color = color;
            this.bag.ChangeColorRecursively(color);
        }
    }

    private void FixedUpdate()
    {
        if (knockback.gettingKnockedBack) { return; }
        this.rigidBody2D.velocity = new Vector2((float)this.speedX, (float)this.speedY);
        if (IsDead())
        {
            Destroy(gameObject);
        }
    }

    public bool IsDead()
    {
        return this.liveCount <= 0;
    }

    public bool IsAlive()
    {
        return this.liveCount > 0;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (IsAttacking()) return;

        if (other.transform.tag == "Monster" || other.transform.tag == "Trap")
        {
            gameManager.CollisionHandler(this.tag, this.name, other.transform.tag, other.transform.name);
        }
    }
    
    public void PauseAnimation()
    {
        this.GetComponent<Animator>().enabled = false;
    }

    public void ContinueAnimation()
    {
        this.GetComponent<Animator>().enabled = true;
    }

    public bool IsAttacking()
    {
        return this.bag.IsAttacking();
    }

    public bool IsInvincible()
    {
        return this.invincible;
    }

    public bool IncreaseWeaponCount(string weaponName, int number)
    {
        if (number <= 0)
            return false;
        return this.bag.ChangeWeaponCount(weaponName, number);
    }

    public bool DecreaseWeaponCount(string weaponName, int number)
    {
        if (number <= 0)
            return false;
        return this.bag.ChangeWeaponCount(weaponName, -number);
    }
}
