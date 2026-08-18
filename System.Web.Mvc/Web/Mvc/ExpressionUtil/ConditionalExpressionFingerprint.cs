using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000AA RID: 170
	internal sealed class ConditionalExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x06000499 RID: 1177 RVA: 0x0000D562 File Offset: 0x0000B762
		public ConditionalExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
		{
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0000D56C File Offset: 0x0000B76C
		public override bool Equals(object obj)
		{
			ConditionalExpressionFingerprint conditionalExpressionFingerprint = obj as ConditionalExpressionFingerprint;
			return conditionalExpressionFingerprint != null && base.Equals(conditionalExpressionFingerprint);
		}
	}
}
