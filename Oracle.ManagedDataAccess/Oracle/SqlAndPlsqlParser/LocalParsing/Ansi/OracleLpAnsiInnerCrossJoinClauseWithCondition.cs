using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E3 RID: 483
	internal class OracleLpAnsiInnerCrossJoinClauseWithCondition : OracleLpBaseAnsiJoinClauseWithCondition
	{
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060011DA RID: 4570 RVA: 0x000C44CC File Offset: 0x000C26CC
		public override OracleLpJoinClauseType ClauseType
		{
			get
			{
				return OracleLpJoinClauseType.Inner;
			}
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x000C44D0 File Offset: 0x000C26D0
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x000C44D8 File Offset: 0x000C26D8
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

		// Token: 0x060011DD RID: 4573 RVA: 0x000C44E4 File Offset: 0x000C26E4
		public OracleLpAnsiInnerCrossJoinClauseWithCondition(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000C44F0 File Offset: 0x000C26F0
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.ClauseType);
			sb.Append("  InnerJoinType:  ");
			sb.Append(this.ConditionalType);
			sb.Append('\n');
			this.m_vTableReference.ToString(sb);
		}

		// Token: 0x0400141F RID: 5151
		protected bool m_vInner;
	}
}
