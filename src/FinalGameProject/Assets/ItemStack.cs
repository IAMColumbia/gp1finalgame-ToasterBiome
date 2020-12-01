using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ItemStack
{
    public Item item;
    public int amount;

    public void setAmount(int amt)
    {
        amount = amt;
        if(amt > item.maxStack)
        {
            amount = item.maxStack;
        }
    }

    public void addAmount(int amt)
    {
        amount += amt;
        if (amt > item.maxStack)
        {
            amount = item.maxStack;
        }
    }

    public ItemStack(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
