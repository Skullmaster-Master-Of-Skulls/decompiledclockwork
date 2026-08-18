using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B2 RID: 178
	public sealed class DiscoveryClientSettingsElement : ConfigurationElement
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x00012A44 File Offset: 0x00010C44
		[ConfigurationProperty("endpoint")]
		public ChannelEndpointElement DiscoveryEndpoint
		{
			get
			{
				return (ChannelEndpointElement)base["endpoint"];
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x00012A56 File Offset: 0x00010C56
		[ConfigurationProperty("findCriteria")]
		public FindCriteriaElement FindCriteria
		{
			get
			{
				return (FindCriteriaElement)base["findCriteria"];
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x00012BEC File Offset: 0x00010DEC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("endpoint", typeof(ChannelEndpointElement), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("findCriteria", typeof(FindCriteriaElement), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x040001C9 RID: 457
		private ConfigurationPropertyCollection properties;
	}
}
