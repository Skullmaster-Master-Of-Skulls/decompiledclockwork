using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E2 RID: 482
	internal abstract class OracleLpBaseAnsiJoinClauseNoCondition : OracleLpBaseAnsiJoinClause
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x000C4444 File Offset: 0x000C2644
		internal override OracleLpJoinClauseConditionalType ConditionalType
		{
			get
			{
				return OracleLpJoinClauseConditionalType.NoCondition;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x000C4448 File Offset: 0x000C2648
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vTablePrimary.ColumnDescriptors;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x000C4458 File Offset: 0x000C2658
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x000C4460 File Offset: 0x000C2660
		public OracleLpTablePrimary TablePrimary
		{
			get
			{
				return this.m_vTablePrimary;
			}
			set
			{
				this.m_vTablePrimary = value;
				if (this.m_vTablePrimary != null)
				{
					this.m_vTablePrimary.Parent = this;
				}
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x000C4480 File Offset: 0x000C2680
		internal override List<OracleLpTablePrimary> TablePrimaryList
		{
			get
			{
				if (this.m_vTablePrimaryList == null)
				{
					this.m_vTablePrimaryList = new List<OracleLpTablePrimary>(1);
					this.m_vTablePrimaryList.Add(this.m_vTablePrimary);
				}
				return this.m_vTablePrimaryList;
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000C44B0 File Offset: 0x000C26B0
		public OracleLpBaseAnsiJoinClauseNoCondition(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000C44BC File Offset: 0x000C26BC
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTablePrimary.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x0400141E RID: 5150
		protected OracleLpTablePrimary m_vTablePrimary;
	}
}
