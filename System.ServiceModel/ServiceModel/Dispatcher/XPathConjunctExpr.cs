using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000512 RID: 1298
	internal class XPathConjunctExpr : XPathExpr
	{
		// Token: 0x0600316B RID: 12651 RVA: 0x000BE1C3 File Offset: 0x000BC3C3
		internal XPathConjunctExpr(XPathExprType type, ValueDataType returnType, XPathExpr left, XPathExpr right) : base(type, returnType)
		{
			if (left == null || right == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new QueryCompileException(QueryCompileError.InvalidExpression));
			}
			base.SubExpr.Add(left);
			base.SubExpr.Add(right);
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600316C RID: 12652 RVA: 0x000BE1FF File Offset: 0x000BC3FF
		internal XPathExpr Left
		{
			get
			{
				return base.SubExpr[0];
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x000BE20D File Offset: 0x000BC40D
		internal XPathExpr Right
		{
			get
			{
				return base.SubExpr[1];
			}
		}
	}
}
