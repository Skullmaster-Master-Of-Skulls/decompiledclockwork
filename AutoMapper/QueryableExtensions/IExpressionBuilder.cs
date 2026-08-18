using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x02000054 RID: 84
	public interface IExpressionBuilder
	{
		// Token: 0x06000332 RID: 818
		Expression CreateMapExpression(Type sourceType, Type destinationType, IDictionary<string, object> parameters = null, params MemberInfo[] membersToExpand);

		// Token: 0x06000333 RID: 819
		Expression<Func<TSource, TDestination>> CreateMapExpression<TSource, TDestination>(IDictionary<string, object> parameters = null, params MemberInfo[] membersToExpand);

		// Token: 0x06000334 RID: 820
		LambdaExpression CreateMapExpression(ExpressionRequest request, ConcurrentDictionary<ExpressionRequest, int> typePairCount);

		// Token: 0x06000335 RID: 821
		Expression CreateMapExpression(ExpressionRequest request, Expression instanceParameter, ConcurrentDictionary<ExpressionRequest, int> typePairCount);
	}
}
