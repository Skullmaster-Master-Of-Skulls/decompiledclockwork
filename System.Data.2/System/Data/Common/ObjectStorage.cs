using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200030B RID: 779
	internal sealed class ObjectStorage : DataStorage
	{
		// Token: 0x06003133 RID: 12595 RVA: 0x00131EC0 File Offset: 0x001312C0
		internal ObjectStorage(DataColumn column, Type type) : base(column, type, ObjectStorage.defaultValue, DBNull.Value, typeof(ICloneable).IsAssignableFrom(type), DataStorage.GetStorageType(type))
		{
			this.implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x00131F0C File Offset: 0x0013130C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x00131F28 File Offset: 0x00131328
		public override int Compare(int recordNo1, int recordNo2)
		{
			object obj = this.values[recordNo1];
			object obj2 = this.values[recordNo2];
			if (obj == obj2)
			{
				return 0;
			}
			if (obj == null)
			{
				return -1;
			}
			if (obj2 == null)
			{
				return 1;
			}
			IComparable comparable = obj as IComparable;
			if (comparable != null)
			{
				try
				{
					return comparable.CompareTo(obj2);
				}
				catch (ArgumentException e)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				}
			}
			return this.CompareWithFamilies(obj, obj2);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x00131F9C File Offset: 0x0013139C
		public override int CompareValueTo(int recordNo1, object value)
		{
			object obj = this.Get(recordNo1);
			if (obj is IComparable && value.GetType() == obj.GetType())
			{
				return ((IComparable)obj).CompareTo(value);
			}
			if (obj == value)
			{
				return 0;
			}
			if (obj == null)
			{
				if (this.NullValue == value)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (this.NullValue == value || value == null)
				{
					return 1;
				}
				return this.CompareWithFamilies(obj, value);
			}
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x00132008 File Offset: 0x00131408
		private int CompareTo(object valueNo1, object valueNo2)
		{
			if (valueNo1 == null)
			{
				return -1;
			}
			if (valueNo2 == null)
			{
				return 1;
			}
			if (valueNo1 == valueNo2)
			{
				return 0;
			}
			if (valueNo1 == this.NullValue)
			{
				return -1;
			}
			if (valueNo2 == this.NullValue)
			{
				return 1;
			}
			if (valueNo1 is IComparable)
			{
				try
				{
					return ((IComparable)valueNo1).CompareTo(valueNo2);
				}
				catch (ArgumentException e)
				{
					ExceptionBuilder.TraceExceptionWithoutRethrow(e);
				}
			}
			return this.CompareWithFamilies(valueNo1, valueNo2);
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x00132084 File Offset: 0x00131484
		private int CompareWithFamilies(object valueNo1, object valueNo2)
		{
			ObjectStorage.Families family = this.GetFamily(valueNo1.GetType());
			ObjectStorage.Families family2 = this.GetFamily(valueNo2.GetType());
			if (family < family2)
			{
				return -1;
			}
			if (family > family2)
			{
				return 1;
			}
			switch (family)
			{
			case ObjectStorage.Families.DATETIME:
				valueNo1 = Convert.ToDateTime(valueNo1, base.FormatProvider);
				valueNo2 = Convert.ToDateTime(valueNo1, base.FormatProvider);
				goto IL_134;
			case ObjectStorage.Families.NUMBER:
				valueNo1 = Convert.ToDouble(valueNo1, base.FormatProvider);
				valueNo2 = Convert.ToDouble(valueNo2, base.FormatProvider);
				goto IL_134;
			case ObjectStorage.Families.BOOLEAN:
				valueNo1 = Convert.ToBoolean(valueNo1, base.FormatProvider);
				valueNo2 = Convert.ToBoolean(valueNo2, base.FormatProvider);
				goto IL_134;
			case ObjectStorage.Families.ARRAY:
			{
				Array array = (Array)valueNo1;
				Array array2 = (Array)valueNo2;
				if (array.Length > array2.Length)
				{
					return 1;
				}
				if (array.Length < array2.Length)
				{
					return -1;
				}
				for (int i = 0; i < array.Length; i++)
				{
					int num = this.CompareTo(array.GetValue(i), array2.GetValue(i));
					if (num != 0)
					{
						return num;
					}
				}
				return 0;
			}
			}
			valueNo1 = valueNo1.ToString();
			valueNo2 = valueNo2.ToString();
			IL_134:
			return ((IComparable)valueNo1).CompareTo(valueNo2);
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x001321D4 File Offset: 0x001315D4
		public override void Copy(int recordNo1, int recordNo2)
		{
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x001321F4 File Offset: 0x001315F4
		public override object Get(int recordNo)
		{
			object obj = this.values[recordNo];
			if (obj != null)
			{
				return obj;
			}
			return this.NullValue;
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x00132218 File Offset: 0x00131618
		private ObjectStorage.Families GetFamily(Type dataType)
		{
			switch (Type.GetTypeCode(dataType))
			{
			case TypeCode.Boolean:
				return ObjectStorage.Families.BOOLEAN;
			case TypeCode.Char:
				return ObjectStorage.Families.STRING;
			case TypeCode.SByte:
				return ObjectStorage.Families.STRING;
			case TypeCode.Byte:
				return ObjectStorage.Families.STRING;
			case TypeCode.Int16:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.UInt16:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Int32:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.UInt32:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Int64:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.UInt64:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Single:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Double:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.Decimal:
				return ObjectStorage.Families.NUMBER;
			case TypeCode.DateTime:
				return ObjectStorage.Families.DATETIME;
			case TypeCode.String:
				return ObjectStorage.Families.STRING;
			}
			if (typeof(TimeSpan) == dataType)
			{
				return ObjectStorage.Families.DATETIME;
			}
			if (dataType.IsArray)
			{
				return ObjectStorage.Families.ARRAY;
			}
			return ObjectStorage.Families.STRING;
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x001322B4 File Offset: 0x001316B4
		public override bool IsNull(int record)
		{
			return this.values[record] == null;
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x001322CC File Offset: 0x001316CC
		public override void Set(int recordNo, object value)
		{
			if (this.NullValue == value)
			{
				this.values[recordNo] = null;
				return;
			}
			if (this.DataType == typeof(object) || this.DataType.IsInstanceOfType(value))
			{
				this.values[recordNo] = value;
				return;
			}
			Type type = value.GetType();
			if (this.DataType == typeof(Guid) && type == typeof(string))
			{
				this.values[recordNo] = new Guid((string)value);
				return;
			}
			if (!(this.DataType == typeof(byte[])))
			{
				throw ExceptionBuilder.StorageSetFailed();
			}
			if (type == typeof(bool))
			{
				this.values[recordNo] = BitConverter.GetBytes((bool)value);
				return;
			}
			if (type == typeof(char))
			{
				this.values[recordNo] = BitConverter.GetBytes((char)value);
				return;
			}
			if (type == typeof(short))
			{
				this.values[recordNo] = BitConverter.GetBytes((short)value);
				return;
			}
			if (type == typeof(int))
			{
				this.values[recordNo] = BitConverter.GetBytes((int)value);
				return;
			}
			if (type == typeof(long))
			{
				this.values[recordNo] = BitConverter.GetBytes((long)value);
				return;
			}
			if (type == typeof(ushort))
			{
				this.values[recordNo] = BitConverter.GetBytes((ushort)value);
				return;
			}
			if (type == typeof(uint))
			{
				this.values[recordNo] = BitConverter.GetBytes((uint)value);
				return;
			}
			if (type == typeof(ulong))
			{
				this.values[recordNo] = BitConverter.GetBytes((ulong)value);
				return;
			}
			if (type == typeof(float))
			{
				this.values[recordNo] = BitConverter.GetBytes((float)value);
				return;
			}
			if (type == typeof(double))
			{
				this.values[recordNo] = BitConverter.GetBytes((double)value);
				return;
			}
			throw ExceptionBuilder.StorageSetFailed();
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x00132508 File Offset: 0x00131908
		public override void SetCapacity(int capacity)
		{
			object[] destinationArray = new object[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x00132548 File Offset: 0x00131948
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(string s)
		{
			Type dataType = this.DataType;
			if (dataType == typeof(byte[]))
			{
				return Convert.FromBase64String(s);
			}
			if (dataType == typeof(Type))
			{
				return Type.GetType(s);
			}
			if (dataType == typeof(Guid))
			{
				return new Guid(s);
			}
			if (dataType == typeof(Uri))
			{
				return new Uri(s);
			}
			if (this.implementsIXmlSerializable)
			{
				object obj = Activator.CreateInstance(this.DataType, true);
				StringReader input = new StringReader(s);
				using (XmlTextReader xmlTextReader = new XmlTextReader(input))
				{
					((IXmlSerializable)obj).ReadXml(xmlTextReader);
				}
				return obj;
			}
			StringReader textReader = new StringReader(s);
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(dataType);
			return xmlSerializer.Deserialize(textReader);
		}

		// Token: 0x06003140 RID: 12608 RVA: 0x00132638 File Offset: 0x00131A38
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			bool flag = false;
			bool flag2 = false;
			object obj;
			if (xmlAttrib == null)
			{
				Type type = null;
				string attribute = xmlReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				if (attribute == null || attribute.Length == 0)
				{
					string text = xmlReader.GetAttribute("type", "http://www.w3.org/2001/XMLSchema-instance");
					if (text != null && text.Length > 0)
					{
						string[] array = text.Split(new char[]
						{
							':'
						});
						if (array.Length == 2 && xmlReader.LookupNamespace(array[0]) == "http://www.w3.org/2001/XMLSchema")
						{
							text = array[1];
						}
						type = XSDSchema.XsdtoClr(text);
						flag = true;
					}
					else if (this.DataType == typeof(object))
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					obj = xmlReader.ReadString();
				}
				else if (attribute == "Type")
				{
					obj = Type.GetType(xmlReader.ReadString());
					xmlReader.Read();
				}
				else
				{
					if (null == type)
					{
						type = ((attribute == null) ? this.DataType : DataStorage.GetType(attribute));
					}
					if (type == typeof(char) || type == typeof(Guid))
					{
						flag = true;
					}
					if (type == typeof(object))
					{
						throw ExceptionBuilder.CanNotDeserializeObjectType();
					}
					TypeLimiter.EnsureTypeIsAllowed(type, null);
					if (!flag)
					{
						obj = Activator.CreateInstance(type, true);
						((IXmlSerializable)obj).ReadXml(xmlReader);
					}
					else
					{
						if (type == typeof(string) && xmlReader.NodeType == XmlNodeType.Element && xmlReader.IsEmptyElement)
						{
							obj = string.Empty;
						}
						else
						{
							obj = xmlReader.ReadString();
							if (type != typeof(byte[]))
							{
								obj = SqlConvert.ChangeTypeForXML(obj, type);
							}
							else
							{
								obj = Convert.FromBase64String(obj.ToString());
							}
						}
						xmlReader.Read();
					}
				}
			}
			else
			{
				XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(this.DataType, xmlAttrib);
				obj = xmlSerializer.Deserialize(xmlReader);
			}
			return obj;
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x0013281C File Offset: 0x00131C1C
		public override string ConvertObjectToXml(object value)
		{
			if (value == null || value == this.NullValue)
			{
				return string.Empty;
			}
			Type dataType = this.DataType;
			if (dataType == typeof(byte[]) || (dataType == typeof(object) && value is byte[]))
			{
				return Convert.ToBase64String((byte[])value);
			}
			if (dataType == typeof(Type) || (dataType == typeof(object) && value is Type))
			{
				return ((Type)value).AssemblyQualifiedName;
			}
			if (!DataStorage.IsTypeCustomType(value.GetType()))
			{
				return (string)SqlConvert.ChangeTypeForXML(value, typeof(string));
			}
			if (Type.GetTypeCode(value.GetType()) != TypeCode.Object)
			{
				return value.ToString();
			}
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			if (this.implementsIXmlSerializable)
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					((IXmlSerializable)value).WriteXml(xmlTextWriter);
				}
				return stringWriter.ToString();
			}
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(value.GetType());
			xmlSerializer.Serialize(stringWriter, value);
			return stringWriter.ToString();
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x0013295C File Offset: 0x00131D5C
		public override void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				((IXmlSerializable)value).WriteXml(xmlWriter);
				return;
			}
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(value.GetType(), xmlAttrib);
			xmlSerializer.Serialize(xmlWriter, value);
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x00132990 File Offset: 0x00131D90
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x06003144 RID: 12612 RVA: 0x001329A4 File Offset: 0x00131DA4
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			object[] array = (object[])store;
			array[storeIndex] = this.values[record];
			bool flag = this.IsNull(record);
			nullbits.Set(storeIndex, flag);
			if (!flag && array[storeIndex] is DateTime)
			{
				DateTime dateTime = (DateTime)array[storeIndex];
				if (dateTime.Kind == DateTimeKind.Local)
				{
					array[storeIndex] = DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Local);
				}
			}
		}

		// Token: 0x06003145 RID: 12613 RVA: 0x00132A10 File Offset: 0x00131E10
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (object[])store;
			for (int i = 0; i < this.values.Length; i++)
			{
				if (this.values[i] is DateTime)
				{
					DateTime value = (DateTime)this.values[i];
					if (value.Kind == DateTimeKind.Local)
					{
						this.values[i] = DateTime.SpecifyKind(value, DateTimeKind.Utc).ToLocalTime();
					}
				}
			}
		}

		// Token: 0x06003146 RID: 12614 RVA: 0x00132A80 File Offset: 0x00131E80
		internal static void VerifyIDynamicMetaObjectProvider(Type type)
		{
			if (typeof(IDynamicMetaObjectProvider).IsAssignableFrom(type) && !typeof(IXmlSerializable).IsAssignableFrom(type))
			{
				throw ADP.InvalidOperation(Res.GetString("Xml_DynamicWithoutXmlSerializable"));
			}
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x00132AC4 File Offset: 0x00131EC4
		internal static XmlSerializer GetXmlSerializer(Type type)
		{
			ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
			return ObjectStorage._serializerFactory.CreateSerializer(type);
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x00132AE4 File Offset: 0x00131EE4
		internal static XmlSerializer GetXmlSerializer(Type type, XmlRootAttribute attribute)
		{
			XmlSerializer xmlSerializer = null;
			KeyValuePair<Type, XmlRootAttribute> key = new KeyValuePair<Type, XmlRootAttribute>(type, attribute);
			Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> dictionary = ObjectStorage._tempAssemblyCache;
			if (dictionary == null || !dictionary.TryGetValue(key, out xmlSerializer))
			{
				object tempAssemblyCacheLock = ObjectStorage._tempAssemblyCacheLock;
				lock (tempAssemblyCacheLock)
				{
					dictionary = ObjectStorage._tempAssemblyCache;
					if (dictionary == null || !dictionary.TryGetValue(key, out xmlSerializer))
					{
						ObjectStorage.VerifyIDynamicMetaObjectProvider(type);
						if (dictionary != null)
						{
							Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> dictionary2 = new Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer>(1 + dictionary.Count, ObjectStorage.TempAssemblyComparer.Default);
							foreach (KeyValuePair<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> keyValuePair in dictionary)
							{
								dictionary2.Add(keyValuePair.Key, keyValuePair.Value);
							}
							dictionary = dictionary2;
						}
						else
						{
							dictionary = new Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer>(ObjectStorage.TempAssemblyComparer.Default);
						}
						key = new KeyValuePair<Type, XmlRootAttribute>(type, new XmlRootAttribute());
						key.Value.ElementName = attribute.ElementName;
						key.Value.Namespace = attribute.Namespace;
						key.Value.DataType = attribute.DataType;
						key.Value.IsNullable = attribute.IsNullable;
						xmlSerializer = ObjectStorage._serializerFactory.CreateSerializer(type, attribute);
						dictionary.Add(key, xmlSerializer);
						ObjectStorage._tempAssemblyCache = dictionary;
					}
				}
			}
			return xmlSerializer;
		}

		// Token: 0x04001D79 RID: 7545
		private static readonly object defaultValue = null;

		// Token: 0x04001D7A RID: 7546
		private object[] values;

		// Token: 0x04001D7B RID: 7547
		private readonly bool implementsIXmlSerializable;

		// Token: 0x04001D7C RID: 7548
		private static readonly object _tempAssemblyCacheLock = new object();

		// Token: 0x04001D7D RID: 7549
		private static Dictionary<KeyValuePair<Type, XmlRootAttribute>, XmlSerializer> _tempAssemblyCache;

		// Token: 0x04001D7E RID: 7550
		private static readonly XmlSerializerFactory _serializerFactory = new XmlSerializerFactory();

		// Token: 0x02000440 RID: 1088
		private enum Families
		{
			// Token: 0x04002374 RID: 9076
			DATETIME,
			// Token: 0x04002375 RID: 9077
			NUMBER,
			// Token: 0x04002376 RID: 9078
			STRING,
			// Token: 0x04002377 RID: 9079
			BOOLEAN,
			// Token: 0x04002378 RID: 9080
			ARRAY
		}

		// Token: 0x02000441 RID: 1089
		private class TempAssemblyComparer : IEqualityComparer<KeyValuePair<Type, XmlRootAttribute>>
		{
			// Token: 0x06003654 RID: 13908 RVA: 0x00149AC8 File Offset: 0x00148EC8
			private TempAssemblyComparer()
			{
			}

			// Token: 0x06003655 RID: 13909 RVA: 0x00149ADC File Offset: 0x00148EDC
			public bool Equals(KeyValuePair<Type, XmlRootAttribute> x, KeyValuePair<Type, XmlRootAttribute> y)
			{
				return x.Key == y.Key && ((x.Value == null && y.Value == null) || (x.Value != null && y.Value != null && x.Value.ElementName == y.Value.ElementName && x.Value.Namespace == y.Value.Namespace && x.Value.DataType == y.Value.DataType && x.Value.IsNullable == y.Value.IsNullable));
			}

			// Token: 0x06003656 RID: 13910 RVA: 0x00149BAC File Offset: 0x00148FAC
			public int GetHashCode(KeyValuePair<Type, XmlRootAttribute> obj)
			{
				return obj.Key.GetHashCode() + obj.Value.ElementName.GetHashCode();
			}

			// Token: 0x04002379 RID: 9081
			internal static readonly IEqualityComparer<KeyValuePair<Type, XmlRootAttribute>> Default = new ObjectStorage.TempAssemblyComparer();
		}
	}
}
