using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    
    public delegate void EnemyIsDeathedHandle(bool death);
    public static event EnemyIsDeathedHandle enemyIsDeath;
    
    [SerializeField] List<Transform> points;
    [SerializeField] int nexId = 0 ;
    [SerializeField] int idChangeValue = 1; 

    [SerializeField] float speed;
    [SerializeField] Animator animEnemy;

    [SerializeField] AudioSource deathAudio;
    public bool enemyDeath = false;


    void Awake()
    {
        animEnemy = GetComponent<Animator>();
    }

    void OnEnable()
    {
        KnockBack.deathEnemy += AnimDeath;
        
    }

    void OnDisable()
    {
        KnockBack.deathEnemy -= AnimDeath;
    }


    void Reset() {
        init();
    }

    void init(){
        GetComponent<CapsuleCollider2D>().isTrigger = false;
        
        GameObject root = new GameObject(name + "_Root");

        root.transform.position = transform.position;

        transform.SetParent(root.transform);

        GameObject wayPoints = new GameObject("WayPoints");

        wayPoints.transform.SetParent(root.transform);
        wayPoints.transform.position = root.transform.position;

        GameObject p1 = new GameObject("Point1");
        GameObject p2 = new GameObject("Point2");

        p1.transform.SetParent(wayPoints.transform);
        p2.transform.SetParent(wayPoints.transform);

        p1.transform.position =  root.transform.position;
        p2.transform.position =  root.transform.position;

        points = new List<Transform>();
        points.Add(p1.transform); 
        points.Add(p2.transform);

    }


    void FixedUpdate() {
        MoveToNextPoint();
    }

    void MoveToNextPoint(){
        Transform goalPoint = points[nexId];

        if(goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        transform.position = Vector2.MoveTowards(transform.position , goalPoint.position , speed );

        if(Vector2.Distance(transform.position, goalPoint.position) < 1f){
            if(nexId == points.Count -1 ){
                idChangeValue -=1;
            }

            if(nexId == 0){
                idChangeValue =1;
            }

            nexId += idChangeValue;
        }
    }

    public void AnimDeath(){
        speed = 0;
        // deathAudio.Play();
        enemyDeath = true;
        animEnemy.SetTrigger("Death");
    }

    public void death ()
    {
        Destroy(this.gameObject);
    }
}
