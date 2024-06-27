using UnityEngine;

public class HFlameTrap : Trap
{
    protected override string AttackState => "hflamethrower";
    protected override string SleepState => "empty";

    // protected override void Start()
    // {
    //     base.Start();
    //     triggerInterval = 3.0f;  // For example, change the interval for FlameTrap
    // }
}
