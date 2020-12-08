using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InspectionBox : MonoBehaviour
{
    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemQuantity;
    public TextMeshProUGUI itemDescription;

    public void setItem(ItemStack item)
    {
       
        this.itemName.text = item.item.Name;

        
        this.itemQuantity.text = item.amount.ToString() + "x";
        this.itemDescription.text = '"' + item.item.description + '"';
    }
}
