using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public int oreAmount;

    public TextMeshProUGUI oreText;

    public List<ItemStack> items;
    public List<ItemSlot> itemSlots;

    public ChargableItemStack startingBattery;
    public ChargableItemStack startingBattery2;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }

        AddItem(startingBattery);
        AddItem(startingBattery2);

        //give them a small battery to start
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddItem(ItemStack ItemS)
    {
        foreach (ItemStack invItemStack in items)
        {

            if (ItemS.item == invItemStack.item)
            {
                Debug.Log("Found same item");

                Debug.Log("Starting: ");
                Debug.Log(ItemS.item + ", " + ItemS.amount);
                Debug.Log(invItemStack.item + ", " + invItemStack.amount);
                int difference = invItemStack.item.maxStack - invItemStack.amount; //how much can fit
                Debug.Log("Difference: " + difference);
                if (ItemS.amount <= difference) //all of it can fit
                {
                    Debug.Log("Enough space to add to existing ItemStack");
                    //add it all to this slot
                    invItemStack.amount += ItemS.amount;
                    ItemS.amount = 0;
                }
                else
                {
                    Debug.Log("Not enough space to add to existing ItemStack, filling and making new one");
                    invItemStack.amount += difference;
                    ItemS.amount -= difference;
                }
                Debug.Log("Ending: ");
                Debug.Log(ItemS.item + ", " + ItemS.amount);
                Debug.Log(invItemStack.item + ", " + invItemStack.amount);
            }
        }
        if (ItemS.amount > 0)
        {
            Debug.Log("Making new itemStack");
            Debug.Log("Making new: ");
            Debug.Log(ItemS.item + ", " + ItemS.amount);
            while (ItemS.amount > ItemS.item.maxStack)
            {
                AddItem(new ItemStack(ItemS.item, ItemS.item.maxStack));
                ItemS.amount -= ItemS.item.maxStack;
            }
            items.Add(ItemS);
        }



        RefreshUI();
    }

    public bool attemptRemove(ItemStack itemS)
    {
        Debug.Log("Attempting to remove item");
        foreach (ItemStack i in items)
        {
            if (i.item == itemS.item)
            {
                Debug.Log("Item good, amount in inv: " + i.amount + ", amount in transaction: " + itemS.amount);
                //make sure there's enough to remove
                if (itemS.amount <= i.amount)
                {
                    //we good
                    i.amount -= itemS.amount;

                    if (i.amount <= 0)
                    {
                        items.Remove(i);

                    }
                    RefreshUI();
                    Debug.Log("Successful");
                    return true;
                }
                else
                {
                    Debug.Log("Not Enough");
                    return false;
                }
            }
        }
        Debug.Log("Not in inventory");
        return false;
    }

    public void RefreshUI()
    {
        oreText.text = "";
        foreach (ItemStack itemStack in items)
        {
            oreText.text += itemStack.item.name + ": " + itemStack.amount + "\n";
        }

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(i < items.Count)
            {
               itemSlots[i].Item = items[i]; 
            } 
            else
            {
                itemSlots[i].Item = null;
            }
            
            
        }
    }

    public float GetTotalCharge()
    {
        float totalCharge = 0;
        foreach (ItemStack item in items)
        {
            if (item is ChargableItemStack)
            {
                totalCharge += (item as ChargableItemStack).chargeLevel;
            }
        }
        return totalCharge;
    }

    public float GetMaxCharge()
    {
        float totalCharge = 0;
        foreach (ItemStack item in items)
        {
            if (item is ChargableItemStack)
            {
                totalCharge += (item as ChargableItemStack).getBattery().maxCharge;
            }
        }
        return totalCharge;
    }

    public void LazyCharge(float amount)
    {
        if (amount > 0)
        {
            float remainder = amount;
            foreach (ItemStack item in items)
            {
                if (item is ChargableItemStack)
                {
                    ChargableItemStack battery = item as ChargableItemStack;
                    float difference = battery.getBattery().maxCharge - battery.chargeLevel;
                    if (difference <= remainder)
                    {
                        battery.Charge(difference);
                        remainder -= difference;
                    }
                    else
                    {
                        battery.Charge(remainder);
                        remainder = 0;
                    }

                }

                if (remainder <= 0) //that's all the juice we got!
                {
                    return;
                }
            }
        } else
        {
            float remainder = -amount;
            foreach (ItemStack item in items)
            {
                if (item is ChargableItemStack)
                {
                    ChargableItemStack battery = item as ChargableItemStack;

                    if (remainder > battery.chargeLevel)
                    {
                        remainder -= battery.chargeLevel;
                        battery.Charge(-battery.chargeLevel);
                    }
                    else
                    {
                        battery.Charge(-remainder);
                        remainder = 0;
                    }

                }

                if (remainder <= 0) //done taking juice!
                {
                    return;
                }
            }
        }


    }

    public bool CanUse(float energyUse)
    {
        return energyUse <= GetTotalCharge();
    }

    public void Dump()
    {
        for(int i = 0; i < items.Count; i++)
        {
            Debug.Log(items[i].GetType());
            if(items[i].GetType() != typeof(ChargableItemStack))
            {
                attemptRemove(items[i]);
            }
        }
        RefreshUI();
    }


}
