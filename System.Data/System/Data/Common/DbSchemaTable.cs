using System;

namespace System.Data.Common
{
	// Token: 0x02000145 RID: 325
	internal sealed class DbSchemaTable
	{
		// Token: 0x06001503 RID: 5379 RVA: 0x00242328 File Offset: 0x00241728
		internal DbSchemaTable(DataTable dataTable, bool returnProviderSpecificTypes)
		{
			this.dataTable = dataTable;
			this.columns = dataTable.Columns;
			this._returnProviderSpecificTypes = returnProviderSpecificTypes;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001504 RID: 5380 RVA: 0x00242368 File Offset: 0x00241768
		internal DataColumn ColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnName);
			}
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00242388 File Offset: 0x00241788
		internal DataColumn Size
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.ColumnSize);
			}
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x002423A8 File Offset: 0x002417A8
		internal DataColumn BaseServerName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseServerName);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x002423C8 File Offset: 0x002417C8
		internal DataColumn BaseColumnName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseColumnName);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06001508 RID: 5384 RVA: 0x002423E8 File Offset: 0x002417E8
		internal DataColumn BaseTableName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseTableName);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x00242408 File Offset: 0x00241808
		internal DataColumn BaseCatalogName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseCatalogName);
			}
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600150A RID: 5386 RVA: 0x00242428 File Offset: 0x00241828
		internal DataColumn BaseSchemaName
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.BaseSchemaName);
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x00242448 File Offset: 0x00241848
		internal DataColumn IsAutoIncrement
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsAutoIncrement);
			}
		}

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00242468 File Offset: 0x00241868
		internal DataColumn IsUnique
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsUnique);
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00242488 File Offset: 0x00241888
		internal DataColumn IsKey
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsKey);
			}
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x002424A8 File Offset: 0x002418A8
		internal DataColumn IsRowVersion
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsRowVersion);
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x002424C8 File Offset: 0x002418C8
		internal DataColumn AllowDBNull
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.AllowDBNull);
			}
		}

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x002424E8 File Offset: 0x002418E8
		internal DataColumn IsExpression
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsExpression);
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00242508 File Offset: 0x00241908
		internal DataColumn IsHidden
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsHidden);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x00242528 File Offset: 0x00241928
		internal DataColumn IsLong
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsLong);
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x00242548 File Offset: 0x00241948
		internal DataColumn IsReadOnly
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.IsReadOnly);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x00242568 File Offset: 0x00241968
		internal DataColumn UnsortedIndex
		{
			get
			{
				return this.CachedDataColumn(DbSchemaTable.ColumnEnum.SchemaMappingUnsortedIndex);
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x00242588 File Offset: 0x00241988
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

		// Token: 0x06001516 RID: 5398 RVA: 0x002425B8 File Offset: 0x002419B8
		private DataColumn CachedDataColumn(DbSchemaTable.ColumnEnum column)
		{
			return this.CachedDataColumn(column, column);
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x002425D8 File Offset: 0x002419D8
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

		// Token: 0x04000C6C RID: 3180
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

		// Token: 0x04000C6D RID: 3181
		internal DataTable dataTable;

		// Token: 0x04000C6E RID: 3182
		private DataColumnCollection columns;

		// Token: 0x04000C6F RID: 3183
		private DataColumn[] columnCache = new DataColumn[DbSchemaTable.DBCOLUMN_NAME.Length];

		// Token: 0x04000C70 RID: 3184
		private bool _returnProviderSpecificTypes;

		// Token: 0x02000146 RID: 326
		private enum ColumnEnum
		{
			// Token: 0x04000C72 RID: 3186
			ColumnName,
			// Token: 0x04000C73 RID: 3187
			ColumnOrdinal,
			// Token: 0x04000C74 RID: 3188
			ColumnSize,
			// Token: 0x04000C75 RID: 3189
			BaseServerName,
			// Token: 0x04000C76 RID: 3190
			BaseCatalogName,
			// Token: 0x04000C77 RID: 3191
			BaseColumnName,
			// Token: 0x04000C78 RID: 3192
			BaseSchemaName,
			// Token: 0x04000C79 RID: 3193
			BaseTableName,
			// Token: 0x04000C7A RID: 3194
			IsAutoIncrement,
			// Token: 0x04000C7B RID: 3195
			IsUnique,
			// Token: 0x04000C7C RID: 3196
			IsKey,
			// Token: 0x04000C7D RID: 3197
			IsRowVersion,
			// Token: 0x04000C7E RID: 3198
			DataType,
			// Token: 0x04000C7F RID: 3199
			ProviderSpecificDataType,
			// Token: 0x04000C80 RID: 3200
			AllowDBNull,
			// Token: 0x04000C81 RID: 3201
			ProviderType,
			// Token: 0x04000C82 RID: 3202
			IsExpression,
			// Token: 0x04000C83 RID: 3203
			IsHidden,
			// Token: 0x04000C84 RID: 3204
			IsLong,
			// Token: 0x04000C85 RID: 3205
			IsReadOnly,
			// Token: 0x04000C86 RID: 3206
			SchemaMappingUnsortedIndex
		}
	}
}
