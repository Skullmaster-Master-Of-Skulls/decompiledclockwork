using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C1 RID: 705
	internal class OracleLpCompoundUnaryExpression : OracleLpCompoundExpression
	{
		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001A24 RID: 6692 RVA: 0x0010A268 File Offset: 0x00108468
		// (set) Token: 0x06001A25 RID: 6693 RVA: 0x0010A270 File Offset: 0x00108470
		public OracleLpCompoundExpressionUnaryOperator UnaryOperator
		{
			get
			{
				return this.m_vUnaryOperator;
			}
			set
			{
				this.m_vUnaryOperator = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0010A27C File Offset: 0x0010847C
		// (set) Token: 0x06001A27 RID: 6695 RVA: 0x0010A284 File Offset: 0x00108484
		public OracleLpExpression Operand
		{
			get
			{
				return this.m_vOperand;
			}
			set
			{
				this.m_vOperand = value;
				if (value != null)
				{
					value.ParentExpression = this;
				}
			}
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x0010A298 File Offset: 0x00108498
		public OracleLpCompoundUnaryExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vCompoundType = OracleLpCompoundExpressionType.UNARY;
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0010A2A8 File Offset: 0x001084A8
		public override void EvaluateDatatype()
		{
			if (this.m_vOperand != null)
			{
				this.m_vOperand.EvaluateDatatype();
			}
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x0010A2C0 File Offset: 0x001084C0
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = this.m_vOperand.GetAllTerminalExpressions();
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x04001C6D RID: 7277
		protected OracleLpCompoundExpressionUnaryOperator m_vUnaryOperator;

		// Token: 0x04001C6E RID: 7278
		protected OracleLpExpression m_vOperand;
	}
}
