using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000235 RID: 565
	internal sealed class TypedDynamicExpression3 : DynamicExpression3
	{
		// Token: 0x060014B3 RID: 5299 RVA: 0x0004612D File Offset: 0x0004432D
		internal TypedDynamicExpression3(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2) : base(delegateType, binder, arg0, arg1, arg2)
		{
			this._retType = retType;
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x00046144 File Offset: 0x00044344
		public sealed override Type Type
		{
			get
			{
				return this._retType;
			}
		}

		// Token: 0x0400099E RID: 2462
		private readonly Type _retType;
	}
}
