using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000261 RID: 609
	[DebuggerTypeProxy(typeof(Expression.NewExpressionProxy))]
	[__DynamicallyInvokable]
	public class NewExpression : Expression, IArgumentProvider
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x00049110 File Offset: 0x00047310
		internal NewExpression(ConstructorInfo constructor, IList<Expression> arguments, ReadOnlyCollection<MemberInfo> members)
		{
			this._constructor = constructor;
			this._arguments = arguments;
			this._members = members;
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060015F9 RID: 5625 RVA: 0x0004912D File Offset: 0x0004732D
		[__DynamicallyInvokable]
		public override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._constructor.DeclaringType;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x0004913A File Offset: 0x0004733A
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.New;
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0004913E File Offset: 0x0004733E
		[__DynamicallyInvokable]
		public ConstructorInfo Constructor
		{
			[__DynamicallyInvokable]
			get
			{
				return this._constructor;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x00049146 File Offset: 0x00047346
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Arguments
		{
			[__DynamicallyInvokable]
			get
			{
				return Expression.ReturnReadOnly<Expression>(ref this._arguments);
			}
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x00049153 File Offset: 0x00047353
		[__DynamicallyInvokable]
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00049161 File Offset: 0x00047361
		[__DynamicallyInvokable]
		int IArgumentProvider.ArgumentCount
		{
			[__DynamicallyInvokable]
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0004916E File Offset: 0x0004736E
		[__DynamicallyInvokable]
		public ReadOnlyCollection<MemberInfo> Members
		{
			[__DynamicallyInvokable]
			get
			{
				return this._members;
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x00049176 File Offset: 0x00047376
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitNew(this);
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0004917F File Offset: 0x0004737F
		[__DynamicallyInvokable]
		public NewExpression Update(IEnumerable<Expression> arguments)
		{
			if (arguments == this.Arguments)
			{
				return this;
			}
			if (this.Members != null)
			{
				return Expression.New(this.Constructor, arguments, this.Members);
			}
			return Expression.New(this.Constructor, arguments);
		}

		// Token: 0x04000A47 RID: 2631
		private readonly ConstructorInfo _constructor;

		// Token: 0x04000A48 RID: 2632
		private IList<Expression> _arguments;

		// Token: 0x04000A49 RID: 2633
		private readonly ReadOnlyCollection<MemberInfo> _members;
	}
}
