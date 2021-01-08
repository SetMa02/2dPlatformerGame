﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private float delta;
    private float healthValue;
    private float currentHealth;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        healthValue = player.Health.CurrentHealth / 100.0f;
    }

    private void Update()
    {
        currentHealth = player.Health.CurrentHealth / 100.0f;
        if (currentHealth > healthValue)
            healthValue += delta;
        if (currentHealth < healthValue)
            healthValue -= delta;
        if (currentHealth < delta)
            healthValue = currentHealth;

        health.fillAmount = healthValue;
        
    }
}
