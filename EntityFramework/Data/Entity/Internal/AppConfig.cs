using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Internal.ConfigFile;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x020006C4 RID: 1732
	internal class AppConfig
	{
		// Token: 0x060044C3 RID: 17603 RVA: 0x00144B5C File Offset: 0x00142D5C
		public AppConfig(Configuration configuration) : this(configuration.ConnectionStrings.ConnectionStrings, configuration.AppSettings.Settings, (EntityFrameworkSection)configuration.GetSection("entityFramework"), null)
		{
		}

		// Token: 0x060044C4 RID: 17604 RVA: 0x00144B8B File Offset: 0x00142D8B
		public AppConfig(ConnectionStringSettingsCollection connectionStrings) : this(connectionStrings, null, null, null)
		{
		}

		// Token: 0x060044C5 RID: 17605 RVA: 0x00144B97 File Offset: 0x00142D97
		private AppConfig() : this(ConfigurationManager.ConnectionStrings, AppConfig.Convert(ConfigurationManager.AppSettings), (EntityFrameworkSection)ConfigurationManager.GetSection("entityFramework"), null)
		{
		}

		// Token: 0x060044C6 RID: 17606 RVA: 0x00144C74 File Offset: 0x00142E74
		internal AppConfig(ConnectionStringSettingsCollection connectionStrings, KeyValueConfigurationCollection appSettings, EntityFrameworkSection entityFrameworkSettings, ProviderServicesFactory providerServicesFactory = null)
		{
			this._connectionStrings = connectionStrings;
			this._appSettings = (appSettings ?? new KeyValueConfigurationCollection());
			this._entityFrameworkSettings = (entityFrameworkSettings ?? new EntityFrameworkSection());
			this._providerServicesFactory = (providerServicesFactory ?? new ProviderServicesFactory());
			this._providerServices = new Lazy<IList<NamedDbProviderService>>(() => (from e in this._entityFrameworkSettings.Providers.OfType<ProviderElement>()
			select new NamedDbProviderService(e.InvariantName, this._providerServicesFactory.GetInstance(e.ProviderTypeName, e.InvariantName))).ToList<NamedDbProviderService>());
			if (this._entityFrameworkSettings.DefaultConnectionFactory.ElementInformation.IsPresent)
			{
				this._defaultConnectionFactory = new Lazy<IDbConnectionFactory>(delegate()
				{
					DefaultConnectionFactoryElement defaultConnectionFactory = this._entityFrameworkSettings.DefaultConnectionFactory;
					IDbConnectionFactory result;
					try
					{
						Type factoryType = defaultConnectionFactory.GetFactoryType();
						object[] typedParameterValues = defaultConnectionFactory.Parameters.GetTypedParameterValues();
						result = (IDbConnectionFactory)Activator.CreateInstance(factoryType, typedParameterValues);
					}
					catch (Exception innerException)
					{
						throw new InvalidOperationException(Strings.SetConnectionFactoryFromConfigFailed(defaultConnectionFactory.FactoryTypeName), innerException);
					}
					return result;
				}, true);
				return;
			}
			this._defaultConnectionFactory = this._defaultDefaultConnectionFactory;
		}

		// Token: 0x060044C7 RID: 17607 RVA: 0x00144D49 File Offset: 0x00142F49
		public virtual IDbConnectionFactory TryGetDefaultConnectionFactory()
		{
			return this._defaultConnectionFactory.Value;
		}

		// Token: 0x060044C8 RID: 17608 RVA: 0x00144D56 File Offset: 0x00142F56
		public ConnectionStringSettings GetConnectionString(string name)
		{
			return this._connectionStrings[name];
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060044C9 RID: 17609 RVA: 0x00144D64 File Offset: 0x00142F64
		public static AppConfig DefaultInstance
		{
			get
			{
				return AppConfig._defaultInstance;
			}
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x00144D6C File Offset: 0x00142F6C
		private static KeyValueConfigurationCollection Convert(NameValueCollection collection)
		{
			KeyValueConfigurationCollection keyValueConfigurationCollection = new KeyValueConfigurationCollection();
			foreach (string text in collection.AllKeys)
			{
				keyValueConfigurationCollection.Add(text, ConfigurationManager.AppSettings[text]);
			}
			return keyValueConfigurationCollection;
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060044CB RID: 17611 RVA: 0x00144DAB File Offset: 0x00142FAB
		public virtual InitializerConfig Initializers
		{
			get
			{
				return new InitializerConfig(this._entityFrameworkSettings, this._appSettings);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060044CC RID: 17612 RVA: 0x00144DBE File Offset: 0x00142FBE
		public virtual string ConfigurationTypeName
		{
			get
			{
				return this._entityFrameworkSettings.ConfigurationTypeName;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x060044CD RID: 17613 RVA: 0x00144DCB File Offset: 0x00142FCB
		public virtual IList<NamedDbProviderService> DbProviderServices
		{
			get
			{
				return this._providerServices.Value;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x00144DD8 File Offset: 0x00142FD8
		public virtual IEnumerable<IDbInterceptor> Interceptors
		{
			get
			{
				return this._entityFrameworkSettings.Interceptors.Interceptors;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x060044CF RID: 17615 RVA: 0x00144DEA File Offset: 0x00142FEA
		public virtual QueryCacheConfig QueryCache
		{
			get
			{
				return new QueryCacheConfig(this._entityFrameworkSettings);
			}
		}

		// Token: 0x04001956 RID: 6486
		public const string EFSectionName = "entityFramework";

		// Token: 0x04001957 RID: 6487
		private static readonly AppConfig _defaultInstance = new AppConfig();

		// Token: 0x04001958 RID: 6488
		private readonly KeyValueConfigurationCollection _appSettings;

		// Token: 0x04001959 RID: 6489
		private readonly ConnectionStringSettingsCollection _connectionStrings;

		// Token: 0x0400195A RID: 6490
		private readonly EntityFrameworkSection _entityFrameworkSettings;

		// Token: 0x0400195B RID: 6491
		private readonly Lazy<IDbConnectionFactory> _defaultConnectionFactory;

		// Token: 0x0400195C RID: 6492
		private readonly Lazy<IDbConnectionFactory> _defaultDefaultConnectionFactory = new Lazy<IDbConnectionFactory>(() => null, true);

		// Token: 0x0400195D RID: 6493
		private readonly ProviderServicesFactory _providerServicesFactory;

		// Token: 0x0400195E RID: 6494
		private readonly Lazy<IList<NamedDbProviderService>> _providerServices;
	}
}
