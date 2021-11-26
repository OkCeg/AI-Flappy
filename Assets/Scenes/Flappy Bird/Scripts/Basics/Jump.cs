using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//complete
public class Jump : MonoBehaviour
{
    private Rigidbody2D rb;
    public int jumpPower;
    public int limit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void CharacterJumpCheck()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CharacterJump();
        }
    }

    public void CharacterJump()
    {
        rb.velocity = Vector2.zero;
        rb.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
    }

    private void LimitVelocity()
    {
        if (rb.velocity.y > limit)
        {
            rb.velocity = new Vector2(0, limit);
        }
        else if (rb.velocity.y < -limit)
        {
            rb.velocity = new Vector2(0, -limit);
        }
    }

    private void Update()
    {
        CharacterJumpCheck();
        LimitVelocity();
    }
}
