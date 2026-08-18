using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200007E RID: 126
	public sealed class ProtectedConfigurationSection : ConfigurationSection
	{
		// Token: 0x060004BF RID: 1215 RVA: 0x000195A0 File Offset: 0x000177A0
		internal ProtectedConfigurationProvider GetProviderFromName(string providerName)
		{
			ProviderSettings providerSettings = this.Providers[providerName];
			if (providerSettings == null)
			{
				throw new Exception(SR.GetString("ProtectedConfigurationProvider_not_found", new object[]
				{
					providerName
				}));
			}
			return this.InstantiateProvider(providerSettings);
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x000195E0 File Offset: 0x000177E0
		internal ProtectedConfigurationProviderCollection GetAllProviders()
		{
			ProtectedConfigurationProviderCollection protectedConfigurationProviderCollection = new ProtectedConfigurationProviderCollection();
			foreach (object obj in this.Providers)
			{
				ProviderSettings pn = (ProviderSettings)obj;
				protectedConfigurationProviderCollection.Add(this.InstantiateProvider(pn));
			}
			return protectedConfigurationProviderCollection;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x00019648 File Offset: 0x00017848
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private ProtectedConfigurationProvider CreateAndInitializeProviderWithAssert(Type t, ProviderSettings pn)
		{
			ProtectedConfigurationProvider protectedConfigurationProvider = (ProtectedConfigurationProvider)TypeUtil.CreateInstanceWithReflectionPermission(t);
			NameValueCollection parameters = pn.Parameters;
			NameValueCollection nameValueCollection = new NameValueCollection(parameters.Count);
			foreach (object obj in parameters)
			{
				string name = (string)obj;
				nameValueCollection[name] = parameters[name];
			}
			protectedConfigurationProvider.Initialize(pn.Name, nameValueCollection);
			return protectedConfigurationProvider;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x000196D8 File Offset: 0x000178D8
		private ProtectedConfigurationProvider InstantiateProvider(ProviderSettings pn)
		{
			Type typeWithReflectionPermission = TypeUtil.GetTypeWithReflectionPermission(pn.Type, true);
			if (!typeof(ProtectedConfigurationProvider).IsAssignableFrom(typeWithReflectionPermission))
			{
				throw new Exception(SR.GetString("WrongType_of_Protected_provider"));
			}
			if (!TypeUtil.IsTypeAllowedInConfig(typeWithReflectionPermission))
			{
				throw new Exception(SR.GetString("Type_from_untrusted_assembly", new object[]
				{
					typeWithReflectionPermission.FullName
				}));
			}
			return this.CreateAndInitializeProviderWithAssert(typeWithReflectionPermission, pn);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x00019744 File Offset: 0x00017944
		internal static string DecryptSection(string encryptedXml, ProtectedConfigurationProvider provider)
		{
			XmlDocument xmlDocument = new XmlDocument();
			ProtectedConfigurationProvider.LoadXml(xmlDocument, encryptedXml);
			XmlNode xmlNode = provider.Decrypt(xmlDocument.DocumentElement);
			return xmlNode.OuterXml;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x00019771 File Offset: 0x00017971
		internal static string FormatEncryptedSection(string encryptedXml, string sectionName, string providerName)
		{
			return string.Format(CultureInfo.InvariantCulture, "<{0} {1}=\"{2}\"> {3} </{0}>", new object[]
			{
				sectionName,
				"configProtectionProvider",
				providerName,
				encryptedXml
			});
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001979C File Offset: 0x0001799C
		internal static string EncryptSection(string clearXml, ProtectedConfigurationProvider provider)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;
			ProtectedConfigurationProvider.LoadXml(xmlDocument, clearXml);
			string name = xmlDocument.DocumentElement.Name;
			XmlNode xmlNode = provider.Encrypt(xmlDocument.DocumentElement);
			return xmlNode.OuterXml;
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x000197DC File Offset: 0x000179DC
		static ProtectedConfigurationSection()
		{
			ProtectedConfigurationSection._properties = new ConfigurationPropertyCollection();
			ProtectedConfigurationSection._properties.Add(ProtectedConfigurationSection._propProviders);
			ProtectedConfigurationSection._properties.Add(ProtectedConfigurationSection._propDefaultProvider);
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00019855 File Offset: 0x00017A55
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtectedConfigurationSection._properties;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0001985C File Offset: 0x00017A5C
		private ProtectedProviderSettings _Providers
		{
			get
			{
				return (ProtectedProviderSettings)base[ProtectedConfigurationSection._propProviders];
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x0001986E File Offset: 0x00017A6E
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return this._Providers.Providers;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0001987B File Offset: 0x00017A7B
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0001988D File Offset: 0x00017A8D
		[ConfigurationProperty("defaultProvider", DefaultValue = "RsaProtectedConfigurationProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[ProtectedConfigurationSection._propDefaultProvider];
			}
			set
			{
				base[ProtectedConfigurationSection._propDefaultProvider] = value;
			}
		}

		// Token: 0x040002D1 RID: 721
		private const string EncryptedSectionTemplate = "<{0} {1}=\"{2}\"> {3} </{0}>";

		// Token: 0x040002D2 RID: 722
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x040002D3 RID: 723
		private static readonly ConfigurationProperty _propProviders = new ConfigurationProperty("providers", typeof(ProtectedProviderSettings), new ProtectedProviderSettings(), ConfigurationPropertyOptions.None);

		// Token: 0x040002D4 RID: 724
		private static readonly ConfigurationProperty _propDefaultProvider = new ConfigurationProperty("defaultProvider", typeof(string), "RsaProtectedConfigurationProvider", null, ConfigurationProperty.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
	}
}
