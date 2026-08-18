using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000726 RID: 1830
	public sealed class PartialTrustVisibleAssembly : ConfigurationElement
	{
		// Token: 0x06005839 RID: 22585 RVA: 0x00134DE0 File Offset: 0x00132FE0
		static PartialTrustVisibleAssembly()
		{
			PartialTrustVisibleAssembly._properties = new ConfigurationPropertyCollection();
			PartialTrustVisibleAssembly._properties.Add(PartialTrustVisibleAssembly._propAssemblyName);
			PartialTrustVisibleAssembly._properties.Add(PartialTrustVisibleAssembly._propPublicKey);
		}

		// Token: 0x0600583A RID: 22586 RVA: 0x00117E9E File Offset: 0x0011609E
		internal PartialTrustVisibleAssembly()
		{
		}

		// Token: 0x0600583B RID: 22587 RVA: 0x00134E57 File Offset: 0x00133057
		public PartialTrustVisibleAssembly(string assemblyName, string publicKey)
		{
			this.AssemblyName = assemblyName;
			this.PublicKey = publicKey;
		}

		// Token: 0x17001987 RID: 6535
		// (get) Token: 0x0600583C RID: 22588 RVA: 0x00134E6D File Offset: 0x0013306D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PartialTrustVisibleAssembly._properties;
			}
		}

		// Token: 0x17001988 RID: 6536
		// (get) Token: 0x0600583D RID: 22589 RVA: 0x00134E74 File Offset: 0x00133074
		// (set) Token: 0x0600583E RID: 22590 RVA: 0x00134E86 File Offset: 0x00133086
		[ConfigurationProperty("assemblyName", IsRequired = true, IsKey = true, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string AssemblyName
		{
			get
			{
				return (string)base[PartialTrustVisibleAssembly._propAssemblyName];
			}
			set
			{
				base[PartialTrustVisibleAssembly._propAssemblyName] = value;
			}
		}

		// Token: 0x17001989 RID: 6537
		// (get) Token: 0x0600583F RID: 22591 RVA: 0x00134E94 File Offset: 0x00133094
		// (set) Token: 0x06005840 RID: 22592 RVA: 0x00134EA6 File Offset: 0x001330A6
		[ConfigurationProperty("publicKey", IsRequired = true, IsKey = false, DefaultValue = "")]
		[StringValidator(MinLength = 1)]
		public string PublicKey
		{
			get
			{
				return (string)base[PartialTrustVisibleAssembly._propPublicKey];
			}
			set
			{
				base[PartialTrustVisibleAssembly._propPublicKey] = value;
			}
		}

		// Token: 0x04002EE7 RID: 12007
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002EE8 RID: 12008
		private static readonly ConfigurationProperty _propAssemblyName = new ConfigurationProperty("assemblyName", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002EE9 RID: 12009
		private static readonly ConfigurationProperty _propPublicKey = new ConfigurationProperty("publicKey", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}
