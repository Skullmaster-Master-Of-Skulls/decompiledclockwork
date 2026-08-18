using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x0200022F RID: 559
	internal class TypedDynamicExpressionN : DynamicExpressionN
	{
		// Token: 0x0600149E RID: 5278 RVA: 0x00045FA3 File Offset: 0x000441A3
		internal TypedDynamicExpressionN(Type returnType, Type delegateType, CallSiteBinder binder, IList<Expression> arguments) : base(delegateType, binder, arguments)
		{
			this._returnType = returnType;
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600149F RID: 5279 RVA: 0x00045FB6 File Offset: 0x000441B6
		public sealed override Type Type
		{
			get
			{
				return this._returnType;
			}
		}

		// Token: 0x04000995 RID: 2453
		private readonly Type _returnType;
	}
}
