using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000231 RID: 561
	internal sealed class TypedDynamicExpression1 : DynamicExpression1
	{
		// Token: 0x060014A5 RID: 5285 RVA: 0x0004600C File Offset: 0x0004420C
		internal TypedDynamicExpression1(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0) : base(delegateType, binder, arg0)
		{
			this._retType = retType;
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060014A6 RID: 5286 RVA: 0x0004601F File Offset: 0x0004421F
		public sealed override Type Type
		{
			get
			{
				return this._retType;
			}
		}

		// Token: 0x04000997 RID: 2455
		private readonly Type _retType;
	}
}
