using UnityEngine;     
using System.Collections.Generic;

public class Snowball : MonoBehaviour
{
    public float speed; 
    public GameObject hitParticles;
    public string targettedPlayer;
    public float angle; 

    private Rigidbody2D rb2D;
    private Animator animator;
    public float direction = 1.0f; 

    void OnEnable()
    {
        	rb2D = GetComponent<Rigidbody2D>();
            animator = transform.GetComponentInChildren<Animator>();
    }

    void Update() 
    {

        rb2D.MovePosition(transform.position + new Vector3(speed * direction,0, 0) * Time.deltaTime);
        //rb2D.MovePosition(transform.position + new Vector3(Mathf.Tan(DegreeToRadian(angle)) * speed,speed, 0) * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Quaternion rot = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 0);
        Vector2 pos = transform.position;
        if (collision.name == targettedPlayer)
        {
            GameObject exp = Instantiate(hitParticles, pos, rot);
            GameObject player = GameObject.Find(targettedPlayer);
            player.GetComponent<PlayerPlatformerController>().hit();
            Destroy(exp, 1.0f);
            Destroy(gameObject);
        }
    }

    private float DegreeToRadian(float angle)
    {
        return Mathf.PI * angle / 180.0f;
    }
    private float RadianToDegree(float angle)
    {
        return angle * (180.0f / Mathf.PI);
    }
    public void setDir(float value) 
    {
        direction = value;
    }
}
