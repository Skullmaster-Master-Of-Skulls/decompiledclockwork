using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200063C RID: 1596
	public class MessageSecurityOverHttpElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06003D73 RID: 15731 RVA: 0x000EAA1C File Offset: 0x000E8C1C
		// (set) Token: 0x06003D74 RID: 15732 RVA: 0x000EAA2E File Offset: 0x000E8C2E
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

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x000EAA41 File Offset: 0x000E8C41
		// (set) Token: 0x06003D76 RID: 15734 RVA: 0x000EAA53 File Offset: 0x000E8C53
		[ConfigurationProperty("negotiateServiceCredential", DefaultValue = true)]
		public bool NegotiateServiceCredential
		{
			get
			{
				return (bool)base["negotiateServiceCredential"];
			}
			set
			{
				base["negotiateServiceCredential"] = value;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06003D77 RID: 15735 RVA: 0x000EAA66 File Offset: 0x000E8C66
		// (set) Token: 0x06003D78 RID: 15736 RVA: 0x000EAA78 File Offset: 0x000E8C78
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

		// Token: 0x06003D79 RID: 15737 RVA: 0x000EAA88 File Offset: 0x000E8C88
		internal void ApplyConfiguration(MessageSecurityOverHttp security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.ClientCredentialType = this.ClientCredentialType;
			security.NegotiateServiceCredential = this.NegotiateServiceCredential;
			if (base.ElementInformation.Properties["algorithmSuite"].ValueOrigin != PropertyValueOrigin.Default)
			{
				security.AlgorithmSuite = this.AlgorithmSuite;
			}
		}

		// Token: 0x06003D7A RID: 15738 RVA: 0x000EAAE8 File Offset: 0x000E8CE8
		internal void InitializeFrom(MessageSecurityOverHttp security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<MessageCredentialType>("clientCredentialType", security.ClientCredentialType);
			base.SetPropertyValueIfNotDefaultValue<bool>("negotiateServiceCredential", security.NegotiateServiceCredential);
			if (security.WasAlgorithmSuiteSet)
			{
				base.SetPropertyValueIfNotDefaultValue<SecurityAlgorithmSuite>("algorithmSuite", security.AlgorithmSuite);
			}
		}

		// Token: 0x06003D7B RID: 15739 RVA: 0x000EAB43 File Offset: 0x000E8D43
		internal MessageSecurityOverHttpElement()
		{
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06003D7C RID: 15740 RVA: 0x000EAB4C File Offset: 0x000E8D4C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("clientCredentialType", typeof(MessageCredentialType), MessageCredentialType.Windows, null, new ServiceModelEnumValidator(typeof(MessageCredentialTypeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("negotiateServiceCredential", typeof(bool), true, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("algorithmSuite", typeof(SecurityAlgorithmSuite), "Default", new SecurityAlgorithmSuiteConverter(), null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C91 RID: 11409
		private ConfigurationPropertyCollection properties;
	}
}
