using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorDirection : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Vector3 direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent != null)
        {
            Quaternion parentRotation = transform.parent.rotation;
            transform.rotation = parentRotation;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.forward);
        Gizmos.color = Color.black;
        Gizmos.DrawLine(transform.position, transform.right);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.up);
        
    }
}
