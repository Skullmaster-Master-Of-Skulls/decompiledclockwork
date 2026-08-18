using System;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x0200011D RID: 285
	internal sealed class DataRecordInternal : DbDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x06001246 RID: 4678 RVA: 0x002367D8 File Offset: 0x00235BD8
		internal DataRecordInternal(SchemaInfo[] schemaInfo, object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._schemaInfo = schemaInfo;
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00236808 File Offset: 0x00235C08
		internal DataRecordInternal(object[] values, PropertyDescriptorCollection descriptors, FieldNameLookup fieldNameLookup)
		{
			this._values = values;
			this._propertyDescriptors = descriptors;
			this._fieldNameLookup = fieldNameLookup;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x00236838 File Offset: 0x00235C38
		internal void SetSchemaInfo(SchemaInfo[] schemaInfo)
		{
			this._schemaInfo = schemaInfo;
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00236858 File Offset: 0x00235C58
		public override int FieldCount
		{
			get
			{
				return this._schemaInfo.Length;
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00236878 File Offset: 0x00235C78
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

		// Token: 0x0600124B RID: 4683 RVA: 0x002368C8 File Offset: 0x00235CC8
		public override string GetName(int i)
		{
			return this._schemaInfo[i].name;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x002368E8 File Offset: 0x00235CE8
		public override object GetValue(int i)
		{
			return this._values[i];
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00236908 File Offset: 0x00235D08
		public override string GetDataTypeName(int i)
		{
			return this._schemaInfo[i].typeName;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x00236928 File Offset: 0x00235D28
		public override Type GetFieldType(int i)
		{
			return this._schemaInfo[i].type;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00236948 File Offset: 0x00235D48
		public override int GetOrdinal(string name)
		{
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x17000261 RID: 609
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		// Token: 0x17000262 RID: 610
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x002369A8 File Offset: 0x00235DA8
		public override bool GetBoolean(int i)
		{
			return (bool)this._values[i];
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x002369C8 File Offset: 0x00235DC8
		public override byte GetByte(int i)
		{
			return (byte)this._values[i];
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x002369E8 File Offset: 0x00235DE8
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

		// Token: 0x06001255 RID: 4693 RVA: 0x00236AD8 File Offset: 0x00235ED8
		public override char GetChar(int i)
		{
			string text = (string)this._values[i];
			char[] array = text.ToCharArray();
			return array[0];
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x00236B08 File Offset: 0x00235F08
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

		// Token: 0x06001257 RID: 4695 RVA: 0x00236BF8 File Offset: 0x00235FF8
		public override Guid GetGuid(int i)
		{
			return (Guid)this._values[i];
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00236C18 File Offset: 0x00236018
		public override short GetInt16(int i)
		{
			return (short)this._values[i];
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00236C38 File Offset: 0x00236038
		public override int GetInt32(int i)
		{
			return (int)this._values[i];
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00236C58 File Offset: 0x00236058
		public override long GetInt64(int i)
		{
			return (long)this._values[i];
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00236C78 File Offset: 0x00236078
		public override float GetFloat(int i)
		{
			return (float)this._values[i];
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x00236C98 File Offset: 0x00236098
		public override double GetDouble(int i)
		{
			return (double)this._values[i];
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x00236CB8 File Offset: 0x002360B8
		public override string GetString(int i)
		{
			return (string)this._values[i];
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x00236CD8 File Offset: 0x002360D8
		public override decimal GetDecimal(int i)
		{
			return (decimal)this._values[i];
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00236CF8 File Offset: 0x002360F8
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this._values[i];
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x00236D18 File Offset: 0x00236118
		public override bool IsDBNull(int i)
		{
			object obj = this._values[i];
			return obj == null || Convert.IsDBNull(obj);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x00236D48 File Offset: 0x00236148
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x00236D68 File Offset: 0x00236168
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00236D78 File Offset: 0x00236178
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00236D88 File Offset: 0x00236188
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00236D98 File Offset: 0x00236198
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00236DA8 File Offset: 0x002361A8
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00236DB8 File Offset: 0x002361B8
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00236DC8 File Offset: 0x002361C8
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x00236DE8 File Offset: 0x002361E8
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00236E08 File Offset: 0x00236208
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00236E28 File Offset: 0x00236228
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			if (this._propertyDescriptors == null)
			{
				this._propertyDescriptors = new PropertyDescriptorCollection(null);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00236E58 File Offset: 0x00236258
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000B8D RID: 2957
		private SchemaInfo[] _schemaInfo;

		// Token: 0x04000B8E RID: 2958
		private object[] _values;

		// Token: 0x04000B8F RID: 2959
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x04000B90 RID: 2960
		private FieldNameLookup _fieldNameLookup;
	}
}
