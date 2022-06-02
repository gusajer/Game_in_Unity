using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movment : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 10f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius; 
    public LayerMask GroundLayer;
    private bool isTouchingGround;

    private Animator playerAni;
    private Vector3 respawnPoint;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        playerAni = GetComponent<Animator>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, GroundLayer );
        direction = Input.GetAxis("Horizontal");
        
        if (direction > 0f) {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(5f, 5f);

        }

        else if (direction < 0f) {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-5f, 5f);
        }

        else{
           player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        playerAni.SetFloat("speed", Mathf.Abs(player.velocity.x));
        playerAni.SetBool("jumping", isTouchingGround);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "death")
        {
            transform.position = respawnPoint;
        }
        else if (collision.tag == "Nextlvl")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            respawnPoint = transform.position;
        }
        else if (collision.tag == "prevLvl")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
            respawnPoint = transform.position;
        }
    }
}
