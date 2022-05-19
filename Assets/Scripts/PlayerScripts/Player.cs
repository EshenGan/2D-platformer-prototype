using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    public List<GameObject> lives;
    [SerializeField] float fallmultiplier = 2.5f;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr;
    private Vector2 playervelocity;
    private int groundMask = 1 << 8;
    private bool canJump = true;
    [SerializeField] private float movementspeed = 6f;
    private bool isStay;
    private bool isLeft;
    private int isStayKey;
    private int canJumpKey;
    public bool goInPortal;
    [SerializeField] private int setPortalTimer;
    [SerializeField] private int portaltimerr;
    public bool canUsePortal;
    //[SerializeField] private float startingX;
    //[SerializeField] private float startingY;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        isStayKey = Animator.StringToHash("isStay");
        canJumpKey = Animator.StringToHash("canJump");
        canUsePortal = true;
        portaltimerr = setPortalTimer;
        for (int i = 0; i < lives.Count; i++)
        {
            lives[i].SetActive(true);
        }
    }
    private void OnEnable()
    {
        //rb.mass = 2f;
        //rb.gravityScale = 4f;
        
        transform.position = new Vector2(0, 0);
        playervelocity = Vector2.zero;
        canUsePortal = true;
        portaltimerr = setPortalTimer;
        for (int i = 0; i < lives.Count; i++)
        {
            lives[i].SetActive(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool(isStayKey, isStay);
        anim.SetBool(canJumpKey, canJump);
        sr.flipX = isLeft;
    }

    private void FixedUpdate()
    {
        playervelocity = Vector2.zero;
        isStay = true;

        moveleftright();
        jump();
        portal();


        rb.velocity = new Vector2(playervelocity.x, rb.velocity.y);


    }

    private void moveleftright()
    {

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {

            isStay = false;
            isLeft = true;
            playervelocity.x -= movementspeed;

        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            isLeft = false;
            isStay = false;
            playervelocity.x += movementspeed;
        }

    }

    private void jump()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (canJump)
            {
               
                rb.velocity = Vector2.up * 21f;
                canJump = false;

            }
        }
        if (Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), -transform.up, 2.5f, groundMask))
        {
            canJump = true;
        }

        if (rb.velocity.y < 0) //
        {

            rb.velocity += Vector2.up * Physics2D.gravity.y * fallmultiplier * Time.deltaTime;

        }

    }


    private void portal()
    {
        if (canUsePortal == true)
        {
            portaltimerr = setPortalTimer;
        }
        else if (canUsePortal == false)
        {
            portaltimerr--;

            if (portaltimerr <= 0)
            {
                canUsePortal = true;
            }
        }

        if (Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.UpArrow))
        {
            goInPortal = true;
        }
        else if (!Input.GetKey(KeyCode.V) || !Input.GetKey(KeyCode.UpArrow))
        {
            goInPortal = false;
        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("outofbound"))
        {

            
            for (int i = 0; i < lives.Count; i++)
            {
                lives[i].SetActive(false);
            }
            FindObjectOfType<Level_Manager>().GameOver();

        }

        if (collision.gameObject.CompareTag("spike"))
        {

            //reduce life count 1 by 1
            for (int i = 0; i < lives.Count; i++)
            {

                if (lives[i].activeSelf == true)
                {
                    lives[i].SetActive(false);

                    break;
                }
            }
            FindObjectOfType<Level_Manager>().GameOver();
            if (isLeft)
            {
                transform.position = new Vector2(transform.position.x + 2f, transform.position.y);


            }
            else if (!isLeft)
            {
                transform.position = new Vector2(transform.position.x - 2f, transform.position.y);


            }
            

        }
        if (collision.gameObject.CompareTag("fallingspike")) {

            for (int i = 0; i < lives.Count; i++)
            {

                if (lives[i].activeSelf == true)
                {
                    lives[i].SetActive(false);

                    break;
                }
            }
            FindObjectOfType<Level_Manager>().GameOver();

            
        }
        if (collision.gameObject.CompareTag("pillar"))
        {
            for (int i = 0; i < lives.Count; i++)
            {
                lives[i].SetActive(false);
            }
            FindObjectOfType<Level_Manager>().GameOver();



        }
        if (collision.gameObject.CompareTag("crystal1")) {
            
            SceneManager.LoadScene(1);
        }

        if (collision.gameObject.CompareTag("crystal2")) {
            
            SceneManager.LoadScene(2);
        }

        if (collision.gameObject.CompareTag("crystal3"))
        {
            FindObjectOfType<Level_Manager>().GameWon();

        }




    }
}
