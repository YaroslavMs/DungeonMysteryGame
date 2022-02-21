using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public enum States
    {
        Idle,
        Walking,
        Attacking
    }

    public float maxHpWithoutBuffs = 100;
    public float maxHp = 100;
    public float hp = 100;
    
    public float maxStaminaWithoutBuffs = 100;
    public float maxStamina = 100;
    public float stamina = 100;
    
    public float movementSpeedWithoutBuffs = 5;
    public float movementSpeed = 5;
    public float exhaustedMovementSpeed = 2;
    
    public float damageWithoutBuffs;
    public float damage = 1.0f;
    
    public float experience = 0;
    public int currentLevel = 1;
    
    public States currentState;
    public Bar healthBar;
    public Bar staminaBar;
    public PlayerInventory inventory;

    private void Start()
    {
        healthBar.SetMaxValueOnBar(hp);
        staminaBar.SetMaxValueOnBar(stamina);
    }

    private void OnEnable()
    {
        UpdateInventoryBuffs();
        inventory.equippedInventory.OnChange += UpdateInventoryBuffs;
        inventory.inventory.OnChange += UpdateInventoryBuffs;
    }

    private void OnDisable()
    {
        inventory.equippedInventory.OnChange -= UpdateInventoryBuffs;
        inventory.inventory.OnChange -= UpdateInventoryBuffs;
    }

    private void LateUpdate()
    {
        healthBar.SetValueOnBar(hp);
        staminaBar.SetValueOnBar(stamina);
    }
    public void ChangeHpValue(float changeValue)
    {
        if (hp + changeValue < 0)
        {
            hp = 0;
        }
        else if (hp + changeValue > maxHp)
        {
            hp = maxHp;
        }
        else hp += changeValue;
    }

    public void ChangeStaminaValue(float changeValue)
    {
        if (stamina + changeValue < 0)
        {
            stamina = 0;
        }
        else if (stamina + changeValue > maxStamina)
        {
            stamina = maxStamina;
        }
        else stamina += changeValue;
    }

    public void UpdateInventoryBuffs()
    {
        maxHp = maxHpWithoutBuffs;
        maxStamina = maxStaminaWithoutBuffs;
        if (hp > maxHpWithoutBuffs)
            hp = maxHpWithoutBuffs;
        if (stamina > maxStaminaWithoutBuffs)
            stamina = maxStaminaWithoutBuffs;
        movementSpeed = movementSpeedWithoutBuffs;
        damage = damageWithoutBuffs;
        foreach (var slot in inventory.equippedInventory.slots)
        {
            if (slot.item.id == 0)
                continue;
            foreach (var boost in slot.item.boosts)
            {
                switch (boost.boostType)
                {
                    case Boost.Speed:
                        movementSpeed += boost.value;
                        break;
                    case Boost.AttackPower:
                        damage += boost.value;
                        break;
                    case Boost.MaxHealth:
                        maxHp += boost.value;
                        break;
                    case Boost.MaxStamina:
                        maxStamina += boost.value;
                        
                        break;
                }
            }
        }
        staminaBar.SetOnlyMaxValueOnBar(maxStamina);
        healthBar.SetOnlyMaxValueOnBar(maxHp);
    }

    public void LevelUp(int type)
    {
        if (experience >= currentLevel * 15)
        {
            experience -= currentLevel * 15;
            currentLevel++;
            switch (type)
            {
                case 1: maxHpWithoutBuffs += 10;
                    break;
                case 2: maxStaminaWithoutBuffs += 10;
                    break;
                case 3: movementSpeedWithoutBuffs += 0.1f;
                    break;
                case 4: damageWithoutBuffs += 1;
                    break;
            }
            UpdateInventoryBuffs();
        }

    }

    public void AddExperience(float amount)
    {
        experience += amount;
    }
}