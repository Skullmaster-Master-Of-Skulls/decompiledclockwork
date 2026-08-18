using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002BB RID: 699
	internal class OracleLpStarExpression : OracleLpExpression
	{
		// Token: 0x060019FF RID: 6655 RVA: 0x00109F4C File Offset: 0x0010814C
		public OracleLpStarExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.STAR_EXPRESSION;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00109F60 File Offset: 0x00108160
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00109F64 File Offset: 0x00108164
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = new List<OracleLpExpression>();
				this.m_vAllTerminalExpressions.Add(this);
			}
			return this.m_vAllTerminalExpressions;
		}
	}
}
