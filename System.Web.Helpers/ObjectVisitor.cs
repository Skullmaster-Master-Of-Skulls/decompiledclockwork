using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Microsoft.Internal.Web.Utils;

namespace System.Web.Helpers
{
	// Token: 0x02000011 RID: 17
	internal class ObjectVisitor
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00003FBD File Offset: 0x000021BD
		public ObjectVisitor(int recursionLimit, int enumerationLimit)
		{
			this._enumerationLimit = enumerationLimit;
			this._recursionLimit = recursionLimit;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003FE0 File Offset: 0x000021E0
		protected string GetObjectId(object value)
		{
			string result;
			if (this._visited.TryGetValue(value, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004000 File Offset: 0x00002200
		public virtual void Visit(object value, int depth)
		{
			if (value == null || DBNull.Value.Equals(value))
			{
				this.VisitNull();
				return;
			}
			string text;
			if (this._visited.TryGetValue(value, out text))
			{
				this.VisitVisitedObject(text, value);
				return;
			}
			string text2 = value as string;
			if (text2 != null)
			{
				this.VisitStringValue(text2);
				return;
			}
			if (ObjectVisitor.TryConvertToString(value, out text2))
			{
				this.VisitConvertedValue(value, text2);
				return;
			}
			ObjectVisitor.ObjectVisitorException ex = value as ObjectVisitor.ObjectVisitorException;
			if (ex != null)
			{
				this.VisitObjectVisitorException(ex);
				return;
			}
			text = this.CreateObjectId(value);
			this._visited.Add(value, text);
			NameValueCollection nameValueCollection = value as NameValueCollection;
			if (nameValueCollection != null)
			{
				this.VisitNameValueCollection(nameValueCollection, depth);
				return;
			}
			IDictionary dictionary = value as IDictionary;
			if (dictionary != null)
			{
				this.VisitDictionary(dictionary, depth);
				return;
			}
			IEnumerable enumerable = value as IEnumerable;
			if (enumerable != null)
			{
				this.VisitEnumerable(enumerable, depth);
				return;
			}
			this.VisitComplexObject(value, depth + 1);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000040D2 File Offset: 0x000022D2
		public virtual void VisitObjectVisitorException(ObjectVisitor.ObjectVisitorException exception)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000040D4 File Offset: 0x000022D4
		public virtual void VisitConvertedValue(object value, string convertedValue)
		{
			this.VisitStringValue(convertedValue);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000040DD File Offset: 0x000022DD
		public virtual void VisitVisitedObject(string id, object value)
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000040DF File Offset: 0x000022DF
		public virtual void VisitNull()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040E1 File Offset: 0x000022E1
		public virtual void VisitStringValue(string stringValue)
		{
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004170 File Offset: 0x00002370
		public virtual void VisitComplexObject(object value, int depth)
		{
			if (depth > this._recursionLimit)
			{
				return;
			}
			IDynamicMetaObjectProvider dynamicObject = value as IDynamicMetaObjectProvider;
			if (dynamicObject != null && !(dynamicObject is ICustomTypeDescriptor))
			{
				IEnumerable<string> memberNames = DynamicHelper.GetMemberNames(dynamicObject);
				if (memberNames != null)
				{
					this.VisitMembers(memberNames, (string name) => null, (string name) => DynamicHelper.GetMemberValue(dynamicObject, name), depth);
					return;
				}
			}
			else
			{
				PropertyDescriptorCollection props = TypeDescriptor.GetProperties(value);
				IEnumerable<string> names = from PropertyDescriptor p in props
				select p.Name;
				this.VisitMembers(names, (string name) => props.Find(name, true).PropertyType, (string name) => ObjectVisitor.GetPropertyDescriptorValue(value, name, props), depth);
				Dictionary<string, FieldInfo> fields = value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public).ToDictionary((FieldInfo field) => field.Name);
				this.VisitMembers(fields.Keys, (string name) => fields[name].FieldType, (string name) => ObjectVisitor.GetFieldValue(value, name, fields), depth);
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000042FC File Offset: 0x000024FC
		public virtual void VisitNameValueCollection(NameValueCollection collection, int depth)
		{
			this.VisitKeyValues(collection, collection.AllKeys.Cast<object>(), (object key) => collection[(string)key], depth);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004358 File Offset: 0x00002558
		public virtual void VisitDictionary(IDictionary dictionary, int depth)
		{
			this.VisitKeyValues(dictionary, dictionary.Keys.Cast<object>(), (object key) => dictionary[key], depth);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000439C File Offset: 0x0000259C
		public virtual void VisitEnumerable(IEnumerable enumerable, int depth)
		{
			if (depth > this._recursionLimit)
			{
				return;
			}
			Type type = enumerable.GetType();
			bool flag = ObjectVisitor.ImplementsInterface(type, typeof(IList<>)) || ObjectVisitor.ImplementsInterface(type, typeof(IList));
			int num = 0;
			foreach (object item in enumerable)
			{
				if (num >= this._enumerationLimit)
				{
					this.VisitEnumeratonLimitExceeded();
					break;
				}
				if (flag)
				{
					this.VisitIndexedEnumeratedValue(num, item, depth);
				}
				else
				{
					this.VisitEnumeratedValue(item, depth);
				}
				num++;
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004450 File Offset: 0x00002650
		public virtual void VisitEnumeratedValue(object item, int depth)
		{
			this.Visit(item, depth);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000445A File Offset: 0x0000265A
		public virtual void VisitIndexedEnumeratedValue(int index, object item, int depth)
		{
			this.Visit(item, depth);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004464 File Offset: 0x00002664
		public virtual void VisitEnumeratonLimitExceeded()
		{
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004468 File Offset: 0x00002668
		public virtual void VisitMembers(IEnumerable<string> names, Func<string, Type> typeSelector, Func<string, object> valueSelector, int depth)
		{
			foreach (string text in names)
			{
				Type type = null;
				object obj = null;
				try
				{
					type = typeSelector(text);
					obj = valueSelector(text);
					if (obj != null && type == null)
					{
						type = obj.GetType();
					}
				}
				catch (Exception inner)
				{
					obj = new ObjectVisitor.ObjectVisitorException(null, inner);
				}
				finally
				{
					this.VisitMember(text, type, obj, depth);
				}
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004508 File Offset: 0x00002708
		public virtual void VisitMember(string name, Type type, object value, int depth)
		{
			this.Visit(value, depth);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004514 File Offset: 0x00002714
		public virtual void VisitKeyValues(object value, IEnumerable<object> keys, Func<object, object> valueSelector, int depth)
		{
			if (depth > this._recursionLimit)
			{
				return;
			}
			foreach (object obj in keys)
			{
				this.VisitKeyValue(obj, valueSelector(obj), depth);
			}
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004570 File Offset: 0x00002770
		public virtual void VisitKeyValue(object key, object value, int depth)
		{
			this.Visit(key, depth);
			this.Visit(value, depth);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004584 File Offset: 0x00002784
		protected virtual string CreateObjectId(object value)
		{
			return value.GetHashCode().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000045AC File Offset: 0x000027AC
		internal static string GetTypeName(Type type)
		{
			string result;
			if (ObjectVisitor._typeNames.TryGetValue(type, out result))
			{
				return result;
			}
			if (type.IsGenericType)
			{
				string genericTypeName = ObjectVisitor.GetGenericTypeName(type);
				IEnumerable<string> values = from argType in type.GetGenericArguments()
				select ObjectVisitor.GetTypeName(argType);
				return string.Format(CultureInfo.InvariantCulture, "{0}<{1}>", new object[]
				{
					genericTypeName,
					string.Join(", ", values)
				});
			}
			if (type.IsByRef || type.IsArray || type.IsPointer)
			{
				string typeName = ObjectVisitor.GetTypeName(type.GetElementType());
				int startIndex = type.Name.IndexOfAny(ObjectVisitor._separators);
				return typeName + type.Name.Substring(startIndex);
			}
			return type.Name;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004680 File Offset: 0x00002880
		private static string GetGenericTypeName(Type type)
		{
			if (ObjectVisitor.IsAnonymousType(type))
			{
				return "AnonymousType";
			}
			string name = type.GetGenericTypeDefinition().Name;
			int length = name.IndexOf('`');
			return name.Substring(0, length);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000046B8 File Offset: 0x000028B8
		private static bool IsAnonymousType(Type type)
		{
			if (Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false) && type.IsGenericType && type.Name.Contains("AnonymousType") && (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) || type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase)))
			{
				TypeAttributes attributes = type.Attributes;
				return 0 == 0;
			}
			return false;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004758 File Offset: 0x00002958
		private static bool ImplementsInterface(Type type, Type targetInterfaceType)
		{
			Func<Type, bool> func = (Type t) => targetInterfaceType.IsAssignableFrom(t);
			if (targetInterfaceType.IsGenericType)
			{
				func = ((Type t) => t.IsGenericType && targetInterfaceType.IsAssignableFrom(t.GetGenericTypeDefinition()));
			}
			return func(type) || type.GetInterfaces().Any(func);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000047B8 File Offset: 0x000029B8
		private static object GetFieldValue(object value, string name, IDictionary<string, FieldInfo> fields)
		{
			FieldInfo fieldInfo;
			fields.TryGetValue(name, out fieldInfo);
			return fieldInfo.GetValue(value);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000047D8 File Offset: 0x000029D8
		private static object GetPropertyDescriptorValue(object value, string name, PropertyDescriptorCollection props)
		{
			PropertyDescriptor propertyDescriptor = props.Find(name, true);
			return propertyDescriptor.GetValue(value);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000047F8 File Offset: 0x000029F8
		private static bool TryConvertToString(object value, out string stringValue)
		{
			stringValue = null;
			try
			{
				IConvertible convertible = value as IConvertible;
				if (convertible != null)
				{
					stringValue = convertible.ToString(CultureInfo.CurrentCulture);
					return true;
				}
				TypeConverter converter = TypeDescriptor.GetConverter(value);
				if (converter.CanConvertFrom(typeof(string)))
				{
					stringValue = converter.ConvertToString(value);
					return true;
				}
				Type type = value.GetType();
				if (type == typeof(object))
				{
					stringValue = value.ToString();
					return true;
				}
				Type type2 = value as Type;
				if (type2 != null)
				{
					stringValue = "typeof(" + ObjectVisitor.GetTypeName(type2) + ")";
					return true;
				}
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x04000036 RID: 54
		private static readonly Dictionary<Type, string> _typeNames = new Dictionary<Type, string>
		{
			{
				typeof(string),
				"string"
			},
			{
				typeof(object),
				"object"
			},
			{
				typeof(int),
				"int"
			},
			{
				typeof(byte),
				"byte"
			},
			{
				typeof(short),
				"short"
			},
			{
				typeof(long),
				"long"
			},
			{
				typeof(decimal),
				"decimal"
			},
			{
				typeof(float),
				"float"
			},
			{
				typeof(double),
				"double"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(char),
				"char"
			},
			{
				typeof(void),
				"void"
			}
		};

		// Token: 0x04000037 RID: 55
		private static readonly char[] _separators = new char[]
		{
			'&',
			'[',
			'*'
		};

		// Token: 0x04000038 RID: 56
		private readonly int _recursionLimit;

		// Token: 0x04000039 RID: 57
		private readonly int _enumerationLimit;

		// Token: 0x0400003A RID: 58
		private Dictionary<object, string> _visited = new Dictionary<object, string>();

		// Token: 0x02000012 RID: 18
		[Serializable]
		public class ObjectVisitorException : Exception
		{
			// Token: 0x060000C5 RID: 197 RVA: 0x000049F3 File Offset: 0x00002BF3
			public ObjectVisitorException()
			{
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x000049FB File Offset: 0x00002BFB
			public ObjectVisitorException(string message) : base(message)
			{
			}

			// Token: 0x060000C7 RID: 199 RVA: 0x00004A04 File Offset: 0x00002C04
			public ObjectVisitorException(string message, Exception inner) : base(message, inner)
			{
			}

			// Token: 0x060000C8 RID: 200 RVA: 0x00004A0E File Offset: 0x00002C0E
			protected ObjectVisitorException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}
		}
	}
}
