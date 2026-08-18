using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x0200010F RID: 271
	internal sealed class ColumnVar : Var
	{
		// Token: 0x06000DA6 RID: 3494 RVA: 0x0003D15D File Offset: 0x0003B35D
		internal ColumnVar(int id, Table table, ColumnMD columnMetadata) : base(id, VarType.Column, columnMetadata.Type)
		{
			this.m_table = table;
			this.m_columnMetadata = columnMetadata;
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000DA7 RID: 3495 RVA: 0x0003D17B File Offset: 0x0003B37B
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0003D183 File Offset: 0x0003B383
		internal ColumnMD ColumnMetadata
		{
			get
			{
				return this.m_columnMetadata;
			}
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0003D18B File Offset: 0x0003B38B
		internal override bool TryGetName(out string name)
		{
			name = this.m_columnMetadata.Name;
			return true;
		}

		// Token: 0x040009D6 RID: 2518
		private ColumnMD m_columnMetadata;

		// Token: 0x040009D7 RID: 2519
		private Table m_table;
	}
}
