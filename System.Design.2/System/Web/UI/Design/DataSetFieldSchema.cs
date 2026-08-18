using System;
using System.Data;

namespace System.Web.UI.Design
{
	// Token: 0x02000027 RID: 39
	public sealed class DataSetFieldSchema : IDataSourceFieldSchema
	{
		// Token: 0x0600013F RID: 319 RVA: 0x0000C155 File Offset: 0x0000A355
		public DataSetFieldSchema(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			this._column = column;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000C172 File Offset: 0x0000A372
		public Type DataType
		{
			get
			{
				return this._column.DataType;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000C17F File Offset: 0x0000A37F
		public bool Identity
		{
			get
			{
				return this._column.AutoIncrement;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000C18C File Offset: 0x0000A38C
		public bool IsReadOnly
		{
			get
			{
				return this._column.ReadOnly;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000C199 File Offset: 0x0000A399
		public bool IsUnique
		{
			get
			{
				return this._column.Unique;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000C1A6 File Offset: 0x0000A3A6
		public int Length
		{
			get
			{
				return this._column.MaxLength;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000C1B3 File Offset: 0x0000A3B3
		public string Name
		{
			get
			{
				return this._column.ColumnName;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public bool Nullable
		{
			get
			{
				return this._column.AllowDBNull;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public int Precision
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000C1D0 File Offset: 0x0000A3D0
		public bool PrimaryKey
		{
			get
			{
				if (this._column.Table == null || this._column.Table.PrimaryKey == null)
				{
					return false;
				}
				foreach (DataColumn dataColumn in this._column.Table.PrimaryKey)
				{
					if (dataColumn == this._column)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public int Scale
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x04000110 RID: 272
		private DataColumn _column;
	}
}
