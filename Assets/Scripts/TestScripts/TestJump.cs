using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJump : MonoBehaviour
{
    [SerializeField] Rigidbody2D rig;
    [SerializeField] LayerMask layer;
    [SerializeField] GameObject feetposition;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isGraudend = Physics2D.OverlapCircle(feetposition.transform.position, .3f, layer);
        if (Input.GetButtonDown("Jump") && isGraudend)
        {
            rig.velocity = Vector2.up * 10;
        }
    }
}
