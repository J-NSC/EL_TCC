using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    [SerializeField] float KnockBackForce;
    [SerializeField] Player player;

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if(player.rig.velocity.y < 0)
            {
                Debug.Log(player.rig.velocity.y);
                player.rig.AddForce(Vector2.up * KnockBackForce, ForceMode2D.Impulse);
                Destroy(other.gameObject);
            }
        }
    }
}
