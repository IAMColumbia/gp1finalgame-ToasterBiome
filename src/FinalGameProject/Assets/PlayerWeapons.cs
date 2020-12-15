using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{

    public List<GameObject> weapons = new List<GameObject>();

    public GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon = weapons[0];
        currentWeapon.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchWeapon();
        }
    }

    public void SwitchWeapon()
    {
        int index = weapons.IndexOf(currentWeapon);
        index++;
        if(index > weapons.Count - 1)
        {
            index = 0;
        }
        currentWeapon.SetActive(false);
        currentWeapon = weapons[index];
        currentWeapon.SetActive(true);
    }
}
