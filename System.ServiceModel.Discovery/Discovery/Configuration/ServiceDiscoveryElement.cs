using System;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000BC RID: 188
	public sealed class ServiceDiscoveryElement : BehaviorExtensionElement
	{
		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00013890 File Offset: 0x00011A90
		[ConfigurationProperty("announcementEndpoints")]
		public AnnouncementChannelEndpointElementCollection AnnouncementEndpoints
		{
			get
			{
				return (AnnouncementChannelEndpointElementCollection)base["announcementEndpoints"];
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x000138A2 File Offset: 0x00011AA2
		public override Type BehaviorType
		{
			get
			{
				return typeof(ServiceDiscoveryBehavior);
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000138B0 File Offset: 0x00011AB0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("announcementEndpoints", typeof(AnnouncementChannelEndpointElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x000138F8 File Offset: 0x00011AF8
		protected internal override object CreateBehavior()
		{
			ServiceDiscoveryBehavior serviceDiscoveryBehavior = new ServiceDiscoveryBehavior();
			foreach (object obj in this.AnnouncementEndpoints)
			{
				ChannelEndpointElement channelEndpointElement = (ChannelEndpointElement)obj;
				if (string.IsNullOrEmpty(channelEndpointElement.Kind))
				{
					throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigAnnouncementEndpointMissingKind(typeof(AnnouncementEndpoint).FullName)));
				}
				ServiceEndpoint serviceEndpoint = ConfigLoader.LookupEndpoint(channelEndpointElement, null);
				if (serviceEndpoint == null)
				{
					throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigInvalidEndpointConfiguration(channelEndpointElement.Kind)));
				}
				AnnouncementEndpoint announcementEndpoint = serviceEndpoint as AnnouncementEndpoint;
				if (announcementEndpoint == null)
				{
					throw FxTrace.Exception.AsError(new InvalidOperationException(SR.DiscoveryConfigInvalidAnnouncementEndpoint(channelEndpointElement.Kind, serviceEndpoint.GetType().FullName, typeof(AnnouncementEndpoint).FullName)));
				}
				serviceDiscoveryBehavior.AnnouncementEndpoints.Add(announcementEndpoint);
			}
			return serviceDiscoveryBehavior;
		}

		// Token: 0x040001CF RID: 463
		private ConfigurationPropertyCollection properties;
	}
}
