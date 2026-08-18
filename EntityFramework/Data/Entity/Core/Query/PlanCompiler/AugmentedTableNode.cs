using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000657 RID: 1623
	internal sealed class AugmentedTableNode : AugmentedNode
	{
		// Token: 0x06003F72 RID: 16242 RVA: 0x00122838 File Offset: 0x00120A38
		internal AugmentedTableNode(int id, Node node) : base(id, node)
		{
			ScanTableOp scanTableOp = (ScanTableOp)node.Op;
			this.m_table = scanTableOp.Table;
			this.LastVisibleId = id;
			this.m_replacementTable = this;
			this.m_newLocationId = id;
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003F73 RID: 16243 RVA: 0x0012287A File Offset: 0x00120A7A
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06003F74 RID: 16244 RVA: 0x00122882 File Offset: 0x00120A82
		// (set) Token: 0x06003F75 RID: 16245 RVA: 0x0012288A File Offset: 0x00120A8A
		internal int LastVisibleId { get; set; }

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06003F76 RID: 16246 RVA: 0x00122893 File Offset: 0x00120A93
		internal bool IsEliminated
		{
			get
			{
				return this.m_replacementTable != this;
			}
		}

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x06003F77 RID: 16247 RVA: 0x001228A1 File Offset: 0x00120AA1
		// (set) Token: 0x06003F78 RID: 16248 RVA: 0x001228A9 File Offset: 0x00120AA9
		internal AugmentedTableNode ReplacementTable
		{
			get
			{
				return this.m_replacementTable;
			}
			set
			{
				this.m_replacementTable = value;
			}
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06003F79 RID: 16249 RVA: 0x001228B2 File Offset: 0x00120AB2
		// (set) Token: 0x06003F7A RID: 16250 RVA: 0x001228BA File Offset: 0x00120ABA
		internal int NewLocationId
		{
			get
			{
				return this.m_newLocationId;
			}
			set
			{
				this.m_newLocationId = value;
			}
		}

		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06003F7B RID: 16251 RVA: 0x001228C3 File Offset: 0x00120AC3
		internal bool IsMoved
		{
			get
			{
				return this.m_newLocationId != base.Id;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x001228D6 File Offset: 0x00120AD6
		// (set) Token: 0x06003F7D RID: 16253 RVA: 0x001228DE File Offset: 0x00120ADE
		internal VarVec NullableColumns { get; set; }

		// Token: 0x040017AD RID: 6061
		private readonly Table m_table;

		// Token: 0x040017AE RID: 6062
		private AugmentedTableNode m_replacementTable;

		// Token: 0x040017AF RID: 6063
		private int m_newLocationId;
	}
}
