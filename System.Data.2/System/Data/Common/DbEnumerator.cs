using System;
using System.Collections;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x020002F2 RID: 754
	public class DbEnumerator : IEnumerator
	{
		// Token: 0x06003023 RID: 12323 RVA: 0x0012E05C File Offset: 0x0012D45C
		public DbEnumerator(IDataReader reader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x0012E084 File Offset: 0x0012D484
		public DbEnumerator(IDataReader reader, bool closeReader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
			this.closeReader = closeReader;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x0012E0B4 File Offset: 0x0012D4B4
		public DbEnumerator(DbDataReader reader) : this(reader)
		{
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x0012E0C8 File Offset: 0x0012D4C8
		public DbEnumerator(DbDataReader reader, bool closeReader) : this(reader, closeReader)
		{
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06003027 RID: 12327 RVA: 0x0012E0E0 File Offset: 0x0012D4E0
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x0012E0F4 File Offset: 0x0012D4F4
		public bool MoveNext()
		{
			if (this._schemaInfo == null)
			{
				this.BuildSchemaInfo();
			}
			this._current = null;
			if (this._reader.Read())
			{
				object[] values = new object[this._schemaInfo.Length];
				this._reader.GetValues(values);
				this._current = new DataRecordInternal(this._schemaInfo, values, this._descriptors, this._fieldNameLookup);
				return true;
			}
			if (this.closeReader)
			{
				this._reader.Close();
			}
			return false;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x0012E174 File Offset: 0x0012D574
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Reset()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x0012E188 File Offset: 0x0012D588
		private void BuildSchemaInfo()
		{
			int fieldCount = this._reader.FieldCount;
			string[] array = new string[fieldCount];
			for (int i = 0; i < fieldCount; i++)
			{
				array[i] = this._reader.GetName(i);
			}
			ADP.BuildSchemaTableInfoTableNames(array);
			SchemaInfo[] array2 = new SchemaInfo[fieldCount];
			PropertyDescriptor[] array3 = new PropertyDescriptor[this._reader.FieldCount];
			for (int j = 0; j < array2.Length; j++)
			{
				SchemaInfo schemaInfo = default(SchemaInfo);
				schemaInfo.name = this._reader.GetName(j);
				schemaInfo.type = this._reader.GetFieldType(j);
				schemaInfo.typeName = this._reader.GetDataTypeName(j);
				array3[j] = new DbEnumerator.DbColumnDescriptor(j, array[j], schemaInfo.type);
				array2[j] = schemaInfo;
			}
			this._schemaInfo = array2;
			this._fieldNameLookup = new FieldNameLookup(this._reader, -1);
			this._descriptors = new PropertyDescriptorCollection(array3);
		}

		// Token: 0x04001D2E RID: 7470
		internal IDataReader _reader;

		// Token: 0x04001D2F RID: 7471
		internal DbDataRecord _current;

		// Token: 0x04001D30 RID: 7472
		internal SchemaInfo[] _schemaInfo;

		// Token: 0x04001D31 RID: 7473
		internal PropertyDescriptorCollection _descriptors;

		// Token: 0x04001D32 RID: 7474
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04001D33 RID: 7475
		private bool closeReader;

		// Token: 0x02000439 RID: 1081
		private sealed class DbColumnDescriptor : PropertyDescriptor
		{
			// Token: 0x0600363F RID: 13887 RVA: 0x001496DC File Offset: 0x00148ADC
			internal DbColumnDescriptor(int ordinal, string name, Type type) : base(name, null)
			{
				this._ordinal = ordinal;
				this._type = type;
			}

			// Token: 0x1700087D RID: 2173
			// (get) Token: 0x06003640 RID: 13888 RVA: 0x00149700 File Offset: 0x00148B00
			public override Type ComponentType
			{
				get
				{
					return typeof(IDataRecord);
				}
			}

			// Token: 0x1700087E RID: 2174
			// (get) Token: 0x06003641 RID: 13889 RVA: 0x00149718 File Offset: 0x00148B18
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700087F RID: 2175
			// (get) Token: 0x06003642 RID: 13890 RVA: 0x00149728 File Offset: 0x00148B28
			public override Type PropertyType
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06003643 RID: 13891 RVA: 0x0014973C File Offset: 0x00148B3C
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x06003644 RID: 13892 RVA: 0x0014974C File Offset: 0x00148B4C
			public override object GetValue(object component)
			{
				return ((IDataRecord)component)[this._ordinal];
			}

			// Token: 0x06003645 RID: 13893 RVA: 0x0014976C File Offset: 0x00148B6C
			public override void ResetValue(object component)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06003646 RID: 13894 RVA: 0x00149780 File Offset: 0x00148B80
			public override void SetValue(object component, object value)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x06003647 RID: 13895 RVA: 0x00149794 File Offset: 0x00148B94
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04002354 RID: 9044
			private int _ordinal;

			// Token: 0x04002355 RID: 9045
			private Type _type;
		}
	}
}
