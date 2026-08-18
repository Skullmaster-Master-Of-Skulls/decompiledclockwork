using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000236 RID: 566
	internal class DynamicExpression4 : DynamicExpression, IArgumentProvider
	{
		// Token: 0x060014B5 RID: 5301 RVA: 0x0004614C File Offset: 0x0004434C
		internal DynamicExpression4(Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3) : base(delegateType, binder)
		{
			this._arg0 = arg0;
			this._arg1 = arg1;
			this._arg2 = arg2;
			this._arg3 = arg3;
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x00046175 File Offset: 0x00044375
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

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000461B5 File Offset: 0x000443B5
		int IArgumentProvider.ArgumentCount
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x000461B8 File Offset: 0x000443B8
		internal override ReadOnlyCollection<Expression> GetOrMakeArguments()
		{
			return Expression.ReturnReadOnly(this, ref this._arg0);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x000461C6 File Offset: 0x000443C6
		internal override DynamicExpression Rewrite(Expression[] args)
		{
			return Expression.MakeDynamic(base.DelegateType, base.Binder, args[0], args[1], args[2], args[3]);
		}

		// Token: 0x0400099F RID: 2463
		private object _arg0;

		// Token: 0x040009A0 RID: 2464
		private readonly Expression _arg1;

		// Token: 0x040009A1 RID: 2465
		private readonly Expression _arg2;

		// Token: 0x040009A2 RID: 2466
		private readonly Expression _arg3;
	}
}
