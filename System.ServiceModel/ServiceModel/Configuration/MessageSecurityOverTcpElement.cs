using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200063E RID: 1598
	public sealed class MessageSecurityOverTcpElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x000EAD4F File Offset: 0x000E8F4F
		// (set) Token: 0x06003D86 RID: 15750 RVA: 0x000EAD61 File Offset: 0x000E8F61
		[ConfigurationProperty("clientCredentialType", DefaultValue = MessageCredentialType.Windows)]
		[ServiceModelEnumValidator(typeof(MessageCredentialTypeHelper))]
		public MessageCredentialType ClientCredentialType
		{
			get
			{
				return (MessageCredentialType)base["clientCredentialType"];
			}
			set
			{
				base["clientCredentialType"] = value;
			}
		}

		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06003D87 RID: 15751 RVA: 0x000EAD74 File Offset: 0x000E8F74
		// (set) Token: 0x06003D88 RID: 15752 RVA: 0x000EAD86 File Offset: 0x000E8F86
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

		// Token: 0x06003D89 RID: 15753 RVA: 0x000EAD94 File Offset: 0x000E8F94
		internal void ApplyConfiguration(MessageSecurityOverTcp security)
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

		// Token: 0x06003D8A RID: 15754 RVA: 0x000EADE8 File Offset: 0x000E8FE8
		internal void InitializeFrom(MessageSecurityOverTcp security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<MessageCredentialType>("clientCredentialType", security.ClientCredentialType);
			if (security.WasAlgorithmSuiteSet)
			{
				base.SetPropertyValueIfNotDefaultValue<SecurityAlgorithmSuite>("algorithmSuite", security.AlgorithmSuite);
			}
		}

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x000EAE28 File Offset: 0x000E9028
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientCredentialType", typeof(MessageCredentialType), MessageCredentialType.Windows, null, new ServiceModelEnumValidator(typeof(MessageCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("algorithmSuite", typeof(SecurityAlgorithmSuite), "Default", new SecurityAlgorithmSuiteConverter(), null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C93 RID: 11411
		private ConfigurationPropertyCollection properties;
	}
}
