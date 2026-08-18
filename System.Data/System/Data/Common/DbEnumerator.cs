using System;
using System.Collections;
using System.ComponentModel;
using System.Data.ProviderBase;

namespace System.Data.Common
{
	// Token: 0x02000138 RID: 312
	public class DbEnumerator : IEnumerator
	{
		// Token: 0x0600147E RID: 5246 RVA: 0x00240D58 File Offset: 0x00240158
		public DbEnumerator(IDataReader reader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00240D88 File Offset: 0x00240188
		public DbEnumerator(IDataReader reader, bool closeReader)
		{
			if (reader == null)
			{
				throw ADP.ArgumentNull("reader");
			}
			this._reader = reader;
			this.closeReader = closeReader;
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x00240DB8 File Offset: 0x002401B8
		public object Current
		{
			get
			{
				return this._current;
			}
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00240DD8 File Offset: 0x002401D8
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

		// Token: 0x06001482 RID: 5250 RVA: 0x00240E58 File Offset: 0x00240258
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Reset()
		{
			throw ADP.NotSupported();
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00240E78 File Offset: 0x00240278
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

		// Token: 0x04000C57 RID: 3159
		internal IDataReader _reader;

		// Token: 0x04000C58 RID: 3160
		internal IDataRecord _current;

		// Token: 0x04000C59 RID: 3161
		internal SchemaInfo[] _schemaInfo;

		// Token: 0x04000C5A RID: 3162
		internal PropertyDescriptorCollection _descriptors;

		// Token: 0x04000C5B RID: 3163
		private FieldNameLookup _fieldNameLookup;

		// Token: 0x04000C5C RID: 3164
		private bool closeReader;

		// Token: 0x02000139 RID: 313
		private sealed class DbColumnDescriptor : PropertyDescriptor
		{
			// Token: 0x06001484 RID: 5252 RVA: 0x00240F78 File Offset: 0x00240378
			internal DbColumnDescriptor(int ordinal, string name, Type type) : base(name, null)
			{
				this._ordinal = ordinal;
				this._type = type;
			}

			// Token: 0x170002D4 RID: 724
			// (get) Token: 0x06001485 RID: 5253 RVA: 0x00240FA8 File Offset: 0x002403A8
			public override Type ComponentType
			{
				get
				{
					return typeof(IDataRecord);
				}
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x06001486 RID: 5254 RVA: 0x00240FC8 File Offset: 0x002403C8
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x06001487 RID: 5255 RVA: 0x00240FD8 File Offset: 0x002403D8
			public override Type PropertyType
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x06001488 RID: 5256 RVA: 0x00240FF8 File Offset: 0x002403F8
			public override bool CanResetValue(object component)
			{
				return false;
			}

			// Token: 0x06001489 RID: 5257 RVA: 0x00241008 File Offset: 0x00240408
			public override object GetValue(object component)
			{
				return ((IDataRecord)component)[this._ordinal];
			}

			// Token: 0x0600148A RID: 5258 RVA: 0x00241028 File Offset: 0x00240428
			public override void ResetValue(object component)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x0600148B RID: 5259 RVA: 0x00241048 File Offset: 0x00240448
			public override void SetValue(object component, object value)
			{
				throw ADP.NotSupported();
			}

			// Token: 0x0600148C RID: 5260 RVA: 0x00241068 File Offset: 0x00240468
			public override bool ShouldSerializeValue(object component)
			{
				return false;
			}

			// Token: 0x04000C5D RID: 3165
			private int _ordinal;

			// Token: 0x04000C5E RID: 3166
			private Type _type;
		}
	}
}
