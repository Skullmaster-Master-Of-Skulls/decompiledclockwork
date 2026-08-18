using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000160 RID: 352
	internal class InternalConfiguration
	{
		// Token: 0x06000B61 RID: 2913 RVA: 0x00038DA0 File Offset: 0x00036FA0
		public InternalConfiguration(ResolverChain appConfigChain = null, ResolverChain normalResolverChain = null, RootDependencyResolver rootResolver = null, AppConfigDependencyResolver appConfigResolver = null, Func<DbDispatchers> dispatchers = null)
		{
			this._rootResolver = (rootResolver ?? new RootDependencyResolver());
			this._resolvers = new CompositeResolver<ResolverChain, ResolverChain>(appConfigChain ?? new ResolverChain(), normalResolverChain ?? new ResolverChain());
			this._resolvers.Second.Add(this._rootResolver);
			this._resolvers.First.Add(appConfigResolver ?? new AppConfigDependencyResolver(AppConfig.DefaultInstance, this, null));
			Func<DbDispatchers> dispatchers2 = dispatchers;
			if (dispatchers == null)
			{
				dispatchers2 = (() => DbInterception.Dispatch);
			}
			this._dispatchers = dispatchers2;
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000B62 RID: 2914 RVA: 0x00038E43 File Offset: 0x00037043
		// (set) Token: 0x06000B63 RID: 2915 RVA: 0x00038E4F File Offset: 0x0003704F
		public static InternalConfiguration Instance
		{
			get
			{
				return DbConfigurationManager.Instance.GetConfiguration();
			}
			set
			{
				DbConfigurationManager.Instance.SetConfiguration(value);
			}
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x00038E5C File Offset: 0x0003705C
		public virtual void Lock()
		{
			List<IDbInterceptor> list = this.DependencyResolver.GetServices<IDbInterceptor>().ToList<IDbInterceptor>();
			list.Each(new Action<IDbInterceptor>(this._dispatchers().AddInterceptor));
			DbConfigurationManager.Instance.OnLoaded(this);
			this._isLocked = true;
			this.DependencyResolver.GetServices<IDbInterceptor>().Except(list).Each(new Action<IDbInterceptor>(this._dispatchers().AddInterceptor));
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x00038ED6 File Offset: 0x000370D6
		public void DispatchLoadedInterceptors(DbConfigurationLoadedEventArgs loadedEventArgs)
		{
			this._dispatchers().Configuration.Loaded(loadedEventArgs, new DbInterceptionContext());
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x00038EF3 File Offset: 0x000370F3
		public virtual void AddAppConfigResolver(IDbDependencyResolver resolver)
		{
			this._resolvers.First.Add(resolver);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x00038F06 File Offset: 0x00037106
		public virtual void AddDependencyResolver(IDbDependencyResolver resolver, bool overrideConfigFile = false)
		{
			(overrideConfigFile ? this._resolvers.First : this._resolvers.Second).Add(resolver);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x00038F29 File Offset: 0x00037129
		public virtual void AddDefaultResolver(IDbDependencyResolver resolver)
		{
			this._rootResolver.AddDefaultResolver(resolver);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00038F37 File Offset: 0x00037137
		public virtual void SetDefaultProviderServices(DbProviderServices provider, string invariantName)
		{
			this._rootResolver.SetDefaultProviderServices(provider, invariantName);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x00038F46 File Offset: 0x00037146
		public virtual void RegisterSingleton<TService>(TService instance) where TService : class
		{
			this.AddDependencyResolver(new SingletonDependencyResolver<TService>(instance, null), false);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x00038F56 File Offset: 0x00037156
		public virtual void RegisterSingleton<TService>(TService instance, object key) where TService : class
		{
			this.AddDependencyResolver(new SingletonDependencyResolver<TService>(instance, key), false);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x00038F66 File Offset: 0x00037166
		public virtual void RegisterSingleton<TService>(TService instance, Func<object, bool> keyPredicate) where TService : class
		{
			this.AddDependencyResolver(new SingletonDependencyResolver<TService>(instance, keyPredicate), false);
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x00038F76 File Offset: 0x00037176
		public virtual TService GetService<TService>(object key)
		{
			return this._resolvers.GetService(key);
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x00038F84 File Offset: 0x00037184
		public virtual IDbDependencyResolver DependencyResolver
		{
			get
			{
				return this._resolvers;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000B6F RID: 2927 RVA: 0x00038F8C File Offset: 0x0003718C
		public virtual RootDependencyResolver RootResolver
		{
			get
			{
				return this._rootResolver;
			}
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x00038F94 File Offset: 0x00037194
		public virtual void SwitchInRootResolver(RootDependencyResolver value)
		{
			ResolverChain resolverChain = new ResolverChain();
			resolverChain.Add(value);
			this._resolvers.Second.Resolvers.Skip(1).Each(new Action<IDbDependencyResolver>(resolverChain.Add));
			this._rootResolver = value;
			this._resolvers = new CompositeResolver<ResolverChain, ResolverChain>(this._resolvers.First, resolverChain);
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00038FF4 File Offset: 0x000371F4
		public virtual IDbDependencyResolver ResolverSnapshot
		{
			get
			{
				ResolverChain resolverChain = new ResolverChain();
				this._resolvers.Second.Resolvers.Each(new Action<IDbDependencyResolver>(resolverChain.Add));
				this._resolvers.First.Resolvers.Each(new Action<IDbDependencyResolver>(resolverChain.Add));
				return resolverChain;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0003904C File Offset: 0x0003724C
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x00039054 File Offset: 0x00037254
		public virtual DbConfiguration Owner { get; set; }

		// Token: 0x06000B74 RID: 2932 RVA: 0x0003905D File Offset: 0x0003725D
		public virtual void CheckNotLocked(string memberName)
		{
			if (this._isLocked)
			{
				throw new InvalidOperationException(Strings.ConfigurationLocked(memberName));
			}
		}

		// Token: 0x04000322 RID: 802
		private CompositeResolver<ResolverChain, ResolverChain> _resolvers;

		// Token: 0x04000323 RID: 803
		private RootDependencyResolver _rootResolver;

		// Token: 0x04000324 RID: 804
		private readonly Func<DbDispatchers> _dispatchers;

		// Token: 0x04000325 RID: 805
		private bool _isLocked;
	}
}
