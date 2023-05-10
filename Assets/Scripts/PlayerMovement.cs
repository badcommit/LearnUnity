using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    private Animator anim;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontalInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public async void FaceLeft()
    {
        horizontalInput = -0.1f;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        await Task.Delay(500);
        horizontalInput = 0;
    }

    public async void FaceRight()
    {
        horizontalInput = 0.1f;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        await Task.Delay(500);
        horizontalInput = 0;
    }
    public void MoveLeft()
    {
        Debug.Log($"Moveleft !!!!!!!!!!");
        horizontalInput = -1f;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        StartCoroutine(Trigger(3));
    }

    public void MoveRight()
    {
        Debug.Log($"Moveright !!!!!!!!!!");
        horizontalInput = 1f;
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
        StartCoroutine(Trigger(3));
    }

    private void Awake(){
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }



    // Update is called once per frame
    void Update()
    {
        //horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        if(horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if(horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        anim.SetBool("run", horizontalInput != 0);
        anim.SetBool("grounded", isGrounded());

        if(wallJumpCoolDown < 0.2f)
        {
           

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if(onWall() && !isGrounded())
            {
                body.gravityScale = 0;
            } else
            {
                body.gravityScale = 3;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                //Jump();
            }
        } else
        {
            wallJumpCoolDown += Time.deltaTime;
        }
        

       
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpSpeed);
            anim.SetTrigger("jump");
        } else if(onWall() && !isGrounded())
        {
            if(horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            } else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCoolDown = 0;
           
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }

    IEnumerator Trigger(int times)
    {
        for(int i=0; i < times; i++)
        {
            yield return new WaitForSeconds(0.5f);
        }
        horizontalInput = 0;
        //code here will execute after 5 seconds
    }


}
