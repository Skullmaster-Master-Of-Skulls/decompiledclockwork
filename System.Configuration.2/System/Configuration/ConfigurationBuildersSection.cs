using System;
using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;

namespace System.Configuration
{
	// Token: 0x02000020 RID: 32
	public sealed class ConfigurationBuildersSection : ConfigurationSection
	{
		// Token: 0x06000136 RID: 310 RVA: 0x00009534 File Offset: 0x00007734
		public ConfigurationBuilder GetBuilderFromName(string builderName)
		{
			string[] array = builderName.Split(new char[]
			{
				','
			});
			bool flag = AppDomain.CurrentDomain.GetData("ConfigurationBuilders.IgnoreLoadFailure") == null;
			if (array.Length == 1)
			{
				ProviderSettings providerSettings = this.Builders[builderName];
				if (providerSettings == null)
				{
					throw new ConfigurationErrorsException(SR.GetString("Config_builder_not_found", new object[]
					{
						builderName
					}));
				}
				try
				{
					return this.InstantiateBuilder(providerSettings);
				}
				catch (FileNotFoundException)
				{
					if (flag)
					{
						throw;
					}
				}
				catch (TypeLoadException)
				{
					if (flag)
					{
						throw;
					}
				}
				return null;
			}
			else
			{
				ConfigurationBuilderChain configurationBuilderChain = new ConfigurationBuilderChain();
				configurationBuilderChain.Initialize(builderName, null);
				foreach (string text in array)
				{
					ProviderSettings providerSettings2 = this.Builders[text.Trim()];
					if (providerSettings2 == null)
					{
						throw new ConfigurationErrorsException(SR.GetString("Config_builder_not_found", new object[]
						{
							text
						}));
					}
					try
					{
						configurationBuilderChain.Builders.Add(this.InstantiateBuilder(providerSettings2));
					}
					catch (FileNotFoundException)
					{
						if (flag)
						{
							throw;
						}
					}
					catch (TypeLoadException)
					{
						if (flag)
						{
							throw;
						}
					}
				}
				if (configurationBuilderChain.Builders.Count == 0)
				{
					return null;
				}
				return configurationBuilderChain;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00009684 File Offset: 0x00007884
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private ConfigurationBuilder CreateAndInitializeBuilderWithAssert(Type t, ProviderSettings ps)
		{
			ConfigurationBuilder configurationBuilder = (ConfigurationBuilder)TypeUtil.CreateInstanceWithReflectionPermission(t);
			NameValueCollection parameters = ps.Parameters;
			NameValueCollection nameValueCollection = new NameValueCollection(parameters.Count);
			foreach (object obj in parameters)
			{
				string name = (string)obj;
				nameValueCollection[name] = parameters[name];
			}
			try
			{
				configurationBuilder.Initialize(ps.Name, nameValueCollection);
			}
			catch (Exception e)
			{
				throw ExceptionUtil.WrapAsConfigException(SR.GetString("ConfigBuilder_init_error", new object[]
				{
					ps.Name
				}), e, null);
			}
			return configurationBuilder;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009748 File Offset: 0x00007948
		private ConfigurationBuilder InstantiateBuilder(ProviderSettings ps)
		{
			Type typeWithReflectionPermission = TypeUtil.GetTypeWithReflectionPermission(ps.Type, true);
			if (!typeof(ConfigurationBuilder).IsAssignableFrom(typeWithReflectionPermission))
			{
				throw new ConfigurationErrorsException("[" + ps.Name + "] - " + SR.GetString("WrongType_of_config_builder"));
			}
			if (!TypeUtil.IsTypeAllowedInConfig(typeWithReflectionPermission))
			{
				throw new ConfigurationErrorsException("[" + ps.Name + "] - " + SR.GetString("Type_from_untrusted_assembly", new object[]
				{
					typeWithReflectionPermission.FullName
				}));
			}
			return this.CreateAndInitializeBuilderWithAssert(typeWithReflectionPermission, ps);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000097DD File Offset: 0x000079DD
		static ConfigurationBuildersSection()
		{
			ConfigurationBuildersSection._properties = new ConfigurationPropertyCollection();
			ConfigurationBuildersSection._properties.Add(ConfigurationBuildersSection._propBuilders);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000981F File Offset: 0x00007A1F
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ConfigurationBuildersSection._properties;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00009826 File Offset: 0x00007A26
		private ConfigurationBuilderSettings _Builders
		{
			get
			{
				return (ConfigurationBuilderSettings)base[ConfigurationBuildersSection._propBuilders];
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00009838 File Offset: 0x00007A38
		[ConfigurationProperty("builders")]
		public ProviderSettingsCollection Builders
		{
			get
			{
				return this._Builders.Builders;
			}
		}

		// Token: 0x04000187 RID: 391
		private const string _ignoreLoadFailuresSwitch = "ConfigurationBuilders.IgnoreLoadFailure";

		// Token: 0x04000188 RID: 392
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04000189 RID: 393
		private static readonly ConfigurationProperty _propBuilders = new ConfigurationProperty("builders", typeof(ConfigurationBuilderSettings), new ConfigurationBuilderSettings(), ConfigurationPropertyOptions.None);
	}
}
