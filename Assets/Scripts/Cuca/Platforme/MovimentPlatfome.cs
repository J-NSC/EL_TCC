using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentPlatfome : MonoBehaviour
{
    [SerializeField] float speed = 3f;
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] bool moveDown = true;
    [SerializeField] bool moveRight = true;
    [SerializeField] bool TypePlataform = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(TypePlataform){
            PlataformVertical();
        }else{
            PlataformHorizontal();
        }
        

    }

    void PlataformVertical(){
        if(transform.position.y > pointA.position.y){
            moveDown = true;
        }if(transform.position.y < pointB.position.y)
            moveDown = false;

        if(moveDown)
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        else    
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);  
    }

    public void PlataformHorizontal(){
        if(transform.position.x < pointA.position.x)
            moveRight = true;
        if(transform.position.x > pointB.position.x)
            moveRight = false;

        if(moveRight)
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime , transform.position.y );
        else    
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime,  transform.position.y );
    }


    // private void OnTriggerEnter2D(Collider2D other) {
    //     if(other.CompareTag("Player")){
    //         StartCoroutine(ChangeSide());
    //         Debug.Log("x");
    //     }
    // }

    // private IEnumerator ChangeSide(){
    //     yield return new WaitForSeconds(2);
    //     efeito.rotationalOffset = 180;
    // }
}
