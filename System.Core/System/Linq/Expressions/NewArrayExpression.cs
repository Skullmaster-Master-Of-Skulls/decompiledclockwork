using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace System.Linq.Expressions
{
	// Token: 0x0200025E RID: 606
	[DebuggerTypeProxy(typeof(Expression.NewArrayExpressionProxy))]
	[__DynamicallyInvokable]
	public class NewArrayExpression : Expression
	{
		// Token: 0x060015EE RID: 5614 RVA: 0x00049075 File Offset: 0x00047275
		internal NewArrayExpression(Type type, ReadOnlyCollection<Expression> expressions)
		{
			this._expressions = expressions;
			this._type = type;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0004908B File Offset: 0x0004728B
		internal static NewArrayExpression Make(ExpressionType nodeType, Type type, ReadOnlyCollection<Expression> expressions)
		{
			if (nodeType == ExpressionType.NewArrayInit)
			{
				return new NewArrayInitExpression(type, expressions);
			}
			return new NewArrayBoundsExpression(type, expressions);
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060015F0 RID: 5616 RVA: 0x000490A1 File Offset: 0x000472A1
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._type;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060015F1 RID: 5617 RVA: 0x000490A9 File Offset: 0x000472A9
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Expressions
		{
			[__DynamicallyInvokable]
			get
			{
				return this._expressions;
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x000490B1 File Offset: 0x000472B1
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNewArray(this);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000490BA File Offset: 0x000472BA
		[__DynamicallyInvokable]
		public NewArrayExpression Update(IEnumerable<Expression> expressions)
		{
			if (expressions == this.Expressions)
			{
				return this;
			}
			if (this.NodeType == ExpressionType.NewArrayInit)
			{
				return Expression.NewArrayInit(this.Type.GetElementType(), expressions);
			}
			return Expression.NewArrayBounds(this.Type.GetElementType(), expressions);
		}

		// Token: 0x04000A45 RID: 2629
		private readonly ReadOnlyCollection<Expression> _expressions;

		// Token: 0x04000A46 RID: 2630
		private readonly Type _type;
	}
}
