using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float Speed = 4f;
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Explosion") 
            && animator.GetCurrentAnimatorStateInfo(0).normalizedTime > .9f)
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5f);
        }
        
    }

    private void FixedUpdate()
    {
       GetComponent<Rigidbody2D>().velocity = new Vector2(0f, Speed);
    }
}
