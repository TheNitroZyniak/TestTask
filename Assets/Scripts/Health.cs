using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour{
    [Header("Health stats")]
    [SerializeField] int _maxHealth = 100;
    int _currentHealth = 100;
    public event Action<float> HealthChanged;
    public event Action OnDeath;

    private void Start() {      
        _currentHealth = _maxHealth;
    }

    public void GetDamage(int value) {
        _currentHealth -= value;
        if (_currentHealth <= 0) {
            gameObject.SetActive(false);
            OnDeath?.Invoke();
        } 
        else {
            float _currentHealthAsPercantage = (float)+_currentHealth / _maxHealth;
            HealthChanged?.Invoke(_currentHealthAsPercantage);
        }
    }
}
