using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E9 RID: 489
	internal class OracleLpAnsiCrossOuterApplyClauseTablePrimary : OracleLpAnsiCrossOuterApplyClause
	{
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000C47F4 File Offset: 0x000C29F4
		// (set) Token: 0x06001202 RID: 4610 RVA: 0x000C47FC File Offset: 0x000C29FC
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

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x000C481C File Offset: 0x000C2A1C
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

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001204 RID: 4612 RVA: 0x000C484C File Offset: 0x000C2A4C
		public override List<OracleLpColumnDescriptor> ColumnDescriptors
		{
			get
			{
				return this.m_vTablePrimary.ColumnDescriptors;
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x000C485C File Offset: 0x000C2A5C
		public OracleLpAnsiCrossOuterApplyClauseTablePrimary(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000C4868 File Offset: 0x000C2A68
		public override void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vTablePrimary.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x000C4878 File Offset: 0x000C2A78
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.ClauseType);
			sb.Append("  CrossOuterApplyJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTablePrimary.ToString(sb);
		}

		// Token: 0x04001428 RID: 5160
		protected OracleLpTablePrimary m_vTablePrimary;
	}
}
