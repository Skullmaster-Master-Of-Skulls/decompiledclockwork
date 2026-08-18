using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002BA RID: 698
	internal class OracleLpIntervalExpression : OracleLpExpression
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x00109E88 File Offset: 0x00108088
		// (set) Token: 0x060019FA RID: 6650 RVA: 0x00109E90 File Offset: 0x00108090
		public OracleLpIntervalExpressionType IntervalExpessionType
		{
			get
			{
				return this.m_vIntervalExpessionType;
			}
			set
			{
				this.m_vIntervalExpessionType = value;
			}
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00109E9C File Offset: 0x0010809C
		public OracleLpIntervalExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.INTERVAL_EXPRESSION;
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x00109EAC File Offset: 0x001080AC
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00109EB0 File Offset: 0x001080B0
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = new List<OracleLpExpression>();
				this.m_vAllTerminalExpressions.Add(this);
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00109ED8 File Offset: 0x001080D8
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  Interval expression: ");
			sb.Append("  Type: ");
			switch (this.m_vIntervalExpessionType)
			{
			case OracleLpIntervalExpressionType.YEAR_TO_MONTH:
				sb.Append("YEAR TO MONTH");
				break;
			case OracleLpIntervalExpressionType.DAY_TO_SECOND:
				sb.Append("DAY TO SECOND");
				break;
			}
			sb.Append(this.m_vText.Fragment);
		}

		// Token: 0x04001C61 RID: 7265
		protected OracleLpIntervalExpressionType m_vIntervalExpessionType;
	}
}
