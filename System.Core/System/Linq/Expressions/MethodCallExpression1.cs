using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000257 RID: 599
	internal class MethodCallExpression1 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015C9 RID: 5577 RVA: 0x00048B8D File Offset: 0x00046D8D
		public MethodCallExpression1(MethodInfo method, Expression arg0) : base(method)
		{
			this._arg0 = arg0;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00048B9D File Offset: 0x00046D9D
		Expression IArgumentProvider.GetArgument(int index)
		{
			if (index == 0)
			{
				return Expression.ReturnObject<Expression>(this._arg0);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x00048BB3 File Offset: 0x00046DB3
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00048BB6 File Offset: 0x00046DB6
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00048BC4 File Offset: 0x00046DC4
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0]);
			}
			return Expression.Call(base.Method, Expression.ReturnObject<Expression>(this._arg0));
		}

		// Token: 0x04000A2F RID: 2607
		private object _arg0;
	}
}
