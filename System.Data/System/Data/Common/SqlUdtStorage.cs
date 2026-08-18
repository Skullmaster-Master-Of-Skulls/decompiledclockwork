using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace System.Data.Common
{
	// Token: 0x0200019A RID: 410
	internal sealed class SqlUdtStorage : DataStorage
	{
		// Token: 0x06001810 RID: 6160 RVA: 0x00250048 File Offset: 0x0024F448
		public SqlUdtStorage(DataColumn column, Type type) : this(column, type, SqlUdtStorage.GetStaticNullForUdtType(type))
		{
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00250068 File Offset: 0x0024F468
		private SqlUdtStorage(DataColumn column, Type type, object nullValue) : base(column, type, nullValue, nullValue, typeof(ICloneable).IsAssignableFrom(type))
		{
			this.implementsIXmlSerializable = typeof(IXmlSerializable).IsAssignableFrom(type);
			this.implementsIComparable = typeof(IComparable).IsAssignableFrom(type);
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x002500C8 File Offset: 0x0024F4C8
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
					if (field == null)
					{
						throw ExceptionBuilder.INullableUDTwithoutStaticNull(type.AssemblyQualifiedName);
					}
					value = field.GetValue(null);
				}
				lock (SqlUdtStorage.TypeToNull)
				{
					SqlUdtStorage.TypeToNull[type] = value;
				}
			}
			return value;
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00250168 File Offset: 0x0024F568
		public override bool IsNull(int record)
		{
			return ((INullable)this.values[record]).IsNull;
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00250188 File Offset: 0x0024F588
		public override object Aggregate(int[] records, AggregateType kind)
		{
			throw ExceptionBuilder.AggregateException(kind, this.DataType);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x002501A8 File Offset: 0x0024F5A8
		public override int Compare(int recordNo1, int recordNo2)
		{
			return this.CompareValueTo(recordNo1, this.values[recordNo2]);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x002501C8 File Offset: 0x0024F5C8
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

		// Token: 0x06001817 RID: 6167 RVA: 0x00250238 File Offset: 0x0024F638
		public override void Copy(int recordNo1, int recordNo2)
		{
			base.CopyBits(recordNo1, recordNo2);
			this.values[recordNo2] = this.values[recordNo1];
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00250268 File Offset: 0x0024F668
		public override object Get(int recordNo)
		{
			return this.values[recordNo];
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00250288 File Offset: 0x0024F688
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

		// Token: 0x0600181A RID: 6170 RVA: 0x00250308 File Offset: 0x0024F708
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

		// Token: 0x0600181B RID: 6171 RVA: 0x00250358 File Offset: 0x0024F758
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

		// Token: 0x0600181C RID: 6172 RVA: 0x002503F8 File Offset: 0x0024F7F8
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
				TypeLimiter.EnsureTypeIsAllowed(type);
				object obj = Activator.CreateInstance(type, true);
				((IXmlSerializable)obj).ReadXml(xmlReader);
				return obj;
			}
			XmlSerializer xmlSerializer = ObjectStorage.GetXmlSerializer(this.DataType, xmlAttrib);
			return xmlSerializer.Deserialize(xmlReader);
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00250488 File Offset: 0x0024F888
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

		// Token: 0x0600181E RID: 6174 RVA: 0x00250508 File Offset: 0x0024F908
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

		// Token: 0x0600181F RID: 6175 RVA: 0x00250548 File Offset: 0x0024F948
		protected override object GetEmptyStorage(int recordCount)
		{
			return new object[recordCount];
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00250568 File Offset: 0x0024F968
		protected override void CopyValue(int record, object store, BitArray nullbits, int storeIndex)
		{
			object[] array = (object[])store;
			array[storeIndex] = this.values[record];
			nullbits.Set(storeIndex, this.IsNull(record));
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00250598 File Offset: 0x0024F998
		protected override void SetStorage(object store, BitArray nullbits)
		{
			this.values = (object[])store;
		}

		// Token: 0x04000D19 RID: 3353
		private object[] values;

		// Token: 0x04000D1A RID: 3354
		private readonly bool implementsIXmlSerializable;

		// Token: 0x04000D1B RID: 3355
		private readonly bool implementsIComparable;

		// Token: 0x04000D1C RID: 3356
		private static readonly Dictionary<Type, object> TypeToNull = new Dictionary<Type, object>();
	}
}
