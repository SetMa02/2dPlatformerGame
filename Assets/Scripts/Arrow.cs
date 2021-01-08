﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, IObjectDestroyer
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private int lifeTime;
    [SerializeField] private TriggerDamage triggerDamage;
    private Player player;
    public float force;
    public float Force
    {
        get { return force; }
        set { force = value; }
    }

    public void Destroy(GameObject gameObject)
    {
        player.ReturnArrowToPool(this);
    }

    public void SetImpulse(Vector2 direction, float force,int bonusDamage, Player player)
    {
        this.player = player;
        triggerDamage.Init(this);
        triggerDamage.Parent = player.gameObject;
        triggerDamage.Damage += bonusDamage;
        rigidbody.AddForce(direction * force, ForceMode2D.Impulse); 
        if (force < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);
        StartCoroutine(StartLife());
    }


    private IEnumerator StartLife()
    {
        yield return new WaitForSecondsRealtime(lifeTime);
        Destroy(gameObject);
        yield break;
    }
}