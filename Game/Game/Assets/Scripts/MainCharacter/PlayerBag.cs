using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBag : MonoBehaviour
{

    private double totalWeight;

    // Start is called before the first frame update
    void Start()
    {
        totalWeight = 0;
    }

    public double GetTotalWeight()
    {
        return totalWeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
