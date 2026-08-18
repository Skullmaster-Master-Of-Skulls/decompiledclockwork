using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002D7 RID: 727
	internal class OracleLpOuterJoinClause : OracleLpSpecificJoinClause
	{
		// Token: 0x17000456 RID: 1110
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0010AEFC File Offset: 0x001090FC
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x0010AF04 File Offset: 0x00109104
		public bool Outer
		{
			get
			{
				return this.m_vOuter;
			}
			set
			{
				this.m_vOuter = value;
			}
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x0010AF10 File Offset: 0x00109110
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x0010AF18 File Offset: 0x00109118
		public OracleLpOuterJoinType Type
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

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0010AF24 File Offset: 0x00109124
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0010AF2C File Offset: 0x0010912C
		public bool Natural
		{
			get
			{
				return this.m_vNatural;
			}
			set
			{
				this.m_vNatural = value;
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x0010AF38 File Offset: 0x00109138
		public OracleLpOuterJoinClause(OracleLpFromListTerm ft) : base(ft)
		{
			this.m_vClauseType = OracleLpJoinClauseType.Outer;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x0010AF48 File Offset: 0x00109148
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.m_vClauseType);
			sb.Append("  OuterJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTableReference.ToString(sb);
		}

		// Token: 0x04001CA3 RID: 7331
		protected bool m_vOuter;

		// Token: 0x04001CA4 RID: 7332
		protected OracleLpOuterJoinType m_vType;

		// Token: 0x04001CA5 RID: 7333
		protected bool m_vNatural;
	}
}
