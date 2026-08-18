using System;

namespace System.Linq.Expressions
{
	// Token: 0x0200023C RID: 572
	[__DynamicallyInvokable]
	public abstract class DynamicExpressionVisitor : ExpressionVisitor
	{
		// Token: 0x06001522 RID: 5410 RVA: 0x00048030 File Offset: 0x00046230
		[__DynamicallyInvokable]
		protected DynamicExpressionVisitor()
		{
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00048038 File Offset: 0x00046238
		[__DynamicallyInvokable]
		protected internal override Expression VisitDynamic(DynamicExpression node)
		{
			return base.VisitDynamic(node);
		}
	}
}
