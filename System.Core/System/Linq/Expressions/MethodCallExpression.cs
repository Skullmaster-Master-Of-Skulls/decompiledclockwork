using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Dynamic.Utils;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000254 RID: 596
	[DebuggerTypeProxy(typeof(Expression.MethodCallExpressionProxy))]
	[__DynamicallyInvokable]
	public class MethodCallExpression : Expression, IArgumentProvider
	{
		// Token: 0x060015B1 RID: 5553 RVA: 0x00048A5B File Offset: 0x00046C5B
		internal MethodCallExpression(MethodInfo method)
		{
			this._method = method;
		}

		// Token: 0x060015B2 RID: 5554 RVA: 0x00048A6A File Offset: 0x00046C6A
		internal virtual Expression GetInstance()
		{
			return null;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060015B3 RID: 5555 RVA: 0x00048A6D File Offset: 0x00046C6D
		[__DynamicallyInvokable]
		public sealed override ExpressionType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return ExpressionType.Call;
			}
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x00048A70 File Offset: 0x00046C70
		[__DynamicallyInvokable]
		public sealed override Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this._method.ReturnType;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060015B5 RID: 5557 RVA: 0x00048A7D File Offset: 0x00046C7D
		[__DynamicallyInvokable]
		public MethodInfo Method
		{
			[__DynamicallyInvokable]
			get
			{
				return this._method;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00048A85 File Offset: 0x00046C85
		[__DynamicallyInvokable]
		public Expression Object
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetInstance();
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060015B7 RID: 5559 RVA: 0x00048A8D File Offset: 0x00046C8D
		[__DynamicallyInvokable]
		public ReadOnlyCollection<Expression> Arguments
		{
			[__DynamicallyInvokable]
			get
			{
				return this.GetOrMakeArguments();
			}
		}

		// Token: 0x060015B8 RID: 5560 RVA: 0x00048A95 File Offset: 0x00046C95
		[__DynamicallyInvokable]
		public MethodCallExpression Update(Expression @object, IEnumerable<Expression> arguments)
		{
			if (@object == this.Object && arguments == this.Arguments)
			{
				return this;
			}
			return Expression.Call(@object, this.Method, arguments);
		}

		// Token: 0x060015B9 RID: 5561 RVA: 0x00048AB8 File Offset: 0x00046CB8
		internal virtual ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060015BA RID: 5562 RVA: 0x00048ABF File Offset: 0x00046CBF
		[__DynamicallyInvokable]
		protected internal override Expression Accept(ExpressionVisitor visitor)
		{
			return visitor.VisitMethodCall(this);
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00048AC8 File Offset: 0x00046CC8
		internal virtual MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00048ACF File Offset: 0x00046CCF
		[__DynamicallyInvokable]
		Expression IArgumentProvider.GetArgument(int index)
		{
			throw ContractUtils.Unreachable;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x00048AD6 File Offset: 0x00046CD6
		[__DynamicallyInvokable]
		int IArgumentProvider.ArgumentCount
		{
			[__DynamicallyInvokable]
			get
			{
				throw ContractUtils.Unreachable;
			}
		}

		// Token: 0x04000A2B RID: 2603
		private readonly MethodInfo _method;
	}
}
