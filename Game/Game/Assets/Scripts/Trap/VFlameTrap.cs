using UnityEngine;

public class VFlameTrap : Trap
{
    protected override string AttackState => "vflamethrower";
    protected override string SleepState => "empty";

    // protected override void Start()
    // {
    //     base.Start();
    //     triggerInterval = 3.0f;  // For example, change the interval for FlameTrap
    // }
}
