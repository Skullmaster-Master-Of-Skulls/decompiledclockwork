using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x0200011C RID: 284
	public abstract class DbDataRecord : ICustomTypeDescriptor, IDataRecord
	{
		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06001220 RID: 4640
		public abstract int FieldCount { get; }

		// Token: 0x1700025E RID: 606
		public abstract object this[int i]
		{
			get;
		}

		// Token: 0x1700025F RID: 607
		public abstract object this[string name]
		{
			get;
		}

		// Token: 0x06001223 RID: 4643
		public abstract bool GetBoolean(int i);

		// Token: 0x06001224 RID: 4644
		public abstract byte GetByte(int i);

		// Token: 0x06001225 RID: 4645
		public abstract long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length);

		// Token: 0x06001226 RID: 4646
		public abstract char GetChar(int i);

		// Token: 0x06001227 RID: 4647
		public abstract long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length);

		// Token: 0x06001228 RID: 4648 RVA: 0x00236688 File Offset: 0x00235A88
		public IDataReader GetData(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x002366A8 File Offset: 0x00235AA8
		protected virtual DbDataReader GetDbDataReader(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600122A RID: 4650
		public abstract string GetDataTypeName(int i);

		// Token: 0x0600122B RID: 4651
		public abstract DateTime GetDateTime(int i);

		// Token: 0x0600122C RID: 4652
		public abstract decimal GetDecimal(int i);

		// Token: 0x0600122D RID: 4653
		public abstract double GetDouble(int i);

		// Token: 0x0600122E RID: 4654
		public abstract Type GetFieldType(int i);

		// Token: 0x0600122F RID: 4655
		public abstract float GetFloat(int i);

		// Token: 0x06001230 RID: 4656
		public abstract Guid GetGuid(int i);

		// Token: 0x06001231 RID: 4657
		public abstract short GetInt16(int i);

		// Token: 0x06001232 RID: 4658
		public abstract int GetInt32(int i);

		// Token: 0x06001233 RID: 4659
		public abstract long GetInt64(int i);

		// Token: 0x06001234 RID: 4660
		public abstract string GetName(int i);

		// Token: 0x06001235 RID: 4661
		public abstract int GetOrdinal(string name);

		// Token: 0x06001236 RID: 4662
		public abstract string GetString(int i);

		// Token: 0x06001237 RID: 4663
		public abstract object GetValue(int i);

		// Token: 0x06001238 RID: 4664
		public abstract int GetValues(object[] values);

		// Token: 0x06001239 RID: 4665
		public abstract bool IsDBNull(int i);

		// Token: 0x0600123A RID: 4666 RVA: 0x002366C8 File Offset: 0x00235AC8
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x002366E8 File Offset: 0x00235AE8
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x002366F8 File Offset: 0x00235AF8
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00236708 File Offset: 0x00235B08
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00236718 File Offset: 0x00235B18
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x00236728 File Offset: 0x00235B28
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00236738 File Offset: 0x00235B38
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00236748 File Offset: 0x00235B48
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00236768 File Offset: 0x00235B68
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00236788 File Offset: 0x00235B88
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x002367A8 File Offset: 0x00235BA8
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x002367C8 File Offset: 0x00235BC8
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
