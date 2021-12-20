using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class ThemeStyling : MonoBehaviour
    {
        public enum Style
        {
            BackgroundDark,BackgroundLight,ForegroundDark,ForegroundLight,Highlight
        }

        public Style style;
    }
}