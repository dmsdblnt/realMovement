using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    [SerializeField] float maxMove = 10;
    [SerializeField] private Rigidbody2D rBody;
    [SerializeField] private float speed = 2;

    Vector3 startPosition;
    int sign = 1;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if((startPosition.x - transform.position.x) * - sign >= maxMove)
        {
            sign = -sign;
        }
        rBody.velocity = new Vector2(sign * speed, rBody.velocity.y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            var smallerY = collision.contacts.Min(x => x.point.y);
            
            if (smallerY >= transform.position.y)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                Debug.Log("Game Over");
            }
            
        }
    }
}
