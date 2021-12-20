using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Nodes.Serialization;
using SaveLoad;
using Saving;
using ShipAbstractComponents.ports;
using shipComponents;
using UnityEditor;
using UnityEngine;
using Utils;
using Object = System.Object;

namespace ModularContainers {
	public class ModularContainer : MonoBehaviour, ISavable, IOutputPortListener{
		public List<FunctionalComponent> components;
		public Dictionary<OutputPort,InputPort> connections;
		private string saveData;

		public delegate void OnUpdate();
		public event OnUpdate onContainerUpdate;
		private string path;

		private void Awake() {
			path = $"{Application.dataPath}/Saves/ModularContainers/{name}.json";
			components = new List<FunctionalComponent>();
			connections = new Dictionary<OutputPort, InputPort>();
			SaveManager.onSave += () => {
				saveData = Save();
				SaveToFile();
			};
			SaveManager.onLoad += () => {
				LoadFromFile();
				Load(saveData);
			};
		}

		void SaveToFile() {
			System.IO.File.WriteAllText(path, saveData);
		}

		void LoadFromFile() {
			saveData = System.IO.File.ReadAllText(path);
		}

		private void Start() {
			transform.RecurseChildren(child => {
				if (child.TryGetComponent<FunctionalComponent>(out FunctionalComponent component)) {
					components.Add(component);
				}
				return false;
			});
			AddConnectionListeners();
			onContainerUpdate?.Invoke();
		}

		private void AddConnectionListeners() {
			components.ForEach(component => ReflectionUtilities.GetFieldValuesOfType<OutputPort>(component).ForEach(port => port.AddListener(this)));
		}

		private void Update() {
			if (Input.GetKeyDown(KeyCode.O)) {
				SaveManager.Save();
			}

			if (Input.GetKeyDown(KeyCode.P)) {
				SaveManager.Load();
			}
		}

		// output port listening
		public void OnSend() { }

		public void OnConnection(OutputPort from, InputPort to) {
			connections.Add(from,to);
		}

		// data structures
		
		public struct TypedData {
			public Type type;
			public string data;
		}
		private struct PortData {
			public int componentIndex;
			public string variableName;
		}
		
		// save / load
		
		private struct SaveData {
			public List<Connection> connections;
			public List<TypedData> components;
		}
		
		private struct Connection {
			public PortData from;
			public PortData to;
		}

		public void Load(string obj) {
			SaveData saveData = JsonConvert.DeserializeObject<SaveData>(obj, new UnitySerialisationSettings());
			//start fresh
			transform.RecurseChildren(t=> {
				if(t.TryGetComponent<FunctionalComponent>(out FunctionalComponent c)) Destroy(t.gameObject);
				return false;
			});
			components.Clear();
			connections.Clear();
			
			//instantiate components
			List<TypedData> componentData = saveData.components;
			foreach (TypedData typedData in componentData) {
				GameObject newObject = new GameObject();
				newObject.transform.parent = transform;
				FunctionalComponent functionalComponent = (FunctionalComponent)newObject.AddComponent(typedData.type);
				functionalComponent.Load(typedData.data);
				components.Add(functionalComponent);
			}
			
			//create connections
			List<Connection> connectionData = saveData.connections;
			foreach (Connection connection in connectionData) {
				PortData from = connection.from;
				PortData to = connection.to;
				FunctionalComponent fromComponent = components[from.componentIndex];
				FunctionalComponent toComponent = components[to.componentIndex];
				OutputPort outputPort = ReflectionUtilities.GetFieldWithName<OutputPort>(fromComponent, from.variableName);
				InputPort inputPort = ReflectionUtilities.GetFieldWithName<InputPort>(toComponent, to.variableName);
				outputPort.SetInput(inputPort);
				connections[outputPort] = inputPort;
			}

			AddConnectionListeners();
			onContainerUpdate?.Invoke();
		}

		public string Save() {
			SaveData saveData = new SaveData();
			
			// get the component data
			List<TypedData> componentData = new List<TypedData>();
			components.ForEach(elem => {
				TypedData data = new TypedData {type = elem.GetType(), data = elem.Save()};
				componentData.Add(data);
			});
			saveData.components = componentData;
			
			// get the connection data
			List<Connection> connectionData = new List<Connection>();
			foreach (KeyValuePair<OutputPort,InputPort> connection in connections) {
				connectionData.Add(new Connection {
					from = GetPortData(connection.Key),
					to = GetPortData(connection.Value)
				});
			}

			saveData.connections = connectionData;

			return JsonConvert.SerializeObject(saveData);
		}
		
		// utility functions

		private PortData GetPortData(Port p) {
			foreach (FunctionalComponent component in components) {
				if (ReflectionUtilities.ContainsField(component, p, out string portName)) {
					return new PortData {
						componentIndex = components.IndexOf(component), variableName = portName
					};
				}
			}
			throw new Exception("port was not found!");
		}
	}
}