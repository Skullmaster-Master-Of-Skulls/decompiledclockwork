using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000225 RID: 549
	internal class FullConditionalExpressionWithType : FullConditionalExpression
	{
		// Token: 0x06001408 RID: 5128 RVA: 0x00043F5C File Offset: 0x0004215C
		internal FullConditionalExpressionWithType(Expression test, Expression ifTrue, Expression ifFalse, Type type) : base(test, ifTrue, ifFalse)
		{
			this._type = type;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x00043F6F File Offset: 0x0004216F
		public sealed override Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0400097E RID: 2430
		private readonly Type _type;
	}
}
