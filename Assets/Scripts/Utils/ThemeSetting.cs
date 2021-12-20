using System;
using System.Collections.Generic;
using UnityEngine;
using static Utils.ThemeStyling.Style;

namespace Utils
{
    public class ThemeSetting : MonoBehaviour
    {
        private Dictionary<ThemeStyling.Style, Color> colorDicitonary;

        public delegate void GameObjectAction(GameObject gameObject);

        private void Awake()
        {
            colorDicitonary = new Dictionary<ThemeStyling.Style, Color>();
        }

        public void SetStyle(ThemeStyling.Style style, Color c)
        {
            colorDicitonary[style] = c;
            RecurseTransform(transform,SetColourToStyle);
        }

        private void RecurseTransform(Transform t,GameObjectAction action)
        {
            GameObject obj = t.gameObject;
            action(obj);
            foreach (Transform childTransform in t)
            {
                RecurseTransform(childTransform,action);
            }
        }

        private void SetColourToStyle(GameObject obj)
        {
            if (obj.TryGetComponent<ThemeStyling>(out ThemeStyling theme))
            {
                Color styleColor = colorDicitonary[theme.style];
                obj.GetComponent<Renderer>().material.color = styleColor;
            }
        }
    }
}