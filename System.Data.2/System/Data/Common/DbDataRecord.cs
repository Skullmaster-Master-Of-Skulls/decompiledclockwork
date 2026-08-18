using System;
using System.ComponentModel;

namespace System.Data.Common
{
	// Token: 0x020002F0 RID: 752
	public abstract class DbDataRecord : ICustomTypeDescriptor, IDataRecord
	{
		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06002FFB RID: 12283
		public abstract int FieldCount { get; }

		// Token: 0x170007CE RID: 1998
		public abstract object this[int i]
		{
			get;
		}

		// Token: 0x170007CF RID: 1999
		public abstract object this[string name]
		{
			get;
		}

		// Token: 0x06002FFE RID: 12286
		public abstract bool GetBoolean(int i);

		// Token: 0x06002FFF RID: 12287
		public abstract byte GetByte(int i);

		// Token: 0x06003000 RID: 12288
		public abstract long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length);

		// Token: 0x06003001 RID: 12289
		public abstract char GetChar(int i);

		// Token: 0x06003002 RID: 12290
		public abstract long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length);

		// Token: 0x06003003 RID: 12291 RVA: 0x0012DF4C File Offset: 0x0012D34C
		public IDataReader GetData(int i)
		{
			return this.GetDbDataReader(i);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x0012DF60 File Offset: 0x0012D360
		protected virtual DbDataReader GetDbDataReader(int i)
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06003005 RID: 12293
		public abstract string GetDataTypeName(int i);

		// Token: 0x06003006 RID: 12294
		public abstract DateTime GetDateTime(int i);

		// Token: 0x06003007 RID: 12295
		public abstract decimal GetDecimal(int i);

		// Token: 0x06003008 RID: 12296
		public abstract double GetDouble(int i);

		// Token: 0x06003009 RID: 12297
		public abstract Type GetFieldType(int i);

		// Token: 0x0600300A RID: 12298
		public abstract float GetFloat(int i);

		// Token: 0x0600300B RID: 12299
		public abstract Guid GetGuid(int i);

		// Token: 0x0600300C RID: 12300
		public abstract short GetInt16(int i);

		// Token: 0x0600300D RID: 12301
		public abstract int GetInt32(int i);

		// Token: 0x0600300E RID: 12302
		public abstract long GetInt64(int i);

		// Token: 0x0600300F RID: 12303
		public abstract string GetName(int i);

		// Token: 0x06003010 RID: 12304
		public abstract int GetOrdinal(string name);

		// Token: 0x06003011 RID: 12305
		public abstract string GetString(int i);

		// Token: 0x06003012 RID: 12306
		public abstract object GetValue(int i);

		// Token: 0x06003013 RID: 12307
		public abstract int GetValues(object[] values);

		// Token: 0x06003014 RID: 12308
		public abstract bool IsDBNull(int i);

		// Token: 0x06003015 RID: 12309 RVA: 0x0012DF74 File Offset: 0x0012D374
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x0012DF88 File Offset: 0x0012D388
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x0012DF98 File Offset: 0x0012D398
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x0012DFA8 File Offset: 0x0012D3A8
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x0012DFB8 File Offset: 0x0012D3B8
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x0012DFC8 File Offset: 0x0012D3C8
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x0012DFD8 File Offset: 0x0012D3D8
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x0012DFE8 File Offset: 0x0012D3E8
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x0012DFFC File Offset: 0x0012D3FC
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x0012E010 File Offset: 0x0012D410
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x0012E024 File Offset: 0x0012D424
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return new PropertyDescriptorCollection(null);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x0012E038 File Offset: 0x0012D438
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}
