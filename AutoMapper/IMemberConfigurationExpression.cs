using System;
using System.Linq.Expressions;

namespace AutoMapper
{
	// Token: 0x0200001C RID: 28
	public interface IMemberConfigurationExpression<TSource>
	{
		// Token: 0x060000CF RID: 207
		void NullSubstitute(object nullSubstitute);

		// Token: 0x060000D0 RID: 208
		IResolverConfigurationExpression<TSource, TValueResolver> ResolveUsing<TValueResolver>() where TValueResolver : IValueResolver;

		// Token: 0x060000D1 RID: 209
		IResolverConfigurationExpression<TSource> ResolveUsing(Type valueResolverType);

		// Token: 0x060000D2 RID: 210
		IResolutionExpression<TSource> ResolveUsing(IValueResolver valueResolver);

		// Token: 0x060000D3 RID: 211
		void ResolveUsing(Func<TSource, object> resolver);

		// Token: 0x060000D4 RID: 212
		void ResolveUsing(Func<ResolutionResult, object> resolver);

		// Token: 0x060000D5 RID: 213
		void ResolveUsing(Func<ResolutionResult, TSource, object> resolver);

		// Token: 0x060000D6 RID: 214
		void MapFrom<TMember>(Expression<Func<TSource, TMember>> sourceMember);

		// Token: 0x060000D7 RID: 215
		void MapFrom<TMember>(string property);

		// Token: 0x060000D8 RID: 216
		void Ignore();

		// Token: 0x060000D9 RID: 217
		void SetMappingOrder(int mappingOrder);

		// Token: 0x060000DA RID: 218
		void UseDestinationValue();

		// Token: 0x060000DB RID: 219
		void DoNotUseDestinationValue();

		// Token: 0x060000DC RID: 220
		void UseValue<TValue>(TValue value);

		// Token: 0x060000DD RID: 221
		void UseValue(object value);

		// Token: 0x060000DE RID: 222
		void Condition(Func<TSource, bool> condition);

		// Token: 0x060000DF RID: 223
		void Condition(Func<ResolutionContext, bool> condition);

		// Token: 0x060000E0 RID: 224
		void PreCondition(Func<TSource, bool> condition);

		// Token: 0x060000E1 RID: 225
		void PreCondition(Func<ResolutionContext, bool> condition);

		// Token: 0x060000E2 RID: 226
		void ExplicitExpansion();
	}
}
