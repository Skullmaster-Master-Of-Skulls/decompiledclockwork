using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Infrastructure.DependencyResolution
{
	// Token: 0x02000159 RID: 345
	internal class DbConfigurationManager
	{
		// Token: 0x06000B36 RID: 2870 RVA: 0x0003835C File Offset: 0x0003655C
		public DbConfigurationManager(DbConfigurationLoader loader, DbConfigurationFinder finder)
		{
			this._loader = loader;
			this._finder = finder;
			this._configuration = new Lazy<InternalConfiguration>(delegate()
			{
				DbConfiguration dbConfiguration = this._newConfiguration ?? this._newConfigurationType.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadDbConfigurationType), null);
				dbConfiguration.InternalConfiguration.Lock();
				return dbConfiguration.InternalConfiguration;
			});
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x000383EB File Offset: 0x000365EB
		public static DbConfigurationManager Instance
		{
			get
			{
				return DbConfigurationManager._configManager;
			}
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000383F2 File Offset: 0x000365F2
		public virtual void AddLoadedHandler(EventHandler<DbConfigurationLoadedEventArgs> handler)
		{
			if (this.ConfigurationSet)
			{
				throw new InvalidOperationException(Strings.AddHandlerToInUseConfiguration);
			}
			this._loadedHandler = (EventHandler<DbConfigurationLoadedEventArgs>)Delegate.Combine(this._loadedHandler, handler);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x0003841E File Offset: 0x0003661E
		public virtual void RemoveLoadedHandler(EventHandler<DbConfigurationLoadedEventArgs> handler)
		{
			this._loadedHandler = (EventHandler<DbConfigurationLoadedEventArgs>)Delegate.Remove(this._loadedHandler, handler);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00038438 File Offset: 0x00036638
		public virtual void OnLoaded(InternalConfiguration configuration)
		{
			DbConfigurationLoadedEventArgs dbConfigurationLoadedEventArgs = new DbConfigurationLoadedEventArgs(configuration);
			EventHandler<DbConfigurationLoadedEventArgs> loadedHandler = this._loadedHandler;
			if (loadedHandler != null)
			{
				loadedHandler(configuration.Owner, dbConfigurationLoadedEventArgs);
			}
			configuration.DispatchLoadedInterceptors(dbConfigurationLoadedEventArgs);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x0003846C File Offset: 0x0003666C
		public virtual InternalConfiguration GetConfiguration()
		{
			if (this._configurationOverrides.IsValueCreated)
			{
				lock (this._lock)
				{
					if (this._configurationOverrides.Value.Count != 0)
					{
						return this._configurationOverrides.Value.Last<Tuple<AppConfig, InternalConfiguration>>().Item2;
					}
				}
			}
			return this._configuration.Value;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000384EC File Offset: 0x000366EC
		public virtual void SetConfigurationType(Type configurationType)
		{
			this._newConfigurationType = configurationType;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000384F8 File Offset: 0x000366F8
		public virtual void SetConfiguration(InternalConfiguration configuration)
		{
			Type type = this._loader.TryLoadFromConfig(AppConfig.DefaultInstance);
			if (type != null)
			{
				configuration = type.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadDbConfigurationType), null).InternalConfiguration;
			}
			this._newConfiguration = configuration.Owner;
			if (!(this._configuration.Value.Owner.GetType() != configuration.Owner.GetType()))
			{
				return;
			}
			if (this._configuration.Value.Owner.GetType() == typeof(DbConfiguration))
			{
				throw new InvalidOperationException(Strings.DefaultConfigurationUsedBeforeSet(configuration.Owner.GetType().Name));
			}
			throw new InvalidOperationException(Strings.ConfigurationSetTwice(configuration.Owner.GetType().Name, this._configuration.Value.Owner.GetType().Name));
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000385E4 File Offset: 0x000367E4
		public virtual void EnsureLoadedForContext(Type contextType)
		{
			this.EnsureLoadedForAssembly(contextType.Assembly(), contextType);
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000385F4 File Offset: 0x000367F4
		public virtual void EnsureLoadedForAssembly(Assembly assemblyHint, Type contextTypeHint)
		{
			if (contextTypeHint == typeof(DbContext) || this._knownAssemblies.ContainsKey(assemblyHint))
			{
				return;
			}
			if (this._configurationOverrides.IsValueCreated)
			{
				lock (this._lock)
				{
					if (this._configurationOverrides.Value.Count != 0)
					{
						return;
					}
				}
			}
			if (!this.ConfigurationSet)
			{
				Type type = this._loader.TryLoadFromConfig(AppConfig.DefaultInstance) ?? this._finder.TryFindConfigurationType(assemblyHint, this._finder.TryFindContextType(assemblyHint, contextTypeHint, null), null);
				if (type != null)
				{
					this.SetConfigurationType(type);
				}
			}
			else if (!assemblyHint.IsDynamic && !this._loader.AppConfigContainsDbConfigurationType(AppConfig.DefaultInstance))
			{
				contextTypeHint = this._finder.TryFindContextType(assemblyHint, contextTypeHint, null);
				Type type2 = this._finder.TryFindConfigurationType(assemblyHint, contextTypeHint, null);
				if (type2 != null)
				{
					if (this._configuration.Value.Owner.GetType() == typeof(DbConfiguration))
					{
						throw new InvalidOperationException(Strings.ConfigurationNotDiscovered(type2.Name));
					}
					if (contextTypeHint != null && type2 != this._configuration.Value.Owner.GetType())
					{
						throw new InvalidOperationException(Strings.SetConfigurationNotDiscovered(this._configuration.Value.Owner.GetType().Name, contextTypeHint.Name));
					}
				}
			}
			this._knownAssemblies.TryAdd(assemblyHint, null);
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x000387A4 File Offset: 0x000369A4
		private bool ConfigurationSet
		{
			get
			{
				return this._configuration.IsValueCreated;
			}
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000387B4 File Offset: 0x000369B4
		public virtual bool PushConfiguration(AppConfig config, Type contextType)
		{
			if (config == AppConfig.DefaultInstance && (contextType == typeof(DbContext) || this._knownAssemblies.ContainsKey(contextType.Assembly())))
			{
				return false;
			}
			Type type;
			if ((type = this._loader.TryLoadFromConfig(config)) == null)
			{
				type = (this._finder.TryFindConfigurationType(contextType, null) ?? typeof(DbConfiguration));
			}
			InternalConfiguration internalConfiguration = type.CreateInstance(new Func<string, string, string>(Strings.CreateInstance_BadDbConfigurationType), null).InternalConfiguration;
			internalConfiguration.SwitchInRootResolver(this._configuration.Value.RootResolver);
			internalConfiguration.AddAppConfigResolver(new AppConfigDependencyResolver(config, internalConfiguration, null));
			lock (this._lock)
			{
				this._configurationOverrides.Value.Add(Tuple.Create<AppConfig, InternalConfiguration>(config, internalConfiguration));
			}
			internalConfiguration.Lock();
			return true;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000388BC File Offset: 0x00036ABC
		public virtual void PopConfiguration(AppConfig config)
		{
			lock (this._lock)
			{
				Tuple<AppConfig, InternalConfiguration> tuple = this._configurationOverrides.Value.FirstOrDefault((Tuple<AppConfig, InternalConfiguration> c) => c.Item1 == config);
				if (tuple != null)
				{
					this._configurationOverrides.Value.Remove(tuple);
				}
			}
		}

		// Token: 0x04000311 RID: 785
		private static readonly DbConfigurationManager _configManager = new DbConfigurationManager(new DbConfigurationLoader(), new DbConfigurationFinder());

		// Token: 0x04000312 RID: 786
		private EventHandler<DbConfigurationLoadedEventArgs> _loadedHandler;

		// Token: 0x04000313 RID: 787
		private readonly DbConfigurationLoader _loader;

		// Token: 0x04000314 RID: 788
		private readonly DbConfigurationFinder _finder;

		// Token: 0x04000315 RID: 789
		private readonly Lazy<InternalConfiguration> _configuration;

		// Token: 0x04000316 RID: 790
		private volatile DbConfiguration _newConfiguration;

		// Token: 0x04000317 RID: 791
		private volatile Type _newConfigurationType = typeof(DbConfiguration);

		// Token: 0x04000318 RID: 792
		private readonly object _lock = new object();

		// Token: 0x04000319 RID: 793
		private readonly ConcurrentDictionary<Assembly, object> _knownAssemblies = new ConcurrentDictionary<Assembly, object>();

		// Token: 0x0400031A RID: 794
		private readonly Lazy<List<Tuple<AppConfig, InternalConfiguration>>> _configurationOverrides = new Lazy<List<Tuple<AppConfig, InternalConfiguration>>>(() => new List<Tuple<AppConfig, InternalConfiguration>>());
	}
}
