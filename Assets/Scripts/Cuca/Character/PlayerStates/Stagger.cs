using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stagger : State
{
    Player player;
    float duration = 0.6f;
    float pingPongDuration = 0.1f;
    public Stagger(Player player) : base("Stagger")
    {
        this.player = player;
    }

    public override void Enter()
    {
        base.Enter();
        player.cucaSprite.color = Color.red;
        player.StartCoroutine(StaggerTime());
    }

    public override void Exit()
    {
        base.Exit();
        player.cucaSprite.color = Color.white;
    }

    IEnumerator StaggerTime()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            float t = Mathf.PingPong(Time.time, pingPongDuration) / pingPongDuration;
            player.cucaSprite.color = Color.Lerp(Color.red, Color.white, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        player.stateMachine.ChangeState(player.idle);
    }
}
