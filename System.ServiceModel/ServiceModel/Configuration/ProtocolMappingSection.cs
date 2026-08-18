using System;
using System.Configuration;
using System.Security;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200068A RID: 1674
	public sealed class ProtocolMappingSection : ConfigurationSection
	{
		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x060040B2 RID: 16562 RVA: 0x000F5860 File Offset: 0x000F3A60
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("", typeof(ProtocolMappingElementCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x060040B4 RID: 16564 RVA: 0x000F58AE File Offset: 0x000F3AAE
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public ProtocolMappingElementCollection ProtocolMappingCollection
		{
			get
			{
				return (ProtocolMappingElementCollection)base[""];
			}
		}

		// Token: 0x060040B5 RID: 16565 RVA: 0x000F58C0 File Offset: 0x000F3AC0
		protected override void InitializeDefault()
		{
			this.ProtocolMappingCollection.Add(new ProtocolMappingElement("http", "basicHttpBinding", ""));
			this.ProtocolMappingCollection.Add(new ProtocolMappingElement("net.tcp", "netTcpBinding", ""));
			this.ProtocolMappingCollection.Add(new ProtocolMappingElement("net.pipe", "netNamedPipeBinding", ""));
			this.ProtocolMappingCollection.Add(new ProtocolMappingElement("net.msmq", "netMsmqBinding", ""));
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x000F5949 File Offset: 0x000F3B49
		internal static ProtocolMappingSection GetSection()
		{
			return (ProtocolMappingSection)ConfigurationHelpers.GetSection(ConfigurationStrings.ProtocolMappingSectionPath);
		}

		// Token: 0x060040B7 RID: 16567 RVA: 0x000F595A File Offset: 0x000F3B5A
		[SecurityCritical]
		internal static ProtocolMappingSection UnsafeGetSection()
		{
			return (ProtocolMappingSection)ConfigurationHelpers.UnsafeGetSection(ConfigurationStrings.ProtocolMappingSectionPath);
		}

		// Token: 0x04002CD7 RID: 11479
		private ConfigurationPropertyCollection properties;
	}
}
