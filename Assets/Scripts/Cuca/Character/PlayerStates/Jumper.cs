using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : State
{
    Player player;
    bool hasJump;
    
    public Jumping(Player player) : base("Jumping")
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            hasJump = false;
        }
        
        if (player.isGrounded && hasJump)
        {
            player.stateMachine.ChangeState(player.idle);
            hasJump = false;
            return;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();

        bool hasDoubleJumpPowerUp = player.characteSO.powerUps[0].actived;

        if (!hasJump && !hasDoubleJumpPowerUp)
        {
            hasJump = true;
            if (player.characteSO.CountJump >= 0)
            {
                ApplyImpulse();
            }
        }
        
        if (!hasJump && hasDoubleJumpPowerUp)
        {
            if (player.characteSO.CountJump >= 0)
            {
                ApplyImpulse();
            }
            hasJump = true;
        }
        
        player.rig.velocity = new Vector2(player.dir * player.speed, player.rig.velocity.y);
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
    }

    public void ApplyImpulse()
    {
        player.rig.velocity = new Vector2(player.rig.velocity.x, player.jumpForce);
    }
}
