using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective : Entity
{
    protected override void OnDie()
    {
        base.OnDie();
        GameManager.main.End(false);
    }
    protected override void OnHeal()
    {
        throw new NotImplementedException();
    }
    protected override void OnHurt()
    {
        base.OnHurt();
    }
}
