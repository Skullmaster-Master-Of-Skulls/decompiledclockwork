using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace System.Data.SqlClient
{
	// Token: 0x020001DC RID: 476
	internal class SqlAuthenticationProviderManager
	{
		// Token: 0x06001E09 RID: 7689 RVA: 0x000D34B8 File Offset: 0x000D28B8
		static SqlAuthenticationProviderManager()
		{
			ActiveDirectoryNativeAuthenticationProvider provider = new ActiveDirectoryNativeAuthenticationProvider();
			SqlAuthenticationProviderConfigurationSection configSection;
			try
			{
				configSection = (SqlAuthenticationProviderConfigurationSection)ConfigurationManager.GetSection("SqlAuthenticationProviders");
			}
			catch (ConfigurationErrorsException e)
			{
				throw SQL.CannotGetAuthProviderConfig(e);
			}
			SqlAuthenticationProviderManager.Instance = new SqlAuthenticationProviderManager(configSection);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryIntegrated, provider);
			SqlAuthenticationProviderManager.Instance.SetProvider(SqlAuthenticationMethod.ActiveDirectoryPassword, provider);
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x000D3528 File Offset: 0x000D2928
		public SqlAuthenticationProviderManager(SqlAuthenticationProviderConfigurationSection configSection)
		{
			this._typeName = base.GetType().Name;
			string method = "Ctor";
			this._providers = new ConcurrentDictionary<SqlAuthenticationMethod, SqlAuthenticationProvider>();
			HashSet<SqlAuthenticationMethod> hashSet = new HashSet<SqlAuthenticationMethod>();
			this._authenticationsWithAppSpecifiedProvider = hashSet;
			if (configSection == null)
			{
				this._sqlAuthLogger.LogInfo(this._typeName, method, "No SqlAuthProviders configuration section found.");
				return;
			}
			if (!string.IsNullOrEmpty(configSection.InitializerType))
			{
				try
				{
					Type type = Type.GetType(configSection.InitializerType, true);
					this._initializer = (SqlAuthenticationInitializer)Activator.CreateInstance(type);
					this._initializer.Initialize();
				}
				catch (Exception e)
				{
					throw SQL.CannotCreateSqlAuthInitializer(configSection.InitializerType, e);
				}
				this._sqlAuthLogger.LogInfo(this._typeName, method, "Created user-defined SqlAuthenticationInitializer.");
			}
			else
			{
				this._sqlAuthLogger.LogInfo(this._typeName, method, "No user-defined SqlAuthenticationInitializer found.");
			}
			if (configSection.Providers != null && configSection.Providers.Count > 0)
			{
				using (IEnumerator enumerator = configSection.Providers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						ProviderSettings providerSettings = (ProviderSettings)obj;
						SqlAuthenticationMethod sqlAuthenticationMethod = SqlAuthenticationProviderManager.AuthenticationEnumFromString(providerSettings.Name);
						SqlAuthenticationProvider sqlAuthenticationProvider;
						try
						{
							Type type2 = Type.GetType(providerSettings.Type, true);
							sqlAuthenticationProvider = (SqlAuthenticationProvider)Activator.CreateInstance(type2);
						}
						catch (Exception e2)
						{
							throw SQL.CannotCreateAuthProvider(sqlAuthenticationMethod.ToString(), providerSettings.Type, e2);
						}
						if (!sqlAuthenticationProvider.IsSupported(sqlAuthenticationMethod))
						{
							throw SQL.UnsupportedAuthenticationByProvider(sqlAuthenticationMethod.ToString(), providerSettings.Type);
						}
						this._providers[sqlAuthenticationMethod] = sqlAuthenticationProvider;
						hashSet.Add(sqlAuthenticationMethod);
						this._sqlAuthLogger.LogInfo(this._typeName, method, string.Format("Added user-defined auth provider: {0} for authentication {1}.", providerSettings.Type, sqlAuthenticationMethod));
					}
					return;
				}
			}
			this._sqlAuthLogger.LogInfo(this._typeName, method, "No user-defined auth providers.");
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x000D3778 File Offset: 0x000D2B78
		public SqlAuthenticationProvider GetProvider(SqlAuthenticationMethod authenticationMethod)
		{
			SqlAuthenticationProvider result;
			if (!this._providers.TryGetValue(authenticationMethod, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x000D3798 File Offset: 0x000D2B98
		public bool SetProvider(SqlAuthenticationMethod authenticationMethod, SqlAuthenticationProvider provider)
		{
			if (!provider.IsSupported(authenticationMethod))
			{
				throw SQL.UnsupportedAuthenticationByProvider(authenticationMethod.ToString(), provider.GetType().Name);
			}
			string methodName = "SetProvider";
			if (this._authenticationsWithAppSpecifiedProvider.Contains(authenticationMethod))
			{
				this._sqlAuthLogger.LogError(this._typeName, methodName, string.Format("Failed to add provider {0} because a user-defined provider with type {1} already existed for authentication {2}.", SqlAuthenticationProviderManager.GetProviderType(provider), SqlAuthenticationProviderManager.GetProviderType(this._providers[authenticationMethod]), authenticationMethod));
				return false;
			}
			this._providers.AddOrUpdate(authenticationMethod, provider, delegate(SqlAuthenticationMethod key, SqlAuthenticationProvider oldProvider)
			{
				if (oldProvider != null)
				{
					oldProvider.BeforeUnload(authenticationMethod);
				}
				if (provider != null)
				{
					provider.BeforeLoad(authenticationMethod);
				}
				this._sqlAuthLogger.LogInfo(this._typeName, methodName, string.Format("Added auth provider {0}, overriding existed provider {1} for authentication {2}.", SqlAuthenticationProviderManager.GetProviderType(provider), SqlAuthenticationProviderManager.GetProviderType(oldProvider), authenticationMethod));
				return provider;
			});
			return true;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000D388C File Offset: 0x000D2C8C
		private static SqlAuthenticationMethod AuthenticationEnumFromString(string authentication)
		{
			string a = authentication.ToLowerInvariant();
			if (a == "active directory integrated")
			{
				return SqlAuthenticationMethod.ActiveDirectoryIntegrated;
			}
			if (a == "active directory password")
			{
				return SqlAuthenticationMethod.ActiveDirectoryPassword;
			}
			if (!(a == "active directory interactive"))
			{
				throw SQL.UnsupportedAuthentication(authentication);
			}
			return SqlAuthenticationMethod.ActiveDirectoryInteractive;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000D38D8 File Offset: 0x000D2CD8
		private static string GetProviderType(SqlAuthenticationProvider provider)
		{
			if (provider == null)
			{
				return "null";
			}
			return provider.GetType().FullName;
		}

		// Token: 0x04001121 RID: 4385
		private const string ActiveDirectoryPassword = "active directory password";

		// Token: 0x04001122 RID: 4386
		private const string ActiveDirectoryIntegrated = "active directory integrated";

		// Token: 0x04001123 RID: 4387
		private const string ActiveDirectoryInteractive = "active directory interactive";

		// Token: 0x04001124 RID: 4388
		public static readonly SqlAuthenticationProviderManager Instance;

		// Token: 0x04001125 RID: 4389
		private readonly string _typeName;

		// Token: 0x04001126 RID: 4390
		private readonly SqlAuthenticationInitializer _initializer;

		// Token: 0x04001127 RID: 4391
		private readonly IReadOnlyCollection<SqlAuthenticationMethod> _authenticationsWithAppSpecifiedProvider;

		// Token: 0x04001128 RID: 4392
		private readonly ConcurrentDictionary<SqlAuthenticationMethod, SqlAuthenticationProvider> _providers;

		// Token: 0x04001129 RID: 4393
		private readonly SqlClientLogger _sqlAuthLogger = new SqlClientLogger();
	}
}
