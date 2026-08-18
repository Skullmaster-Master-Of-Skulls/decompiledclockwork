using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x02000243 RID: 579
	[DebuggerTypeProxy(typeof(Expression.InvocationExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class InvocationExpression : Expression, IArgumentProvider
	{
		// Token: 0x0600153D RID: 5437 RVA: 0x000481DA File Offset: 0x000463DA
		internal InvocationExpression(Expression lambda, IList<Expression> arguments, Type returnType)
		{
			this._lambda = lambda;
			this._arguments = arguments;
			this._returnType = returnType;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x000481F7 File Offset: 0x000463F7
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x000481FF File Offset: 0x000463FF
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Invoke;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00048203 File Offset: 0x00046403
		[__DynamicallyInvokable]
		public Expression Expression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0004820B File Offset: 0x0004640B
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Arguments
		{
			[__DynamicallyInvokable]
			get
			{
				return Expression.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00048218 File Offset: 0x00046418
		[__DynamicallyInvokable]
		public InvocationExpression Update(Expression expression, IEnumerable<Expression> arguments)
		{
			if (expression == this.Expression && arguments == this.Arguments)
			{
				return this;
			}
			return Expression.Invoke(expression, arguments);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00048235 File Offset: 0x00046435
		[__DynamicallyInvokable]
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x00048243 File Offset: 0x00046443
		[__DynamicallyInvokable]
		int IArgumentProvider.ArgumentCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00048250 File Offset: 0x00046450
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitInvocation(this);
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x0004825C File Offset: 0x0004645C
		internal InvocationExpression Rewrite(Expression lambda, Expression[] arguments)
		{
			return Expression.Invoke(lambda, arguments ?? this._arguments);
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x0004827C File Offset: 0x0004647C
		internal LambdaExpression LambdaOperand
		{
			get
			{
				if (this._lambda.NodeType != ExpressionType.Quote)
				{
					return this._lambda as LambdaExpression;
				}
				return (LambdaExpression)((UnaryExpression)this._lambda).Operand;
			}
		}

		// Token: 0x04000A0A RID: 2570
		private IList<Expression> _arguments;

		// Token: 0x04000A0B RID: 2571
		private readonly Expression _lambda;

		// Token: 0x04000A0C RID: 2572
		private readonly Type _returnType;
	}
}
