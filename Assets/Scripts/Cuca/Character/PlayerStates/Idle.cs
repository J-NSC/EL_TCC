using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle  : State
{
    Player player;
    public Idle(Player player) : base("Idle")
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        player.rig.velocity = Vector2.zero;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (player.hasJumpInput)
        {
            player.stateMachine.ChangeState(player.jumping);
            return;
        }

        if (player.dir != 0)
        {
            player.stateMachine.ChangeState(player.runnig);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}
