using System;
using System.Collections;
using System.Collections.Generic;
using Nodes.NodeEnvironment;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NodeViewport : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    private RenderTexture texture;
    private RawImage image;
    private RectTransform rectTransform;
    public NodeEnvironment nodeEnvironment;
    private bool ortho;
    private bool capturingMouse;
    // Start is called before the first frame update
    void Start() {
        image = GetComponent<RawImage>();
        rectTransform = GetComponent<RectTransform>();
        GetRenderTexture();
    }

    private void Update() {
        if (capturingMouse)
        {
            GetInput();
        }
    }

    private void GetInput()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        nodeEnvironment.Zoom(scroll);
        if (capturingMouse) {
            SendMousePosition(GetMousePosition());
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            ortho ^= true;
            nodeEnvironment.SetOrthoMode(ortho);
        }
    }

    private Vector2 GetMousePosition() {
        return Input.mousePosition;
    }

    private void GetRenderTexture() {
        Rect rect = rectTransform.rect;
        int width = (int)rect.width;
        int height = (int) rect.height;
        texture = nodeEnvironment.GetRenderTexture(width * 2,height * 2);
        image.texture = texture;
    }

    private void SendMousePosition(Vector2 position) {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(image.rectTransform, position, null, out Vector2 localPoint);
        Vector2 normalizedPoint = Rect.PointToNormalized(image.rectTransform.rect, localPoint);
        nodeEnvironment.SendRayCast(normalizedPoint);
    }

    public void OnPointerEnter(PointerEventData eventData) {
        capturingMouse = true;
    }

    public void OnPointerExit(PointerEventData eventData) {
        capturingMouse = false;
    }
}
