using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000233 RID: 563
	internal sealed class TypedDynamicExpression2 : DynamicExpression2
	{
		// Token: 0x060014AC RID: 5292 RVA: 0x0004608D File Offset: 0x0004428D
		internal TypedDynamicExpression2(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1) : base(delegateType, binder, arg0, arg1)
		{
			this._retType = retType;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060014AD RID: 5293 RVA: 0x000460A2 File Offset: 0x000442A2
		public sealed override Type Type
		{
			get
			{
				return this._retType;
			}
		}

		// Token: 0x0400099A RID: 2458
		private readonly Type _retType;
	}
}
