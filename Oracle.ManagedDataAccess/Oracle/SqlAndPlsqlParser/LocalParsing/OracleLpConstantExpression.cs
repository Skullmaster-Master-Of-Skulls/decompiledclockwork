using System;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002BE RID: 702
	internal class OracleLpConstantExpression : OracleLpSimpleExpression
	{
		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06001A12 RID: 6674 RVA: 0x0010A118 File Offset: 0x00108318
		// (set) Token: 0x06001A13 RID: 6675 RVA: 0x0010A120 File Offset: 0x00108320
		public object ExpressionValue { get; set; }

		// Token: 0x06001A14 RID: 6676 RVA: 0x0010A12C File Offset: 0x0010832C
		public OracleLpConstantExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vSimpleExpressionType = OracleLpSimpleExpressionType.CONSTANT;
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0010A13C File Offset: 0x0010833C
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  Value: ").Append(this.ExpressionValue.ToString());
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0010A164 File Offset: 0x00108364
		public override string ToString()
		{
			return this.ExpressionValue.ToString();
		}
	}
}
