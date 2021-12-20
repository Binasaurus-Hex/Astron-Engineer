using System;

namespace Nodes.ComponentReading {
	public class MemberVariablePackage {
		private Object obj;
		private string name;
		
		public MemberVariablePackage(Object obj, string name) {
			this.obj = obj;
			this.name = name;
		}

		public Object GetObject() {
			return obj;
		}

		public string GetName() {
			return name;
		}

	}
}