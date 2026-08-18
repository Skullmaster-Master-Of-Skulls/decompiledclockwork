using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000234 RID: 564
	internal class DynamicExpression3 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x060014AE RID: 5294 RVA: 0x000460AA File Offset: 0x000442AA
		internal DynamicExpression3(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2) : base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000460CB File Offset: 0x000442CB
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

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060014B0 RID: 5296 RVA: 0x00046100 File Offset: 0x00044300
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x00046103 File Offset: 0x00044303
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x00046111 File Offset: 0x00044311
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return Expression.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1], args[2]);
		}

		// Token: 0x0400099B RID: 2459
		private object _arg0;

		// Token: 0x0400099C RID: 2460
		private readonly Expression _arg1;

		// Token: 0x0400099D RID: 2461
		private readonly Expression _arg2;
	}
}
