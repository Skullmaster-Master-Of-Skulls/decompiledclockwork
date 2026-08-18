using System;
using System.Collections.ObjectModel;

namespace System.Linq.Expressions
{
	// Token: 0x0200025F RID: 607
	internal sealed class NewArrayInitExpression : NewArrayExpression
	{
		// Token: 0x060015F4 RID: 5620 RVA: 0x000490F4 File Offset: 0x000472F4
		internal NewArrayInitExpression(Type type, ReadOnlyCollection<Expression> expressions) : base(type, expressions)
		{
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060015F5 RID: 5621 RVA: 0x000490FE File Offset: 0x000472FE
		public sealed override ExpressionType NodeType
		{
			get
			{
				return ExpressionType.NewArrayInit;
			}
		}
	}
}
