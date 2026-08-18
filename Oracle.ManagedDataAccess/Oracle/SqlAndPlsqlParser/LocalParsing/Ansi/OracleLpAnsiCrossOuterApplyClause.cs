using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E8 RID: 488
	internal abstract class OracleLpAnsiCrossOuterApplyClause : OracleLpBaseAnsiJoinClause
	{
		// Token: 0x1700030C RID: 780
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x000C476C File Offset: 0x000C296C
		public override OracleLpJoinClauseType ClauseType
		{
			get
			{
				return OracleLpJoinClauseType.CrossOuterApply;
			}
		}

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x000C4770 File Offset: 0x000C2970
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x000C4778 File Offset: 0x000C2978
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

		// Token: 0x060011FF RID: 4607 RVA: 0x000C4784 File Offset: 0x000C2984
		public OracleLpAnsiCrossOuterApplyClause(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x000C4790 File Offset: 0x000C2990
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.ClauseType);
			sb.Append("  CrossOuterApplyJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
		}

		// Token: 0x04001427 RID: 5159
		protected OracleLpCrossOuterApplyType m_vType;
	}
}
