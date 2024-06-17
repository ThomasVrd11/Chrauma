using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider healthSlider;
    [SerializeField] public float maxHealth = 100f;
    [SerializeField] public float health;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {   // * Update the health bar value
        if (healthSlider.value != health)
        {
            healthSlider.value = health;
        }
    }
}
