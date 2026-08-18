using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200025A RID: 602
	internal class MethodCallExpression4 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015D8 RID: 5592 RVA: 0x00048D33 File Offset: 0x00046F33
		public MethodCallExpression4(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00048D5A File Offset: 0x00046F5A
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
			case 3:
				return this._arg3;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060015DA RID: 5594 RVA: 0x00048D9A File Offset: 0x00046F9A
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00048D9D File Offset: 0x00046F9D
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00048DAC File Offset: 0x00046FAC
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3]);
			}
			return Expression.Call(base.Method, Expression.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3);
		}

		// Token: 0x04000A35 RID: 2613
		private object _arg0;

		// Token: 0x04000A36 RID: 2614
		private readonly Expression _arg1;

		// Token: 0x04000A37 RID: 2615
		private readonly Expression _arg2;

		// Token: 0x04000A38 RID: 2616
		private readonly Expression _arg3;
	}
}
