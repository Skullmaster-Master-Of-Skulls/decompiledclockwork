using System;
using System.Diagnostics;
using System.Dynamic.Utils;

namespace System.Linq.Expressions
{
	// Token: 0x0200026D RID: 621
	[DebuggerTypeProxy(typeof(Expression.TypeBinaryExpressionProxy))]
	[__DynamicallyInvokable]
	public sealed class TypeBinaryExpression : Expression
	{
		// Token: 0x0600163B RID: 5691 RVA: 0x00049666 File Offset: 0x00047866
		internal TypeBinaryExpression(Expression expression, Type typeOperand, ExpressionType nodeKind)
		{
			this._expression = expression;
			this._typeOperand = typeOperand;
			this._nodeKind = nodeKind;
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x0600163C RID: 5692 RVA: 0x00049683 File Offset: 0x00047883
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return typeof(bool);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x0600163D RID: 5693 RVA: 0x0004968F File Offset: 0x0004788F
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return this._nodeKind;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x0600163E RID: 5694 RVA: 0x00049697 File Offset: 0x00047897
		[__DynamicallyInvokable]
		public Expression Expression
		{
			[__DynamicallyInvokable]
			get
			{
				return this._expression;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x0004969F File Offset: 0x0004789F
		[__DynamicallyInvokable]
		public Type TypeOperand
		{
			[__DynamicallyInvokable]
			get
			{
				return this._typeOperand;
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000496A8 File Offset: 0x000478A8
		internal Expression ReduceTypeEqual()
		{
			Type type = this.Expression.Type;
			if (type.IsValueType && !type.IsNullableType())
			{
				return Expression.Block(this.Expression, Expression.Constant(type == this._typeOperand.GetNonNullableType()));
			}
			if (this.Expression.NodeType == ExpressionType.Constant)
			{
				return this.ReduceConstantTypeEqual();
			}
			if (type.IsSealed && type == this._typeOperand)
			{
				if (type.IsNullableType())
				{
					return Expression.NotEqual(this.Expression, Expression.Constant(null, this.Expression.Type));
				}
				return Expression.ReferenceNotEqual(this.Expression, Expression.Constant(null, this.Expression.Type));
			}
			else
			{
				ParameterExpression parameterExpression = this.Expression as ParameterExpression;
				if (parameterExpression != null && !parameterExpression.IsByRef)
				{
					return this.ByValParameterTypeEqual(parameterExpression);
				}
				parameterExpression = Expression.Parameter(typeof(object));
				Expression expression = this.Expression;
				if (!TypeUtils.AreReferenceAssignable(typeof(object), expression.Type))
				{
					expression = Expression.Convert(expression, typeof(object));
				}
				return Expression.Block(new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					Expression.Assign(parameterExpression, expression),
					this.ByValParameterTypeEqual(parameterExpression)
				});
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x000497F0 File Offset: 0x000479F0
		private Expression ByValParameterTypeEqual(ParameterExpression value)
		{
			Expression expression = Expression.Call(value, typeof(object).GetMethod("GetType"));
			if (this._typeOperand.IsInterface)
			{
				ParameterExpression parameterExpression = Expression.Parameter(typeof(Type));
				expression = Expression.Block(new ParameterExpression[]
				{
					parameterExpression
				}, new Expression[]
				{
					Expression.Assign(parameterExpression, expression),
					parameterExpression
				});
			}
			return Expression.AndAlso(Expression.ReferenceNotEqual(value, Expression.Constant(null)), Expression.ReferenceEqual(expression, Expression.Constant(this._typeOperand.GetNonNullableType(), typeof(Type))));
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0004988C File Offset: 0x00047A8C
		private Expression ReduceConstantTypeEqual()
		{
			ConstantExpression constantExpression = this.Expression as ConstantExpression;
			if (constantExpression.Value == null)
			{
				return Expression.Constant(false);
			}
			return Expression.Constant(this._typeOperand.GetNonNullableType() == constantExpression.Value.GetType());
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x000498DE File Offset: 0x00047ADE
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitTypeBinary(this);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x000498E7 File Offset: 0x00047AE7
		[__DynamicallyInvokable]
		public TypeBinaryExpression Update(Expression expression)
		{
			if (expression == this.Expression)
			{
				return this;
			}
			if (this.NodeType == ExpressionType.TypeIs)
			{
				return Expression.TypeIs(expression, this.TypeOperand);
			}
			return Expression.TypeEqual(expression, this.TypeOperand);
		}

		// Token: 0x04000A5E RID: 2654
		private readonly Expression _expression;

		// Token: 0x04000A5F RID: 2655
		private readonly Type _typeOperand;

		// Token: 0x04000A60 RID: 2656
		private readonly ExpressionType _nodeKind;
	}
}
