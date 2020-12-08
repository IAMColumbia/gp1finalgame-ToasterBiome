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

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
        }
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
        foreach(ItemStack itemStack in items)
        {
            oreText.text += itemStack.item.name + ": " + itemStack.amount + "\n";
        }

        for(int i = 0; i < items.Count; i++)
        {
            itemSlots[i].Item = items[i];
        }
    }


}
