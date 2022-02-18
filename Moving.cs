using UnityEngine;
using System;

public class Moving : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRb;
    [SerializeField] private Animator bodyAnimator;
    [SerializeField] private Animator armsAnimator;
    public Transform playerT;
    private bool isGrounded;
    public bool facingRight = true;
    private float speed = 8;
    private float jumpSpeed = 800;
    public int wallDirection;
    public bool weaponUsing;
    private float y;

    public void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == -1 && facingRight && !weaponUsing)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = !facingRight;
        }
        if (Input.GetAxisRaw("Horizontal") == 1 && !facingRight && !weaponUsing)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            facingRight = !facingRight;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        bodyAnimator.SetFloat("Walk", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        bodyAnimator.SetBool("isGrounded", isGrounded);
        armsAnimator.SetFloat("Walk", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
        armsAnimator.SetBool("isGrounded", isGrounded);
        Move();
        if(y != playerT.localPosition.y)
        {
            y = playerT.localPosition.y;
            isGrounded = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.position.y + 1 < Math.Round(playerT.position.y))
        {
            y = playerT.localPosition.y;
            isGrounded = true;
        }
    }
    public void Move()
    {
        if (Input.GetAxisRaw("Horizontal") == 1 && wallDirection != 1 || Input.GetAxisRaw("Horizontal") == -1 && wallDirection != -1)
        {
            playerT.position = Vector3.MoveTowards(playerT.position, playerT.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0), speed * Time.deltaTime);
        }
    }
    public void Jump()
    {
        playerRb.AddForce(new Vector2(0, jumpSpeed));
    }
}