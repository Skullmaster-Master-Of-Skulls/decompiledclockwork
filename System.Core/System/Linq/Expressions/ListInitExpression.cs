using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000249 RID: 585
	[DebuggerTypeProxy(typeof(Expression.ListInitExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class ListInitExpression : Expression
	{
		// Token: 0x06001579 RID: 5497 RVA: 0x000485F6 File Offset: 0x000467F6
		internal ListInitExpression(NewExpression newExpression, ReadOnlyCollection<ElementInit> initializers)
		{
			this._newExpression = newExpression;
			this._initializers = initializers;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x0004860C File Offset: 0x0004680C
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.ListInit;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00048610 File Offset: 0x00046810
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._newExpression.Type;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x0004861D File Offset: 0x0004681D
		[__DynamicallyInvokable]
		public override bool CanReduce
		{
			[__DynamicallyInvokable]
			get
			{
				return true;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00048620 File Offset: 0x00046820
		[__DynamicallyInvokable]
		public NewExpression NewExpression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._newExpression;
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00048628 File Offset: 0x00046828
		[__DynamicallyInvokable]
		public ReadOnlyCollection<ElementInit> Initializers
		{
			[__DynamicallyInvokable]
			get
			{
				return this._initializers;
			}
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00048630 File Offset: 0x00046830
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitListInit(this);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00048639 File Offset: 0x00046839
		[__DynamicallyInvokable]
		public override Expression Reduce()
		{
			return MemberInitExpression.ReduceListInit(this._newExpression, this._initializers, true);
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x0004864D File Offset: 0x0004684D
		[__DynamicallyInvokable]
		public ListInitExpression Update(NewExpression newExpression, IEnumerable<ElementInit> initializers)
		{
			if (newExpression == this.NewExpression && initializers == this.Initializers)
			{
				return this;
			}
			return Expression.ListInit(newExpression, initializers);
		}

		// Token: 0x04000A18 RID: 2584
		private readonly NewExpression _newExpression;

		// Token: 0x04000A19 RID: 2585
		private readonly ReadOnlyCollection<ElementInit> _initializers;
	}
}
