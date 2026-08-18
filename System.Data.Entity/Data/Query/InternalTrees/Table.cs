using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000B2 RID: 178
	internal class Table
	{
		// Token: 0x06000B42 RID: 2882 RVA: 0x000393D4 File Offset: 0x000375D4
		internal Table(Command command, TableMD tableMetadata, int tableId)
		{
			this.m_tableMetadata = tableMetadata;
			this.m_columns = Command.CreateVarList();
			this.m_keys = command.CreateVarVec();
			this.m_nonnullableColumns = command.CreateVarVec();
			this.m_tableId = tableId;
			Dictionary<string, ColumnVar> dictionary = new Dictionary<string, ColumnVar>();
			foreach (ColumnMD columnMD in tableMetadata.Columns)
			{
				ColumnVar columnVar = command.CreateColumnVar(this, columnMD);
				dictionary[columnMD.Name] = columnVar;
				if (!columnMD.IsNullable)
				{
					this.m_nonnullableColumns.Set(columnVar);
				}
			}
			foreach (ColumnMD columnMD2 in tableMetadata.Keys)
			{
				ColumnVar v = dictionary[columnMD2.Name];
				this.m_keys.Set(v);
			}
			this.m_referencedColumns = command.CreateVarVec(this.m_columns);
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x000394F4 File Offset: 0x000376F4
		internal TableMD TableMetadata
		{
			get
			{
				return this.m_tableMetadata;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x000394FC File Offset: 0x000376FC
		internal VarList Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00039504 File Offset: 0x00037704
		internal VarVec ReferencedColumns
		{
			get
			{
				return this.m_referencedColumns;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x0003950C File Offset: 0x0003770C
		internal VarVec NonNullableColumns
		{
			get
			{
				return this.m_nonnullableColumns;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00039514 File Offset: 0x00037714
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x0003951C File Offset: 0x0003771C
		internal int TableId
		{
			get
			{
				return this.m_tableId;
			}
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00039524 File Offset: 0x00037724
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}::{1}", new object[]
			{
				this.m_tableMetadata.ToString(),
				this.TableId
			});
		}

		// Token: 0x040008E5 RID: 2277
		private TableMD m_tableMetadata;

		// Token: 0x040008E6 RID: 2278
		private VarList m_columns;

		// Token: 0x040008E7 RID: 2279
		private VarVec m_referencedColumns;

		// Token: 0x040008E8 RID: 2280
		private VarVec m_keys;

		// Token: 0x040008E9 RID: 2281
		private VarVec m_nonnullableColumns;

		// Token: 0x040008EA RID: 2282
		private int m_tableId;
	}
}
