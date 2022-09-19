using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float upwardsForce = 200f;
    public Collider2D activationCol;

    bool onCooldown = false;
    Animator anim;

    private void Start()
    {
        anim = GetComponentInParent<Animator>();

    }

    void ResetCD()
    {
        onCooldown = false;
    }

    public void Activate(Rigidbody2D rb)
    {
        if (onCooldown)
            return;

        anim.Play("TrampolineActivate");
        rb.AddForce(transform.up * upwardsForce);
        onCooldown = true;
        Invoke("ResetCD", 0.8f);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherCollider == activationCol)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>())
                Activate(collision.gameObject.GetComponent<Rigidbody2D>());

            if (collision.gameObject.GetComponent<PlayerContoller>())
            {
                collision.gameObject.GetComponentInChildren<Animator>().Play("Jump");
            }
        }
    }
}
