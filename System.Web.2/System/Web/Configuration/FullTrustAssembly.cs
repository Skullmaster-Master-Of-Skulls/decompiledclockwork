using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006E4 RID: 1764
	public sealed class FullTrustAssembly : ConfigurationElement
	{
		// Token: 0x060054CC RID: 21708 RVA: 0x001288F8 File Offset: 0x00126AF8
		static FullTrustAssembly()
		{
			FullTrustAssembly._properties = new ConfigurationPropertyCollection();
			FullTrustAssembly._properties.Add(FullTrustAssembly._propAssemblyName);
			FullTrustAssembly._properties.Add(FullTrustAssembly._propVersion);
			FullTrustAssembly._properties.Add(FullTrustAssembly._propPublicKey);
		}

		// Token: 0x060054CD RID: 21709 RVA: 0x00117E9E File Offset: 0x0011609E
		internal FullTrustAssembly()
		{
		}

		// Token: 0x060054CE RID: 21710 RVA: 0x0012899F File Offset: 0x00126B9F
		public FullTrustAssembly(string assemblyName, string version, string publicKey)
		{
			this.AssemblyName = assemblyName;
			this.Version = version;
			this.PublicKey = publicKey;
		}

		// Token: 0x17001834 RID: 6196
		// (get) Token: 0x060054CF RID: 21711 RVA: 0x001289BC File Offset: 0x00126BBC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FullTrustAssembly._properties;
			}
		}

		// Token: 0x17001835 RID: 6197
		// (get) Token: 0x060054D0 RID: 21712 RVA: 0x001289C3 File Offset: 0x00126BC3
		// (set) Token: 0x060054D1 RID: 21713 RVA: 0x001289D5 File Offset: 0x00126BD5
		[ConfigurationProperty("assemblyName", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string AssemblyName
		{
			get
			{
				return (string)base[FullTrustAssembly._propAssemblyName];
			}
			set
			{
				base[FullTrustAssembly._propAssemblyName] = value;
			}
		}

		// Token: 0x17001836 RID: 6198
		// (get) Token: 0x060054D2 RID: 21714 RVA: 0x001289E3 File Offset: 0x00126BE3
		// (set) Token: 0x060054D3 RID: 21715 RVA: 0x001289F5 File Offset: 0x00126BF5
		[ConfigurationProperty("version", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string Version
		{
			get
			{
				return (string)base[FullTrustAssembly._propVersion];
			}
			set
			{
				base[FullTrustAssembly._propVersion] = value;
			}
		}

		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x060054D4 RID: 21716 RVA: 0x00128A03 File Offset: 0x00126C03
		// (set) Token: 0x060054D5 RID: 21717 RVA: 0x00128A15 File Offset: 0x00126C15
		[ConfigurationProperty("publicKey", IsRequired = true, IsKey = false, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string PublicKey
		{
			get
			{
				return (string)base[FullTrustAssembly._propPublicKey];
			}
			set
			{
				base[FullTrustAssembly._propPublicKey] = value;
			}
		}

		// Token: 0x04002C7F RID: 11391
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C80 RID: 11392
		private static readonly ConfigurationProperty _propAssemblyName = new ConfigurationProperty("assemblyName", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002C81 RID: 11393
		private static readonly ConfigurationProperty _propVersion = new ConfigurationProperty("version", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002C82 RID: 11394
		private static readonly ConfigurationProperty _propPublicKey = new ConfigurationProperty("publicKey", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}
