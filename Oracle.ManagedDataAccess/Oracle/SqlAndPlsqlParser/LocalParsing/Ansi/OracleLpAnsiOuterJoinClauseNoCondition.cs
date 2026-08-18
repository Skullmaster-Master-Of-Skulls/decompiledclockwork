using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing.Ansi
{
	// Token: 0x020001E7 RID: 487
	internal class OracleLpAnsiOuterJoinClauseNoCondition : OracleLpBaseAnsiJoinClauseNoCondition, IOracleLpAnsiOuterJoinClauseBase
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x000C46B0 File Offset: 0x000C28B0
		public override OracleLpJoinClauseType ClauseType
		{
			get
			{
				return OracleLpJoinClauseType.Outer;
			}
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060011F4 RID: 4596 RVA: 0x000C46B4 File Offset: 0x000C28B4
		// (set) Token: 0x060011F5 RID: 4597 RVA: 0x000C46BC File Offset: 0x000C28BC
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

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x000C46C8 File Offset: 0x000C28C8
		// (set) Token: 0x060011F7 RID: 4599 RVA: 0x000C46D0 File Offset: 0x000C28D0
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

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x000C46DC File Offset: 0x000C28DC
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x000C46E4 File Offset: 0x000C28E4
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

		// Token: 0x060011FA RID: 4602 RVA: 0x000C46F0 File Offset: 0x000C28F0
		public OracleLpAnsiOuterJoinClauseNoCondition(OracleLpFromListTerm ft) : base(ft)
		{
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000C46FC File Offset: 0x000C28FC
		internal override void ToString(StringBuilder sb)
		{
			string depthIndent = base.DepthIndent;
			sb.Append(depthIndent);
			sb.Append("JoinType:  ");
			sb.Append(this.ClauseType);
			sb.Append("  OuterJoinType:  ");
			sb.Append(this.m_vType);
			sb.Append('\n');
			this.m_vTablePrimary.ToString(sb);
		}

		// Token: 0x04001424 RID: 5156
		protected bool m_vOuter;

		// Token: 0x04001425 RID: 5157
		protected OracleLpOuterJoinType m_vType;

		// Token: 0x04001426 RID: 5158
		protected bool m_vNatural;
	}
}
