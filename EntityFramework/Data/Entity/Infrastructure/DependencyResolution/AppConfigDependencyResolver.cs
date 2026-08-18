using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000151 RID: 337
	internal class AppConfigDependencyResolver : IDbDependencyResolver
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x00037859 File Offset: 0x00035A59
		public AppConfigDependencyResolver()
		{
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00037884 File Offset: 0x00035A84
		public AppConfigDependencyResolver(AppConfig appConfig, InternalConfiguration internalConfiguration, ProviderServicesFactory providerServicesFactory = null)
		{
			this._appConfig = appConfig;
			this._internalConfiguration = internalConfiguration;
			this._providerServicesFactory = (providerServicesFactory ?? new ProviderServicesFactory());
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000378FC File Offset: 0x00035AFC
		public virtual object GetService(Type type, object key)
		{
			return this._serviceFactories.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> t) => this.GetServiceFactory(type, key as string))();
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x00037984 File Offset: 0x00035B84
		public IEnumerable<object> GetServices(Type type, object key)
		{
			return (from f in this._servicesFactories.GetOrAdd(Tuple.Create<Type, object>(type, key), (Tuple<Type, object> t) => this.GetServicesFactory(type, key))
			select f() into s
			where s != null
			select s).ToList<object>();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00037A58 File Offset: 0x00035C58
		public virtual IEnumerable<Func<object>> GetServicesFactory(Type type, object key)
		{
			if (type == typeof(IDbInterceptor))
			{
				return (from i in this._appConfig.Interceptors
				select () => i).ToList<Func<object>>();
			}
			return new List<Func<object>>
			{
				this.GetServiceFactory(type, key as string)
			};
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00037AF8 File Offset: 0x00035CF8
		public virtual Func<object> GetServiceFactory(Type type, string name)
		{
			if (!this._providersRegistered)
			{
				lock (this._providerFactories)
				{
					if (!this._providersRegistered)
					{
						this.RegisterDbProviderServices();
						this._providersRegistered = true;
					}
				}
			}
			if (!string.IsNullOrWhiteSpace(name) && type == typeof(DbProviderServices))
			{
				DbProviderServices providerFactory;
				this._providerFactories.TryGetValue(name, out providerFactory);
				return () => providerFactory;
			}
			if (type == typeof(IDbConnectionFactory))
			{
				if (!Database.DefaultConnectionFactoryChanged)
				{
					IDbConnectionFactory dbConnectionFactory = this._appConfig.TryGetDefaultConnectionFactory();
					if (dbConnectionFactory != null)
					{
						Database.DefaultConnectionFactory = dbConnectionFactory;
					}
				}
				return delegate()
				{
					if (!Database.DefaultConnectionFactoryChanged)
					{
						return null;
					}
					return Database.SetDefaultConnectionFactory;
				};
			}
			Type type2 = type.TryGetElementType(typeof(IDatabaseInitializer<>));
			if (type2 != null)
			{
				object initializer = this._appConfig.Initializers.TryGetInitializer(type2);
				return () => initializer;
			}
			return () => null;
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00037C80 File Offset: 0x00035E80
		private void RegisterDbProviderServices()
		{
			IList<NamedDbProviderService> dbProviderServices = this._appConfig.DbProviderServices;
			if (dbProviderServices.All((NamedDbProviderService p) => p.InvariantName != "System.Data.SqlClient"))
			{
				this.RegisterSqlServerProvider();
			}
			dbProviderServices.Each(delegate(NamedDbProviderService p)
			{
				this._providerFactories[p.InvariantName] = p.ProviderServices;
				this._internalConfiguration.AddDefaultResolver(p.ProviderServices);
			});
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00037CD8 File Offset: 0x00035ED8
		private void RegisterSqlServerProvider()
		{
			string providerTypeName = string.Format(CultureInfo.InvariantCulture, "System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer, Version={0}, Culture=neutral, PublicKeyToken=b77a5c561934e089", new object[]
			{
				new AssemblyName(typeof(DbContext).Assembly().FullName).Version
			});
			DbProviderServices dbProviderServices = this._providerServicesFactory.TryGetInstance(providerTypeName);
			if (dbProviderServices != null)
			{
				this._internalConfiguration.SetDefaultProviderServices(dbProviderServices, "System.Data.SqlClient");
			}
		}

		// Token: 0x040002FA RID: 762
		private readonly AppConfig _appConfig;

		// Token: 0x040002FB RID: 763
		private readonly InternalConfiguration _internalConfiguration;

		// Token: 0x040002FC RID: 764
		private readonly ConcurrentDictionary<Tuple<Type, object>, Func<object>> _serviceFactories = new ConcurrentDictionary<Tuple<Type, object>, Func<object>>();

		// Token: 0x040002FD RID: 765
		private readonly ConcurrentDictionary<Tuple<Type, object>, IEnumerable<Func<object>>> _servicesFactories = new ConcurrentDictionary<Tuple<Type, object>, IEnumerable<Func<object>>>();

		// Token: 0x040002FE RID: 766
		private readonly Dictionary<string, DbProviderServices> _providerFactories = new Dictionary<string, DbProviderServices>();

		// Token: 0x040002FF RID: 767
		private bool _providersRegistered;

		// Token: 0x04000300 RID: 768
		private readonly ProviderServicesFactory _providerServicesFactory;
	}
}
