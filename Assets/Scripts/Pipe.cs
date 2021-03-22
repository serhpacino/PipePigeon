using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    

    private float speed = 1.5f;
    
    void Start()
    {
        Vector2 position = transform.position;
        position.y = Random.Range(-2F, 0F);
        transform.position = position;
            Destroy(gameObject, 6.0F);
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, transform.position - transform.right, speed * Time.deltaTime);
    }
   
}
