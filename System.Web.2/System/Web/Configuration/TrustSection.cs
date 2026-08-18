using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000767 RID: 1895
	public sealed class TrustSection : ConfigurationSection
	{
		// Token: 0x06005B57 RID: 23383 RVA: 0x0013CFFC File Offset: 0x0013B1FC
		static TrustSection()
		{
			TrustSection._properties = new ConfigurationPropertyCollection();
			TrustSection._properties.Add(TrustSection._propLevel);
			TrustSection._properties.Add(TrustSection._propOriginUrl);
			TrustSection._properties.Add(TrustSection._propProcessRequestInApplicationTrust);
			TrustSection._properties.Add(TrustSection._propLegacyCasModel);
			TrustSection._properties.Add(TrustSection._propPermissionSetName);
			TrustSection._properties.Add(TrustSection._propHostSecurityPolicyResolverType);
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06005B59 RID: 23385 RVA: 0x0013D12F File Offset: 0x0013B32F
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustSection._properties;
			}
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06005B5A RID: 23386 RVA: 0x0013D136 File Offset: 0x0013B336
		// (set) Token: 0x06005B5B RID: 23387 RVA: 0x0013D148 File Offset: 0x0013B348
		[ConfigurationProperty("level", IsRequired = true, DefaultValue = "Full")]
		[StringValidator(MinLength = 1)]
		public string Level
		{
			get
			{
				return (string)base[TrustSection._propLevel];
			}
			set
			{
				base[TrustSection._propLevel] = value;
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x0013D156 File Offset: 0x0013B356
		// (set) Token: 0x06005B5D RID: 23389 RVA: 0x0013D168 File Offset: 0x0013B368
		[ConfigurationProperty("originUrl", DefaultValue = "")]
		public string OriginUrl
		{
			get
			{
				return (string)base[TrustSection._propOriginUrl];
			}
			set
			{
				base[TrustSection._propOriginUrl] = value;
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x0013D176 File Offset: 0x0013B376
		// (set) Token: 0x06005B5F RID: 23391 RVA: 0x0013D188 File Offset: 0x0013B388
		[ConfigurationProperty("processRequestInApplicationTrust", DefaultValue = true)]
		public bool ProcessRequestInApplicationTrust
		{
			get
			{
				return (bool)base[TrustSection._propProcessRequestInApplicationTrust];
			}
			set
			{
				base[TrustSection._propProcessRequestInApplicationTrust] = value;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06005B60 RID: 23392 RVA: 0x0013D19B File Offset: 0x0013B39B
		// (set) Token: 0x06005B61 RID: 23393 RVA: 0x0013D1AD File Offset: 0x0013B3AD
		[ConfigurationProperty("legacyCasModel", DefaultValue = false)]
		public bool LegacyCasModel
		{
			get
			{
				return (bool)base[TrustSection._propLegacyCasModel];
			}
			set
			{
				base[TrustSection._propLegacyCasModel] = value;
			}
		}

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x0013D1C0 File Offset: 0x0013B3C0
		// (set) Token: 0x06005B63 RID: 23395 RVA: 0x0013D1D2 File Offset: 0x0013B3D2
		[ConfigurationProperty("permissionSetName", DefaultValue = "ASP.Net")]
		public string PermissionSetName
		{
			get
			{
				return (string)base[TrustSection._propPermissionSetName];
			}
			set
			{
				base[TrustSection._propPermissionSetName] = value;
			}
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06005B64 RID: 23396 RVA: 0x0013D1E0 File Offset: 0x0013B3E0
		// (set) Token: 0x06005B65 RID: 23397 RVA: 0x0013D1F2 File Offset: 0x0013B3F2
		[ConfigurationProperty("hostSecurityPolicyResolverType", DefaultValue = "")]
		public string HostSecurityPolicyResolverType
		{
			get
			{
				return (string)base[TrustSection._propHostSecurityPolicyResolverType];
			}
			set
			{
				base[TrustSection._propHostSecurityPolicyResolverType] = value;
			}
		}

		// Token: 0x04003031 RID: 12337
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04003032 RID: 12338
		private static readonly ConfigurationProperty _propLevel = new ConfigurationProperty("level", typeof(string), "Full", null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04003033 RID: 12339
		private static readonly ConfigurationProperty _propOriginUrl = new ConfigurationProperty("originUrl", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04003034 RID: 12340
		private static readonly ConfigurationProperty _propProcessRequestInApplicationTrust = new ConfigurationProperty("processRequestInApplicationTrust", typeof(bool), true, ConfigurationPropertyOptions.None);

		// Token: 0x04003035 RID: 12341
		private static readonly ConfigurationProperty _propLegacyCasModel = new ConfigurationProperty("legacyCasModel", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04003036 RID: 12342
		private static readonly ConfigurationProperty _propPermissionSetName = new ConfigurationProperty("permissionSetName", typeof(string), "ASP.Net", ConfigurationPropertyOptions.None);

		// Token: 0x04003037 RID: 12343
		private static readonly ConfigurationProperty _propHostSecurityPolicyResolverType = new ConfigurationProperty("hostSecurityPolicyResolverType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);
	}
}
