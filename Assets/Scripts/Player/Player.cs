using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    // backing field
    [SerializeField] int _maxHealth = 3;
    // property. Can be retrieved, but not set
    public int MaxHealth
    {
        get { return _maxHealth; }
    }
    int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        private set
        {
            if (value > _maxHealth)
                value = _maxHealth;
            _currentHealth = value;
        }
    }
    int _treasureCount = 0;;

    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        Debug.Log("Player's Health: " + _currentHealth);
    }

    public void DecreaseHealth(int amount)
    {
        _currentHealth -= amount;
        Debug.Log("Player's health: " + _currentHealth);
        if(_currentHealth <= 0)
        {
            Kill();
        }
    }

    public void IncreaseTreasure(int amount)
    {
        _treasureCount += amount;
        Debug.Log("Treasure Collected: " + _treasureCount);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
        // play particles
        // play sounds
    }
}
