using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C5 RID: 709
	internal class OracleLpModelExpression : OracleLpExpression
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x0010A4F0 File Offset: 0x001086F0
		// (set) Token: 0x06001A45 RID: 6725 RVA: 0x0010A4F8 File Offset: 0x001086F8
		public OracleLpModelExpressionType ModelExpressionType
		{
			get
			{
				return this.m_vModelExpressionType;
			}
			set
			{
				this.m_vModelExpressionType = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x0010A504 File Offset: 0x00108704
		// (set) Token: 0x06001A47 RID: 6727 RVA: 0x0010A50C File Offset: 0x0010870C
		public OracleLpAnalyticFunction AnalyticFunction
		{
			get
			{
				return this.m_vAnalyticFunction;
			}
			set
			{
				this.m_vAnalyticFunction = value;
			}
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x0010A518 File Offset: 0x00108718
		public OracleLpModelExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vExpressionType = OracleLpExpressionType.MODEL_EXPRESSION;
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x0010A528 File Offset: 0x00108728
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x0010A52C File Offset: 0x0010872C
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				switch (this.m_vModelExpressionType)
				{
				case OracleLpModelExpressionType.ANALYTIC_FUNCTION:
					this.m_vAllTerminalExpressions = this.m_vAnalyticFunction.Arguments[0].Expression.GetAllTerminalExpressions();
					using (IEnumerator<OracleLpExpression> enumerator = this.m_vAllTerminalExpressions.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							OracleLpExpression oracleLpExpression = enumerator.Current;
							if (oracleLpExpression.ParentExpression == null)
							{
								oracleLpExpression.ParentExpression = this;
							}
						}
						goto IL_8D;
					}
					break;
				}
				this.m_vAllTerminalExpressions = base.GetAllTerminalExpressions();
			}
			IL_8D:
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x04001C77 RID: 7287
		protected OracleLpModelExpressionType m_vModelExpressionType;

		// Token: 0x04001C78 RID: 7288
		protected OracleLpAnalyticFunction m_vAnalyticFunction;
	}
}
