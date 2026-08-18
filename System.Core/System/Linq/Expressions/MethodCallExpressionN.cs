using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000255 RID: 597
	internal class MethodCallExpressionN : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015BE RID: 5566 RVA: 0x00048ADD File Offset: 0x00046CDD
		public MethodCallExpressionN(MethodInfo method, IList<Expression> args) : base(method)
		{
			this._arguments = args;
		}

		// Token: 0x060015BF RID: 5567 RVA: 0x00048AED File Offset: 0x00046CED
		Expression IArgumentProvider.GetArgument(int index)
		{
			return this._arguments[index];
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x00048AFB File Offset: 0x00046CFB
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return this._arguments.Count;
			}
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x00048B08 File Offset: 0x00046D08
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly<Expression>(ref this._arguments);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x00048B15 File Offset: 0x00046D15
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			return Expression.Call(base.Method, args ?? this._arguments);
		}

		// Token: 0x04000A2C RID: 2604
		private IList<Expression> _arguments;
	}
}
