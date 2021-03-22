using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
   
    
    public void OnDisable()
    {
        gameObject.SetActive(false);
    }
    public void OnEnable()
    {
        gameObject.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.gameObject.tag=="Pipes")
        { 
        Destroy(collision.gameObject);
        }
       

    }


}
