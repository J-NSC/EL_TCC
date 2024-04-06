using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : State
{
    Player player;
    bool hasJump;
    
    public Jumper(Player player) : base("jumper")
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        hasJump = false;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
           
        if (player.isGrounded && hasJump)
        {
            player.stateMachine.ChangeState(player.idle);
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        if (!hasJump)
        {
            hasJump = true;
            ApplyImpulse();
        }
        
        player.rig.velocity = new Vector2(player.dir * player.speed, player.rig.velocity.y);
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void ApplyImpulse()
    {
        player.rig.velocity = Vector2.up * player.jumpForce;
    }
}
