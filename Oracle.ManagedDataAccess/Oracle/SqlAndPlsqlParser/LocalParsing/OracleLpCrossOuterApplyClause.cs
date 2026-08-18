using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002D8 RID: 728
	internal class OracleLpCrossOuterApplyClause : OracleLpSpecificJoinClause
	{
		// Token: 0x17000459 RID: 1113
		// (get) Token: 0x06001A8B RID: 6795 RVA: 0x0010AFB8 File Offset: 0x001091B8
		// (set) Token: 0x06001A8C RID: 6796 RVA: 0x0010AFC0 File Offset: 0x001091C0
		public OracleLpCrossOuterApplyType Type
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

		// Token: 0x06001A8D RID: 6797 RVA: 0x0010AFCC File Offset: 0x001091CC
		public OracleLpCrossOuterApplyClause(OracleLpFromListTerm ft) : base(ft)
		{
			this.m_vClauseType = OracleLpJoinClauseType.CrossOuterApply;
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x0010AFDC File Offset: 0x001091DC
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.m_vClauseType);
			sb.Append("  CrossOuterApplyJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTableReference.ToString(sb);
		}

		// Token: 0x04001CA6 RID: 7334
		protected OracleLpCrossOuterApplyType m_vType;
	}
}
