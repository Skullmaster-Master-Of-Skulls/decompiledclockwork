using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200063D RID: 1597
	public sealed class MessageSecurityOverMsmqElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06003D7D RID: 15741 RVA: 0x000EABF1 File Offset: 0x000E8DF1
		// (set) Token: 0x06003D7E RID: 15742 RVA: 0x000EAC03 File Offset: 0x000E8E03
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

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06003D7F RID: 15743 RVA: 0x000EAC16 File Offset: 0x000E8E16
		// (set) Token: 0x06003D80 RID: 15744 RVA: 0x000EAC28 File Offset: 0x000E8E28
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

		// Token: 0x06003D81 RID: 15745 RVA: 0x000EAC38 File Offset: 0x000E8E38
		internal void ApplyConfiguration(MessageSecurityOverMsmq security)
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

		// Token: 0x06003D82 RID: 15746 RVA: 0x000EAC8C File Offset: 0x000E8E8C
		internal void InitializeFrom(MessageSecurityOverMsmq security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<MessageCredentialType>("clientCredentialType", security.ClientCredentialType);
			if (security.WasAlgorithmSuiteSet)
			{
				this.AlgorithmSuite = security.AlgorithmSuite;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06003D83 RID: 15747 RVA: 0x000EACC8 File Offset: 0x000E8EC8
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

		// Token: 0x04002C92 RID: 11410
		private ConfigurationPropertyCollection properties;
	}
}
