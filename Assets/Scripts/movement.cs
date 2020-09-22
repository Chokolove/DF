using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private LayerMask ground;

    public Animator animator;
    public float Basespeed = 10f;
    public float JumpForce = 10f;

    private bool m_FacingRight = true;
    private Rigidbody2D rb;
    private Vector3 vel = Vector3.zero;
    private CircleCollider2D cc;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 1f)
        {
            if (m_FacingRight != true)
            {
                Flip();
            }
            animator.SetFloat("speed", Basespeed);
            Mov(Basespeed, Input.GetAxisRaw("Horizontal"));
        }
        else if (Input.GetAxisRaw("Horizontal") == -1f)
        {
            if (m_FacingRight == true)
            {
                Flip();
            }
            animator.SetFloat("speed", Basespeed);
            Mov(Basespeed, Input.GetAxisRaw("Horizontal"));
        }
        else
        {
            animator.SetFloat("speed", 0f);
            Mov(Basespeed, Input.GetAxisRaw("Horizontal"));
        }

        //if (Input.GetAxisRaw("Vertical") > 0)
        if (isGrounded() && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetTrigger("jump");
            //rb.AddForce(new Vector2(0f, JumpForce));
            rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }

    }

    private void Mov(float b_Speed, float dir)
    {
        Vector3 targetVelocity = new Vector2(b_Speed * dir, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref vel, .05f);
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;

        transform.Rotate(0f, 180f, 0f);
    }

    private bool isGrounded() {
        float extraHeight = 0.3f;

        RaycastHit2D raycastHit = Physics2D.BoxCast(cc.bounds.center, cc.bounds.size, 0f, Vector2.down, extraHeight, ground);
        return raycastHit.collider != null;
    }
}
