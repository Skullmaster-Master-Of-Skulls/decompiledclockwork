using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x02000323 RID: 803
	internal sealed class SqlUdtStorage : DataStorage
	{
		// Token: 0x0600328B RID: 12939 RVA: 0x0013A2CC File Offset: 0x001396CC
		public SqlUdtStorage(DataColumn column, Type type) : this(column, type, SqlUdtStorage.GetStaticNullForUdtType(type))
		{
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x0013A2E8 File Offset: 0x001396E8
		private SqlUdtStorage(DataColumn column, Type type, object nullValue) : base(column, type, nullValue, nullValue, typeof(ICloneable).IsAssignableFrom(type), DataStorage.GetStorageType(type))
		{
			this.implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
			this.implementsIComparable = typeof(IComparable).IsAssignableFrom(type);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x0013A344 File Offset: 0x00139744
		internal static object GetStaticNullForUdtType(Type type)
		{
			object value;
			if (!SqlUdtStorage.TypeToNull.TryGetValue(type, out value))
			{
				PropertyInfo property = type.GetProperty("Null", BindingFlags.Static | BindingFlags.Public);
				if (property != null)
				{
					value = property.GetValue(null, null);
				}
				else
				{
					FieldInfo field = type.GetField("Null", BindingFlags.Static | BindingFlags.Public);
					if (!(field != null))
					{
						throw ExceptionBuilder.INullableUDTwithoutStaticNull(type.AssemblyQualifiedName);
					}
					value = field.GetValue(null);
				}
				Dictionary<Type, object> typeToNull = SqlUdtStorage.TypeToNull;
				lock (typeToNull)
				{
					SqlUdtStorage.TypeToNull[type] = value;
				}
			}
			return value;
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x0013A3FC File Offset: 0x001397FC
		public override bool IsNull(int record)
		{
			return ((INullable)this.values[record]).IsNull;
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x0013A41C File Offset: 0x0013981C
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06003290 RID: 12944 RVA: 0x0013A438 File Offset: 0x00139838
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.CompareValueTo(recordNo1, this.values[recordNo2]);
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x0013A454 File Offset: 0x00139854
		public override int CompareValueTo(int recordNo1, object value)
		{
			if (DBNull.Value == value)
			{
				value = this.NullValue;
			}
			if (this.implementsIComparable)
			{
				IComparable comparable = (IComparable)this.values[recordNo1];
				return comparable.CompareTo(value);
			}
			if (this.NullValue != value)
			{
				throw ExceptionBuilder.IComparableNotImplemented(this.DataType.AssemblyQualifiedName);
			}
			INullable nullable = (INullable)this.values[recordNo1];
			if (!nullable.IsNull)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x0013A4C4 File Offset: 0x001398C4
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x0013A4EC File Offset: 0x001398EC
		public override object Get(int recordNo)
		{
			return this.values[recordNo];
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x0013A504 File Offset: 0x00139904
		public override void Set(int recordNo, object value)
		{
			if (DBNull.Value == value)
			{
				this.values[recordNo] = this.NullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			if (value == null)
			{
				if (this.IsValueType)
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this.values[recordNo] = this.NullValue;
				base.SetNullBit(recordNo, true);
				return;
			}
			else
			{
				if (!this.DataType.IsInstanceOfType(value))
				{
					throw ExceptionBuilder.StorageSetFailed();
				}
				this.values[recordNo] = value;
				base.SetNullBit(recordNo, false);
				return;
			}
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x0013A580 File Offset: 0x00139980
		public override void SetCapacity(int capacity)
		{
			object[] destinationArray = new object[capacity];
			if (this.values != null)
			{
				Array.Copy(this.values, 0, destinationArray, 0, Math.Min(capacity, this.values.Length));
			}
			this.values = destinationArray;
			base.SetCapacity(capacity);
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x0013A5C8 File Offset: 0x001399C8
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(string s)
		{
			if (this.implementsIXmlSerializable)
			{
				object obj = Activator.CreateInstance(this.DataType, true);
				string s2 = "<col>" + s + "</col>";
				StringReader input = new StringReader(s2);
				using (XmlTextReader xmlTextReader = new XmlTextReader(input))
				{
					((IXmlSerializable)obj).ReadXml(xmlTextReader);
				}
				return obj;
			}
			StringReader textReader = new StringReader(s);
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(this.DataType);
			return xmlSerializer.Deserialize(textReader);
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x0013A660 File Offset: 0x00139A60
		[MethodImpl(MethodImplOptions.NoInlining)]
		public override object ConvertXmlToObject(XmlReader xmlReader, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				string text = xmlReader.GetAttribute("InstanceType", "urn:schemas-microsoft-com:xml-msdata");
				if (text == null)
				{
					string attribute = xmlReader.GetAttribute("InstanceType", "http://www.w3.org/2001/XMLSchema-instance");
					if (attribute != null)
					{
						text = XSDSchema.XsdtoClr(attribute).FullName;
					}
				}
				Type type = (text == null) ? this.DataType : Type.GetType(text);
				TypeLimiter.EnsureTypeIsAllowed(type, null);
				object obj = Activator.CreateInstance(type, true);
				((IXmlSerializable)obj).ReadXml(xmlReader);
				return obj;
			}
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(this.DataType, xmlAttrib);
			return xmlSerializer.Deserialize(xmlReader);
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x0013A6EC File Offset: 0x00139AEC
		public override string ConvertObjectToXml(object value)
		{
			StringWriter stringWriter = new StringWriter(base.FormatProvider);
			if (this.implementsIXmlSerializable)
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					((IXmlSerializable)value).WriteXml(xmlTextWriter);
					goto IL_47;
				}
			}
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(value.GetType());
			xmlSerializer.Serialize(stringWriter, value);
			IL_47:
			return stringWriter.ToString();
		}

		// Token: 0x06003299 RID: 12953 RVA: 0x0013A764 File Offset: 0x00139B64
		public override void ConvertObjectToXml(object value, XmlWriter xmlWriter, XmlRootAttribute xmlAttrib)
		{
			if (xmlAttrib == null)
			{
				((IXmlSerializable)value).WriteXml(xmlWriter);
				return;
			}
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(this.DataType, xmlAttrib);
			xmlSerializer.Serialize(xmlWriter, value);
		}

		// Token: 0x0600329A RID: 12954 RVA: 0x0013A798 File Offset: 0x00139B98
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x0600329B RID: 12955 RVA: 0x0013A7AC File Offset: 0x00139BAC
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			object[] array = (object[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x0600329C RID: 12956 RVA: 0x0013A7DC File Offset: 0x00139BDC
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (object[])store;
		}

		// Token: 0x04001DBF RID: 7615
		private object[] values;

		// Token: 0x04001DC0 RID: 7616
		private readonly bool implementsIXmlSerializable;

		// Token: 0x04001DC1 RID: 7617
		private readonly bool implementsIComparable;

		// Token: 0x04001DC2 RID: 7618
		private static readonly Dictionary<Type, object> TypeToNull = new Dictionary<Type, object>();
	}
}
