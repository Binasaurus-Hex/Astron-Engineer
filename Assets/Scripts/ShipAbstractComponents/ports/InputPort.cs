using System;
using UnityEngine;
using System.Collections;
using ShipAbstractComponents.ports;

public class InputPort : Port{
    private Medium value;
    public Type mediumType;

    public InputPort(Type mediumType) {
        this.mediumType = mediumType;
    }

    public bool Recieve(Medium m) {
        if (m.GetType() != mediumType) {
            return false;
        }
        value = m;
        return true;
    }

    public Medium ReadValue() {
        Medium copyValue = value;
        value = default;
        return copyValue;
    }
}
