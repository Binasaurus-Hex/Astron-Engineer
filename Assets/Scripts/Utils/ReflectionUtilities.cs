using System;
using System.Collections.Generic;
using System.Reflection;
using shipComponents;

namespace Utils {
	public class ReflectionUtilities {
		public static string GetNameOfMember(object rootObject, object member) {
			Type type = rootObject.GetType();
			FieldInfo[] fields = type.GetFields();
			foreach (FieldInfo fieldInfo in fields) {
				if (fieldInfo.GetValue(rootObject) == member) {
					return fieldInfo.Name;
				}
			}

			return "";
		}

		public static List<T> GetFieldValuesOfType<T>(object obj) {
			FieldInfo[] fields = obj.GetType().GetFields();
			List<T> matchingFields = new List<T>();
			foreach (FieldInfo field in fields) {
				if (field.FieldType == typeof(T)) {
					matchingFields.Add((T)field.GetValue(obj));
				}
			}

			return matchingFields;
		}

		public static bool ContainsField(object obj,object fieldValue, out string foundFieldName) {
			FieldInfo[] fields = obj.GetType().GetFields();
			foreach (FieldInfo field in fields) {
				if (field.GetValue(obj) == fieldValue) {
					foundFieldName = field.Name;
					return true;
				}
			}
			foundFieldName = "";
			return false;
		}

		public static T GetFieldWithName<T>(object obj, string name) {
			FieldInfo[] fields = obj.GetType().GetFields();
			foreach (FieldInfo field in fields) {
				if (field.Name == name) {
					return (T)field.GetValue(obj);
				}
			}

			return default;
		}
	}
}