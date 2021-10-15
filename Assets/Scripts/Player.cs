using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header ("Player Movement")]
    public float speed;
    private float move;
    public float jumpforce;
    public bool isJumping;
    public bool isdead;
    public bool ismove;

    private float jumptimecounter;
    public float jumpTime;
    private bool jumping;
    
    [Header ("GAMEOVER")]
    public GameObject Gameover;
    public float waitTimeforgameover = 4f;

    [Header("Pause Menu")]
    public GameObject PauseMenu;
    public bool Gameispaused = false;

    [Header("Complete Game")]
    public GameObject CompleteLevel;
    public bool levelistrigger;

    [Header ("AUDIO")]
    public AudioSource caughtaudio;//gameover audio
    public AudioSource jumpaudio; //jump
    public AudioSource Collectedsound; //collecting tusker
    public AudioSource HeartbeatSound;
    public AudioSource Countdownsound;
    public AudioSource levelcompleted;

    [Header ("COUNTDOWN")]
    public Text Countdowntext;
    public int count;
    public Text RunTimerText;
    float runtime = 0f;

    Rigidbody2D rbody;
    Animator anim;

    [SerializeField]
    private enemy police;
    [SerializeField]
    private Camera_FollowChar cam;

    [Header ("COLLECTABLES")]
    [SerializeField]
    private int collectedTusker;
    public Text TuskerText;

    // Use this for initialization
    public void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collectedTusker = 0;
        StartCoroutine(Countdown());
        ismove = false;
        isdead = false;
    }

    IEnumerator Countdown()
    {
        ismove = false;
        police.isfollowplayer = false;

        while (count > 0)
        {
            Countdowntext.text = count.ToString();

            yield return new WaitForSeconds(1f);

            count--;

            Countdownsound.Play();
        }

        Countdowntext.text = "GO!";

        yield return new WaitForSeconds(1f);

        //enable player and enemy movement
        ismove = true;
        police.isfollowplayer = true;
        HeartbeatSound.Play();
        Countdowntext.gameObject.SetActive(false);

    }

    public void FixedUpdate()
    {
        //movement condition
        if (ismove) //ismove is set to true
        {
            Movement();
            jump();

        }
        else
        {
            //no movement in y and x axis
            //rbody.velocity = Vector2.zero;
            //isdead = true;

        }
    }

    public void Update() 
    {
        Animations();
        GameOver();
        Timer();
        Killzone();
        Levelcomplete();
        PauseUI();
        
        //tusker ui collected
        TuskerText.text = GetCollected().ToString();
    }
   

    public void Timer()
    {
        if (ismove)
        {
            runtime += Time.deltaTime;

            int seconds = (int)(runtime % 60);
            int minutes = (int)(runtime / 60) % 60;
            int hours = (int)(runtime / 3600) % 24;

            string timestring = string.Format("{0:0}:{1:00}:{2:00}", hours, minutes, seconds);
            RunTimerText.text = timestring;
        }
    }

    public void Movement()
    {

        move = Input.GetAxisRaw("Horizontal");
        rbody.velocity = new Vector2(move * speed, rbody.velocity.y);
        
        if (move < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (move > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }
    public void jump()
    {
        if (Input.GetKey(KeyCode.Space) && !isJumping) //Input.GetButtonDown("Jump")
        {
            isJumping = true;
            jumping = true;
            jumptimecounter = jumpTime;
            rbody.velocity = Vector2.up * jumpforce;
            
            //rbody.AddForce(new Vector2(rbody.velocity.x, jumpforce));
            //play jump audio
            jumpaudio.Play();
        }

        if (Input.GetKey(KeyCode.Space) && jumping == true) 
        {
            if(jumptimecounter > 0)
            {
                rbody.velocity = Vector2.up * jumpforce;
                jumptimecounter -= Time.deltaTime;
            }
            else
            {
                jumping = false;
            }
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            jumping = false;
        }

    }

    public void Levelcomplete()
    {
        if(levelistrigger == true)
        {
            //set the levelcomplete to active
            CompleteLevel.SetActive(true);

            HeartbeatSound.Stop();

            ismove = false;
            police.isfollowplayer = false;
        }
        
    }
    public void PauseUI()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Gameispaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        
    }
    public void Resume()
    {
        Gameispaused = false;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void Pause()
    {
        Gameispaused = true;
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;

            rbody.velocity = Vector2.zero;
        }

        if (other.gameObject.tag.Equals("Enemy"))
        {
            isdead = true;

            //play caught audio
            caughtaudio.Play();
        }
    }

    public void Killzone()
    {
        if (police.isplayerdead == true)
        {
            isdead = true;  
        }
    }

    public void Animations()
    {
        anim.SetFloat("movement", Mathf.Abs(move));
        anim.SetBool("isjumping", isJumping);
        anim.SetBool("death", isdead);
    }

    //GameOver
    private void GameOver()
    {
        if(isdead == true)
        {
            Invoke("ShowGameOverScreen", waitTimeforgameover);
            ismove = false;
           // HeartbeatSound.Stop();
        }
    }
    private void ShowGameOverScreen()
    {
        Gameover.SetActive(true);
    }
    
    
    public int GetCollected()
    {
        return collectedTusker;
    }
    //collectables
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //which tag
        if (collision.CompareTag("Tusker"))
        {
            //play collect audio
            Collectedsound.Play();
            //add
            collectedTusker++;
            //destroy
            Destroy(collision.gameObject);
        }

        if(collision.CompareTag("LevelComplete"))
        {
            //play Level complete sound
            levelcompleted.Play();

            //Set the Finish Game ui active
            levelistrigger = true;
        }
    }
}
