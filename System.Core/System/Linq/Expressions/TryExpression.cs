using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x0200026C RID: 620
	[DebuggerTypeProxy(typeof(Expression.TryExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class TryExpression : Expression
	{
		// Token: 0x06001632 RID: 5682 RVA: 0x000495CB File Offset: 0x000477CB
		internal TryExpression(Type type, Expression body, Expression @finally, Expression fault, ReadOnlyCollection<CatchBlock> handlers)
		{
			this._type = type;
			this._body = body;
			this._handlers = handlers;
			this._finally = @finally;
			this._fault = fault;
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06001633 RID: 5683 RVA: 0x000495F8 File Offset: 0x000477F8
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06001634 RID: 5684 RVA: 0x00049600 File Offset: 0x00047800
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Try;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06001635 RID: 5685 RVA: 0x00049604 File Offset: 0x00047804
		[__DynamicallyInvokable]
		public Expression Body
		{
			[__DynamicallyInvokable]
			get
			{
				return this._body;
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x0004960C File Offset: 0x0004780C
		[__DynamicallyInvokable]
		public ReadOnlyCollection<CatchBlock> Handlers
		{
			[__DynamicallyInvokable]
			get
			{
				return this._handlers;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001637 RID: 5687 RVA: 0x00049614 File Offset: 0x00047814
		[__DynamicallyInvokable]
		public Expression Finally
		{
			[__DynamicallyInvokable]
			get
			{
				return this._finally;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x0004961C File Offset: 0x0004781C
		[__DynamicallyInvokable]
		public Expression Fault
		{
			[__DynamicallyInvokable]
			get
			{
				return this._fault;
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x00049624 File Offset: 0x00047824
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTry(this);
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0004962D File Offset: 0x0004782D
		[__DynamicallyInvokable]
		public TryExpression Update(Expression body, IEnumerable<CatchBlock> handlers, Expression @finally, Expression fault)
		{
			if (body == this.Body && handlers == this.Handlers && @finally == this.Finally && fault == this.Fault)
			{
				return this;
			}
			return Expression.MakeTry(this.Type, body, @finally, fault, handlers);
		}

		// Token: 0x04000A59 RID: 2649
		private readonly Type _type;

		// Token: 0x04000A5A RID: 2650
		private readonly Expression _body;

		// Token: 0x04000A5B RID: 2651
		private readonly ReadOnlyCollection<CatchBlock> _handlers;

		// Token: 0x04000A5C RID: 2652
		private readonly Expression _finally;

		// Token: 0x04000A5D RID: 2653
		private readonly Expression _fault;
	}
}
