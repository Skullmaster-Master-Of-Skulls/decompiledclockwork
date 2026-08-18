using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C2 RID: 706
	internal class OracleLpCompoundBinaryExpression : OracleLpCompoundExpression
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0010A2E4 File Offset: 0x001084E4
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x0010A2EC File Offset: 0x001084EC
		public OracleLpCompoundExpressionBinaryOperator BinaryOperator
		{
			get
			{
				return this.m_vBinaryOperator;
			}
			set
			{
				this.m_vBinaryOperator = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001A2D RID: 6701 RVA: 0x0010A2F8 File Offset: 0x001084F8
		// (set) Token: 0x06001A2E RID: 6702 RVA: 0x0010A300 File Offset: 0x00108500
		public OracleLpExpression LeftOperand
		{
			get
			{
				return this.m_vLeftOperand;
			}
			set
			{
				this.m_vLeftOperand = value;
				if (value != null)
				{
					value.ParentExpression = this;
				}
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001A2F RID: 6703 RVA: 0x0010A314 File Offset: 0x00108514
		// (set) Token: 0x06001A30 RID: 6704 RVA: 0x0010A31C File Offset: 0x0010851C
		public OracleLpExpression RightOperand
		{
			get
			{
				return this.m_vRightOperand;
			}
			set
			{
				this.m_vRightOperand = value;
				if (value != null)
				{
					value.ParentExpression = this;
				}
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0010A330 File Offset: 0x00108530
		public OracleLpCompoundBinaryExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vCompoundType = OracleLpCompoundExpressionType.BINARY;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0010A340 File Offset: 0x00108540
		public override void EvaluateDatatype()
		{
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0010A344 File Offset: 0x00108544
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				List<OracleLpExpression> list = new List<OracleLpExpression>();
				this.m_vAllTerminalExpressions = list;
				list.AddRange(this.m_vLeftOperand.GetAllTerminalExpressions());
				list.AddRange(this.m_vRightOperand.GetAllTerminalExpressions());
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x04001C6F RID: 7279
		protected OracleLpCompoundExpressionBinaryOperator m_vBinaryOperator;

		// Token: 0x04001C70 RID: 7280
		protected OracleLpExpression m_vLeftOperand;

		// Token: 0x04001C71 RID: 7281
		protected OracleLpExpression m_vRightOperand;
	}
}
