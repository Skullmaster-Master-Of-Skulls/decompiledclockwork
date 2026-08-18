using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000AB RID: 171
	public class AnnouncementEndpointElement : StandardEndpointElement
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x00012426 File Offset: 0x00010626
		// (set) Token: 0x0600071A RID: 1818 RVA: 0x00012438 File Offset: 0x00010638
		[ConfigurationProperty("maxAnnouncementDelay", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaxAnnouncementDelay
		{
			get
			{
				return (TimeSpan)base["maxAnnouncementDelay"];
			}
			set
			{
				base["maxAnnouncementDelay"] = value;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001244B File Offset: 0x0001064B
		// (set) Token: 0x0600071C RID: 1820 RVA: 0x0001245D File Offset: 0x0001065D
		[ConfigurationProperty("discoveryVersion", DefaultValue = "WSDiscovery11")]
		[TypeConverter(typeof(DiscoveryVersionConverter))]
		public DiscoveryVersion DiscoveryVersion
		{
			get
			{
				return (DiscoveryVersion)base["discoveryVersion"];
			}
			set
			{
				base["discoveryVersion"] = value;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001246B File Offset: 0x0001066B
		protected internal override Type EndpointType
		{
			get
			{
				return typeof(AnnouncementEndpoint);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x00012478 File Offset: 0x00010678
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					object lockObj = this.lockObj;
					lock (lockObj)
					{
						if (this.properties == null)
						{
							ConfigurationPropertyCollection configurationPropertyCollection = base.Properties;
							configurationPropertyCollection.Add(new ConfigurationProperty("maxAnnouncementDelay", typeof(TimeSpan), TimeSpan.Zero, new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Zero, TimeSpan.MaxValue), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("discoveryVersion", typeof(DiscoveryVersion), DiscoveryVersion.DefaultDiscoveryVersion, new DiscoveryVersionConverter(), null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00012538 File Offset: 0x00010738
		protected internal override ServiceEndpoint CreateServiceEndpoint(ContractDescription contractDescription)
		{
			return new AnnouncementEndpoint(this.DiscoveryVersion);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00012548 File Offset: 0x00010748
		protected internal override void InitializeFrom(ServiceEndpoint endpoint)
		{
			base.InitializeFrom(endpoint);
			AnnouncementEndpoint announcementEndpoint = (AnnouncementEndpoint)endpoint;
			this.MaxAnnouncementDelay = announcementEndpoint.MaxAnnouncementDelay;
			this.DiscoveryVersion = announcementEndpoint.DiscoveryVersion;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001257B File Offset: 0x0001077B
		protected override void OnInitializeAndValidate(ChannelEndpointElement channelEndpointElement)
		{
			if (!string.IsNullOrEmpty(channelEndpointElement.Contract))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigContractSpecified(channelEndpointElement.Kind)));
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x000125A5 File Offset: 0x000107A5
		protected override void OnInitializeAndValidate(ServiceEndpointElement serviceEndpointElement)
		{
			if (!string.IsNullOrEmpty(serviceEndpointElement.Contract))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigContractSpecified(serviceEndpointElement.Kind)));
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000125CF File Offset: 0x000107CF
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement)
		{
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000125CF File Offset: 0x000107CF
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement serviceEndpointElement)
		{
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000125D8 File Offset: 0x000107D8
		private void ApplyConfiguration(ServiceEndpoint endpoint)
		{
			AnnouncementEndpoint announcementEndpoint = (AnnouncementEndpoint)endpoint;
			announcementEndpoint.MaxAnnouncementDelay = this.MaxAnnouncementDelay;
		}

		// Token: 0x040001A4 RID: 420
		private ConfigurationPropertyCollection properties;
	}
}
