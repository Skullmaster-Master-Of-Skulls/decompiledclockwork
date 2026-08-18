using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x02000259 RID: 601
	internal class MethodCallExpression3 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015D3 RID: 5587 RVA: 0x00048C78 File Offset: 0x00046E78
		public MethodCallExpression3(MethodInfo method, Expression arg0, Expression arg1, Expression arg2) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00048C97 File Offset: 0x00046E97
		Expression IArgumentProvider.GetArgument(int index)
		{
			switch (index)
			{
			case 0:
				return Expression.ReturnObject<Expression>(this._arg0);
			case 1:
				return this._arg1;
			case 2:
				return this._arg2;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060015D5 RID: 5589 RVA: 0x00048CCC File Offset: 0x00046ECC
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00048CCF File Offset: 0x00046ECF
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00048CE0 File Offset: 0x00046EE0
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2]);
			}
			return Expression.Call(base.Method, Expression.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2);
		}

		// Token: 0x04000A32 RID: 2610
		private object _arg0;

		// Token: 0x04000A33 RID: 2611
		private readonly Expression _arg1;

		// Token: 0x04000A34 RID: 2612
		private readonly Expression _arg2;
	}
}
