using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    //distance when the player is caught
    public float catchdistance;
    public float speed;
    public float enemyjumpforce;

    public bool iscaught;
    public bool isjump;
    public bool isplayerdead;
    public bool isfollowplayer = false;

    public float Castingdistance;
    public float Killdistance;


    [SerializeField]
    public Transform target;
    public Transform detector;
    public Transform KillStick;
    public Player player;
    public AudioSource Gamesound;
    

    // Start is called before the first frame update
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = GetComponent<Player>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        
        isjump = false;
    }

    // Update is called once per frame
    public void FixedUpdate()
    {
        
        if (isfollowplayer == false)
        {
            anim.enabled = false;
            rb.velocity = Vector2.zero;
        }
        else
        {
            Move();
            anim.enabled = true;
        }
            jump();
            KillPlayer();
    }

    public void Move()
    {
        float distance = Vector2.Distance(transform.position, target.position);
                
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);


        if (distance < catchdistance)
        {
            anim.SetBool("caught", true);
            //dont move force
            speed = 0f;
            
        }
        else if (distance > catchdistance)
        {
            anim.SetBool("caught", false);
            //change speed of enemy
            speed = 62f;
            
        }


    }

    public void jump()
    {
        //if player jumps disable police from going up 
        /*if(player.isJumping)
        {
            rb.velocity = Vector2.zero;
        }*/
        //If enemy detects an obstacle at a certain range then make the enemy jump auto
        Vector2 EndPosition = detector.position + Vector3.right * Castingdistance;

        RaycastHit2D hit = Physics2D.Linecast(detector.position, EndPosition);

        if(hit.collider != null)
        {
            if(hit.collider.CompareTag("Obstacle"))
            {
                //rb.AddForce(new Vector2(rb.velocity.x, enemyjumpforce));
                rb.velocity = Vector2.up * enemyjumpforce;
            }
            else
            {
                rb.velocity = Vector2.zero;
            }

            Debug.DrawLine(detector.position, EndPosition, Color.blue);
        }
    }

    public void KillPlayer()
    {

        Vector2 EndPos = KillStick.position + Vector3.right * Killdistance;

        RaycastHit2D hit = Physics2D.Linecast(KillStick.position, EndPos);

       if (hit.collider != null)
       {
            if (hit.collider.CompareTag("Player"))
            {
                //player is dead
                isplayerdead = true;
                
            }
            else
            {
                //player is still alive
                isplayerdead = false;

            }

            Debug.DrawLine(KillStick.position, EndPos, Color.red);
       }
    }
}
