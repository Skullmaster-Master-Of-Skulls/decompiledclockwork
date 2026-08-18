using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E1 RID: 481
	internal abstract class OracleLpBaseAnsiJoinClauseWithCondition : OracleLpBaseAnsiJoinClause
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x000C43A4 File Offset: 0x000C25A4
		internal override OracleLpJoinClauseConditionalType ConditionalType
		{
			get
			{
				return OracleLpJoinClauseConditionalType.OnUsingCondition;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x000C43A8 File Offset: 0x000C25A8
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vTableReference.ColumnDescriptors;
			}
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x000C43B8 File Offset: 0x000C25B8
		// (set) Token: 0x060011CD RID: 4557 RVA: 0x000C43C0 File Offset: 0x000C25C0
		public OracleLpTableReferenceAnsi TableReference
		{
			get
			{
				return this.m_vTableReference;
			}
			set
			{
				this.m_vTableReference = value;
				if (this.m_vTableReference != null)
				{
					this.m_vTableReference.Parent = this;
				}
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060011CE RID: 4558 RVA: 0x000C43E0 File Offset: 0x000C25E0
		// (set) Token: 0x060011CF RID: 4559 RVA: 0x000C43E8 File Offset: 0x000C25E8
		public OracleLpJoinCondition Condition
		{
			get
			{
				return this.m_vCondition;
			}
			set
			{
				this.m_vCondition = value;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x000C43F4 File Offset: 0x000C25F4
		internal override List<OracleLpTablePrimary> TablePrimaryList
		{
			get
			{
				if (this.m_vTablePrimaryList == null)
				{
					this.m_vTablePrimaryList = new List<OracleLpTablePrimary>();
					this.m_vTablePrimaryList.AddRange(this.m_vTableReference.TablePrimaryList);
				}
				return this.m_vTablePrimaryList;
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000C4428 File Offset: 0x000C2628
		public OracleLpBaseAnsiJoinClauseWithCondition(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000C4434 File Offset: 0x000C2634
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTableReference.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x0400141C RID: 5148
		protected OracleLpTableReferenceAnsi m_vTableReference;

		// Token: 0x0400141D RID: 5149
		protected OracleLpJoinCondition m_vCondition;
	}
}
