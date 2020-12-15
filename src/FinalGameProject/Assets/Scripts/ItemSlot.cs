using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private bool _pressable = false;

    public Slider slider;

    public bool Pressable
    {
        get
        {
            return _pressable;
        }
        set
        {
            _pressable = value;
            if(_pressable)
            {
                GetComponent<Button>().enabled = true;
            } else
            {
                GetComponent<Button>().enabled = false;
            }
        }
    }

    public bool mouseOver = false;

    public InspectionBox inspectionBox;

    [SerializeField] Image Image;
    [SerializeField] TextMeshProUGUI amountText;

    private ItemStack _item;
    [SerializeField]
    public ItemStack Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null || _item.item == null)
            {
                Image.enabled = false;
                amountText.gameObject.SetActive(false);

            }
            else
            {
                Image.sprite = _item.item.sprite;
                Image.enabled = true;
                if(_item.amount > 1)
                {
                    amountText.gameObject.SetActive(true);
                    amountText.text = "x" + _item.amount.ToString();
                } else
                {
                    amountText.text = "";
                    amountText.gameObject.SetActive(false);
                }

                if(_item.GetType() == typeof(ChargableItemStack))
                {
                    Debug.Log("NA BATTERY");
                    slider.gameObject.SetActive(true);
                } else
                {
                    Debug.Log("NOT A BATTERY");
                    slider.gameObject.SetActive(false);
                }
            }
        }
    }

    public void Update()
    {
        if(mouseOver)
        {
            inspectionBox.transform.position = Input.mousePosition;
        }

        if(Item is ChargableItemStack)
        {
            slider.value = (Item as ChargableItemStack).getChargePercentage();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item != null)
        {
            if(Item.item != null)
            {
                inspectionBox.gameObject.SetActive(true);
                inspectionBox.setItem(Item);
            }
            
        }
        mouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        inspectionBox.gameObject.SetActive(false);
        mouseOver = false;
    }

    public void SetSelectedItem()
    {
        //Inventory.instance.SetSelectedItem(Item);
    }
}