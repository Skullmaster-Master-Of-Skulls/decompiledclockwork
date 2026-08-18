using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000256 RID: 598
	internal class InstanceMethodCallExpressionN : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015C3 RID: 5571 RVA: 0x00048B2D File Offset: 0x00046D2D
		public InstanceMethodCallExpressionN(MethodInfo method, Expression instance, IList<Expression> args) : base(method)
		{
			this._instance = instance;
			this._arguments = args;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00048B44 File Offset: 0x00046D44
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x00048B52 File Offset: 0x00046D52
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x00048B5F File Offset: 0x00046D5F
		internal override Expression GetInstance()
		{
			return this._instance;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00048B67 File Offset: 0x00046D67
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00048B74 File Offset: 0x00046D74
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			return Expression.Call(instance, base.Method, args ?? this._arguments);
		}

		// Token: 0x04000A2D RID: 2605
		private IList<Expression> _arguments;

		// Token: 0x04000A2E RID: 2606
		private readonly Expression _instance;
	}
}
