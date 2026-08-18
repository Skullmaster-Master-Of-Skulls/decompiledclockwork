using System;
using System.Linq.Expressions;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x020002D7 RID: 727
	internal class CollectionTranslatorResult : TranslatorResult
	{
		// Token: 0x06001970 RID: 6512 RVA: 0x0007F0AA File Offset: 0x0007D2AA
		internal CollectionTranslatorResult(Expression returnedExpression, Type requestedType, Expression expressionToGetCoordinator) : base(returnedExpression, requestedType)
		{
			this.ExpressionToGetCoordinator = expressionToGetCoordinator;
		}

		// Token: 0x040008CC RID: 2252
		internal readonly Expression ExpressionToGetCoordinator;
	}
}
