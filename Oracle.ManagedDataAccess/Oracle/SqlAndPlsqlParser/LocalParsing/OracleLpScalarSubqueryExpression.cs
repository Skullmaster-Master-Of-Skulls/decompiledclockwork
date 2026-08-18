using System;
using System.Collections.Generic;
using System.Text;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C7 RID: 711
	internal class OracleLpScalarSubqueryExpression : OracleLpExpression, IOracleLpNamedObjectContainer
	{
		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x0010A628 File Offset: 0x00108828
		// (set) Token: 0x06001A50 RID: 6736 RVA: 0x0010A630 File Offset: 0x00108830
		internal OracleLpSubquery Subquery
		{
			get
			{
				return this.m_vSubquery;
			}
			set
			{
				this.m_vSubquery = value;
				if (this.m_vSubquery != null)
				{
					this.m_vSubquery.Parent = this;
				}
			}
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x0010A650 File Offset: 0x00108850
		public OracleLpScalarSubqueryExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.SCALAR_SUBQUERY_EXPRESSION;
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x0010A664 File Offset: 0x00108864
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x0010A668 File Offset: 0x00108868
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x0010A670 File Offset: 0x00108870
		public virtual void RetrieveNamedObjectReferences(OracleLpStatement statement)
		{
			this.m_vSubquery.RetrieveNamedObjectReferences(statement);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x0010A680 File Offset: 0x00108880
		internal override void ToString(StringBuilder sb)
		{
			base.ToString(sb);
			sb.Append("  ExpressionText: ");
			if (this.m_vText != null)
			{
				sb.Append(this.m_vText.Fragment);
			}
		}

		// Token: 0x04001C79 RID: 7289
		private OracleLpSubquery m_vSubquery;
	}
}
