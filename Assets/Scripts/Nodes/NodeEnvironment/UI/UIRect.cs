using UnityEngine;

namespace Nodes.NodeEnvironment.UI {
	[ExecuteInEditMode]
	public class UIRect : MonoBehaviour
	{

		private RectTransform rectTransform;
		public float thickness;
		private MeshFilter meshFilter;
		private bool hasCollider;
		private BoxCollider boxCollider;
		private bool initialized;
		
		private void Start() {
			Initialize();
		}

		private void Initialize()
		{
			GetComponents();
			initialized = true; 
		}

		private void SpaceChildren()
		{
			foreach (Transform t in transform)
			{
				GameObject child = t.gameObject;
				if (child.TryGetComponent<UIRect>(out UIRect rect))
				{
					Vector3 pos = rect.transform.localPosition;
					rect.transform.localPosition = new Vector3(pos.x,pos.y,-thickness/2 - rect.thickness/2);
				}
			}
		}

		private void GetComponents()
		{
			rectTransform = GetComponent<RectTransform>();
			meshFilter = GetComponent<MeshFilter>();
			hasCollider = TryGetComponent<BoxCollider>(out boxCollider);
			
		}

		private void OnRectTransformDimensionsChange() {
			Resize();
		}

		public void Resize()
		{
			if (!initialized)
			{
				Initialize();
			}
			GenerateMesh();
			GenerateBoundingBox();
			SpaceChildren();
		}

		private void GenerateMesh() {
			Mesh mesh = new Mesh();
			Rect rect = rectTransform.rect;
			mesh.recalculateMeshByBounds(new Vector3(rect.width,thickness,rect.height));
			meshFilter.mesh = mesh;
		}

		private void GenerateBoundingBox() {
			Rect rect = rectTransform.rect;
			if (hasCollider) {
				boxCollider.size = new Vector3(rect.width,rect.height,thickness);
			}
		}
	}
}