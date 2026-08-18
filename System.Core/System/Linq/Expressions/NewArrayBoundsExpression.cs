using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x02000260 RID: 608
	internal sealed class NewArrayBoundsExpression : NewArrayExpression
	{
		// Token: 0x060015F6 RID: 5622 RVA: 0x00049102 File Offset: 0x00047302
		internal NewArrayBoundsExpression(Type type, ReadOnlyCollection<Expression> expressions) : base(type, expressions)
		{
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0004910C File Offset: 0x0004730C
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayBounds;
			}
		}
	}
}
