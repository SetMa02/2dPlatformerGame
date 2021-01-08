using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private GameObject LeftBorder;
    [SerializeField] private GameObject RightBorder;
    [SerializeField] private Rigidbody2D Rigidbody;
    [SerializeField] private bool isRightDirection;
    [SerializeField] private float speed;
    [SerializeField] private GroundDetection groundDetection;
    [SerializeField] private SpriteRenderer Sprite;
    [SerializeField] private CollisionDamage collisionDamage;
    

       private void FixedUpdate()
       {//Передвижение противника по заданным границам ЕСЛИ он стоит на платформе
        if(groundDetection.isGrounded)
        { 
            if (transform.position.x > RightBorder.transform.position.x
                || collisionDamage.Direction < 0)
                isRightDirection = false;
            else if (transform.position.x < LeftBorder.transform.position.x
                || collisionDamage.Direction > 0)
                isRightDirection = true;
            Rigidbody.velocity = isRightDirection ? Vector2.right : Vector2.left;
            Rigidbody.velocity *= speed;
        }

        if (Rigidbody.velocity.x > 0)
            Sprite.flipX = false;
        if (Rigidbody.velocity.x < 0)
            Sprite.flipX = true;
        }

}
