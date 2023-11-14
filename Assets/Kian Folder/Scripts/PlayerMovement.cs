using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;

    private float onGroundTimer;
    private float oGravity;

    #region Movement/acceleration/physics
    [Header("MOVEMENT")]
    [SerializeField, Range(0f, 30f), Tooltip("Sets max player speed")]
    private float maxMovementSpeed = 5;
    [SerializeField, Range(0f, 5f), Tooltip("sets min player speed. If the player stops moving this is the speed they will come to a stop at.")]
    private float minWalkSpeed = 2;
    [SerializeField, Range(0f, 20f), Tooltip("When the player begins moving, this is the speed they start at")]
    public float oMoveSpeed = 5;
    [SerializeField, Range(0f, 10f), Tooltip("sets walk acceleration")] 
    private float walkAcceleration = 5;
    [SerializeField, Range(0f, 5f), Tooltip("Mess with this, but keep it around 1.2.")]
    private float velocityPower = 2f;

    private float targetSpeed;
    private float speedDifference;
    private float accelerationRate;
    private float movement;

    [HideInInspector] public float nMoveSpeed;
    [HideInInspector] public Rigidbody2D theRB;
    [HideInInspector] public Vector2 moveDir;
    [HideInInspector] public bool facingRight = true;
    [HideInInspector] public bool canMove = true;
    #endregion

    #region deceleration
    [Header("Deceleration")]
    [SerializeField, Range(0f, 10f)] private float oWalkDeceleration = 5;
    [SerializeField, Range(0f, 10f)] private float maxDeceleration = 5;
    [SerializeField, Range(0f, 10f)] private float minDeceleration = 1;
    [SerializeField, Range(0f, 5f)] private float jumpDecelerationIncrease = 1;
    [SerializeField, Range(0f, 5f)] private float decelerationDiminish = 1;
    [SerializeField] private float forcedDecelerationRaiseTimer;
    private float nWalkDeceleration;
    #endregion

    #region Camera variables
    [Header("CAMERA")]
    private Camera mainCam;
    #endregion

    #region Colliders
    [Header("COLLIDERS")]
    private CapsuleCollider2D capsuleCollider;
    public PhysicsMaterial2D slickMaterial;
    public PhysicsMaterial2D frictionMaterial;
    #endregion

    #region Jumping variables
    [Header("JUMPING")]
    [SerializeField, Range(0f, 20f), Tooltip("The power of the original jump.")] 
    private float oJumpForce = 5;
    private float nJumpForce;

    [SerializeField, Range(0f, 2f), Tooltip("How long holding the jump button has an effect.")]
    private float jumpTime = 1;
    [SerializeField, Range(0f, 2f),Tooltip("The amount of time the player can press jump before landing and still get the next jump.")]
    private float jumpLeeway = 1;
    private float leewayCounter;
    private bool doLeewayJump = false;

    [SerializeField, Range(0f, 2f), Tooltip("The distance below the player that will be checked for ground.")] 
    private float gCheckDist = 5;
    [SerializeField, Range(0f, 5f), Tooltip("Sets the players gravity to this when they fall after a jump")]
    private float postJumpGravity = 0;
    [Range(0f, 40f)]
    public float maxFallSpeed;
    [Tooltip("Sets the amount of jumps the player has. It does this at project startup, so it wont change during runtime!")]
    public int jumpsAvailable = 1;
    private int maxJumps;
    [SerializeField, Tooltip("DONT CHANGE THIS!")] 
    private LayerMask groundMask;

    private RaycastHit2D groundHit;
    private float jumpCounter;
    private bool isJumping;
    private bool isRising; //use for animations
    public bool isFalling; //use for animations
    #endregion

    #region Dashing variables
    /*
    [Header("DASHING")]
    public float dashRange;
    public LayerMask dLayerMask;
    public RaycastHit2D dashCheck;
    [SerializeField, Range(0f, 20f)] private float oDashForce = 5;
    [SerializeField, Range(0f, 5f)] private float powerDashSpeedMultiplier = 1;
    [SerializeField, Range(0f, 1f)] private float powerDashSpeedDiminish = 1;
    [SerializeField, Range(0f, 1f)] private float powerDashJumpHeightDiminish = 1;
    [SerializeField, Range(0f, 1f)] private float postDashVelocityMultX = 1;
    [SerializeField, Range(0f, 1f)] private float postDashVelocityMultY = 1;
    [SerializeField, Range(0f, 5f)] private float dashTimer = 1;


    private Vector3 preDashPos;
    private Vector2 dashDir;
    private Vector3 dashVector;
    private float dashCounter;

    [HideInInspector] public bool canDash = true;
    [HideInInspector] public bool isDashing = false;
    */
    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        maxJumps = jumpsAvailable;
        nWalkDeceleration = oWalkDeceleration;
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        theRB = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        oGravity = theRB.gravityScale;
    }

    private void FixedUpdate()
    {
        if (!canMove)
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
            return;
        }


        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");

        //gets the direciton we want to move in
        targetSpeed = moveDir.x * nMoveSpeed;
        //gets the difference between current velocity and wanted velocity
        speedDifference = targetSpeed - theRB.velocity.x;
        accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? walkAcceleration : nWalkDeceleration;
        movement = Mathf.Pow(Mathf.Abs(speedDifference) * accelerationRate, velocityPower) * Mathf.Sign(speedDifference);
        theRB.AddForce(movement * Vector2.right);


        //makes it so the player doesn't slide once their speed is less than a certain amount;
        if ((Mathf.Abs(theRB.velocity.x) < minWalkSpeed/* && theRB.velocity.x > -minWalkSpeed*/) && moveDir.x == 0)
        {
            theRB.velocity = new Vector2(0f, theRB.velocity.y);
        }

        if (moveDir.x == 0)
        {
            if (capsuleCollider.sharedMaterial == frictionMaterial && frictionMaterial.friction != 0.4f)
                frictionMaterial.friction = 0.4f;
        }
        else if (moveDir.x != 0 && frictionMaterial.friction != 0)
            frictionMaterial.friction = 0;


        /*
        //Change the gravity depending on situation
        if (isDashing)
        {
            theRB.gravityScale = 0;
        }

        //forces the dash to always go the same distance
        if (dashCounter > 0) dashCounter -= Time.fixedDeltaTime;

        else if (!isDashing)
        {
        */
            theRB.gravityScale = oGravity + postJumpGravity;
        //}

        //forces the jump to always go the same distance
        if (jumpCounter > 0)
            jumpCounter -= Time.fixedDeltaTime;

        //if the player is falling faster than the max speed it sets their speed to the max speed
        if (theRB.velocity.y < -maxFallSpeed)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, -maxFallSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;

        mainCam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);

        #region checking direction
        //sets rotation based on movement direction
        if (moveDir.x > 0 && !facingRight)
        {
            flip();
        }
        else if (moveDir.x < 0 && facingRight)
        {
            flip();
        }
        #endregion

        //if the player is grounded it resets their jumps, the onGroundTimer increases
        //(needed because it puts the deceleration back to max)
        if (isGrounded())
        {
            jumpsAvailable = maxJumps;
            onGroundTimer += Time.deltaTime;


            if (onGroundTimer >= forcedDecelerationRaiseTimer && nWalkDeceleration != maxDeceleration)
            {
                raiseDeceleration(1000f);
            }
        }
        //this makes sure to gets the direction they are dashing before they dash,
        //this helps make sure the player doesn't dash in a weird direction they weren't meaning to
        /*
        if (!isDashing)
        {
            dashDir = moveDir;
        }
        */
        //dash();

        jump();

    }

    #region dashing
    //private void dash()
    //{
    //    if (Input.GetButtonDown("Fire2") && moveDir != Vector2.zero && checkDashAvailable(true))
    //    {
    //        //audio

    //        dashDir.Normalize();
    //        dashCounter = dashTimer;
    //        isDashing = true;
    //        canDash = false;
    //        if (dashDir == Vector2.up || dashDir == Vector2.down)
    //        {
    //            nMoveSpeed = oMoveSpeed;
    //        }
    //    }

    //    if (dashCounter > 0)
    //    {

    //        theRB.velocity = new Vector3((oDashForce * dashDir.x), (oDashForce * dashDir.y), 0f);
    //    }

    //    else if (dashCounter <= 0)
    //    {
    //        //if the dash direction is anything except right or left it changes the speed so they don't get weird height
    //        if (isDashing && (dashDir != Vector2.left || dashDir != Vector2.right))
    //        {
    //            theRB.velocity = new Vector3(theRB.velocity.x * postDashVelocityMultX, theRB.velocity.y * postDashVelocityMultY, 0f);
    //        }
    //        isDashing = false;
    //        if (isGrounded() && checkDashAvailable(false))
    //        {
    //            canDash = true;
    //            dashDir = Vector2.zero;
    //        }
    //    }
    //}

    //check both TRUE is checking if they have the dash power AND they can dash.
    //check both FALSE is checking if they even have the dash power
    //private bool checkDashAvailable(bool checkBoth)
    //{
    //    if (checkBoth) return PowerController.instance.hasDashPower && canDash;
    //    else return PowerController.instance.hasDashPower;
    //}
    #endregion

    private void jump()
    {
        if ((Input.GetButtonDown("Jump") && jumpsAvailable > 0) || doLeewayJump)
        {
            nJumpForce = oJumpForce;
            doLeewayJump = false;
            leewayCounter = 0;
            if (isGrounded() && nMoveSpeed > oMoveSpeed)
            {
                raiseDeceleration(jumpDecelerationIncrease);
            }
            onGroundTimer = 0;
            if (moveDir.y < 0)
            {
                nJumpForce = oJumpForce / 1.5f;

            }

            jumpCounter = jumpTime;

            /*
            //if the player jumps while dashing it does the super dash
            if (isDashing)
            {
                //move speed and jump force get changed by a certain amount
                nMoveSpeed *= powerDashSpeedMultiplier + ((jumpsAvailable / maxJumps) * (powerDashSpeedDiminish - (maxJumps - jumpsAvailable)));
                //if speed is above max, set it equal to max speed
                if (nMoveSpeed > maxMovementSpeed)
                {
                    nMoveSpeed = maxMovementSpeed;
                }
                nJumpForce *= powerDashJumpHeightDiminish;
                //lowers the deceleration
                nWalkDeceleration -= decelerationDiminish;
                if (nWalkDeceleration < minDeceleration)
                {
                    nWalkDeceleration = minDeceleration;
                }
                canDash = true;
                isDashing = false;
            }
            */

            //this is for falling through platforms
            if (moveDir.y < 0 && isGrounded())
            {
                isJumping = false;
                isRising = false;
                isFalling = true;
            }
            else
            {
            isJumping = true;
            isRising = true;
            isFalling = false;
            jumpsAvailable--;
            }
        }

        //if the player continues to hold the jump button
        if (Input.GetButton("Jump"))
        {
            //if they arent grounded AND holding the button it increases the leewayCounter
            if (!isGrounded())
            {
                leewayCounter += Time.deltaTime;
            }
            //if they have been holding the button for less time than the leeway timer it lets them jump again
            else if (leewayCounter <= jumpLeeway)
            {
                doLeewayJump = true;
                //only does the leeway jump if the player touches the ground
                if (isGrounded())
                    jump();
            }

            //makes the player actually do the jumping
            if (jumpCounter > 0 && isJumping)
            {
                /*jumpCounter -= Time.deltaTime;*/
                theRB.velocity = new Vector2(theRB.velocity.x, nJumpForce);
                isRising = true;
            }
            //if the jumpcounter runs out of time it makes you fall
            else
            {
                isJumping = false;
                isRising = false;
                isFalling = true;
            }
        }

        //if the player lets go of the jump button
        if (Input.GetButtonUp("Jump"))
        {
            leewayCounter = 0;
            isJumping = false;
            isRising = false;
            isFalling = true;
        }
    }

    public bool isGrounded()
    {
        groundHit = Physics2D.BoxCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, 0f, Vector2.down, gCheckDist, groundMask);
        if (groundHit.collider != null && !isRising)
        {
            //only resets move speed if the deceleration is at the max
            if (nWalkDeceleration == maxDeceleration)
                nMoveSpeed = oMoveSpeed;
            theRB.gravityScale = oGravity;
            nJumpForce = oJumpForce;
            isFalling = false;

            if (groundHit.collider.tag == "Ramp")
            {
                capsuleCollider.sharedMaterial = frictionMaterial;
            }

            if (groundHit.collider.tag == "Ground")
            {
                capsuleCollider.sharedMaterial = slickMaterial;
            }

            return true;
        }

        return false;
    }

    private void flip()
    {
        //makes the facingRight bool become what it wasn't
        facingRight = !facingRight;

        //rotates the player (and all of its children) 180 degrees (this makes it look like the gun is behind the player as well)
        transform.Rotate(0f, 180f, 0f);
        nWalkDeceleration = oWalkDeceleration;

    }

    private void raiseDeceleration(float amount)
    {
        nWalkDeceleration += amount;
        if (nWalkDeceleration > maxDeceleration)
        {
            nWalkDeceleration = maxDeceleration;
        }
    }


}
