using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000204 RID: 516
	internal sealed class MaterializedDataRecord : DbDataRecord, IExtendedDataRecord, IDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x06001290 RID: 4752 RVA: 0x0004E0C0 File Offset: 0x0004C2C0
		internal MaterializedDataRecord(MetadataWorkspace workspace, TypeUsage edmUsage, object[] values)
		{
			this._workspace = workspace;
			this._edmUsage = edmUsage;
			this._values = values;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06001291 RID: 4753 RVA: 0x0004E0E0 File Offset: 0x0004C2E0
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

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x0004E132 File Offset: 0x0004C332
		public override int FieldCount
		{
			get
			{
				return this._values.Length;
			}
		}

		// Token: 0x170001D9 RID: 473
		public override object this[int ordinal]
		{
			get
			{
				return this.GetValue(ordinal);
			}
		}

		// Token: 0x170001DA RID: 474
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		// Token: 0x06001295 RID: 4757 RVA: 0x0004E154 File Offset: 0x0004C354
		public override bool GetBoolean(int ordinal)
		{
			return (bool)this._values[ordinal];
		}

		// Token: 0x06001296 RID: 4758 RVA: 0x0004E163 File Offset: 0x0004C363
		public override byte GetByte(int ordinal)
		{
			return (byte)this._values[ordinal];
		}

		// Token: 0x06001297 RID: 4759 RVA: 0x0004E174 File Offset: 0x0004C374
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public override long GetBytes(int ordinal, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			int num = 0;
			byte[] array = (byte[])this._values[ordinal];
			num = array.Length;
			if (fieldOffset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(num.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
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
				if (e.IsCatchableExceptionType())
				{
					num = array.Length;
					if (length < 0)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
					}
					if (bufferOffset < 0 || bufferOffset >= buffer.Length)
					{
						throw new ArgumentOutOfRangeException("bufferOffset", Strings.ADP_InvalidDestinationBufferIndex(length.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (fieldOffset < 0L || fieldOffset >= (long)num)
					{
						throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(length.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (num + bufferOffset > buffer.Length)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidBufferSizeOrIndex(num.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x0004E2CC File Offset: 0x0004C4CC
		public override char GetChar(int ordinal)
		{
			return ((string)this.GetValue(ordinal))[0];
		}

		// Token: 0x06001299 RID: 4761 RVA: 0x0004E2E0 File Offset: 0x0004C4E0
		[SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
		public override long GetChars(int ordinal, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			int num = 0;
			string text = (string)this._values[ordinal];
			num = text.Length;
			if (fieldOffset > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(num.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
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
				if (e.IsCatchableExceptionType())
				{
					num = text.Length;
					if (length < 0)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidDataLength(((long)length).ToString(CultureInfo.InvariantCulture)));
					}
					if (bufferOffset < 0 || bufferOffset >= buffer.Length)
					{
						throw new ArgumentOutOfRangeException("bufferOffset", Strings.ADP_InvalidDestinationBufferIndex(buffer.Length.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (fieldOffset < 0L || fieldOffset >= (long)num)
					{
						throw new ArgumentOutOfRangeException("fieldOffset", Strings.ADP_InvalidSourceBufferIndex(num.ToString(CultureInfo.InvariantCulture), fieldOffset.ToString(CultureInfo.InvariantCulture)));
					}
					if (num + bufferOffset > buffer.Length)
					{
						throw new IndexOutOfRangeException(Strings.ADP_InvalidBufferSizeOrIndex(num.ToString(CultureInfo.InvariantCulture), bufferOffset.ToString(CultureInfo.InvariantCulture)));
					}
				}
				throw;
			}
			return (long)num;
		}

		// Token: 0x0600129A RID: 4762 RVA: 0x0004E444 File Offset: 0x0004C644
		public DbDataRecord GetDataRecord(int ordinal)
		{
			return (DbDataRecord)this._values[ordinal];
		}

		// Token: 0x0600129B RID: 4763 RVA: 0x0004E453 File Offset: 0x0004C653
		public DbDataReader GetDataReader(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x0004E45C File Offset: 0x0004C65C
		public override string GetDataTypeName(int ordinal)
		{
			return this.GetMember(ordinal).TypeUsage.EdmType.Name;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x0004E474 File Offset: 0x0004C674
		public override DateTime GetDateTime(int ordinal)
		{
			return (DateTime)this._values[ordinal];
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x0004E483 File Offset: 0x0004C683
		public override decimal GetDecimal(int ordinal)
		{
			return (decimal)this._values[ordinal];
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x0004E492 File Offset: 0x0004C692
		public override double GetDouble(int ordinal)
		{
			return (double)this._values[ordinal];
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x0004E4A4 File Offset: 0x0004C6A4
		public override Type GetFieldType(int ordinal)
		{
			EdmType edmType = this.GetMember(ordinal).TypeUsage.EdmType;
			return edmType.ClrType ?? typeof(object);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x0004E4D7 File Offset: 0x0004C6D7
		public override float GetFloat(int ordinal)
		{
			return (float)this._values[ordinal];
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x0004E4E6 File Offset: 0x0004C6E6
		public override Guid GetGuid(int ordinal)
		{
			return (Guid)this._values[ordinal];
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x0004E4F5 File Offset: 0x0004C6F5
		public override short GetInt16(int ordinal)
		{
			return (short)this._values[ordinal];
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x0004E504 File Offset: 0x0004C704
		public override int GetInt32(int ordinal)
		{
			return (int)this._values[ordinal];
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x0004E513 File Offset: 0x0004C713
		public override long GetInt64(int ordinal)
		{
			return (long)this._values[ordinal];
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x0004E522 File Offset: 0x0004C722
		public override string GetName(int ordinal)
		{
			return this.GetMember(ordinal).Name;
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x0004E530 File Offset: 0x0004C730
		public override int GetOrdinal(string name)
		{
			if (this._fieldNameLookup == null)
			{
				this._fieldNameLookup = new FieldNameLookup(this);
			}
			return this._fieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0004E552 File Offset: 0x0004C752
		public override string GetString(int ordinal)
		{
			return (string)this._values[ordinal];
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x0004E561 File Offset: 0x0004C761
		public override object GetValue(int ordinal)
		{
			return this._values[ordinal];
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x0004E56C File Offset: 0x0004C76C
		public override int GetValues(object[] values)
		{
			Check.NotNull<object[]>(values, "values");
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this._values[i];
			}
			return num;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x0004E5AC File Offset: 0x0004C7AC
		private EdmMember GetMember(int ordinal)
		{
			return this.DataRecordInfo.FieldMetadata[ordinal].FieldType;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x0004E5D2 File Offset: 0x0004C7D2
		public override bool IsDBNull(int ordinal)
		{
			return DBNull.Value == this._values[ordinal];
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x0004E5E3 File Offset: 0x0004C7E3
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0004E5EC File Offset: 0x0004C7EC
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x0004E5EF File Offset: 0x0004C7EF
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x0004E5F4 File Offset: 0x0004C7F4
		private PropertyDescriptorCollection InitializePropertyDescriptors()
		{
			if (this._values == null)
			{
				return null;
			}
			if (this._propertyDescriptors == null && 0 < this._values.Length)
			{
				this._propertyDescriptors = MaterializedDataRecord.CreatePropertyDescriptorCollection(this.DataRecordInfo.RecordType.EdmType as StructuralType, typeof(MaterializedDataRecord), true);
			}
			return this._propertyDescriptors;
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x0004E650 File Offset: 0x0004C850
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

		// Token: 0x060012B2 RID: 4786 RVA: 0x0004E6D4 File Offset: 0x0004C8D4
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0004E6E0 File Offset: 0x0004C8E0
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			bool flag = attributes != null && 0 < attributes.Length;
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
			if (this._attrCache == null && attributes != null && 0 < attributes.Length)
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

		// Token: 0x060012B4 RID: 4788 RVA: 0x0004E86C File Offset: 0x0004CA6C
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04000569 RID: 1385
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x0400056A RID: 1386
		private DataRecordInfo _recordInfo;

		// Token: 0x0400056B RID: 1387
		private readonly MetadataWorkspace _workspace;

		// Token: 0x0400056C RID: 1388
		private readonly TypeUsage _edmUsage;

		// Token: 0x0400056D RID: 1389
		private readonly object[] _values;

		// Token: 0x0400056E RID: 1390
		private PropertyDescriptorCollection _propertyDescriptors;

		// Token: 0x0400056F RID: 1391
		private MaterializedDataRecord.FilterCache _filterCache;

		// Token: 0x04000570 RID: 1392
		private Dictionary<object, AttributeCollection> _attrCache;

		// Token: 0x02000205 RID: 517
		private class FilterCache
		{
			// Token: 0x060012B5 RID: 4789 RVA: 0x0004E870 File Offset: 0x0004CA70
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

			// Token: 0x04000571 RID: 1393
			public Attribute[] Attributes;

			// Token: 0x04000572 RID: 1394
			public PropertyDescriptorCollection FilteredProperties;
		}
	}
}
