using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New equipment Object", menuName = "Inventory System/Items/Equipment")]
public class EquipmentObject : ItemObject
{
    public float defenceBonus;
    public float attackBonus;
    private void Awake()
    {
        type = ItemType.Equipment;
    }
}
