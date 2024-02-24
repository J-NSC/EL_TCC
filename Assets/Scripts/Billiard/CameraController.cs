using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    float horizontalInput;
    Transform cueBall;
    
    void Start()
    {
        ResetCamera();
    }

    void Update()
    {
        if (cueBall != null)
        {
            horizontalInput = Input.GetAxis("Mouse X") + rotationSpeed * Time.deltaTime;
            
            transform.RotateAround(cueBall.position, Vector3.up , horizontalInput);
        }        
    }

    public void ResetCamera()
    {
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }
}
