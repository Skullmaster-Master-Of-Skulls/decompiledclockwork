using System;
using System.Collections.Generic;

namespace Oracle.SqlAndPlsqlParser.LocalParsing
{
	// Token: 0x020002C3 RID: 707
	internal class OracleLpCompoundEvaluateExpression : OracleLpCompoundExpression
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001A34 RID: 6708 RVA: 0x0010A390 File Offset: 0x00108590
		// (set) Token: 0x06001A35 RID: 6709 RVA: 0x0010A398 File Offset: 0x00108598
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

		// Token: 0x06001A36 RID: 6710 RVA: 0x0010A3AC File Offset: 0x001085AC
		public OracleLpCompoundEvaluateExpression(OracleLpStatementElement parent) : base(parent)
		{
			this.m_vCompoundType = OracleLpCompoundExpressionType.EVALUATE;
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0010A3BC File Offset: 0x001085BC
		public override void EvaluateDatatype()
		{
			if (this.m_vOperand != null)
			{
				this.m_vOperand.EvaluateDatatype();
			}
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0010A3D4 File Offset: 0x001085D4
		public override IList<OracleLpExpression> GetAllTerminalExpressions()
		{
			if (this.m_vAllTerminalExpressions == null)
			{
				this.m_vAllTerminalExpressions = this.m_vOperand.GetAllTerminalExpressions();
			}
			return this.m_vAllTerminalExpressions;
		}

		// Token: 0x04001C72 RID: 7282
		protected OracleLpExpression m_vOperand;
	}
}
