using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : Trap
{
    protected override string AttackState => "peak";
    protected override string SleepState => "idle";
}

