using System.Collections.Generic;
using Newtonsoft.Json;
using Nodes.Serialization;
using shipComponents;
using shipComponents.Mediums;
using UnityEngine;

public class Radiator : FunctionalComponent{

    public InputPort hotCoolant;
    public OutputPort coldCoolant;
    public float temperature;
    public Stack<Coolant> reservoir;
    public Color color = Color.blue;
    private int reservoirCapacity;

    private void Awake() {
        hotCoolant = new InputPort(typeof(Coolant));
        coldCoolant = new OutputPort(typeof(Coolant));
        reservoir = new Stack<Coolant>();
        reservoirCapacity = 3;
        for (int i = 0; i < reservoirCapacity; i++) {
            reservoir.Push(new Water());
        }

        temperature = 0;
    }

    public void ProcessCoolant() {
        Coolant coolant = reservoir.Pop();
        float tempTemperature = temperature;
        coolant.TransferHeat(ref temperature);
        coldCoolant.Send(coolant);
    }

    private void Update() {
        ReadToReservoir();
        if (coldCoolant.IsConnected() && reservoir.Count != 0) {
            ProcessCoolant();
        }
    }

    private void ReadToReservoir() {
        if (reservoir.Count >= reservoirCapacity) return;
        if (!(hotCoolant.ReadValue() is Coolant readCoolant)) return;
        reservoir.Push(readCoolant);
    }

    struct DataObject {
        public float temperatureValue;
    }

    public override void Load(string obj) {
        DataObject dataObject = JsonConvert.DeserializeObject<DataObject>(obj, new UnitySerialisationSettings());
        temperature = dataObject.temperatureValue;
    }

    public override string Save() {
        return JsonConvert.SerializeObject(new DataObject {temperatureValue = temperature},
            new UnitySerialisationSettings());
    }
}
