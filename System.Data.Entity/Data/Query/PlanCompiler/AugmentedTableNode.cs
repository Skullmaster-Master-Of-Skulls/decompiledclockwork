using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000051 RID: 81
	internal sealed class AugmentedTableNode : AugmentedNode
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x0001F2B0 File Offset: 0x0001D4B0
		internal AugmentedTableNode(int id, Node node) : base(id, node)
		{
			ScanTableOp scanTableOp = (ScanTableOp)node.Op;
			this.m_table = scanTableOp.Table;
			this.m_lastVisibleId = id;
			this.m_replacementTable = this;
			this.m_newLocationId = id;
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060006E2 RID: 1762 RVA: 0x0001F2F2 File Offset: 0x0001D4F2
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0001F2FA File Offset: 0x0001D4FA
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0001F302 File Offset: 0x0001D502
		internal int LastVisibleId
		{
			get
			{
				return this.m_lastVisibleId;
			}
			set
			{
				this.m_lastVisibleId = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001F30B File Offset: 0x0001D50B
		internal bool IsEliminated
		{
			get
			{
				return this.m_replacementTable != this;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x0001F319 File Offset: 0x0001D519
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001F321 File Offset: 0x0001D521
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0001F32A File Offset: 0x0001D52A
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001F332 File Offset: 0x0001D532
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

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x0001F33B File Offset: 0x0001D53B
		internal bool IsMoved
		{
			get
			{
				return this.m_newLocationId != base.Id;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x0001F34E File Offset: 0x0001D54E
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x0001F356 File Offset: 0x0001D556
		internal VarVec NullableColumns
		{
			get
			{
				return this.m_nullableColumns;
			}
			set
			{
				this.m_nullableColumns = value;
			}
		}

		// Token: 0x0400079D RID: 1949
		private int m_lastVisibleId;

		// Token: 0x0400079E RID: 1950
		private Table m_table;

		// Token: 0x0400079F RID: 1951
		private AugmentedTableNode m_replacementTable;

		// Token: 0x040007A0 RID: 1952
		private int m_newLocationId;

		// Token: 0x040007A1 RID: 1953
		private VarVec m_nullableColumns;
	}
}
