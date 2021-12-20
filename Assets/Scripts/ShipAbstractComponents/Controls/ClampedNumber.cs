using UnityEngine;

namespace ShipAbstractComponents.Controls
{
    public class ClampedNumber
    {
        public float min;
        public float max;
        private float currentValue;
        public float CurrentValue
        {
            get => currentValue;
            set => currentValue = Mathf.Clamp(value, min, max);
        }

        public ClampedNumber(float defaultValue, float min, float max)
        {
            this.min = min;
            this.max = max;
            CurrentValue = defaultValue;
        }
    }
}