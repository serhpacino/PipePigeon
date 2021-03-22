using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public float force;
    public Bomb bomb;

    private const float DOUBLE_CLICK = .2f;
    private ProjectHelper projectHelper;
    
    
    private float timeClick;
       void Awake()
    {
        projectHelper = Camera.main.GetComponent<ProjectHelper>();
        
    }

    void Update()
    {
        
     if(Input.GetMouseButtonDown(0))
        {
            float lasttimeClick = Time.time - timeClick;
            if (lasttimeClick <= DOUBLE_CLICK)
            {
                if (projectHelper.bomb <= 0)
                {
                    Debug.Log("You dont have bombs");
                }
                else
                {
                    StartCoroutine(ActivateBomb());
                    
                    projectHelper.bomb--;
                }
            }
            else
            {
                rigidBody.AddForce(Vector2.up * (force - rigidBody.velocity.y), ForceMode2D.Impulse);
                rigidBody.MoveRotation(rigidBody.velocity.y * 2.0F);
                
            }
            timeClick = Time.time;
        }
    }
 
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(message: "Game Over");
        projectHelper.gameoverMenu.gameObject.SetActive(true);

        Time.timeScale = 0.0F;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(message: "You gain Point");
        projectHelper.score++;
        if (projectHelper.score%10==0)
        {
            if (projectHelper.bomb < 3)
            {
                Debug.Log("You Gain Bomb");
                projectHelper.bomb++;
            }
         
        }
       
    }
    IEnumerator ActivateBomb()
    {
        bomb.OnEnable();
        yield return new WaitForSeconds(0.5f);
        bomb.OnDisable();
    }
    

}
