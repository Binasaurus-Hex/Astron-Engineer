using System;
using CompositeRigidbody;
using DataObjects.Thrusters;
using Newtonsoft.Json;
using Nodes.Serialization;
using PhysicalComponents;
using shipComponents;
using UnityEditor;
using UnityEngine;

public class Thruster : FunctionalComponent{

    public InputPort coolantIn;
    public OutputPort coolantOut;
    public InputPort thrustValue;
    
    [NonSerialized]
    public Color color = Color.red;

    [NonSerialized]
    public float temperature;

    public ThrusterData thrusterType;
    private PhysicalThruster physicalThruster;
    private float thrust;

    private void Awake() {
        temperature = 40;
        coolantIn = new InputPort(typeof(Coolant));
        coolantOut = new OutputPort(typeof(Coolant));
        thrustValue = new InputPort(typeof(Information));
    }

    private void Start() {
        if (physicalThruster == null) {
            GeneratePhysical();
        }
    }

    private void GeneratePhysical() {
        if (transform.childCount > 0) {
            physicalThruster = GetComponentInChildren<PhysicalThruster>();
        }
        else { 
            GameObject thrusterObject = Instantiate(thrusterType.prefab, transform);
            physicalThruster = thrusterObject.GetComponent<PhysicalThruster>(); 
        }
    }

    private void ProcessCoolant() {
        if (!(coolantIn.ReadValue() is Coolant coolant)) return;
        coolant.TransferHeat(ref temperature);
        coolantOut.Send(coolant);
    }

    private void ProcessThrustValue() {
        if (!(thrustValue.ReadValue() is Information value))
        {
            thrust = 0;
            return;
        }
        thrust = value.GetValue();
        physicalThruster.body.AddForce(transform.forward * (thrust * -1));
    }

    private void ChangeFlameEffect()
    {
        Vector3 localScale = physicalThruster.flameEffect.localScale;
        float newLength = Mathf.Lerp(0, 2, thrust / 100);
        localScale.z = newLength;
        physicalThruster.flameEffect.localScale = localScale;
    }

    private void Update() {
        if (coolantOut.IsConnected()) {
            ProcessCoolant();
        }
        ChangeFlameEffect();
    }

    private void FixedUpdate() {
        ProcessThrustValue();
    }

    class DataObject {
        public Vector3 localposition;
        public Quaternion localRotation;
        public string thrusterTypePath;
    }

    public override void Load(string obj) {
        DataObject data = JsonConvert.DeserializeObject<DataObject>(obj, new UnitySerialisationSettings());
        thrusterType = AssetDatabase.LoadAssetAtPath<ThrusterData>(data.thrusterTypePath);
        transform.localPosition = data.localposition;
        transform.localRotation = data.localRotation;
        GeneratePhysical();
    }

    public override string Save() {
        return JsonConvert.SerializeObject(new DataObject {
            thrusterTypePath = AssetDatabase.GetAssetPath(thrusterType),
            localposition = transform.localPosition,
            localRotation = transform.localRotation
        }, new UnitySerialisationSettings());
    }

    private void OnDrawGizmos() {
        //Gizmos.DrawLine(transform.position,transform.position + transform.forward * 100);
    }
}
