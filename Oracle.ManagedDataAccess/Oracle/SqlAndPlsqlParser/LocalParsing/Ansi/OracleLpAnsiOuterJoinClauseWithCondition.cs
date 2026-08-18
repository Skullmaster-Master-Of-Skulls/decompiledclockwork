using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E6 RID: 486
	internal class OracleLpAnsiOuterJoinClauseWithCondition : OracleLpBaseAnsiJoinClauseWithCondition, IOracleLpAnsiOuterJoinClauseBase
	{
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060011EA RID: 4586 RVA: 0x000C45F4 File Offset: 0x000C27F4
		public override OracleLpJoinClauseType ClauseType
		{
			get
			{
				return OracleLpJoinClauseType.Outer;
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x000C45F8 File Offset: 0x000C27F8
		// (set) Token: 0x060011EC RID: 4588 RVA: 0x000C4600 File Offset: 0x000C2800
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

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x000C460C File Offset: 0x000C280C
		// (set) Token: 0x060011EE RID: 4590 RVA: 0x000C4614 File Offset: 0x000C2814
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

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060011EF RID: 4591 RVA: 0x000C4620 File Offset: 0x000C2820
		// (set) Token: 0x060011F0 RID: 4592 RVA: 0x000C4628 File Offset: 0x000C2828
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

		// Token: 0x060011F1 RID: 4593 RVA: 0x000C4634 File Offset: 0x000C2834
		public OracleLpAnsiOuterJoinClauseWithCondition(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x000C4640 File Offset: 0x000C2840
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.ClauseType);
			sb.Append("  OuterJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTableReference.ToString(sb);
		}

		// Token: 0x04001421 RID: 5153
		protected bool m_vOuter;

		// Token: 0x04001422 RID: 5154
		protected OracleLpOuterJoinType m_vType;

		// Token: 0x04001423 RID: 5155
		protected bool m_vNatural;
	}
}
