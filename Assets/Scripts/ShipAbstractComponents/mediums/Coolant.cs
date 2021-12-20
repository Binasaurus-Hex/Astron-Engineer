using UnityEngine;
using System.Collections;

public abstract class Coolant : Medium {
    //stats
    public float heatCapacity;
    public float transferRate;

    //state
    public float temperature;

    protected Coolant() { }

    public void TransferHeat(ref float otherTemperature) {
        float midpoint = (temperature + otherTemperature) / 2;
        float transfer = midpoint - temperature;
        if(midpoint > heatCapacity) {
            transfer = heatCapacity - temperature;
        }
        transfer = Mathf.Clamp(transfer, -transferRate, transferRate);
        temperature += transfer;
        otherTemperature -= transfer;
    }
}
