using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006B6 RID: 1718
	public sealed class WSFederationHttpSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06004294 RID: 17044 RVA: 0x000FBD9C File Offset: 0x000F9F9C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("mode", typeof(WSFederationHttpSecurityMode), WSFederationHttpSecurityMode.Message, null, new ServiceModelEnumValidator(typeof(WSFederationHttpSecurityModeHelper)), ConfigurationPropertyOptions.None),
						new ConfigurationProperty("message", typeof(FederatedMessageSecurityOverHttpElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06004295 RID: 17045 RVA: 0x000FBE13 File Offset: 0x000FA013
		// (set) Token: 0x06004296 RID: 17046 RVA: 0x000FBE25 File Offset: 0x000FA025
		[ConfigurationProperty("mode", DefaultValue = WSFederationHttpSecurityMode.Message)]
		[ServiceModelEnumValidator(typeof(WSFederationHttpSecurityModeHelper))]
		public WSFederationHttpSecurityMode Mode
		{
			get
			{
				return (WSFederationHttpSecurityMode)base["mode"];
			}
			set
			{
				base["mode"] = value;
			}
		}

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06004297 RID: 17047 RVA: 0x000FBE38 File Offset: 0x000FA038
		[ConfigurationProperty("message")]
		public FederatedMessageSecurityOverHttpElement Message
		{
			get
			{
				return (FederatedMessageSecurityOverHttpElement)base["message"];
			}
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x000FBE4A File Offset: 0x000FA04A
		internal void ApplyConfiguration(WSFederationHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.Mode = this.Mode;
			this.Message.ApplyConfiguration(security.Message);
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x000FBE7C File Offset: 0x000FA07C
		internal void InitializeFrom(WSFederationHttpSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<WSFederationHttpSecurityMode>("mode", security.Mode);
			this.Message.InitializeFrom(security.Message);
		}

		// Token: 0x04002D05 RID: 11525
		private ConfigurationPropertyCollection properties;
	}
}
