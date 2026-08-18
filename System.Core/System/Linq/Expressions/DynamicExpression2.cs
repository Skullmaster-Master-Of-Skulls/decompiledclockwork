using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000232 RID: 562
	internal class DynamicExpression2 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x060014A7 RID: 5287 RVA: 0x00046027 File Offset: 0x00044227
		internal DynamicExpression2(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1) : base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x00046040 File Offset: 0x00044240
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

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060014A9 RID: 5289 RVA: 0x00046063 File Offset: 0x00044263
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00046066 File Offset: 0x00044266
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00046074 File Offset: 0x00044274
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return Expression.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1]);
		}

		// Token: 0x04000998 RID: 2456
		private object _arg0;

		// Token: 0x04000999 RID: 2457
		private readonly Expression _arg1;
	}
}
