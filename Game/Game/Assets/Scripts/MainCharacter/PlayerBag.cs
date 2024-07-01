using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{

    private double totalWeight;
    [SerializeField] private Weapon currentWeapon;
    private List<Weapon> weapons;

    // Start is called before the first frame update
    void Start()
    {
        this.totalWeight = 0;

        this.weapons = new List<Weapon>();

        this.currentWeapon = null;

        foreach (Transform child in this.transform)
        {
            Weapon weapon = child.GetComponent<Weapon>();
            if (weapon != null && weapon is Weapon)
            {
                this.weapons.Add(weapon);
            }
        }

        if (this.weapons.Count > 0)
        {
            this.currentWeapon = this.weapons[0];
            this.currentWeapon.IncreaseNumber(1);
            Debug.Log(this.currentWeapon.GetNumber());
            Debug.Log(this.currentWeapon.StartUsing());
            //this.currentWeapon.StartUsing();
        }
    }

    public double GetTotalWeight()
    {
        return this.totalWeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool FlipCurrentWeapon(double x)
    {
        Debug.Log("Hello???");

        if (this.currentWeapon == null) 
            return false;

        this.currentWeapon.FlipWithVerticalMirror(x);
        
        return true;
    }
}
