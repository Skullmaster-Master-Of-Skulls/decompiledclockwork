using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C6 RID: 710
	internal class OracleLpFunctionExpression : OracleLpExpression
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x0010A5DC File Offset: 0x001087DC
		public OracleLpFunctionExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.FUNCTION_EXPRESSION;
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x0010A5EC File Offset: 0x001087EC
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x0010A5F0 File Offset: 0x001087F0
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x0010A5F8 File Offset: 0x001087F8
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  ExpressionText: ");
			if (this.m_vText != null)
			{
				sb.Append(this.m_vText.Fragment);
			}
		}
	}
}
