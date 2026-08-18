using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Resources;

namespace System.Web.Script.Serialization
{
	// Token: 0x020000FF RID: 255
	public class JavaScriptSerializer
	{
		// Token: 0x06000D92 RID: 3474 RVA: 0x0002F8FC File Offset: 0x0002DAFC
		internal static string SerializeInternal(object o)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(o);
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x0002F918 File Offset: 0x0002DB18
		internal static object Deserialize(JavaScriptSerializer serializer, string input, Type type, int depthLimit)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			if (input.Length > serializer.MaxJsonLength)
			{
				throw new ArgumentException(AtlasWeb.JSON_MaxJsonLengthExceeded, "input");
			}
			object o = JavaScriptObjectDeserializer.BasicDeserialize(input, depthLimit, serializer);
			return ObjectConverter.ConvertObjectToType(o, type, serializer);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x0002F962 File Offset: 0x0002DB62
		public JavaScriptSerializer() : this(null)
		{
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x0002F96B File Offset: 0x0002DB6B
		public JavaScriptSerializer(JavaScriptTypeResolver resolver)
		{
			this._typeResolver = resolver;
			this.RecursionLimit = 100;
			this.MaxJsonLength = 2097152;
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0002F98D File Offset: 0x0002DB8D
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0002F995 File Offset: 0x0002DB95
		public int MaxJsonLength
		{
			get
			{
				return this._maxJsonLength;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException(AtlasWeb.JSON_InvalidMaxJsonLength);
				}
				this._maxJsonLength = value;
			}
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0002F9AD File Offset: 0x0002DBAD
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0002F9B5 File Offset: 0x0002DBB5
		public int RecursionLimit
		{
			get
			{
				return this._recursionLimit;
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException(AtlasWeb.JSON_InvalidRecursionLimit);
				}
				this._recursionLimit = value;
			}
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0002F9CD File Offset: 0x0002DBCD
		internal JavaScriptTypeResolver TypeResolver
		{
			get
			{
				return this._typeResolver;
			}
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0002F9D5 File Offset: 0x0002DBD5
		private Dictionary<Type, JavaScriptConverter> Converters
		{
			get
			{
				if (this._converters == null)
				{
					this._converters = new Dictionary<Type, JavaScriptConverter>();
				}
				return this._converters;
			}
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x0002F9F0 File Offset: 0x0002DBF0
		public void RegisterConverters(IEnumerable<JavaScriptConverter> converters)
		{
			if (converters == null)
			{
				throw new ArgumentNullException("converters");
			}
			foreach (JavaScriptConverter javaScriptConverter in converters)
			{
				IEnumerable<Type> supportedTypes = javaScriptConverter.SupportedTypes;
				if (supportedTypes != null)
				{
					foreach (Type key in supportedTypes)
					{
						this.Converters[key] = javaScriptConverter;
					}
				}
			}
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x0002FA88 File Offset: 0x0002DC88
		private JavaScriptConverter GetConverter(Type t)
		{
			if (this._converters != null)
			{
				while (t != null)
				{
					if (this._converters.ContainsKey(t))
					{
						return this._converters[t];
					}
					t = t.BaseType;
				}
			}
			return null;
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x0002FAC1 File Offset: 0x0002DCC1
		internal bool ConverterExistsForType(Type t, out JavaScriptConverter converter)
		{
			converter = this.GetConverter(t);
			return converter != null;
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x0002FAD1 File Offset: 0x0002DCD1
		public object DeserializeObject(string input)
		{
			return JavaScriptSerializer.Deserialize(this, input, null, this.RecursionLimit);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x0002FAE1 File Offset: 0x0002DCE1
		public T Deserialize<T>(string input)
		{
			return (T)((object)JavaScriptSerializer.Deserialize(this, input, typeof(T), this.RecursionLimit));
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x0002FAFF File Offset: 0x0002DCFF
		public object Deserialize(string input, Type targetType)
		{
			return JavaScriptSerializer.Deserialize(this, input, targetType, this.RecursionLimit);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x0002FB0F File Offset: 0x0002DD0F
		public T ConvertToType<T>(object obj)
		{
			return (T)((object)ObjectConverter.ConvertObjectToType(obj, typeof(T), this));
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x0002FB27 File Offset: 0x0002DD27
		public object ConvertToType(object obj, Type targetType)
		{
			return ObjectConverter.ConvertObjectToType(obj, targetType, this);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x0002FB31 File Offset: 0x0002DD31
		public string Serialize(object obj)
		{
			return this.Serialize(obj, JavaScriptSerializer.SerializationFormat.JSON);
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x0002FB3C File Offset: 0x0002DD3C
		internal string Serialize(object obj, JavaScriptSerializer.SerializationFormat serializationFormat)
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.Serialize(obj, stringBuilder, serializationFormat);
			return stringBuilder.ToString();
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x0002FB5E File Offset: 0x0002DD5E
		public void Serialize(object obj, StringBuilder output)
		{
			this.Serialize(obj, output, JavaScriptSerializer.SerializationFormat.JSON);
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0002FB69 File Offset: 0x0002DD69
		internal void Serialize(object obj, StringBuilder output, JavaScriptSerializer.SerializationFormat serializationFormat)
		{
			this.SerializeValue(obj, output, 0, null, serializationFormat, null);
			if (serializationFormat == JavaScriptSerializer.SerializationFormat.JSON && output.Length > this.MaxJsonLength)
			{
				throw new InvalidOperationException(AtlasWeb.JSON_MaxJsonLengthExceeded);
			}
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0002FB93 File Offset: 0x0002DD93
		private static void SerializeBoolean(bool o, StringBuilder sb)
		{
			if (o)
			{
				sb.Append("true");
				return;
			}
			sb.Append("false");
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0002FBB1 File Offset: 0x0002DDB1
		private static void SerializeUri(Uri uri, StringBuilder sb)
		{
			sb.Append("\"").Append(uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped)).Append("\"");
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0002FBDA File Offset: 0x0002DDDA
		private static void SerializeGuid(Guid guid, StringBuilder sb)
		{
			sb.Append("\"").Append(guid.ToString()).Append("\"");
		}

		// Token: 0x06000DAB RID: 3499 RVA: 0x0002FC04 File Offset: 0x0002DE04
		private static void SerializeDateTime(DateTime datetime, StringBuilder sb, JavaScriptSerializer.SerializationFormat serializationFormat)
		{
			if (serializationFormat == JavaScriptSerializer.SerializationFormat.JSON)
			{
				sb.Append("\"\\/Date(");
				sb.Append((datetime.ToUniversalTime().Ticks - JavaScriptSerializer.DatetimeMinTimeTicks) / 10000L);
				sb.Append(")\\/\"");
				return;
			}
			sb.Append("new Date(");
			sb.Append((datetime.ToUniversalTime().Ticks - JavaScriptSerializer.DatetimeMinTimeTicks) / 10000L);
			sb.Append(")");
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x0002FC8C File Offset: 0x0002DE8C
		private void SerializeCustomObject(object o, StringBuilder sb, int depth, Hashtable objectsInUse, JavaScriptSerializer.SerializationFormat serializationFormat)
		{
			bool flag = true;
			Type type = o.GetType();
			sb.Append('{');
			if (this.TypeResolver != null)
			{
				string text = this.TypeResolver.ResolveTypeId(type);
				if (text != null)
				{
					JavaScriptSerializer.SerializeString("__type", sb);
					sb.Append(':');
					this.SerializeValue(text, sb, depth, objectsInUse, serializationFormat, null);
					flag = false;
				}
			}
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public);
			foreach (FieldInfo fieldInfo in fields)
			{
				if (!this.CheckScriptIgnoreAttribute(fieldInfo))
				{
					if (!flag)
					{
						sb.Append(',');
					}
					JavaScriptSerializer.SerializeString(fieldInfo.Name, sb);
					sb.Append(':');
					this.SerializeValue(SecurityUtils.FieldInfoGetValue(fieldInfo, o), sb, depth, objectsInUse, serializationFormat, fieldInfo);
					flag = false;
				}
			}
			PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (!this.CheckScriptIgnoreAttribute(propertyInfo))
				{
					MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (!(getMethod == null) && getMethod.GetParameters().Length == 0)
					{
						if (!flag)
						{
							sb.Append(',');
						}
						JavaScriptSerializer.SerializeString(propertyInfo.Name, sb);
						sb.Append(':');
						this.SerializeValue(SecurityUtils.MethodInfoInvoke(getMethod, o, null), sb, depth, objectsInUse, serializationFormat, propertyInfo);
						flag = false;
					}
				}
			}
			sb.Append('}');
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x0002FDE8 File Offset: 0x0002DFE8
		private bool CheckScriptIgnoreAttribute(MemberInfo memberInfo)
		{
			if (memberInfo.IsDefined(typeof(ScriptIgnoreAttribute), true))
			{
				return true;
			}
			ScriptIgnoreAttribute scriptIgnoreAttribute = (ScriptIgnoreAttribute)Attribute.GetCustomAttribute(memberInfo, typeof(ScriptIgnoreAttribute), true);
			return scriptIgnoreAttribute != null && scriptIgnoreAttribute.ApplyToOverrides;
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0002FE30 File Offset: 0x0002E030
		private void SerializeDictionary(IDictionary o, StringBuilder sb, int depth, Hashtable objectsInUse, JavaScriptSerializer.SerializationFormat serializationFormat)
		{
			sb.Append('{');
			bool flag = true;
			bool flag2 = false;
			if (o.Contains("__type"))
			{
				flag = false;
				flag2 = true;
				this.SerializeDictionaryKeyValue("__type", o["__type"], sb, depth, objectsInUse, serializationFormat);
			}
			foreach (object obj in o)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				string text = dictionaryEntry.Key as string;
				if (text == null)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, AtlasWeb.JSON_DictionaryTypeNotSupported, new object[]
					{
						o.GetType().FullName
					}));
				}
				if (flag2 && string.Equals(text, "__type", StringComparison.Ordinal))
				{
					flag2 = false;
				}
				else
				{
					if (!flag)
					{
						sb.Append(',');
					}
					this.SerializeDictionaryKeyValue(text, dictionaryEntry.Value, sb, depth, objectsInUse, serializationFormat);
					flag = false;
				}
			}
			sb.Append('}');
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0002FF40 File Offset: 0x0002E140
		private void SerializeDictionaryKeyValue(string key, object value, StringBuilder sb, int depth, Hashtable objectsInUse, JavaScriptSerializer.SerializationFormat serializationFormat)
		{
			JavaScriptSerializer.SerializeString(key, sb);
			sb.Append(':');
			this.SerializeValue(value, sb, depth, objectsInUse, serializationFormat, null);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0002FF64 File Offset: 0x0002E164
		private void SerializeEnumerable(IEnumerable enumerable, StringBuilder sb, int depth, Hashtable objectsInUse, JavaScriptSerializer.SerializationFormat serializationFormat)
		{
			sb.Append('[');
			bool flag = true;
			foreach (object o in enumerable)
			{
				if (!flag)
				{
					sb.Append(',');
				}
				this.SerializeValue(o, sb, depth, objectsInUse, serializationFormat, null);
				flag = false;
			}
			sb.Append(']');
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002FFDC File Offset: 0x0002E1DC
		private static void SerializeString(string input, StringBuilder sb)
		{
			sb.Append('"');
			sb.Append(HttpUtility.JavaScriptStringEncode(input));
			sb.Append('"');
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00030000 File Offset: 0x0002E200
		private void SerializeValue(object o, StringBuilder sb, int depth, Hashtable objectsInUse, JavaScriptSerializer.SerializationFormat serializationFormat, MemberInfo currentMember = null)
		{
			if (++depth > this._recursionLimit)
			{
				throw new ArgumentException(AtlasWeb.JSON_DepthLimitExceeded);
			}
			JavaScriptConverter javaScriptConverter = null;
			if (o != null && this.ConverterExistsForType(o.GetType(), out javaScriptConverter))
			{
				IDictionary<string, object> dictionary = javaScriptConverter.Serialize(o, this);
				if (this.TypeResolver != null)
				{
					string text = this.TypeResolver.ResolveTypeId(o.GetType());
					if (text != null)
					{
						dictionary["__type"] = text;
					}
				}
				sb.Append(this.Serialize(dictionary, serializationFormat));
				return;
			}
			this.SerializeValueInternal(o, sb, depth, objectsInUse, serializationFormat, currentMember);
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00030090 File Offset: 0x0002E290
		private void SerializeValueInternal(object o, StringBuilder sb, int depth, Hashtable objectsInUse, JavaScriptSerializer.SerializationFormat serializationFormat, MemberInfo currentMember)
		{
			if (o == null || DBNull.Value.Equals(o))
			{
				sb.Append("null");
				return;
			}
			string text = o as string;
			if (text != null)
			{
				JavaScriptSerializer.SerializeString(text, sb);
				return;
			}
			if (o is char)
			{
				if ((char)o == '\0')
				{
					sb.Append("null");
					return;
				}
				JavaScriptSerializer.SerializeString(o.ToString(), sb);
				return;
			}
			else
			{
				if (o is bool)
				{
					JavaScriptSerializer.SerializeBoolean((bool)o, sb);
					return;
				}
				if (o is DateTime)
				{
					JavaScriptSerializer.SerializeDateTime((DateTime)o, sb, serializationFormat);
					return;
				}
				if (o is DateTimeOffset)
				{
					JavaScriptSerializer.SerializeDateTime(((DateTimeOffset)o).UtcDateTime, sb, serializationFormat);
					return;
				}
				if (o is Guid)
				{
					JavaScriptSerializer.SerializeGuid((Guid)o, sb);
					return;
				}
				Uri uri = o as Uri;
				if (uri != null)
				{
					JavaScriptSerializer.SerializeUri(uri, sb);
					return;
				}
				if (o is double)
				{
					sb.Append(((double)o).ToString("r", CultureInfo.InvariantCulture));
					return;
				}
				if (o is float)
				{
					sb.Append(((float)o).ToString("r", CultureInfo.InvariantCulture));
					return;
				}
				if (o.GetType().IsPrimitive || o is decimal)
				{
					IConvertible convertible = o as IConvertible;
					if (convertible != null)
					{
						sb.Append(convertible.ToString(CultureInfo.InvariantCulture));
						return;
					}
					sb.Append(o.ToString());
					return;
				}
				else
				{
					Type type = o.GetType();
					if (!type.IsEnum)
					{
						try
						{
							if (objectsInUse == null)
							{
								objectsInUse = new Hashtable(new JavaScriptSerializer.ReferenceComparer());
							}
							else if (objectsInUse.ContainsKey(o))
							{
								throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, AtlasWeb.JSON_CircularReference, new object[]
								{
									type.FullName
								}));
							}
							objectsInUse.Add(o, null);
							IDictionary dictionary = o as IDictionary;
							if (dictionary != null)
							{
								this.SerializeDictionary(dictionary, sb, depth, objectsInUse, serializationFormat);
							}
							else
							{
								IEnumerable enumerable = o as IEnumerable;
								if (enumerable != null)
								{
									this.SerializeEnumerable(enumerable, sb, depth, objectsInUse, serializationFormat);
								}
								else
								{
									this.SerializeCustomObject(o, sb, depth, objectsInUse, serializationFormat);
								}
							}
						}
						finally
						{
							if (objectsInUse != null)
							{
								objectsInUse.Remove(o);
							}
						}
						return;
					}
					Type underlyingType = Enum.GetUnderlyingType(type);
					if (underlyingType == typeof(long) || underlyingType == typeof(ulong))
					{
						string message = (currentMember != null) ? (string.Format(CultureInfo.CurrentCulture, AtlasWeb.JSON_CannotSerializeMemberGeneric, new object[]
						{
							currentMember.Name,
							currentMember.ReflectedType.FullName
						}) + " " + AtlasWeb.JSON_InvalidEnumType) : AtlasWeb.JSON_InvalidEnumType;
						throw new InvalidOperationException(message);
					}
					sb.Append(((Enum)o).ToString("D"));
					return;
				}
			}
		}

		// Token: 0x040003D4 RID: 980
		internal const string ServerTypeFieldName = "__type";

		// Token: 0x040003D5 RID: 981
		internal const int DefaultRecursionLimit = 100;

		// Token: 0x040003D6 RID: 982
		internal const int DefaultMaxJsonLength = 2097152;

		// Token: 0x040003D7 RID: 983
		private JavaScriptTypeResolver _typeResolver;

		// Token: 0x040003D8 RID: 984
		private int _recursionLimit;

		// Token: 0x040003D9 RID: 985
		private int _maxJsonLength;

		// Token: 0x040003DA RID: 986
		private Dictionary<Type, JavaScriptConverter> _converters;

		// Token: 0x040003DB RID: 987
		internal static readonly long DatetimeMinTimeTicks = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;

		// Token: 0x0200017E RID: 382
		private class ReferenceComparer : IEqualityComparer
		{
			// Token: 0x06001086 RID: 4230 RVA: 0x00038B15 File Offset: 0x00036D15
			bool IEqualityComparer.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x06001087 RID: 4231 RVA: 0x00038B1B File Offset: 0x00036D1B
			int IEqualityComparer.GetHashCode(object obj)
			{
				return RuntimeHelpers.GetHashCode(obj);
			}
		}

		// Token: 0x0200017F RID: 383
		internal enum SerializationFormat
		{
			// Token: 0x0400051F RID: 1311
			JSON,
			// Token: 0x04000520 RID: 1312
			JavaScript
		}
	}
}
