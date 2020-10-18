using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatformerController : PhysicsObject
{

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7;
        public float coolDown = 0.5f;
        public float borderLeft = -9.0f;
        public float borderRight = 9.0f;

        public int temp = 36;
        public Transform scoreObject;

        public string goscene; 
    private UnityEngine.UI.Text score;

    public KeyCode keyLeft;
    public KeyCode keyRight;
    public KeyCode keyJump;
    public KeyCode keyShoot;

    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float coolDownStart = 0f;

    private GameObject enemy;
    public string enemyString = ""; 


    void Start()
    {
        score = scoreObject.GetComponent<UnityEngine.UI.Text>();
        score.text = temp + "C";
        enemy = GameObject.Find(enemyString);
    }
    // Use this for initialization
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;
            // Firing code
            if (Input.GetKey(keyShoot))
            {
                if (Time.time > coolDownStart + coolDown)
                {
                    coolDownStart = Time.time;
                    score = scoreObject.GetComponent<UnityEngine.UI.Text>();
                    score.text = temp + "C";
                Fire();
                }
            }

       if (Input.GetKey(keyLeft)) 
        { 
           if(transform.position.x > borderLeft)
           { 
            move.x = -1;
           }
        }
        if (Input.GetKey(keyRight))
        {
            if (transform.position.x < borderRight)
            {
                move.x = 1;
            }
        }

        if (Input.GetKeyDown(keyJump) && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetKeyUp(keyJump))
        {
            if (velocity.y > 0)
            {
                velocity.y = velocity.y * 0.5f;
            }
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (!flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

        targetVelocity = move * maxSpeed;
    }

    void Fire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            new Quaternion(0, 0, bulletSpawn.eulerAngles.z, 0));
        if (enemy.transform.position.x < transform.position.x)
        {
            bullet.GetComponent<Snowball>().setDir(-1.0f);
        }
        if (enemy.transform.position.x >= transform.position.x)
        {
            bullet.GetComponent<Snowball>().setDir(1.0f);
        }
        // Destroy the bullet after 5 seconds
        Destroy(bullet, 5.0f);
    }

    public void hit() 
    {
        temp--;
        score = scoreObject.GetComponent<UnityEngine.UI.Text>();
        score.text = temp + "C";
        if (temp <= 27)
        {
            Debug.Log("Ded");
            Application.LoadLevel(goscene);
        }
    }
}