using System;

namespace AutoMapper
{
	// Token: 0x02000026 RID: 38
	public interface IResolverConfigurationExpression<TSource> : IResolutionExpression<TSource>, IResolutionExpression
	{
		// Token: 0x0600011D RID: 285
		IResolutionExpression<TSource> ConstructedBy(Func<IValueResolver> constructor);
	}
}
