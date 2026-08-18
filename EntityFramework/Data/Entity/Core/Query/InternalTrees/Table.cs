using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200063A RID: 1594
	internal class Table
	{
		// Token: 0x06003EA3 RID: 16035 RVA: 0x0011F6C4 File Offset: 0x0011D8C4
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

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x06003EA4 RID: 16036 RVA: 0x0011F7E4 File Offset: 0x0011D9E4
		internal TableMD TableMetadata
		{
			get
			{
				return this.m_tableMetadata;
			}
		}

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06003EA5 RID: 16037 RVA: 0x0011F7EC File Offset: 0x0011D9EC
		internal VarList Columns
		{
			get
			{
				return this.m_columns;
			}
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x0011F7F4 File Offset: 0x0011D9F4
		internal VarVec ReferencedColumns
		{
			get
			{
				return this.m_referencedColumns;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06003EA7 RID: 16039 RVA: 0x0011F7FC File Offset: 0x0011D9FC
		internal VarVec NonNullableColumns
		{
			get
			{
				return this.m_nonnullableColumns;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x0011F804 File Offset: 0x0011DA04
		internal VarVec Keys
		{
			get
			{
				return this.m_keys;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x06003EA9 RID: 16041 RVA: 0x0011F80C File Offset: 0x0011DA0C
		internal int TableId
		{
			get
			{
				return this.m_tableId;
			}
		}

		// Token: 0x06003EAA RID: 16042 RVA: 0x0011F814 File Offset: 0x0011DA14
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}::{1}", new object[]
			{
				this.m_tableMetadata,
				this.TableId
			});
		}

		// Token: 0x04001770 RID: 6000
		private readonly TableMD m_tableMetadata;

		// Token: 0x04001771 RID: 6001
		private readonly VarList m_columns;

		// Token: 0x04001772 RID: 6002
		private readonly VarVec m_referencedColumns;

		// Token: 0x04001773 RID: 6003
		private readonly VarVec m_keys;

		// Token: 0x04001774 RID: 6004
		private readonly VarVec m_nonnullableColumns;

		// Token: 0x04001775 RID: 6005
		private readonly int m_tableId;
	}
}
