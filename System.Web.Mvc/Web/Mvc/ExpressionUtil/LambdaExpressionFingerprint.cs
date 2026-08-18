using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000B3 RID: 179
	internal sealed class LambdaExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x060004DC RID: 1244 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		public LambdaExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
		{
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0000DCB8 File Offset: 0x0000BEB8
		public override bool Equals(object obj)
		{
			LambdaExpressionFingerprint lambdaExpressionFingerprint = obj as LambdaExpressionFingerprint;
			return lambdaExpressionFingerprint != null && base.Equals(lambdaExpressionFingerprint);
		}
	}
}
