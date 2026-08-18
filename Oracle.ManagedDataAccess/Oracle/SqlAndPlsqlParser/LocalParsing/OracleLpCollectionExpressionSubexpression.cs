using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020001D1 RID: 465
	internal class OracleLpCollectionExpressionSubexpression : OracleLpCollectionExpression
	{
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060011A7 RID: 4519 RVA: 0x000C3EC8 File Offset: 0x000C20C8
		internal override OracleLpCollectionExpressionType Type
		{
			get
			{
				return OracleLpCollectionExpressionType.Subexpression;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060011A8 RID: 4520 RVA: 0x000C3ECC File Offset: 0x000C20CC
		// (set) Token: 0x060011A9 RID: 4521 RVA: 0x000C3ED4 File Offset: 0x000C20D4
		public OracleLpExpression Expression
		{
			get
			{
				return this.m_vExpression;
			}
			set
			{
				this.m_vExpression = value;
			}
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000C3EE0 File Offset: 0x000C20E0
		public OracleLpCollectionExpressionSubexpression(OracleLpStatementElement p) : base(p)
		{
		}

		// Token: 0x040013FC RID: 5116
		protected OracleLpExpression m_vExpression;
	}
}
