using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }
    public static MossGiant instance;


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
