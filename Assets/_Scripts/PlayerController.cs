using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    [Header("Keys:")]
    [SerializeField] private string jumpKey;

    #endregion
    #region Hide in editor

    private bool grounded;

    #endregion

    void Update()
    {
        // check ground
        CheckGround();

        float horizontal = Input.GetAxis("Horizontal") * speed;
        if (grounded == false)
        {
            horizontal = Mathf.Lerp(rBody.velocity.x, horizontal, airModifier);
        }

        float vertical = rBody.velocity.y;

        if (Input.GetButtonDown(jumpKey) && grounded)
        {
            vertical = jumpVelocity;
        }

        rBody.velocity = new Vector2(horizontal, vertical);

    }



    private void CheckGround()
    {
        grounded = Physics2D.OverlapBox(jumpTransform.position, jumpArea, 0, jumpMask) != null;
    }
}
