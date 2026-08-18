using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005D6 RID: 1494
	internal sealed class ColumnVar : Var
	{
		// Token: 0x06003BBA RID: 15290 RVA: 0x001185DA File Offset: 0x001167DA
		internal ColumnVar(int id, Table table, ColumnMD columnMetadata) : base(id, VarType.Column, columnMetadata.Type)
		{
			this.m_table = table;
			this.m_columnMetadata = columnMetadata;
		}

		// Token: 0x17000920 RID: 2336
		// (get) Token: 0x06003BBB RID: 15291 RVA: 0x001185F8 File Offset: 0x001167F8
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x00118600 File Offset: 0x00116800
		internal ColumnMD ColumnMetadata
		{
			get
			{
				return this.m_columnMetadata;
			}
		}

		// Token: 0x06003BBD RID: 15293 RVA: 0x00118608 File Offset: 0x00116808
		internal override bool TryGetName(out string name)
		{
			name = this.m_columnMetadata.Name;
			return true;
		}

		// Token: 0x0400166A RID: 5738
		private readonly ColumnMD m_columnMetadata;

		// Token: 0x0400166B RID: 5739
		private readonly Table m_table;
	}
}
