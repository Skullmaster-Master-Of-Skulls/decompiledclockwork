using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x02000144 RID: 324
	internal sealed class DbSchemaRow
	{
		// Token: 0x060014EE RID: 5358 RVA: 0x00241CA8 File Offset: 0x002410A8
		internal static DbSchemaRow[] GetSortedSchemaRows(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			DataColumn dataColumn = dataTable.Columns["SchemaMapping Unsorted Index"];
			if (dataColumn == null)
			{
				dataColumn = new DataColumn("SchemaMapping Unsorted Index", typeof(int));
				dataTable.Columns.Add(dataColumn);
			}
			int count = dataTable.Rows.Count;
			for (int i = 0; i < count; i++)
			{
				dataTable.Rows[i][dataColumn] = i;
			}
			DbSchemaTable dbSchemaTable = new DbSchemaTable(dataTable, returnProviderSpecificTypes);
			DataRow[] array = dataTable.Select(null, "ColumnOrdinal ASC", DataViewRowState.CurrentRows);
			DbSchemaRow[] array2 = new DbSchemaRow[array.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = new DbSchemaRow(dbSchemaTable, array[j]);
			}
			return array2;
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00241D68 File Offset: 0x00241168
		internal DbSchemaRow(DbSchemaTable schemaTable, DataRow dataRow)
		{
			this.schemaTable = schemaTable;
			this.dataRow = dataRow;
		}

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x00241D98 File Offset: 0x00241198
		internal DataRow DataRow
		{
			get
			{
				return this.dataRow;
			}
		}

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x00241DB8 File Offset: 0x002411B8
		internal string ColumnName
		{
			get
			{
				object value = this.dataRow[this.schemaTable.ColumnName, DataRowVersion.Default];
				if (!Convert.IsDBNull(value))
				{
					return Convert.ToString(value, CultureInfo.InvariantCulture);
				}
				return "";
			}
		}

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x00241E08 File Offset: 0x00241208
		internal int Size
		{
			get
			{
				object value = this.dataRow[this.schemaTable.Size, DataRowVersion.Default];
				if (!Convert.IsDBNull(value))
				{
					return Convert.ToInt32(value, CultureInfo.InvariantCulture);
				}
				return 0;
			}
		}

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00241E48 File Offset: 0x00241248
		internal string BaseColumnName
		{
			get
			{
				if (this.schemaTable.BaseColumnName != null)
				{
					object value = this.dataRow[this.schemaTable.BaseColumnName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return "";
			}
		}

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060014F4 RID: 5364 RVA: 0x00241E98 File Offset: 0x00241298
		internal string BaseServerName
		{
			get
			{
				if (this.schemaTable.BaseServerName != null)
				{
					object value = this.dataRow[this.schemaTable.BaseServerName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return "";
			}
		}

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00241EE8 File Offset: 0x002412E8
		internal string BaseCatalogName
		{
			get
			{
				if (this.schemaTable.BaseCatalogName != null)
				{
					object value = this.dataRow[this.schemaTable.BaseCatalogName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return "";
			}
		}

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060014F6 RID: 5366 RVA: 0x00241F38 File Offset: 0x00241338
		internal string BaseSchemaName
		{
			get
			{
				if (this.schemaTable.BaseSchemaName != null)
				{
					object value = this.dataRow[this.schemaTable.BaseSchemaName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return "";
			}
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00241F88 File Offset: 0x00241388
		internal string BaseTableName
		{
			get
			{
				if (this.schemaTable.BaseTableName != null)
				{
					object value = this.dataRow[this.schemaTable.BaseTableName, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToString(value, CultureInfo.InvariantCulture);
					}
				}
				return "";
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060014F8 RID: 5368 RVA: 0x00241FD8 File Offset: 0x002413D8
		internal bool IsAutoIncrement
		{
			get
			{
				if (this.schemaTable.IsAutoIncrement != null)
				{
					object value = this.dataRow[this.schemaTable.IsAutoIncrement, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00242028 File Offset: 0x00241428
		internal bool IsUnique
		{
			get
			{
				if (this.schemaTable.IsUnique != null)
				{
					object value = this.dataRow[this.schemaTable.IsUnique, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00242078 File Offset: 0x00241478
		internal bool IsRowVersion
		{
			get
			{
				if (this.schemaTable.IsRowVersion != null)
				{
					object value = this.dataRow[this.schemaTable.IsRowVersion, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x002420C8 File Offset: 0x002414C8
		internal bool IsKey
		{
			get
			{
				if (this.schemaTable.IsKey != null)
				{
					object value = this.dataRow[this.schemaTable.IsKey, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00242118 File Offset: 0x00241518
		internal bool IsExpression
		{
			get
			{
				if (this.schemaTable.IsExpression != null)
				{
					object value = this.dataRow[this.schemaTable.IsExpression, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00242168 File Offset: 0x00241568
		internal bool IsHidden
		{
			get
			{
				if (this.schemaTable.IsHidden != null)
				{
					object value = this.dataRow[this.schemaTable.IsHidden, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x002421B8 File Offset: 0x002415B8
		internal bool IsLong
		{
			get
			{
				if (this.schemaTable.IsLong != null)
				{
					object value = this.dataRow[this.schemaTable.IsLong, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00242208 File Offset: 0x00241608
		internal bool IsReadOnly
		{
			get
			{
				if (this.schemaTable.IsReadOnly != null)
				{
					object value = this.dataRow[this.schemaTable.IsReadOnly, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return false;
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00242258 File Offset: 0x00241658
		internal Type DataType
		{
			get
			{
				if (this.schemaTable.DataType != null)
				{
					object obj = this.dataRow[this.schemaTable.DataType, DataRowVersion.Default];
					if (!Convert.IsDBNull(obj))
					{
						return (Type)obj;
					}
				}
				return null;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x002422A8 File Offset: 0x002416A8
		internal bool AllowDBNull
		{
			get
			{
				if (this.schemaTable.AllowDBNull != null)
				{
					object value = this.dataRow[this.schemaTable.AllowDBNull, DataRowVersion.Default];
					if (!Convert.IsDBNull(value))
					{
						return Convert.ToBoolean(value, CultureInfo.InvariantCulture);
					}
				}
				return true;
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x002422F8 File Offset: 0x002416F8
		internal int UnsortedIndex
		{
			get
			{
				return (int)this.dataRow[this.schemaTable.UnsortedIndex, DataRowVersion.Default];
			}
		}

		// Token: 0x04000C69 RID: 3177
		internal const string SchemaMappingUnsortedIndex = "SchemaMapping Unsorted Index";

		// Token: 0x04000C6A RID: 3178
		private DbSchemaTable schemaTable;

		// Token: 0x04000C6B RID: 3179
		private DataRow dataRow;
	}
}
