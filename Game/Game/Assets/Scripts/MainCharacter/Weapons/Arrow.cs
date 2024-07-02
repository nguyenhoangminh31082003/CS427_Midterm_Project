using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Weapon
{
    [SerializeField] private double MAXIMUM_SPEED;
    [SerializeField] private double damageCausedPerHit;

    public enum ArrowState
    {
        NOT_USED_YET,
        CURRENTLY_USED,
        USED
    }

    private ArrowState arrowStatus;

    // Start is called before the first frame update
    protected override void Start()
    {

        base.Start();

        this.arrowStatus = ArrowState.NOT_USED_YET;

        this.IncreaseNumber(1);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
