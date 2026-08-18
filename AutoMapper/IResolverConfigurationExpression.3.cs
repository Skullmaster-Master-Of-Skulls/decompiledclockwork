using System;

namespace AutoMapper
{
	// Token: 0x02000028 RID: 40
	public interface IResolverConfigurationExpression : IResolutionExpression
	{
		// Token: 0x06000121 RID: 289
		IResolutionExpression ConstructedBy(Func<IValueResolver> constructor);
	}
}
