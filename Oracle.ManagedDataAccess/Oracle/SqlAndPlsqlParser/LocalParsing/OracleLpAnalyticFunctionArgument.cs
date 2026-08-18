using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x0200028F RID: 655
	internal class OracleLpAnalyticFunctionArgument
	{
		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001967 RID: 6503 RVA: 0x00109288 File Offset: 0x00107488
		// (set) Token: 0x06001968 RID: 6504 RVA: 0x00109290 File Offset: 0x00107490
		public OracleLpAnalyticFunctionArgumentModifier Modifier
		{
			get
			{
				return this.m_vModifier;
			}
			set
			{
				this.m_vModifier = value;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001969 RID: 6505 RVA: 0x0010929C File Offset: 0x0010749C
		// (set) Token: 0x0600196A RID: 6506 RVA: 0x001092A4 File Offset: 0x001074A4
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

		// Token: 0x04001B85 RID: 7045
		protected OracleLpAnalyticFunctionArgumentModifier m_vModifier;

		// Token: 0x04001B86 RID: 7046
		protected OracleLpExpression m_vExpression;
	}
}
