using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RainMagic_HavyRain : RainMagic_Rain
{

    [SerializeField] private float m_WaterFillingSpeed;
    
    private List<Water> waterList = new List<Water>();

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        Water water = other.GetComponent<Water>();
        if (water != null)
        {
            waterList.Add(water);
        }
    }

    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        Water water = other.GetComponent<Water>();
        if (water == null) return;

        if (waterList.Contains(water))
        {
            waterList.Remove(water);
        }
    }

    protected override void Update()
    {
        base.Update();
        for (int i = 0; i < waterList.Count; i++)
        {
            waterList[i].AddWater(m_WaterFillingSpeed);
        }
    }
    public override void MagicReset()
    {
        base.MagicReset();
        waterList.Clear();
    }
}
