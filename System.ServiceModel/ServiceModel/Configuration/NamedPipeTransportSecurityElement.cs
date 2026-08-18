using System;
using System.Configuration;
using System.Net.Security;
using System.ServiceModel.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200064D RID: 1613
	public sealed class NamedPipeTransportSecurityElement : ServiceModelConfigurationElement
	{
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06003E3B RID: 15931 RVA: 0x000ED454 File Offset: 0x000EB654
		// (set) Token: 0x06003E3C RID: 15932 RVA: 0x000ED466 File Offset: 0x000EB666
		[ConfigurationProperty("protectionLevel", DefaultValue = ProtectionLevel.EncryptAndSign)]
		[ServiceModelEnumValidator(typeof(ProtectionLevelHelper))]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return (ProtectionLevel)base["protectionLevel"];
			}
			set
			{
				base["protectionLevel"] = value;
			}
		}

		// Token: 0x06003E3D RID: 15933 RVA: 0x000ED479 File Offset: 0x000EB679
		internal void ApplyConfiguration(NamedPipeTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			security.ProtectionLevel = this.ProtectionLevel;
		}

		// Token: 0x06003E3E RID: 15934 RVA: 0x000ED49A File Offset: 0x000EB69A
		internal void InitializeFrom(NamedPipeTransportSecurity security)
		{
			if (security == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("security");
			}
			base.SetPropertyValueIfNotDefaultValue<ProtectionLevel>("protectionLevel", security.ProtectionLevel);
		}

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06003E3F RID: 15935 RVA: 0x000ED4C0 File Offset: 0x000EB6C0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("protectionLevel", typeof(ProtectionLevel), ProtectionLevel.EncryptAndSign, null, new ServiceModelEnumValidator(typeof(ProtectionLevelHelper)), ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C9F RID: 11423
		private ConfigurationPropertyCollection properties;
	}
}
