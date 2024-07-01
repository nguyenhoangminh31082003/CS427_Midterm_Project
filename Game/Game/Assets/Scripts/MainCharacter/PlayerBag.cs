using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{

    private double totalWeight;
    [SerializeField] private Weapon currentWeapon;
    private List<GameObject> weapons;

    // Start is called before the first frame update
    void Start()
    {
        this.totalWeight = 0;

        this.weapons = new List<GameObject>();

        this.currentWeapon = null;

        foreach (Transform child in this.transform)
        {
            Debug.Log("Child GameObject: " + child.gameObject.name);

            Weapon weapon = child.GetComponent<Weapon>();
            if (weapon != null && weapon is Weapon)
            {
                Debug.Log(child.name + " is a Weapon or a subclass of Weapon.");
            }
            else
            {
                Debug.Log(child.name + " is not a Weapon or a subclass of Weapon.");
            }
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
}
