using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using shipComponents;
using UnityEngine;
using Object = System.Object;

namespace Nodes.Serialization
{
    public class SerializationManager : MonoBehaviour
    {
        private UnitySerialisationSettings settings;

        public class Obj
        {
            public Vector3 x;
            public Vector3 y;
        }
        private void Start()
        { 
            settings = new UnitySerialisationSettings();
            Obj x = new Obj();
            Vector3 pos = new Vector3(1,2,3);
            x.x = pos;
            x.y = Vector3.back;
            string json = JsonConvert.SerializeObject(x, settings);
            Obj z = JsonConvert.DeserializeObject<Obj>(json, settings);
            print(z.x == z.y);

        }
        
        
    }
}