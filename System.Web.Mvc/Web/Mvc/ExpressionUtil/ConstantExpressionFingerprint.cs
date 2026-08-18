using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000AB RID: 171
	internal sealed class ConstantExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x0600049B RID: 1179 RVA: 0x0000D58C File Offset: 0x0000B78C
		public ConstantExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
		{
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0000D598 File Offset: 0x0000B798
		public override bool Equals(object obj)
		{
			ConstantExpressionFingerprint constantExpressionFingerprint = obj as ConstantExpressionFingerprint;
			return constantExpressionFingerprint != null && base.Equals(constantExpressionFingerprint);
		}
	}
}
