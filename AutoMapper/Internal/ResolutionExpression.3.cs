using System;
using System.Linq.Expressions;

namespace AutoMapper.Internal
{
	// Token: 0x020000BC RID: 188
	public class ResolutionExpression<TSource, TValueResolver> : IResolverConfigurationExpression<TSource, TValueResolver> where TValueResolver : IValueResolver
	{
		// Token: 0x06000580 RID: 1408 RVA: 0x00014E71 File Offset: 0x00013071
		public ResolutionExpression(Type sourceType, PropertyMap propertyMap)
		{
			this._sourceType = sourceType;
			this._propertyMap = propertyMap;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00014E88 File Offset: 0x00013088
		public IResolverConfigurationExpression<TSource, TValueResolver> FromMember(Expression<Func<TSource, object>> sourceMember)
		{
			MemberExpression memberExpression = sourceMember.Body as MemberExpression;
			if (memberExpression != null)
			{
				this._propertyMap.SourceMember = memberExpression.Member;
			}
			Func<TSource, object> func = sourceMember.Compile();
			this._propertyMap.ChainTypeMemberForResolver(new DelegateBasedResolver<TSource>((ResolutionResult r) => func((TSource)((object)r.Value))));
			return this;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00014EE4 File Offset: 0x000130E4
		public IResolverConfigurationExpression<TSource, TValueResolver> FromMember(string sourcePropertyName)
		{
			this._propertyMap.SourceMember = this._sourceType.GetMember(sourcePropertyName)[0];
			this._propertyMap.ChainTypeMemberForResolver(new PropertyNameResolver(this._sourceType, sourcePropertyName));
			return this;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00014F18 File Offset: 0x00013118
		public IResolverConfigurationExpression<TSource, TValueResolver> ConstructedBy(Func<TValueResolver> constructor)
		{
			this._propertyMap.ChainConstructorForResolver(new DeferredInstantiatedResolver((ResolutionContext ctxt) => constructor()));
			return this;
		}

		// Token: 0x04000105 RID: 261
		private readonly PropertyMap _propertyMap;

		// Token: 0x04000106 RID: 262
		private readonly Type _sourceType;
	}
}
