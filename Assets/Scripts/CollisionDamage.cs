using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private int damage = 10;
    [SerializeField] private string collisionTag;
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private Health health;
    private float direction;
    public float Direction
    {
        get { return direction; }
    }



    private void OnCollisionStay2D(Collision2D collision)
    {   //Проверка , кто прикоснулся к врагу и количество его хп
        if(collision.gameObject.CompareTag(collisionTag))
        {
            health = collision.gameObject.GetComponent<Health>();
        if(health != null)
        {
            direction = (collision.transform.position - transform.position).x;
            animator.SetFloat("direction", Math.Abs(direction));
        }
        }
    }
    public void SetDamage()
    {//Получение урона
        if(health != null)
            health.TakeHit(damage);
        health = null;
        direction = 0;
        animator.SetFloat("direction", 0f);
    }
}
