using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagger : State
{
    Player player;
    public Stagger(Player player) : base("Stagger")
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    
}
