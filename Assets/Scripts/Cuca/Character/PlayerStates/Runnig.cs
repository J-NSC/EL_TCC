using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runnig : State
{
    Player player;
    public Runnig(Player player) : base("Runnig")
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

    public override void Update()
    {
        base.Update();

        if (player.hasJumpInput)
        {
            player.stateMachine.ChangeState(player.jumping);
            return;
        }
        
        if (player.dir == 0)
        {
            player.stateMachine.ChangeState(player.idle);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        player.rig.velocity = new Vector2(player.dir * player.speed, player.rig.velocity.y);
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }
}
