using TMPro;
using System;
using UnityEngine;
using System.Collections.Generic;
public class ThePlayerBag : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI silverKeyCountText;
    [SerializeField] private TextMeshProUGUI goldenKeyCountText;

    [SerializeField] private GameObject canvasUIWeaponsContainer;
    [SerializeField] private int numberOfCanvasUIWeaponBoxes;
    [SerializeField] private GameObject sampleBox;

    [SerializeField] private int currentWeaponIndex = -1;
    [SerializeField] private int chestKeyCount;
    [SerializeField] private int gateKeyCount;

    private List<GameObject> canvasUIWeaponBoxes;
    private List<TheWeapon> theWeapons;
    private double totalWeight;

    private static ThePlayerBag Instance;

    private bool partiallyInitialized = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetDefaultValuesToPlayerPrefs()
    {
        PlayerPrefs.SetInt("PlayerBag.numberOfCanvasUIWeaponBoxes", 3);
        PlayerPrefs.SetInt("PlayerBag.currentWeaponIndex", -1);
        PlayerPrefs.SetInt("PlayerBag.chestKeyCount", 0);
        PlayerPrefs.SetInt("PlayerBag.gateKeyCount", 0);
        PlayerPrefs.SetString("PlayerBag.totalWeight", "0");

        if (this.theWeapons != null)
        {
            foreach (TheWeapon theWeapon in this.theWeapons)
                theWeapon.SetDefaultValuesToPlayerPrefs();
        }
    }
    public void SaveDataToPlayerPrefs()
    {
        PlayerPrefs.SetInt("PlayerBag.numberOfCanvasUIWeaponBoxes", this.numberOfCanvasUIWeaponBoxes);
        PlayerPrefs.SetInt("PlayerBag.currentWeaponIndex", this.currentWeaponIndex);
        PlayerPrefs.SetInt("PlayerBag.chestKeyCount", this.chestKeyCount);
        PlayerPrefs.SetInt("PlayerBag.gateKeyCount", this.gateKeyCount);
        PlayerPrefs.SetString("PlayerBag.totalWeight", this.totalWeight.ToString());

        if (this.theWeapons != null)
        {
            foreach (TheWeapon theWeapon in this.theWeapons)
                theWeapon.SaveDataToPlayerPrefs();
        }
    }
    public void LoadDataFromPlayerPrefs()
    {

        if (this.theWeapons == null)
        {
            this.theWeapons = new List<TheWeapon>();

            foreach (Transform child in this.transform)
            {
                TheWeapon weapon = child.GetComponent<TheWeapon>();
                if (weapon != null && weapon is TheWeapon)
                {
                    this.theWeapons.Add(weapon);
                }
            }
        }

        foreach (TheWeapon theWeapon in this.theWeapons)
            theWeapon.LoadDataFromPlayerPrefs();

        if (this.currentWeaponIndex >= 0)
            this.theWeapons[this.currentWeaponIndex].StopUsing();

        if (PlayerPrefs.HasKey("PlayerBag.numberOfCanvasUIWeaponBoxes"))
            this.numberOfCanvasUIWeaponBoxes = PlayerPrefs.GetInt("PlayerBag.numberOfCanvasUIWeaponBoxes");
        
        if (PlayerPrefs.HasKey("PlayerBag.currentWeaponIndex"))
            this.currentWeaponIndex = PlayerPrefs.GetInt("PlayerBag.currentWeaponIndex");
        
        if (PlayerPrefs.HasKey("PlayerBag.chestKeyCount"))
            this.chestKeyCount = PlayerPrefs.GetInt("PlayerBag.chestKeyCount");
        
        if (PlayerPrefs.HasKey("PlayerBag.gateKeyCount"))
            this.gateKeyCount = PlayerPrefs.GetInt("PlayerBag.gateKeyCount");
        
        if (PlayerPrefs.HasKey("PlayerBag.totalWeight"))
            this.totalWeight = double.Parse(PlayerPrefs.GetString("PlayerBag.totalWeight"));

        if (this.currentWeaponIndex >= 0)
            this.theWeapons[this.currentWeaponIndex].StartUsing();

        this.partiallyInitialized = true;

    }

    void Start()
    {

        if (this.theWeapons == null)
        {
            this.theWeapons = new List<TheWeapon>();

            foreach (Transform child in this.transform)
            {
                TheWeapon theWeapon = child.GetComponent<TheWeapon>();
                if (theWeapon != null && theWeapon is TheWeapon)
                {
                    this.theWeapons.Add(theWeapon);
                }
            }
        }

        if (!this.partiallyInitialized)
        {
            this.gateKeyCount = 0;

            this.chestKeyCount = 0;

            this.totalWeight = 0;

            this.currentWeaponIndex = -1;
        }

        this.canvasUIWeaponBoxes = new List<GameObject>();

        for (int i = 0; i < numberOfCanvasUIWeaponBoxes; ++i)
        {
            GameObject duplicate = Instantiate(this.sampleBox, this.canvasUIWeaponsContainer.transform);

            RectTransform rectTransform = duplicate.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - i * rectTransform.rect.height);
            }

            duplicate.name = "Weapon box container " + i.ToString();

            duplicate.SetActive(true);

            this.canvasUIWeaponBoxes.Add(duplicate);
        }

    }

    public bool ChangeGateKeyCount(int delta)
    {
        int newGateKeyCount = this.gateKeyCount + delta;
        if (newGateKeyCount < 0)
            return false;
        this.gateKeyCount = newGateKeyCount;
        return true;
    }

    public bool ChangeChestKeyCount(int delta)
    {
        int newChestKeyCount = this.chestKeyCount + delta;
        if (newChestKeyCount < 0)
            return false;
        this.gateKeyCount = newChestKeyCount;
        return true;
    }

    private int FindNextAvailableWeapon(int position)
    {

        for (int i = 0; i < this.theWeapons.Count; ++i)
        {
            ++position;
            if (position >= this.theWeapons.Count)
                position = 0;
            if (this.theWeapons[position].GetNumber() > 0)
                return position;
        }

        return -1;
    }

    public double GetTotalWeight()
    {
        return this.totalWeight;
    }

    private void UpdateTotalWeight()
    {
        this.totalWeight = 0;
        if (this.theWeapons != null)
        {
            foreach (TheWeapon theWeapon in this.theWeapons)
            {
                this.totalWeight += theWeapon.FindTotalWeight();
            }
        }
    }

    void Update()
    {
        this.UpdateCanvasElements();
        this.UpdateTotalWeight();
    }

    private int CountAvailableWeapons()
    {
        int count = 0;

        if (this.theWeapons != null)
        {
            foreach (TheWeapon theWeapon in this.theWeapons)
            {

                if (theWeapon.GetNumber() > 0)
                    ++count;
            }
        }

        return count;
    }

    private void UpdateCanvasElements()
    {
        if (TheKeyManager.Instance != null)
        {
            Debug.Log(this.transform.parent.name);

            if (this.silverKeyCountText != null)
            {
                this.silverKeyCountText.text = TheKeyManager.Instance.CountItem(this.transform.parent.name, TheKeyManager.KeyItem.SilverKey).ToString();
            }

            if (this.goldenKeyCountText != null)
            {
                this.goldenKeyCountText.text = TheKeyManager.Instance.CountItem(this.transform.parent.name, TheKeyManager.KeyItem.GoldKey).ToString();
            }
        }

        if (this.canvasUIWeaponBoxes == null)
            this.canvasUIWeaponBoxes = new List<GameObject>();

        if (this.canvasUIWeaponBoxes.Count < this.numberOfCanvasUIWeaponBoxes)
        {
            if (this.sampleBox != null && this.canvasUIWeaponsContainer != null)
            {
                for (int i = this.canvasUIWeaponBoxes.Count; i < numberOfCanvasUIWeaponBoxes; ++i)
                {
                    GameObject duplicate = Instantiate(this.sampleBox, this.canvasUIWeaponsContainer.transform);

                    RectTransform rectTransform = duplicate.GetComponent<RectTransform>();

                    if (rectTransform != null)
                    {
                        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - i * rectTransform.rect.height);
                    }

                    duplicate.name = "Weapon box container " + i.ToString();

                    duplicate.SetActive(true);

                    this.canvasUIWeaponBoxes.Add(duplicate);
                }
            }
        }

        int position = this.currentWeaponIndex, weaponCount = this.CountAvailableWeapons();

        for (int i = 0; i < this.numberOfCanvasUIWeaponBoxes; ++i)
        {

            if (i >= this.canvasUIWeaponBoxes.Count)
                break;

            WeaponBoxCanvasUI box = this.canvasUIWeaponBoxes[i].GetComponent<WeaponBoxCanvasUI>();

            if (box != null)
            {
                if (i == 0)
                    box.SetTheBoxChosen();
                else
                    box.SetTheBoxUnchosen();

                if (weaponCount <= 0 || this.theWeapons == null)
                {
                    box.TurnIntoEmptyBox();
                }
                else
                {
                    this.theWeapons[position].DisplayInCanvas(box);
                    --weaponCount;
                    position = this.FindNextAvailableWeapon(position);
                }
            }
                
        }
    }

    public bool MoveToTheNextWeaponAsTheCurrentWeapon()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        this.theWeapons[this.currentWeaponIndex].StopUsing();
        this.currentWeaponIndex = this.FindNextAvailableWeapon(this.currentWeaponIndex);
        this.theWeapons[this.currentWeaponIndex].StartUsing();
        return true;
    }

    public bool UseCurrentWeaponToAttack()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        return this.theWeapons[this.currentWeaponIndex].Attack();
    }

    public bool UseCurrentWeaponToAttackWithConsideringKeyboard()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        return this.theWeapons[this.currentWeaponIndex].AttackWithConsideringKeyboard();
    }

    public bool IsAttacking()
    {
        if (this.currentWeaponIndex < 0)
            return false;
        return this.theWeapons[this.currentWeaponIndex].IsBeingUsedToAttack();
    }

    public void ChangeColorRecursively(Color color)
    {
        if (this.currentWeaponIndex >= 0)
        {
            this.theWeapons[this.currentWeaponIndex].ChangeColorRecursively(color);
        }
    }

    public bool ChangeWeaponCount(string weaponName, int number)
    {

        if (number == 0 || this.theWeapons == null)
            return false;

        if (weaponName == Arrow.GetWeaponName())
        {
            string bowWeaponName = Bow.GetWeaponName();
            
            foreach (TheWeapon theWeapon in this.theWeapons)
                if (theWeapon.GetNameOfWeapon() == bowWeaponName)
                {
                    string value = theWeapon.GetWeaponAttributeValue("unusedArrowCount");

                    if (value == null)
                        return false;

                    int parsedValue = Int32.Parse(value);

                    return theWeapon.SetWeaponAttributeValue(
                        "unusedArrowCount",
                        (parsedValue + number).ToString()
                    );
                }

            return false;
        }

        bool result = false;

        int previousCounter = this.CountAvailableWeapons();

        foreach (TheWeapon theWeapon in this.theWeapons)
            if (theWeapon.GetNameOfWeapon() == weaponName)
            {
                if (number > 0)
                    theWeapon.IncreaseNumber(number);
                else
                    theWeapon.DecreaseNumber(-number);
                result = true;
                break;
            }

        if (result)
        {
            int currentCounter = this.CountAvailableWeapons();

            if (previousCounter == 0 && currentCounter > 0)
            {
                this.currentWeaponIndex = this.FindNextAvailableWeapon(0);
                this.theWeapons[this.currentWeaponIndex].StartUsing();
            }
            else if (currentCounter == 0 && previousCounter > 0)
            {
                this.theWeapons[this.currentWeaponIndex].StopUsing();
                this.currentWeaponIndex = -1;
            }
            else if (currentCounter < previousCounter)
            {
                this.MoveToTheNextWeaponAsTheCurrentWeapon();
            }
        }

        return result;
    }

}
