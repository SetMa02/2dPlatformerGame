﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void Start()
    {
        GameManager.Instance.coinContainer.Add(gameObject, this);
    }

    public void StartDestroy()
    {
        animator.SetTrigger("StartDestroy");
    }

    public void EndDestroy()
    {
        Destroy(gameObject);
    }
}
