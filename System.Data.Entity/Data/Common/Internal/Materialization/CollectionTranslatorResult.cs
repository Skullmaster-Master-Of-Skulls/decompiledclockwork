using System;
using System.Data.Query.InternalTrees;
using System.Linq.Expressions;

namespace System.Data.Common.Internal.Materialization
{
	// Token: 0x020003D6 RID: 982
	internal class CollectionTranslatorResult : TranslatorResult
	{
		// Token: 0x060034D8 RID: 13528 RVA: 0x000CBFEA File Offset: 0x000CA1EA
		internal CollectionTranslatorResult(Expression returnedExpression, ColumnMap columnMap, Type requestedType, Expression expressionToGetCoordinator) : base(returnedExpression, requestedType)
		{
			this.ExpressionToGetCoordinator = expressionToGetCoordinator;
		}

		// Token: 0x04001727 RID: 5927
		internal readonly Expression ExpressionToGetCoordinator;
	}
}
