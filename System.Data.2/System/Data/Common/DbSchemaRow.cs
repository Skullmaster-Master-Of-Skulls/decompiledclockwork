using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x020002FC RID: 764
	internal sealed class DbSchemaRow
	{
		// Token: 0x06003090 RID: 12432 RVA: 0x0012EF00 File Offset: 0x0012E300
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

		// Token: 0x06003091 RID: 12433 RVA: 0x0012EFB4 File Offset: 0x0012E3B4
		internal DbSchemaRow(DbSchemaTable schemaTable, DataRow dataRow)
		{
			this.schemaTable = schemaTable;
			this.dataRow = dataRow;
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x0012EFD8 File Offset: 0x0012E3D8
		internal DataRow DataRow
		{
			get
			{
				return this.dataRow;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x0012EFEC File Offset: 0x0012E3EC
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

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x0012F030 File Offset: 0x0012E430
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

		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x06003095 RID: 12437 RVA: 0x0012F070 File Offset: 0x0012E470
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

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x0012F0C0 File Offset: 0x0012E4C0
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

		// Token: 0x170007F2 RID: 2034
		// (get) Token: 0x06003097 RID: 12439 RVA: 0x0012F110 File Offset: 0x0012E510
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

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x0012F160 File Offset: 0x0012E560
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

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x0012F1B0 File Offset: 0x0012E5B0
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

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x0012F200 File Offset: 0x0012E600
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

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x0600309B RID: 12443 RVA: 0x0012F24C File Offset: 0x0012E64C
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

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x0012F298 File Offset: 0x0012E698
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

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x0012F2E4 File Offset: 0x0012E6E4
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

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x0012F330 File Offset: 0x0012E730
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

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x0012F37C File Offset: 0x0012E77C
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

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x0012F3C8 File Offset: 0x0012E7C8
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

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060030A1 RID: 12449 RVA: 0x0012F414 File Offset: 0x0012E814
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

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x0012F460 File Offset: 0x0012E860
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

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060030A3 RID: 12451 RVA: 0x0012F4A8 File Offset: 0x0012E8A8
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

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x0012F4F4 File Offset: 0x0012E8F4
		internal int UnsortedIndex
		{
			get
			{
				return (int)this.dataRow[this.schemaTable.UnsortedIndex, DataRowVersion.Default];
			}
		}

		// Token: 0x04001D50 RID: 7504
		internal const string SchemaMappingUnsortedIndex = "SchemaMapping Unsorted Index";

		// Token: 0x04001D51 RID: 7505
		private DbSchemaTable schemaTable;

		// Token: 0x04001D52 RID: 7506
		private DataRow dataRow;
	}
}
