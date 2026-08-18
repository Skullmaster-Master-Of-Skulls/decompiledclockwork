using System;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C8 RID: 712
	internal class OracleLpParExpression : OracleLpScalarSubqueryExpression
	{
		// Token: 0x06001A56 RID: 6742 RVA: 0x0010A6B0 File Offset: 0x001088B0
		public OracleLpParExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.PAR_EXPRESSION;
		}
	}
}
