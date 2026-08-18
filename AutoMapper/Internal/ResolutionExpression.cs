using System;
using System.Linq.Expressions;

namespace AutoMapper.Internal
{
	// Token: 0x020000BA RID: 186
	public class ResolutionExpression<TSource> : IResolverConfigurationExpression<TSource>, IResolutionExpression<TSource>, IResolutionExpression, IResolverConfigurationExpression
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x00014D82 File Offset: 0x00012F82
		public ResolutionExpression(Type sourceType, PropertyMap propertyMap)
		{
			this._sourceType = sourceType;
			this._propertyMap = propertyMap;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00014D98 File Offset: 0x00012F98
		public void FromMember(Expression<Func<TSource, object>> sourceMember)
		{
			MemberExpression memberExpression = sourceMember.Body as MemberExpression;
			if (memberExpression != null)
			{
				this._propertyMap.SourceMember = memberExpression.Member;
			}
			Func<TSource, object> func = sourceMember.Compile();
			this._propertyMap.ChainTypeMemberForResolver(new DelegateBasedResolver<TSource>((ResolutionResult r) => func((TSource)((object)r.Value))));
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00014DF3 File Offset: 0x00012FF3
		public void FromMember(string sourcePropertyName)
		{
			this._propertyMap.SourceMember = this._sourceType.GetMember(sourcePropertyName)[0];
			this._propertyMap.ChainTypeMemberForResolver(new PropertyNameResolver(this._sourceType, sourcePropertyName));
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00014E25 File Offset: 0x00013025
		IResolutionExpression IResolverConfigurationExpression.ConstructedBy(Func<IValueResolver> constructor)
		{
			return this.ConstructedBy(constructor);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00014E30 File Offset: 0x00013030
		public IResolutionExpression<TSource> ConstructedBy(Func<IValueResolver> constructor)
		{
			this._propertyMap.ChainConstructorForResolver(new DeferredInstantiatedResolver((ResolutionContext ctxt) => constructor()));
			return this;
		}

		// Token: 0x04000103 RID: 259
		private readonly Type _sourceType;

		// Token: 0x04000104 RID: 260
		private readonly PropertyMap _propertyMap;
	}
}
