using System;
using System.Collections.Generic;
using System.Reflection;

namespace Nodes.ComponentReading {
	public class ObjectReader
	{
		private Object obj;
		public ObjectReader(Object obj)
		{
			this.obj = obj;
		}
		public List<MemberVariablePackage> GetPackage() {
			List<MemberVariablePackage> packages = new List<MemberVariablePackage>();
			Type objectType = obj.GetType();
			FieldInfo[] fields = objectType.GetFields();
			foreach (FieldInfo fieldInfo in fields) {
				Object memberObj = fieldInfo.GetValue(obj);
				string memberName = fieldInfo.Name;
				packages.Add(new MemberVariablePackage(memberObj,memberName));
			}
			return packages;
		}

		public string GetName()
		{
			return obj.GetType().Name;
		}


	}
}