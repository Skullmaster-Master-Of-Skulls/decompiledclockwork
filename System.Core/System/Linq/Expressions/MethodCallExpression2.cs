using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000258 RID: 600
	internal class MethodCallExpression2 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015CE RID: 5582 RVA: 0x00048BF2 File Offset: 0x00046DF2
		public MethodCallExpression2(MethodInfo method, Expression arg0, Expression arg1) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00048C09 File Offset: 0x00046E09
		Expression IArgumentProvider.GetArgument(int index)
		{
			if (index == 0)
			{
				return Expression.ReturnObject<Expression>(this._arg0);
			}
			if (index != 1)
			{
				throw new InvalidOperationException();
			}
			return this._arg1;
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x00048C2C File Offset: 0x00046E2C
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00048C2F File Offset: 0x00046E2F
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00048C3D File Offset: 0x00046E3D
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1]);
			}
			return Expression.Call(base.Method, Expression.ReturnObject<Expression>(this._arg0), this._arg1);
		}

		// Token: 0x04000A30 RID: 2608
		private object _arg0;

		// Token: 0x04000A31 RID: 2609
		private readonly Expression _arg1;
	}
}
