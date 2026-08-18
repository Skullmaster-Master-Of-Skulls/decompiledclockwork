using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000230 RID: 560
	internal class DynamicExpression1 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x060014A0 RID: 5280 RVA: 0x00045FBE File Offset: 0x000441BE
		internal DynamicExpression1(Type delegateType, CallSiteBinder binder, Expression arg0) : base(delegateType, binder)
		{
			this._arg0 = arg0;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00045FCF File Offset: 0x000441CF
		Expression IArgumentProvider.GetArgument(int index)
		{
			if (index == 0)
			{
				return Expression.ReturnObject<Expression>(this._arg0);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00045FE5 File Offset: 0x000441E5
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00045FE8 File Offset: 0x000441E8
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x00045FF6 File Offset: 0x000441F6
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return Expression.MakeDynamic(base.DelegateType, base.Binder, args[0]);
		}

		// Token: 0x04000996 RID: 2454
		private object _arg0;
	}
}
