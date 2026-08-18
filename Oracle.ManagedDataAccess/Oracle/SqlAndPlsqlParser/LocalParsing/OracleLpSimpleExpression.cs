using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B6 RID: 694
	internal class OracleLpSimpleExpression : OracleLpExpression
	{
		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x00109B54 File Offset: 0x00107D54
		// (set) Token: 0x060019D9 RID: 6617 RVA: 0x00109B5C File Offset: 0x00107D5C
		public OracleLpSimpleExpressionType SimpleExpressionType
		{
			get
			{
				return this.m_vSimpleExpressionType;
			}
			set
			{
				this.m_vSimpleExpressionType = value;
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00109B68 File Offset: 0x00107D68
		public OracleLpSimpleExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.SIMPLE_EXPRESSION;
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00109B7C File Offset: 0x00107D7C
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00109B80 File Offset: 0x00107D80
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = new List<OracleLpExpression>();
				this.m_vAllTerminalExpressions.Add(this);
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00109BA8 File Offset: 0x00107DA8
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  SimpleExprType: ").Append(this.m_vSimpleExpressionType);
		}

		// Token: 0x04001C59 RID: 7257
		protected OracleLpSimpleExpressionType m_vSimpleExpressionType;
	}
}
