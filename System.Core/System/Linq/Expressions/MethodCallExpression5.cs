using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace System.Linq.Expressions
{
	// Token: 0x0200025B RID: 603
	internal class MethodCallExpression5 : MethodCallExpression, IArgumentProvider
	{
		// Token: 0x060015DD RID: 5597 RVA: 0x00048E0C File Offset: 0x0004700C
		public MethodCallExpression5(MethodInfo method, Expression arg0, Expression arg1, Expression arg2, Expression arg3, Expression arg4) : base(method)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
			this._arg4 = arg4;
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00048E3C File Offset: 0x0004703C
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
			case 4:
				return this._arg4;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x00048E92 File Offset: 0x00047092
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x00048E95 File Offset: 0x00047095
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x00048EA4 File Offset: 0x000470A4
		internal override MethodCallExpression Rewrite(Expression instance, IList<Expression> args)
		{
			if (args != null)
			{
				return Expression.Call(base.Method, args[0], args[1], args[2], args[3], args[4]);
			}
			return Expression.Call(base.Method, Expression.ReturnObject<Expression>(this._arg0), this._arg1, this._arg2, this._arg3, this._arg4);
		}

		// Token: 0x04000A39 RID: 2617
		private object _arg0;

		// Token: 0x04000A3A RID: 2618
		private readonly Expression _arg1;

		// Token: 0x04000A3B RID: 2619
		private readonly Expression _arg2;

		// Token: 0x04000A3C RID: 2620
		private readonly Expression _arg3;

		// Token: 0x04000A3D RID: 2621
		private readonly Expression _arg4;
	}
}
