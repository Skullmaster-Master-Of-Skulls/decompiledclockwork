using System;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x0200022C RID: 556
	[DebuggerTypeProxy(typeof(Expression.DefaultExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class DefaultExpression : Expression
	{
		// Token: 0x06001476 RID: 5238 RVA: 0x00045D34 File Offset: 0x00043F34
		internal DefaultExpression(Type type)
		{
			this._type = type;
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00045D43 File Offset: 0x00043F43
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00045D4B File Offset: 0x00043F4B
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Default;
			}
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00045D4F File Offset: 0x00043F4F
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitDefault(this);
		}

		// Token: 0x04000991 RID: 2449
		private readonly Type _type;
	}
}
