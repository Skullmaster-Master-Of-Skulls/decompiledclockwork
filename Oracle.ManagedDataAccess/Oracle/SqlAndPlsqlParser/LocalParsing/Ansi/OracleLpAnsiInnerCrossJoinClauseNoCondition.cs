using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E4 RID: 484
	internal class OracleLpAnsiInnerCrossJoinClauseNoCondition : OracleLpBaseAnsiJoinClauseNoCondition
	{
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x000C4560 File Offset: 0x000C2760
		public override OracleLpJoinClauseType ClauseType
		{
			get
			{
				return OracleLpJoinClauseType.Inner;
			}
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x000C4564 File Offset: 0x000C2764
		// (set) Token: 0x060011E1 RID: 4577 RVA: 0x000C456C File Offset: 0x000C276C
		public OracleLpInnerCrossJoinNoConditionType Type
		{
			get
			{
				return this.m_vType;
			}
			set
			{
				this.m_vType = value;
			}
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000C4578 File Offset: 0x000C2778
		public OracleLpAnsiInnerCrossJoinClauseNoCondition(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000C4584 File Offset: 0x000C2784
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.ClauseType);
			sb.Append("  InnerJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTablePrimary.ToString(sb);
		}

		// Token: 0x04001420 RID: 5152
		protected OracleLpInnerCrossJoinNoConditionType m_vType;
	}
}
