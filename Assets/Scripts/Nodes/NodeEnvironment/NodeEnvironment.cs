using System;
using System.Collections;
using System.Collections.Generic;
using Nodes.NodeEnvironment.VirtualCursor;
using SaveLoad;
using shipComponents;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Nodes.NodeEnvironment {
	public class NodeEnvironment : MonoBehaviour {
		public Camera environmentCamera;
		public Color backgroundColor;
		private RenderTexture rt;
		private int viewLayer;
		private static int layerCount = 20;
		private VirtualCursor.VirtualCursor virtualCursor;
		private NodeEnvironmentController environmentController;
		private List<GameObject> children;
		private List<Vector3> storedLayout;
		private int storedLayoutIndex;

		public float zoomIncrement = 10;

		private void Awake() {
			children = new List<GameObject>();
			storedLayout = new List<Vector3>();
			SaveManager.onSave += () => {
				storedLayout.Clear();
				storedLayoutIndex = 0;
				children.ForEach(child => storedLayout.Add(child.transform.position));
			};
			SaveManager.onLoadEnd += () => storedLayoutIndex = 0;
		}

		private void Start() {
			viewLayer = layerCount;
			layerCount++;
			environmentCamera.backgroundColor = backgroundColor;
			virtualCursor = GetComponent<VirtualCursor.VirtualCursor>();
			environmentController = new NodeEnvironmentController(this);
			
			virtualCursor.AddListener(environmentController);
			environmentCamera.cullingMask = 1 << viewLayer;
			gameObject.layer = viewLayer;
		}

		private void SetLayer(Transform t) {
			t.gameObject.layer = viewLayer;
			foreach (Transform child in t) {
				SetLayer(child);
			}
		}

		public void AddChild(GameObject obj) {
			RegisterCursorListeners(obj.transform);
			children.Add(obj);
			SetLayer(obj.transform);
			if (storedLayout.Count == 0) return;
			obj.transform.position = storedLayout[storedLayoutIndex++];
		}

		private void RegisterCursorListeners(Transform current) {
			if (current.TryGetComponent<CursorEventCombiner>(out CursorEventCombiner component)) {
				virtualCursor.AddListener(component);
			}

			foreach (Transform t in current) {
				RegisterCursorListeners(t);
			}
		}

		public RenderTexture GetRenderTexture(int width, int height) {
			if(rt != null) rt.Release();
			rt = new RenderTexture(width, height, 16, RenderTextureFormat.Default);
			rt.Create();
			environmentCamera.targetTexture = rt;
			return rt;
		}

		public void SendRayCast(Vector2 point) {
			Ray ray = environmentCamera.ViewportPointToRay(point);
			Plane plane = new Plane(Vector3.back, Vector3.zero);
			if (plane.Raycast(ray, out float entry)) {
				virtualCursor.SetCursorPosition(ray.GetPoint(entry));
			}

			RaycastHit hit;
			if (Physics.Raycast(ray,out hit)) {
				GameObject obj = hit.collider.gameObject;
				if (obj.TryGetComponent<CursorEventCombiner>(out CursorEventCombiner component)) {
					component.OnCursorOver();
				}
			}
		}

		public void Pan(Vector2 translation) {
			transform.position += new Vector3(translation.x,translation.y,0f);
		}

		public void Zoom(float amount) {
			environmentCamera.orthographicSize -= amount * zoomIncrement;
			environmentCamera.orthographicSize = Mathf.Clamp(environmentCamera.orthographicSize, 1, 500);
		}

		public IEnumerator LerpOrthoMode(bool value) {
			Quaternion orthoRot = Quaternion.Euler(30,30,0);
			Quaternion normalRot = Quaternion.Euler(0,0,0);
			Quaternion target = value ? orthoRot : normalRot;
			Quaternion start = environmentCamera.transform.localRotation;
			float percentage = 0;
			float speed = 2;
			while (Quaternion.Angle(environmentCamera.transform.localRotation, target) > 0.1) {
				environmentCamera.transform.localRotation = Quaternion.Lerp(start, target, percentage);
				percentage += Time.deltaTime * speed;
				yield return null;
			}
			environmentCamera.transform.localRotation = target;
		}

		public void SetOrthoMode(bool ortho) {
			StopCoroutine("LerpOrthoMode");
			StartCoroutine("LerpOrthoMode",ortho);
		}

		public void Clear() {
			foreach (GameObject child in children) {
				if (child.TryGetComponent<IVirtualCursorListener>(out IVirtualCursorListener listener)) {
					virtualCursor.RemoveListener(listener);
				}
				Destroy(child);
			}
			children.Clear();
		}
	}
}