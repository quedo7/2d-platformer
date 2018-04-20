using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private Rigidbody2D myRigidBody;
    [SerializeField]
    private float MovementSpeed;
    private bool facingRight;
    private Animator myAnimator;
    private bool attack;
    private bool slide;
    private bool isGrounded;
    private bool Jump;
    [SerializeField]
    private bool airControl;
    [SerializeField]
    private Transform[] groundpoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private LayerMask whatIsGround;
    // Use this for initialization
    void Start () {
        
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame

    private void Update()
    {
        HandleInput();    
    }

    void FixedUpdate () {
        float horizontal = Input.GetAxis("Horizontal");
        isGrounded = IsGrounded();
        HandleMovement(horizontal);
        Flip(horizontal);
        HandleAttacks();
        HandleLayers();
        ResetValues();
	}

    private void HandleMovement(float horizontal) {

        myAnimator.SetFloat("speed",Mathf.Abs(horizontal));

        if (myRigidBody.velocity.y < 0) { myAnimator.SetBool("Land", true); }

        if (!myAnimator.GetBool("slide") && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack") && (isGrounded || airControl)) {
            myRigidBody.velocity = new Vector2(horizontal*MovementSpeed,myRigidBody.velocity.y);
        }
            
        if (isGrounded && Jump) {
            isGrounded = false;
            myRigidBody.AddForce(new Vector2(0, jumpForce));
            myAnimator.SetTrigger("jump");
        }
            
        if (slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("slide"))
        {
            myAnimator.SetBool("slide", true);
        } else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("slide")) {
            myAnimator.SetBool("slide", false);
        }
        
    }

    private void HandleAttacks() {
        if (attack && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("attack")) {
            myAnimator.SetTrigger("attack");
            myRigidBody.velocity = Vector2.zero;
        }

        

        }

    private void HandleInput() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            attack = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }
    }

    private void ResetValues()
    {
        attack = false;
        slide = false;
        Jump = false;
 
    }


    private void Flip(float horizontal) {
        if (horizontal < 0 && !facingRight || horizontal > 0 && facingRight  ) {
            facingRight = !facingRight;
            Vector2 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
       }
    }

    private bool IsGrounded() {
        if (myRigidBody.velocity.y <= 0)
        {

            foreach (Transform point in groundpoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                Debug.Log(colliders.Length);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {
                      
                        myAnimator.ResetTrigger("jump");
                        myAnimator.SetBool("Land",false);
                        return true;
                    }
                }
            }
        }
      return false;
    }

    private void HandleLayers() {
        if (!isGrounded) {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
