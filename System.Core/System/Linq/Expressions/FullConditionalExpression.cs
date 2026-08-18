using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000224 RID: 548
	internal class FullConditionalExpression : ConditionalExpression
	{
		// Token: 0x06001406 RID: 5126 RVA: 0x00043F43 File Offset: 0x00042143
		internal FullConditionalExpression(Expression test, Expression ifTrue, Expression ifFalse) : base(test, ifTrue)
		{
			this._false = ifFalse;
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00043F54 File Offset: 0x00042154
		internal override Expression GetFalse()
		{
			return this._false;
		}

		// Token: 0x0400097D RID: 2429
		private readonly Expression _false;
	}
}
