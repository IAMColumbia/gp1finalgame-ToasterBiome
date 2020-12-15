using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryOld
{

    public float maxCharge;
    public float charge
    {
        get
        {
            return _charge;
        }
        set
        {
            if(value < 0)
            {
                _charge = 0;
            } else if (value > maxCharge)
            {
                _charge = maxCharge;
            } else
            {
                _charge = value;
            }
        }
    }

    private float _charge;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CanUse(float energyUse)
    {
        return energyUse <= _charge;
    }

    public BatteryOld()
    {
        maxCharge = 50f;
        charge = 50f;
    }
}
