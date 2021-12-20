using System.Collections;
using System.Collections.Generic;
using Nodes.NodeEnvironment.UI;
using Nodes.NodeEnvironment.VirtualCursor;
using ShipAbstractComponents.Controls;
using UnityEngine;

public class UISlider : MonoBehaviour,ICursorEventHandler
{
    private float minX;
    private float maxX;
    private float y;
    private float z;
    
    public ClampedNumber number;
    private float numberRange;
    private float sliderRange;
    void Start()
    {
        float halfWidth = 6.2f;
        minX = transform.localPosition.x - halfWidth;
        maxX = transform.localPosition.x + halfWidth;
        y = transform.localPosition.y;
        z = transform.localPosition.z;
        numberRange = number.max - number.min;
        sliderRange = maxX - minX;
        transform.localPosition = new Vector3(NumberToSlider(),y,z);
    }

    // Update is called once per frame
    void Update()
    {
        number.CurrentValue = SliderToNumber();
    }

    private float SliderToNumber() {
        float currentValue = transform.localPosition.x - minX;
        return currentValue * (numberRange / sliderRange) + number.min;
    }

    private float NumberToSlider() {
        float currentValue = number.CurrentValue - number.min;
        return currentValue * (sliderRange / numberRange) + minX;
    }

    public void OnPressed(VirtualCursorEvent @event)
    {
    }

    public void OnReleased(VirtualCursorEvent @event)
    {
    }

    public void OnDrag(VirtualCursorEvent @event)
    {
        Vector3 relativeMouse = transform.parent.InverseTransformPoint(@event.position);
        float clampedX = Mathf.Clamp(relativeMouse.x,minX,maxX);
        transform.localPosition = new Vector3(clampedX,y,z);
    }

    public void OnHover()
    {
    }

    public void OnCursorEnter(VirtualCursorEvent @event)
    {
    }

    public void OnCursorExit(VirtualCursorEvent @event)
    {
    }
}
