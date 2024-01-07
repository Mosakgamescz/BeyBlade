using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public void SetMaxHealth(int MaxHealth)
    {
        slider.value = MaxHealth;
        slider.value = MaxHealth;
    }

    public void SetHealth(int health)
    {
        if(health < 20)
        {
            health = 0;
        }
        slider.value = health;
    }
}
