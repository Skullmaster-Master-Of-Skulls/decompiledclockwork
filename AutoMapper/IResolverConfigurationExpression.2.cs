using System;
using System.Linq.Expressions;

namespace AutoMapper
{
	// Token: 0x02000027 RID: 39
	public interface IResolverConfigurationExpression<TSource, TValueResolver> where TValueResolver : IValueResolver
	{
		// Token: 0x0600011E RID: 286
		IResolverConfigurationExpression<TSource, TValueResolver> FromMember(Expression<Func<TSource, object>> sourceMember);

		// Token: 0x0600011F RID: 287
		IResolverConfigurationExpression<TSource, TValueResolver> FromMember(string sourcePropertyName);

		// Token: 0x06000120 RID: 288
		IResolverConfigurationExpression<TSource, TValueResolver> ConstructedBy(Func<TValueResolver> constructor);
	}
}
