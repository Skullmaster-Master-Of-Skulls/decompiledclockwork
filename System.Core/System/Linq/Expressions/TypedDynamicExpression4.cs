using System;
using System.Runtime.CompilerServices;

namespace System.Linq.Expressions
{
	// Token: 0x02000237 RID: 567
	internal sealed class TypedDynamicExpression4 : DynamicExpression4
	{
		// Token: 0x060014BA RID: 5306 RVA: 0x000461E5 File Offset: 0x000443E5
		internal TypedDynamicExpression4(Type retType, Type delegateType, CallSiteBinder binder, Expression arg0, Expression arg1, Expression arg2, Expression arg3) : base(delegateType, binder, arg0, arg1, arg2, arg3)
		{
			this._retType = retType;
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x000461FE File Offset: 0x000443FE
		public sealed override Type Type
		{
			get
			{
				return this._retType;
			}
		}

		// Token: 0x040009A3 RID: 2467
		private readonly Type _retType;
	}
}
