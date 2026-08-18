using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Metadata.Edm;

namespace System.Data.Objects
{
	// Token: 0x02000134 RID: 308
	internal sealed class MaterializedDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x06001659 RID: 5721 RVA: 0x0004AF4E File Offset: 0x0004914E
		internal MaterializedDataRecord(MetadataWorkspace workspace, TypeUsage edmUsage, object[] values)
		{
			this._workspace = workspace;
			this._edmUsage = edmUsage;
			this._values = values;
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x0004AF6C File Offset: 0x0004916C
		public DataRecordInfo DataRecordInfo
		{
			get
			{
				if (this._recordInfo == null)
				{
					if (this._workspace == null)
					{
						this._recordInfo = new DataRecordInfo(this._edmUsage);
					}
					else
					{
						this._recordInfo = new DataRecordInfo(this._workspace.GetOSpaceTypeUsage(this._edmUsage));
					}
				}
				return this._recordInfo;
			}
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600165B RID: 5723 RVA: 0x0004AFBE File Offset: 0x000491BE
		public override int FieldCount
		{
			get
			{
				return this._values.Length;
			}
		}

		// Token: 0x17000491 RID: 1169
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x17000492 RID: 1170
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x0600165E RID: 5726 RVA: 0x0004AFC8 File Offset: 0x000491C8
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this._values[ordinal];
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x0004AFD7 File Offset: 0x000491D7
		public override byte GetByte(int ordinal)
		{
			return (byte)this._values[ordinal];
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x0004AFE8 File Offset: 0x000491E8
		public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = 0;
			byte[] array = (byte[])this._values[ordinal];
			num = array.Length;
			if (fieldOffset > 2147483647L)
			{
				throw EntityUtil.InvalidSourceBufferIndex(num, fieldOffset, "fieldOffset");
			}
			int num2 = (int)fieldOffset;
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
				Array.Copy(array, num2, buffer, bufferOffset, num);
			}
			catch (Exception e)
			{
				if (EntityUtil.IsCatchableExceptionType(e))
				{
					num = array.Length;
					if (length < 0)
					{
						throw EntityUtil.InvalidDataLength((long)length);
					}
					if (bufferOffset < 0 || bufferOffset >= buffer.Length)
					{
						throw EntityUtil.InvalidDestinationBufferIndex(length, bufferOffset, "bufferOffset");
					}
					if (fieldOffset < 0L || fieldOffset >= (long)num)
					{
						throw EntityUtil.InvalidSourceBufferIndex(length, fieldOffset, "fieldOffset");
					}
					if (num + bufferOffset > buffer.Length)
					{
						throw EntityUtil.InvalidBufferSizeOrIndex(num, bufferOffset);
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x0004B0C0 File Offset: 0x000492C0
		public override char GetChar(int ordinal)
		{
			return ((string)this.GetValue(ordinal))[0];
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x0004B0D4 File Offset: 0x000492D4
		public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = 0;
			string text = (string)this._values[ordinal];
			num = text.Length;
			if (fieldOffset > 2147483647L)
			{
				throw EntityUtil.InvalidSourceBufferIndex(num, fieldOffset, "fieldOffset");
			}
			int num2 = (int)fieldOffset;
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
				text.CopyTo(num2, buffer, bufferOffset, num);
			}
			catch (Exception e)
			{
				if (EntityUtil.IsCatchableExceptionType(e))
				{
					num = text.Length;
					if (length < 0)
					{
						throw EntityUtil.InvalidDataLength((long)length);
					}
					if (bufferOffset < 0 || bufferOffset >= buffer.Length)
					{
						throw EntityUtil.InvalidDestinationBufferIndex(buffer.Length, bufferOffset, "bufferOffset");
					}
					if (fieldOffset < 0L || fieldOffset >= (long)num)
					{
						throw EntityUtil.InvalidSourceBufferIndex(num, fieldOffset, "fieldOffset");
					}
					if (num + bufferOffset > buffer.Length)
					{
						throw EntityUtil.InvalidBufferSizeOrIndex(num, bufferOffset);
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x0004B1B0 File Offset: 0x000493B0
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this._values[ordinal];
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x0001898B File Offset: 0x00016B8B
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0004B1BF File Offset: 0x000493BF
		public override string GetDataTypeName(int ordinal)
		{
			return this.GetMember(ordinal).TypeUsage.EdmType.Name;
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0004B1D7 File Offset: 0x000493D7
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this._values[ordinal];
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004B1E6 File Offset: 0x000493E6
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this._values[ordinal];
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0004B1F5 File Offset: 0x000493F5
		public override double GetDouble(int ordinal)
		{
			return (double)this._values[ordinal];
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x0004B204 File Offset: 0x00049404
		public override Type GetFieldType(int ordinal)
		{
			EdmType edmType = this.GetMember(ordinal).TypeUsage.EdmType;
			return edmType.ClrType ?? typeof(object);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0004B237 File Offset: 0x00049437
		public override float GetFloat(int ordinal)
		{
			return (float)this._values[ordinal];
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x0004B246 File Offset: 0x00049446
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this._values[ordinal];
		}

		// Token: 0x0600166C RID: 5740 RVA: 0x0004B255 File Offset: 0x00049455
		public override short GetInt16(int ordinal)
		{
			return (short)this._values[ordinal];
		}

		// Token: 0x0600166D RID: 5741 RVA: 0x0004B264 File Offset: 0x00049464
		public override int GetInt32(int ordinal)
		{
			return (int)this._values[ordinal];
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x0004B273 File Offset: 0x00049473
		public override long GetInt64(int ordinal)
		{
			return (long)this._values[ordinal];
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x0004B282 File Offset: 0x00049482
		public override string GetName(int ordinal)
		{
			return this.GetMember(ordinal).Name;
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x0004B290 File Offset: 0x00049490
		public override int GetOrdinal(string name)
		{
			if (this._fieldNameLookup == null)
			{
				this._fieldNameLookup = new FieldNameLookup(this, -1);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x0004B2B3 File Offset: 0x000494B3
		public override string GetString(int ordinal)
		{
			return (string)this._values[ordinal];
		}

		// Token: 0x06001672 RID: 5746 RVA: 0x0004B2C2 File Offset: 0x000494C2
		public override object GetValue(int ordinal)
		{
			return this._values[ordinal];
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0004B2CC File Offset: 0x000494CC
		public override int GetValues(object[] values)
		{
			if (values == null)
			{
				throw EntityUtil.ArgumentNull("values");
			}
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0004B310 File Offset: 0x00049510
		private EdmMember GetMember(int ordinal)
		{
			return this.DataRecordInfo.FieldMetadata[ordinal].FieldType;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x0004B336 File Offset: 0x00049536
		public override bool IsDBNull(int ordinal)
		{
			return DBNull.Value == this._values[ordinal];
		}

		// Token: 0x06001676 RID: 5750 RVA: 0x0004B347 File Offset: 0x00049547
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06001677 RID: 5751 RVA: 0x00006174 File Offset: 0x00004374
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06001678 RID: 5752 RVA: 0x00006174 File Offset: 0x00004374
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x0004B350 File Offset: 0x00049550
		private PropertyDescriptorCollection InitializePropertyDescriptors()
		{
			if (this._values == null)
			{
				return null;
			}
			if (this._propertyDescriptors == null && this._values.Length != 0)
			{
				this._propertyDescriptors = MaterializedDataRecord.CreatePropertyDescriptorCollection(this.DataRecordInfo.RecordType.EdmType as StructuralType, typeof(MaterializedDataRecord), true);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0004B3AC File Offset: 0x000495AC
		internal static PropertyDescriptorCollection CreatePropertyDescriptorCollection(StructuralType structuralType, Type componentType, bool isReadOnly)
		{
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			if (structuralType != null)
			{
				foreach (EdmMember edmMember in structuralType.Members)
				{
					if (edmMember.BuiltInTypeKind == BuiltInTypeKind.EdmProperty)
					{
						EdmProperty property = (EdmProperty)edmMember;
						FieldDescriptor item = new FieldDescriptor(componentType, isReadOnly, property);
						list.Add(item);
					}
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0004B430 File Offset: 0x00049630
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0004B43C File Offset: 0x0004963C
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			bool flag = attributes != null && attributes.Length != 0;
			PropertyDescriptorCollection propertyDescriptorCollection = this.InitializePropertyDescriptors();
			if (propertyDescriptorCollection == null)
			{
				return propertyDescriptorCollection;
			}
			MaterializedDataRecord.FilterCache filterCache = this._filterCache;
			if (flag && filterCache != null && filterCache.IsValid(attributes))
			{
				return filterCache.FilteredProperties;
			}
			if (!flag && propertyDescriptorCollection != null)
			{
				return propertyDescriptorCollection;
			}
			if (this._attrCache == null && attributes != null && attributes.Length != 0)
			{
				this._attrCache = new Dictionary<object, AttributeCollection>();
				foreach (object obj in this._propertyDescriptors)
				{
					FieldDescriptor fieldDescriptor = (FieldDescriptor)obj;
					object value = fieldDescriptor.GetValue(this);
					object[] customAttributes = value.GetType().GetCustomAttributes(false);
					Attribute[] array = new Attribute[customAttributes.Length];
					customAttributes.CopyTo(array, 0);
					this._attrCache.Add(fieldDescriptor, new AttributeCollection(array));
				}
			}
			propertyDescriptorCollection = new PropertyDescriptorCollection(null);
			foreach (object obj2 in this._propertyDescriptors)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj2;
				if (this._attrCache[propertyDescriptor].Matches(attributes))
				{
					propertyDescriptorCollection.Add(propertyDescriptor);
				}
			}
			if (flag)
			{
				this._filterCache = new MaterializedDataRecord.FilterCache
				{
					Attributes = attributes,
					FilteredProperties = propertyDescriptorCollection
				};
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x00048AC0 File Offset: 0x00046CC0
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000A53 RID: 2643
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000A54 RID: 2644
		private DataRecordInfo _recordInfo;

		// Token: 0x04000A55 RID: 2645
		private readonly MetadataWorkspace _workspace;

		// Token: 0x04000A56 RID: 2646
		private readonly TypeUsage _edmUsage;

		// Token: 0x04000A57 RID: 2647
		private readonly object[] _values;

		// Token: 0x04000A58 RID: 2648
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x04000A59 RID: 2649
		private MaterializedDataRecord.FilterCache _filterCache;

		// Token: 0x04000A5A RID: 2650
		private Dictionary<object, AttributeCollection> _attrCache;

		// Token: 0x0200049F RID: 1183
		private class FilterCache
		{
			// Token: 0x06003C24 RID: 15396 RVA: 0x000E27C4 File Offset: 0x000E09C4
			public bool IsValid(Attribute[] other)
			{
				if (other == null || this.Attributes == null)
				{
					return false;
				}
				if (this.Attributes.Length != other.Length)
				{
					return false;
				}
				for (int i = 0; i < other.Length; i++)
				{
					if (!this.Attributes[i].Match(other[i]))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04001A27 RID: 6695
			public Attribute[] Attributes;

			// Token: 0x04001A28 RID: 6696
			public PropertyDescriptorCollection FilteredProperties;
		}
	}
}
