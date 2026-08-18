using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001D0 RID: 464
	internal class OracleLpCollectionExpressionSubquery : OracleLpCollectionExpression
	{
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x060011A3 RID: 4515 RVA: 0x000C3EA4 File Offset: 0x000C20A4
		internal override OracleLpCollectionExpressionType Type
		{
			get
			{
				return OracleLpCollectionExpressionType.Subquery;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060011A4 RID: 4516 RVA: 0x000C3EA8 File Offset: 0x000C20A8
		// (set) Token: 0x060011A5 RID: 4517 RVA: 0x000C3EB0 File Offset: 0x000C20B0
		public OracleLpSubquery Subquery
		{
			get
			{
				return this.m_vSubquery;
			}
			internal set
			{
				this.m_vSubquery = value;
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000C3EBC File Offset: 0x000C20BC
		public OracleLpCollectionExpressionSubquery(OracleLpStatementElement p) : base(p)
		{
		}

		// Token: 0x040013FB RID: 5115
		private OracleLpSubquery m_vSubquery;
	}
}
