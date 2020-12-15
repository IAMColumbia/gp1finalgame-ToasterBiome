using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChargableItemStack : ItemStack
{
    public float chargeLevel;

    public ChargableItemStack(Battery item, int amount) : this(item, amount, item.maxCharge) { }

    public ChargableItemStack(Battery item, int amount, float charge) : base(item, amount)
    {
        chargeLevel = charge;
    }

    public void Charge(float amount)
    {
        chargeLevel += amount;

        if(chargeLevel + amount > (item as Battery).maxCharge)
        {
            chargeLevel = (item as Battery).maxCharge;
        }
        if(chargeLevel - amount < 0)
        {
            chargeLevel = 0;
        }
    }

    public float getChargePercentage() //return's percentage of battery level between 0 and 1
    {
        return chargeLevel / (float)(item as Battery).maxCharge;
    }

    public Battery getBattery()
    {
        return item as Battery;
    }
}
