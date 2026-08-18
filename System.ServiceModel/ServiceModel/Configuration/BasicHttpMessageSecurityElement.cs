using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005EE RID: 1518
	public sealed class BasicHttpMessageSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000DD0 RID: 3536
		// (get) Token: 0x06003A84 RID: 14980 RVA: 0x000E0FCD File Offset: 0x000DF1CD
		// (set) Token: 0x06003A85 RID: 14981 RVA: 0x000E0FDF File Offset: 0x000DF1DF
		[ConfigurationProperty("clientCredentialType", DefaultValue = BasicHttpMessageCredentialType.UserName)]
		[ServiceModelEnumValidator(typeof(BasicHttpMessageCredentialTypeHelper))]
		public BasicHttpMessageCredentialType ClientCredentialType
		{
			get
			{
				return (BasicHttpMessageCredentialType)base["clientCredentialType"];
			}
			set
			{
				base["clientCredentialType"] = value;
			}
		}

		// Token: 0x17000DD1 RID: 3537
		// (get) Token: 0x06003A86 RID: 14982 RVA: 0x000E0FF2 File Offset: 0x000DF1F2
		// (set) Token: 0x06003A87 RID: 14983 RVA: 0x000E1004 File Offset: 0x000DF204
		[ConfigurationProperty("algorithmSuite", DefaultValue = "Default")]
		[TypeConverter(typeof(SecurityAlgorithmSuiteConverter))]
		public SecurityAlgorithmSuite AlgorithmSuite
		{
			get
			{
				return (SecurityAlgorithmSuite)base["algorithmSuite"];
			}
			set
			{
				base["algorithmSuite"] = value;
			}
		}

		// Token: 0x06003A88 RID: 14984 RVA: 0x000E1014 File Offset: 0x000DF214
		internal void ApplyConfiguration(BasicHttpMessageSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.ClientCredentialType = this.ClientCredentialType;
			if (base.ElementInformation.Properties["algorithmSuite"].ValueOrigin != PropertyValueOrigin.Default)
			{
				security.AlgorithmSuite = this.AlgorithmSuite;
			}
		}

		// Token: 0x06003A89 RID: 14985 RVA: 0x000E1068 File Offset: 0x000DF268
		internal void InitializeFrom(BasicHttpMessageSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<BasicHttpMessageCredentialType>("clientCredentialType", security.ClientCredentialType);
			base.SetPropertyValueIfNotDefaultValue<SecurityAlgorithmSuite>("algorithmSuite", security.AlgorithmSuite);
		}

		// Token: 0x17000DD2 RID: 3538
		// (get) Token: 0x06003A8A RID: 14986 RVA: 0x000E10A0 File Offset: 0x000DF2A0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientCredentialType", typeof(BasicHttpMessageCredentialType), BasicHttpMessageCredentialType.UserName, null, new ServiceModelEnumValidator(typeof(BasicHttpMessageCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("algorithmSuite", typeof(SecurityAlgorithmSuite), "Default", new SecurityAlgorithmSuiteConverter(), null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002A71 RID: 10865
		private ConfigurationPropertyCollection properties;
	}
}
