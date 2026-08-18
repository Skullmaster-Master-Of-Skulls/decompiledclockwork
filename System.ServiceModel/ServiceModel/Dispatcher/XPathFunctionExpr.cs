using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000515 RID: 1301
	internal class XPathFunctionExpr : XPathExpr
	{
		// Token: 0x06003173 RID: 12659 RVA: 0x000BE25B File Offset: 0x000BC45B
		internal XPathFunctionExpr(QueryFunction function, XPathExprList subExpr) : base(XPathExprType.Function, function.ReturnType, subExpr)
		{
			this.function = function;
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06003174 RID: 12660 RVA: 0x000BE273 File Offset: 0x000BC473
		internal QueryFunction Function
		{
			get
			{
				return this.function;
			}
		}

		// Token: 0x04002662 RID: 9826
		private QueryFunction function;
	}
}
