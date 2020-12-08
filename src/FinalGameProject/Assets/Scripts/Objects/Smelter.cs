using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smelter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Trying to open Smelter");
            List<ItemStack> itemsToDestroy = new List<ItemStack>();
            foreach (ItemStack item in Inventory.instance.items)
            {
                if(item.item.researchValue > 0)
                {
                    ResearchConsole.RESEARCH_POINTS += item.item.researchValue * item.amount;
                    itemsToDestroy.Add(item);
                }
                
            }
            for (int i = 0; i < itemsToDestroy.Count; i++)
            {
                Inventory.instance.attemptRemove(itemsToDestroy[i]);
            }
        }
    }
}
