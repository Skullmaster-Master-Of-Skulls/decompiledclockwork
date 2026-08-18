using System;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x0200032E RID: 814
	internal sealed class DataRecordInternal : DbDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x0600332C RID: 13100 RVA: 0x0013C744 File Offset: 0x0013BB44
		internal DataRecordInternal(SchemaInfo[] schemaInfo, object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._schemaInfo = schemaInfo;
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x0013C774 File Offset: 0x0013BB74
		internal DataRecordInternal(object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x0013C79C File Offset: 0x0013BB9C
		internal void SetSchemaInfo(SchemaInfo[] schemaInfo)
		{
			this._schemaInfo = schemaInfo;
		}

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x0600332F RID: 13103 RVA: 0x0013C7B0 File Offset: 0x0013BBB0
		public override int FieldCount
		{
			get
			{
				return this._schemaInfo.Length;
			}
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x0013C7C8 File Offset: 0x0013BBC8
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw ADP.ArgumentNull("values");
			}
			int num = (values.Length < this._schemaInfo.Length) ? values.Length : this._schemaInfo.Length;
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x0013C818 File Offset: 0x0013BC18
		public override string GetName(int i)
		{
			return this._schemaInfo[i].name;
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x0013C838 File Offset: 0x0013BC38
		public override object GetValue(int i)
		{
			return this._values[i];
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x0013C850 File Offset: 0x0013BC50
		public override string GetDataTypeName(int i)
		{
			return this._schemaInfo[i].typeName;
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x0013C870 File Offset: 0x0013BC70
		public override Type GetFieldType(int i)
		{
			return this._schemaInfo[i].type;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x0013C890 File Offset: 0x0013BC90
		public override int GetOrdinal(string name)
		{
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x1700082B RID: 2091
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x1700082C RID: 2092
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0013C8DC File Offset: 0x0013BCDC
		public override bool GetBoolean(int i)
		{
			return (bool)this._values[i];
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0013C8F8 File Offset: 0x0013BCF8
		public override byte GetByte(int i)
		{
			return (byte)this._values[i];
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x0013C914 File Offset: 0x0013BD14
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			int num = 0;
			byte[] array = (byte[])this._values[i];
			num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					num = array.Length;
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (bufferIndex < 0 || bufferIndex >= buffer.Length)
					{
						throw ADP.InvalidDestinationBufferIndex(length, bufferIndex, "bufferIndex");
					}
					if (dataIndex < 0L || dataIndex >= (long)num)
					{
						throw ADP.InvalidSourceBufferIndex(length, dataIndex, "dataIndex");
					}
					if (num + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x0013C9F8 File Offset: 0x0013BDF8
		public override char GetChar(int i)
		{
			string text = (string)this._values[i];
			char[] array = text.ToCharArray();
			return array[0];
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x0013CA20 File Offset: 0x0013BE20
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			int num = 0;
			string text = (string)this._values[i];
			char[] array = text.ToCharArray();
			num = array.Length;
			if (dataIndex > 2147483647L)
			{
				throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
			}
			int num2 = (int)dataIndex;
			if (buffer == null)
			{
				return (long)num;
			}
			try
			{
				if (num2 < num)
				{
					if (num2 + length > num)
					{
						num -= num2;
					}
					else
					{
						num = length;
					}
				}
				Array.Copy(array, num2, buffer, bufferIndex, num);
			}
			catch (Exception e)
			{
				if (ADP.IsCatchableExceptionType(e))
				{
					num = array.Length;
					if (length < 0)
					{
						throw ADP.InvalidDataLength((long)length);
					}
					if (bufferIndex < 0 || bufferIndex >= buffer.Length)
					{
						throw ADP.InvalidDestinationBufferIndex(buffer.Length, bufferIndex, "bufferIndex");
					}
					if (num2 < 0 || num2 >= num)
					{
						throw ADP.InvalidSourceBufferIndex(num, dataIndex, "dataIndex");
					}
					if (num + bufferIndex > buffer.Length)
					{
						throw ADP.InvalidBufferSizeOrIndex(num, bufferIndex);
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x0013CB0C File Offset: 0x0013BF0C
		public override Guid GetGuid(int i)
		{
			return (Guid)this._values[i];
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x0013CB28 File Offset: 0x0013BF28
		public override short GetInt16(int i)
		{
			return (short)this._values[i];
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x0013CB44 File Offset: 0x0013BF44
		public override int GetInt32(int i)
		{
			return (int)this._values[i];
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x0013CB60 File Offset: 0x0013BF60
		public override long GetInt64(int i)
		{
			return (long)this._values[i];
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x0013CB7C File Offset: 0x0013BF7C
		public override float GetFloat(int i)
		{
			return (float)this._values[i];
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x0013CB98 File Offset: 0x0013BF98
		public override double GetDouble(int i)
		{
			return (double)this._values[i];
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x0013CBB4 File Offset: 0x0013BFB4
		public override string GetString(int i)
		{
			return (string)this._values[i];
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x0013CBD0 File Offset: 0x0013BFD0
		public override decimal GetDecimal(int i)
		{
			return (decimal)this._values[i];
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x0013CBEC File Offset: 0x0013BFEC
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this._values[i];
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x0013CC08 File Offset: 0x0013C008
		public override bool IsDBNull(int i)
		{
			object obj = this._values[i];
			return obj == null || Convert.IsDBNull(obj);
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x0013CC2C File Offset: 0x0013C02C
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x0013CC40 File Offset: 0x0013C040
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x0013CC50 File Offset: 0x0013C050
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0013CC60 File Offset: 0x0013C060
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0013CC70 File Offset: 0x0013C070
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0013CC80 File Offset: 0x0013C080
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x0013CC90 File Offset: 0x0013C090
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x0013CCA0 File Offset: 0x0013C0A0
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x0013CCB4 File Offset: 0x0013C0B4
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x0013CCC8 File Offset: 0x0013C0C8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x0013CCDC File Offset: 0x0013C0DC
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propertyDescriptors == null)
			{
				this._propertyDescriptors = new PropertyDescriptorCollection(null);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x0013CD04 File Offset: 0x0013C104
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001E03 RID: 7683
		private SchemaInfo[] _schemaInfo;

		// Token: 0x04001E04 RID: 7684
		private object[] _values;

		// Token: 0x04001E05 RID: 7685
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x04001E06 RID: 7686
		private FieldNameLookup _fieldNameLookup;
	}
}
