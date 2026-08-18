using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000514 RID: 1300
	internal class XPathMathExpr : XPathConjunctExpr
	{
		// Token: 0x06003171 RID: 12657 RVA: 0x000BE23F File Offset: 0x000BC43F
		internal XPathMathExpr(MathOperator op, XPathExpr left, XPathExpr right) : base(XPathExprType.Math, ValueDataType.Double, left, right)
		{
			this.op = op;
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06003172 RID: 12658 RVA: 0x000BE253 File Offset: 0x000BC453
		internal MathOperator Op
		{
			get
			{
				return this.op;
			}
		}

		// Token: 0x04002661 RID: 9825
		private MathOperator op;
	}
}
