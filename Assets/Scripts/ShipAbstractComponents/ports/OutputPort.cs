using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ShipAbstractComponents.ports;

public class OutputPort : Port{
    public InputPort input;
    public Type mediumType;
    private bool isConnected;

    [NonSerialized]
    private List<IOutputPortListener> listeners;

    public OutputPort(Type mediumType) {
        this.mediumType = mediumType;
        listeners = new List<IOutputPortListener>();
    }

    public void Send(Medium output)
    {
        listeners.ForEach(listener => listener.OnSend());
        input.Recieve(output);
    }

    public bool IsConnected() {
        return isConnected;
    }

    public bool SetInput(InputPort toInput) {
        if (toInput.mediumType != mediumType) {
            return false;
        }
        input = toInput;
        isConnected = true;
        listeners.ForEach(listener => listener.OnConnection(from:this, to:input));
        return true;
    }

    public void AddListener(IOutputPortListener listener)
    {
        listeners.Add(listener);
    }
}
