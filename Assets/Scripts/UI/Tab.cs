using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Tab : MonoBehaviour
    {
        private List<TabSelected> listeners;
        public Color color;

        private void Awake()
        {
            listeners = new List<TabSelected>();
            color = GetComponent<Button>().colors.selectedColor;
        }

        public delegate void TabSelected(Tab t);
        public void RegisterCallback(TabSelected callback)
        {
            listeners.Add(callback);
        }

        public void Select()
        {
            listeners.ForEach(callback => callback(this));
        }
    }
}