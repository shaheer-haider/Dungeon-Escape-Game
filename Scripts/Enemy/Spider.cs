using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public int Health { get; set; }
    public static Spider instance;


    public override void Init()
    {
        base.Init();
        Health = base.health;
        health = Health;
        if (instance == null)
        {
            instance = this;
        }
    }

}
