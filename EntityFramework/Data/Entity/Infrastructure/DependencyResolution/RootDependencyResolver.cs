using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Infrastructure.Pluralization;
using System.Data.Entity.Internal;
using System.Data.Entity.Migrations.History;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000165 RID: 357
	internal class RootDependencyResolver : IDbDependencyResolver
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x00039387 File Offset: 0x00037587
		public RootDependencyResolver() : this(new DefaultProviderServicesResolver(), new DatabaseInitializerResolver())
		{
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000393C4 File Offset: 0x000375C4
		[SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
		[SuppressMessage("Microsoft.Reliability", "CA2000: Dispose objects before losing scope")]
		public RootDependencyResolver(DefaultProviderServicesResolver defaultProviderServicesResolver, DatabaseInitializerResolver databaseInitializerResolver)
		{
			this._databaseInitializerResolver = databaseInitializerResolver;
			this._resolvers.Add(new TransactionContextInitializerResolver());
			this._resolvers.Add(this._databaseInitializerResolver);
			this._resolvers.Add(new DefaultExecutionStrategyResolver());
			this._resolvers.Add(new CachingDependencyResolver(defaultProviderServicesResolver));
			this._resolvers.Add(new CachingDependencyResolver(new DefaultProviderFactoryResolver()));
			this._resolvers.Add(new CachingDependencyResolver(new DefaultInvariantNameResolver()));
			this._resolvers.Add(new SingletonDependencyResolver<IDbConnectionFactory>(new SqlConnectionFactory()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<DbContext, IDbModelCacheKey>>(new Func<DbContext, IDbModelCacheKey>(new DefaultModelCacheKeyFactory().Create)));
			this._resolvers.Add(new SingletonDependencyResolver<IManifestTokenResolver>(new DefaultManifestTokenResolver()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<DbConnection, string, HistoryContext>>(HistoryContext.DefaultFactory));
			this._resolvers.Add(new SingletonDependencyResolver<IPluralizationService>(new EnglishPluralizationService()));
			this._resolvers.Add(new SingletonDependencyResolver<AttributeProvider>(new AttributeProvider()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<DbContext, Action<string>, DatabaseLogFormatter>>((DbContext c, Action<string> w) => new DatabaseLogFormatter(c, w)));
			this._resolvers.Add(new SingletonDependencyResolver<Func<TransactionHandler>>(() => new DefaultTransactionHandler(), (object k) => k is ExecutionStrategyKey));
			this._resolvers.Add(new SingletonDependencyResolver<IDbProviderFactoryResolver>(new DefaultDbProviderFactoryResolver()));
			this._resolvers.Add(new SingletonDependencyResolver<Func<IMetadataAnnotationSerializer>>(() => new ClrTypeAnnotationSerializer(), "ClrType"));
			this._resolvers.Add(new SingletonDependencyResolver<Func<IMetadataAnnotationSerializer>>(() => new IndexAnnotationSerializer(), "Index"));
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x000395E4 File Offset: 0x000377E4
		public DatabaseInitializerResolver DatabaseInitializerResolver
		{
			get
			{
				return this._databaseInitializerResolver;
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000395EC File Offset: 0x000377EC
		public virtual object GetService(Type type, object key)
		{
			object result;
			if ((result = this._defaultResolvers.GetService(type, key)) == null)
			{
				result = (this._defaultProviderResolvers.GetService(type, key) ?? this._resolvers.GetService(type, key));
			}
			return result;
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0003961D File Offset: 0x0003781D
		public virtual void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			this._defaultResolvers.Add(resolver);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0003962B File Offset: 0x0003782B
		public virtual void SetDefaultProviderServices(DbProviderServices provider, string invariantName)
		{
			this._defaultProviderResolvers.Add(new SingletonDependencyResolver<DbProviderServices>(provider, invariantName));
			this._defaultProviderResolvers.Add(provider);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0003964B File Offset: 0x0003784B
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return this._defaultResolvers.GetServices(type, key).Concat(this._resolvers.GetServices(type, key));
		}

		// Token: 0x0400032F RID: 815
		private readonly ResolverChain _defaultProviderResolvers = new ResolverChain();

		// Token: 0x04000330 RID: 816
		private readonly ResolverChain _defaultResolvers = new ResolverChain();

		// Token: 0x04000331 RID: 817
		private readonly ResolverChain _resolvers = new ResolverChain();

		// Token: 0x04000332 RID: 818
		private readonly DatabaseInitializerResolver _databaseInitializerResolver;
	}
}
