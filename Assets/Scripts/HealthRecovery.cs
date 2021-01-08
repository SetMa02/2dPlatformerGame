using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecovery : MonoBehaviour
{
    public int Recovery = 25;
    public string CollisionTag;
    [SerializeField] private Animator animator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(CollisionTag))
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.setHealth(Recovery);
            animator.SetTrigger("TakeHealth");
        }
    }
    public void Destroy()
    {
        Destroy(gameObject);
    }
}
