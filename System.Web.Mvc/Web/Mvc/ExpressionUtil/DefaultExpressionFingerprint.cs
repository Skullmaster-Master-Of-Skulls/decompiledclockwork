using System;
using System.Linq.Expressions;

namespace System.Web.Mvc.ExpressionUtil
{
	// Token: 0x020000AC RID: 172
	internal sealed class DefaultExpressionFingerprint : ExpressionFingerprint
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0000D5B8 File Offset: 0x0000B7B8
		public DefaultExpressionFingerprint(ExpressionType nodeType, Type type) : base(nodeType, type)
		{
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0000D5C4 File Offset: 0x0000B7C4
		public override bool Equals(object obj)
		{
			DefaultExpressionFingerprint defaultExpressionFingerprint = obj as DefaultExpressionFingerprint;
			return defaultExpressionFingerprint != null && base.Equals(defaultExpressionFingerprint);
		}
	}
}
