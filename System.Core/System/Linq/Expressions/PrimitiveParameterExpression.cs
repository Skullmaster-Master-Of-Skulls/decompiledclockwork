using System;

namespace System.Linq.Expressions
{
	// Token: 0x02000266 RID: 614
	internal sealed class PrimitiveParameterExpression<T> : ParameterExpression
	{
		// Token: 0x06001610 RID: 5648 RVA: 0x0004936E File Offset: 0x0004756E
		internal PrimitiveParameterExpression(string name) : base(name)
		{
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x00049377 File Offset: 0x00047577
		public sealed override Type Type
		{
			get
			{
				return typeof(T);
			}
		}
	}
}
