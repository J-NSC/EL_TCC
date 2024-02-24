using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    bool isRed;
    bool isBall = false;
    bool isCueBall = false;
    public bool isDesroyed = false;
    bool isMoving = false;
    [SerializeField] float time = 0.3f;

    Rigidbody rb;

    QuestionManagerBilliard questionManagerBilliard;

    void OnEnable()
    {
        Stick.BallAppliedForce += OnBallAppliedForce;
    }

    void OnDisable()
    {
        Stick.BallAppliedForce -= OnBallAppliedForce;
    }

    void Start()
    {
        questionManagerBilliard = FindObjectOfType<QuestionManagerBilliard>();
        rb = GetComponent<Rigidbody>();
        Debug.Log(isMoving);
    }

 

    void FixedUpdate()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity = Vector3.Scale(rb.velocity, new Vector3(1, 0, 1));
        }

        
        
        if(isMoving)
        {
            if ((rb.velocity.magnitude == 0))
            {
                Debug.Log(rb.velocity.magnitude);
                ResetBall(.3f);
                isMoving = false;
            }
        }
    }

    public void OnBallAppliedForce()
    {
        isMoving = true;
    }
    
    public void MakeCueBall()
    {
        isCueBall = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cacapa"))
        {
            string name = other.gameObject.name.ToLower();
            
            questionManagerBilliard.OnQuestChecked(name);
            ResetBall(time);
        }
    }

    void ResetBall(float time)
    {
        // BallDestroye?.Invoke();
        isDesroyed = true;
        Destroy(this.gameObject, time);
       
    }

    
}
