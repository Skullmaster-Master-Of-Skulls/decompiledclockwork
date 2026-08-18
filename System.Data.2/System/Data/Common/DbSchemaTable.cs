using System;

namespace System.Data.Common
{
	// Token: 0x020002FD RID: 765
	internal sealed class DbSchemaTable
	{
		// Token: 0x060030A5 RID: 12453 RVA: 0x0012F524 File Offset: 0x0012E924
		internal DbSchemaTable(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			this.dataTable = dataTable;
			this.columns = dataTable.Columns;
			this._returnProviderSpecificTypes = returnProviderSpecificTypes;
		}

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x0012F564 File Offset: 0x0012E964
		internal DataColumn ColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnName);
			}
		}

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x0012F578 File Offset: 0x0012E978
		internal DataColumn Size
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnSize);
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x0012F58C File Offset: 0x0012E98C
		internal DataColumn BaseServerName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseServerName);
			}
		}

		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x0012F5A0 File Offset: 0x0012E9A0
		internal DataColumn BaseColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseColumnName);
			}
		}

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x0012F5B4 File Offset: 0x0012E9B4
		internal DataColumn BaseTableName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseTableName);
			}
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x0012F5C8 File Offset: 0x0012E9C8
		internal DataColumn BaseCatalogName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseCatalogName);
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x0012F5DC File Offset: 0x0012E9DC
		internal DataColumn BaseSchemaName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseSchemaName);
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x060030AD RID: 12461 RVA: 0x0012F5F0 File Offset: 0x0012E9F0
		internal DataColumn IsAutoIncrement
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsAutoIncrement);
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x0012F604 File Offset: 0x0012EA04
		internal DataColumn IsUnique
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsUnique);
			}
		}

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x060030AF RID: 12463 RVA: 0x0012F61C File Offset: 0x0012EA1C
		internal DataColumn IsKey
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsKey);
			}
		}

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x0012F634 File Offset: 0x0012EA34
		internal DataColumn IsRowVersion
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsRowVersion);
			}
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x060030B1 RID: 12465 RVA: 0x0012F64C File Offset: 0x0012EA4C
		internal DataColumn AllowDBNull
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.AllowDBNull);
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x0012F664 File Offset: 0x0012EA64
		internal DataColumn IsExpression
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsExpression);
			}
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x060030B3 RID: 12467 RVA: 0x0012F67C File Offset: 0x0012EA7C
		internal DataColumn IsHidden
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsHidden);
			}
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x0012F694 File Offset: 0x0012EA94
		internal DataColumn IsLong
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsLong);
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x0012F6AC File Offset: 0x0012EAAC
		internal DataColumn IsReadOnly
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsReadOnly);
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x0012F6C4 File Offset: 0x0012EAC4
		internal DataColumn UnsortedIndex
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.SchemaMappingUnsortedIndex);
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060030B7 RID: 12471 RVA: 0x0012F6DC File Offset: 0x0012EADC
		internal DataColumn DataType
		{
			get
			{
				if (this._returnProviderSpecificTypes)
				{
					return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ProviderSpecificDataType, DbSchemaTable.ColumnEnum.DataType);
				}
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.DataType);
			}
		}

		// Token: 0x060030B8 RID: 12472 RVA: 0x0012F704 File Offset: 0x0012EB04
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column)
		{
			return this.CachedDataColumn(column, column);
		}

		// Token: 0x060030B9 RID: 12473 RVA: 0x0012F71C File Offset: 0x0012EB1C
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column, DbSchemaTable.ColumnEnum column2)
		{
			DataColumn dataColumn = this.columnCache[(int)column];
			if (dataColumn == null)
			{
				int num = this.columns.IndexOf(DbSchemaTable.DBCOLUMN_NAME[(int)column]);
				if (-1 == num && column != column2)
				{
					num = this.columns.IndexOf(DbSchemaTable.DBCOLUMN_NAME[(int)column2]);
				}
				if (-1 != num)
				{
					dataColumn = this.columns[num];
					this.columnCache[(int)column] = dataColumn;
				}
			}
			return dataColumn;
		}

		// Token: 0x04001D53 RID: 7507
		private static readonly string[] DBCOLUMN_NAME = new string[]
		{
			SchemaTableColumn.ColumnName,
			SchemaTableColumn.ColumnOrdinal,
			SchemaTableColumn.ColumnSize,
			SchemaTableOptionalColumn.BaseServerName,
			SchemaTableOptionalColumn.BaseCatalogName,
			SchemaTableColumn.BaseColumnName,
			SchemaTableColumn.BaseSchemaName,
			SchemaTableColumn.BaseTableName,
			SchemaTableOptionalColumn.IsAutoIncrement,
			SchemaTableColumn.IsUnique,
			SchemaTableColumn.IsKey,
			SchemaTableOptionalColumn.IsRowVersion,
			SchemaTableColumn.DataType,
			SchemaTableOptionalColumn.ProviderSpecificDataType,
			SchemaTableColumn.AllowDBNull,
			SchemaTableColumn.ProviderType,
			SchemaTableColumn.IsExpression,
			SchemaTableOptionalColumn.IsHidden,
			SchemaTableColumn.IsLong,
			SchemaTableOptionalColumn.IsReadOnly,
			"SchemaMapping Unsorted Index"
		};

		// Token: 0x04001D54 RID: 7508
		internal DataTable dataTable;

		// Token: 0x04001D55 RID: 7509
		private DataColumnCollection columns;

		// Token: 0x04001D56 RID: 7510
		private DataColumn[] columnCache = new DataColumn[DbSchemaTable.DBCOLUMN_NAME.Length];

		// Token: 0x04001D57 RID: 7511
		private bool _returnProviderSpecificTypes;

		// Token: 0x0200043C RID: 1084
		private enum ColumnEnum
		{
			// Token: 0x04002357 RID: 9047
			ColumnName,
			// Token: 0x04002358 RID: 9048
			ColumnOrdinal,
			// Token: 0x04002359 RID: 9049
			ColumnSize,
			// Token: 0x0400235A RID: 9050
			BaseServerName,
			// Token: 0x0400235B RID: 9051
			BaseCatalogName,
			// Token: 0x0400235C RID: 9052
			BaseColumnName,
			// Token: 0x0400235D RID: 9053
			BaseSchemaName,
			// Token: 0x0400235E RID: 9054
			BaseTableName,
			// Token: 0x0400235F RID: 9055
			IsAutoIncrement,
			// Token: 0x04002360 RID: 9056
			IsUnique,
			// Token: 0x04002361 RID: 9057
			IsKey,
			// Token: 0x04002362 RID: 9058
			IsRowVersion,
			// Token: 0x04002363 RID: 9059
			DataType,
			// Token: 0x04002364 RID: 9060
			ProviderSpecificDataType,
			// Token: 0x04002365 RID: 9061
			AllowDBNull,
			// Token: 0x04002366 RID: 9062
			ProviderType,
			// Token: 0x04002367 RID: 9063
			IsExpression,
			// Token: 0x04002368 RID: 9064
			IsHidden,
			// Token: 0x04002369 RID: 9065
			IsLong,
			// Token: 0x0400236A RID: 9066
			IsReadOnly,
			// Token: 0x0400236B RID: 9067
			SchemaMappingUnsortedIndex
		}
	}
}
