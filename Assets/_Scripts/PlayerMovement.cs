using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Show in editor

    [Header("Physix:")]
    [SerializeField] private Rigidbody2D rBody;
    [SerializeField] private float speed;
    [SerializeField] private float jumpVelocity;

    [Header("Jump:")]
    [SerializeField] private Vector2 jumpArea;
    [SerializeField] private LayerMask jumpMask;
    [SerializeField] private Transform jumpTransform;
    [SerializeField] private float airModifier;
    [SerializeField] private float groundModifier;
    [SerializeField] private int maxAirJumpCount;

    [Header("Keys:")]
    [SerializeField] private string jumpKey;

    [Header("Timers:")]
    [SerializeField] private float jumpPressMargin;
    [SerializeField] private float coyoteTime;
    [SerializeField] private float jumpHoldTime;
    
    #endregion
    #region Hide in editor

    private bool grounded;
    private bool jumping;
    private int jumpCount;

    private Timer midAirJumpPressTimer;
    private Timer coyoteTimeTimer;
    private Timer jumpHoldTimeTimer;

    private float _horizontal;
    private float _vertical;

    #endregion

    private void Start()
    {
        midAirJumpPressTimer = new Timer(jumpPressMargin, false);
        coyoteTimeTimer = new Timer(coyoteTime, false);
        jumpHoldTimeTimer = new Timer(jumpHoldTime, false);
    }

    void Update()
    {
        // CheckGround();
        
        coyoteTimeTimer.Tick(Time.deltaTime);
        midAirJumpPressTimer.Tick(Time.deltaTime);
        jumpHoldTimeTimer.Tick(Time.deltaTime);

        float horizontal = Input.GetAxis("Horizontal") * speed;

        if (grounded == false)
        {
            horizontal = Mathf.Lerp(rBody.velocity.x, horizontal, airModifier);
        }
        else
        {
            horizontal = Mathf.Lerp(rBody.velocity.x, horizontal, groundModifier);
        }

        float vertical = rBody.velocity.y;

        if (MidAirJumpPress() || CoyoteTime())
        {
            jumping = true;
            jumpHoldTimeTimer.Reset();

        }
        else if (jumpCount > 0 && grounded == false && Input.GetButtonDown(jumpKey))
        {
            jumping = true;
            jumpHoldTimeTimer.Reset();
            jumpCount--;
        }

            if (Input.GetButton(jumpKey) && jumping)
        {
            if (jumpHoldTimeTimer.Elapsed == false)
            {
                vertical = jumpVelocity;
                Debug.Log($"Elapsed Time {jumpHoldTimeTimer.ElapsedTime} s.");
            }
        }
        else if(Input.GetButtonUp(jumpKey) || jumping == false)
        {
            jumping = false;
        }



        rBody.velocity = new Vector2(horizontal, vertical);
        //_horizontal = horizontal;
        //_vertical = vertical;
    }
    private void FixedUpdate()
    {
        CheckGround();
        //rBody.velocity = new Vector2(_horizontal, _vertical);
    }

    private void CheckGround()
    {
        grounded = Physics2D.OverlapBox(jumpTransform.position, jumpArea, 0, jumpMask) != null;

        if (grounded)
        {
            coyoteTimeTimer.Reset();
            jumpCount = maxAirJumpCount;
        }
    }

    private bool CoyoteTime()
    {
        return Input.GetButtonDown(jumpKey) && coyoteTimeTimer.Elapsed == false;
    }

    private bool MidAirJumpPress()
    {
        if (Input.GetButtonDown(jumpKey))
        {
            if (grounded)
            {
                return true;
            }
            else
            {
                midAirJumpPressTimer.Reset();
            }
        }
        else if (midAirJumpPressTimer.Elapsed == false && grounded)
        {
            return true;
        }

        return false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumping = false;
    }

}
