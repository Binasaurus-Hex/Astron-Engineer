using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class PhysicsUtils
    {
        public static Vector3 GetCentreOfMass(List<KeyValuePair<float,Vector3>> massPositionArray)
        {
            Vector3 weightedPosition = new Vector3();
            float totalMass = 0;
            foreach (KeyValuePair<float,Vector3> pair in massPositionArray)
            {
                float mass = pair.Key;
                Vector3 position = pair.Value;
                weightedPosition += position * mass;
                totalMass += mass;
            }

            return weightedPosition / totalMass;
        }
    }
}