using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TankController))]
public class Player : MonoBehaviour
{
    // backing field
    [Header("Player")]
    [SerializeField] int _maxHealth = 3;
    [SerializeField] Material _bodyMaterial;
    public Material BodyMaterial => _bodyMaterial;
    [SerializeField] List<GameObject> _recolorableParts;

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
    int _treasureCount = 0;
    public int TreasureCount => _treasureCount;
    
    //States
    public bool IsInvincible = false;

    [HideInInspector] public UnityEvent m_HealthUpdate = new UnityEvent();
    [HideInInspector] public UnityEvent m_PlayerDeath = new UnityEvent();
    [HideInInspector] public UnityEvent m_TreasureUpdate = new UnityEvent();
    
    TankController _tankController;

    private void Awake()
    {
        _tankController = GetComponent<TankController>();
    }

    private void Start()
    {
        _currentHealth = _maxHealth;
        m_HealthUpdate.Invoke();
    }

    public void IncreaseHealth(int amount)
    {
        _currentHealth += amount;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);
        m_HealthUpdate.Invoke();
    }

    public void DecreaseHealth(int amount)
    {
        if (!IsInvincible)
        {
            _currentHealth -= amount;
            m_HealthUpdate.Invoke();
            if (_currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    public void IncreaseTreasure(int amount)
    {
        _treasureCount += amount;
        m_TreasureUpdate.Invoke();
    }

    public void Recolor(Material material)
    {
        foreach (GameObject go in _recolorableParts)
        {
            go.GetComponent<MeshRenderer>().material = material;
        }
    }

    public void Kill()
    {
        _currentHealth = 0;
        m_HealthUpdate.Invoke();
        m_PlayerDeath.Invoke();
        gameObject.SetActive(false);
        // play particles
        // play sounds
    }
}
