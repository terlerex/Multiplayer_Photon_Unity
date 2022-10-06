using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequirementSystem : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealt = 100f;
    
    [Header("Food")]
    [SerializeField] private float food;
    [SerializeField] private float maxFood = 100f;
    
    [Header("Drink")]
    [SerializeField] private float drink;
    [SerializeField] private float maxDrink = 100f;

    [Header("Radiation")] 
    [SerializeField] private float radiation;
    [SerializeField] private float minRadiation = 0f;
    
    private void Start()
    {
        health = maxHealt;
        food = maxFood;
        drink = maxDrink;
        radiation = minRadiation;
    }
}


