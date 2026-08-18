using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002B7 RID: 695
	internal class OracleLpBindVarExpression : OracleLpExpression
	{
		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00109BD0 File Offset: 0x00107DD0
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x00109BD8 File Offset: 0x00107DD8
		public OracleLpBindParameter BindParameter
		{
			get
			{
				return this.m_vBindParameter;
			}
			set
			{
				this.m_vBindParameter = value;
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00109BE4 File Offset: 0x00107DE4
		public OracleLpBindVarExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.BIND_VAR;
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00109BF4 File Offset: 0x00107DF4
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00109BF8 File Offset: 0x00107DF8
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = new List<OracleLpExpression>();
				this.m_vAllTerminalExpressions.Add(this);
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00109C20 File Offset: 0x00107E20
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  Bind Parameter: ").Append(this.m_vBindParameter.ToString());
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00109C48 File Offset: 0x00107E48
		public override string ToString()
		{
			return ":" + this.m_vBindParameter.Name.ToString();
		}

		// Token: 0x04001C5A RID: 7258
		private OracleLpBindParameter m_vBindParameter;
	}
}
