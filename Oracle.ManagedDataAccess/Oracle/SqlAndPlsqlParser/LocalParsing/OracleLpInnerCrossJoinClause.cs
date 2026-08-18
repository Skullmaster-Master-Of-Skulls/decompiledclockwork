using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002D6 RID: 726
	internal class OracleLpInnerCrossJoinClause : OracleLpSpecificJoinClause
	{
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x0010AE54 File Offset: 0x00109054
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x0010AE5C File Offset: 0x0010905C
		public OracleLpInnerCrossJoinType Type
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

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x0010AE68 File Offset: 0x00109068
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x0010AE70 File Offset: 0x00109070
		public bool Inner
		{
			get
			{
				return this.m_vInner;
			}
			set
			{
				this.m_vInner = value;
			}
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x0010AE7C File Offset: 0x0010907C
		public OracleLpInnerCrossJoinClause(OracleLpFromListTerm ft) : base(ft)
		{
			this.m_vClauseType = OracleLpJoinClauseType.Inner;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x0010AE8C File Offset: 0x0010908C
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.m_vClauseType);
			sb.Append("  InnerJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTableReference.ToString(sb);
		}

		// Token: 0x04001CA1 RID: 7329
		protected OracleLpInnerCrossJoinType m_vType;

		// Token: 0x04001CA2 RID: 7330
		protected bool m_vInner;
	}
}
