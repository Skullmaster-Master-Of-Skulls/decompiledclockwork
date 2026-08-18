using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000B4 RID: 180
	public class DiscoveryEndpointElement : StandardEndpointElement
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x00012C58 File Offset: 0x00010E58
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x00012C6A File Offset: 0x00010E6A
		[ConfigurationProperty("maxResponseDelay", DefaultValue = "00:00:00")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public TimeSpan MaxResponseDelay
		{
			get
			{
				return (TimeSpan)base["maxResponseDelay"];
			}
			set
			{
				base["maxResponseDelay"] = value;
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x00012C7D File Offset: 0x00010E7D
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x00012C8F File Offset: 0x00010E8F
		[ConfigurationProperty("discoveryMode", DefaultValue = ServiceDiscoveryMode.Managed)]
		public ServiceDiscoveryMode DiscoveryMode
		{
			get
			{
				return (ServiceDiscoveryMode)base["discoveryMode"];
			}
			set
			{
				base["discoveryMode"] = value;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001244B File Offset: 0x0001064B
		// (set) Token: 0x0600074D RID: 1869 RVA: 0x0001245D File Offset: 0x0001065D
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

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00012CA2 File Offset: 0x00010EA2
		protected internal override Type EndpointType
		{
			get
			{
				return typeof(DiscoveryEndpoint);
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x0600074F RID: 1871 RVA: 0x00012CB0 File Offset: 0x00010EB0
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
							configurationPropertyCollection.Add(new ConfigurationProperty("maxResponseDelay", typeof(TimeSpan), TimeSpan.Zero, new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Zero, TimeSpan.MaxValue), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("discoveryVersion", typeof(DiscoveryVersion), DiscoveryVersion.DefaultDiscoveryVersion, new DiscoveryVersionConverter(), null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("discoveryMode", typeof(ServiceDiscoveryMode), ServiceDiscoveryMode.Managed, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00012D98 File Offset: 0x00010F98
		protected internal override ServiceEndpoint CreateServiceEndpoint(ContractDescription contractDescription)
		{
			return new DiscoveryEndpoint(this.DiscoveryVersion, this.DiscoveryMode);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00012DAC File Offset: 0x00010FAC
		protected internal override void InitializeFrom(ServiceEndpoint endpoint)
		{
			base.InitializeFrom(endpoint);
			DiscoveryEndpoint discoveryEndpoint = (DiscoveryEndpoint)endpoint;
			this.MaxResponseDelay = discoveryEndpoint.MaxResponseDelay;
			this.DiscoveryVersion = discoveryEndpoint.DiscoveryVersion;
			this.DiscoveryMode = discoveryEndpoint.DiscoveryMode;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0001257B File Offset: 0x0001077B
		protected override void OnInitializeAndValidate(ChannelEndpointElement channelEndpointElement)
		{
			if (!string.IsNullOrEmpty(channelEndpointElement.Contract))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigContractSpecified(channelEndpointElement.Kind)));
			}
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00012DEC File Offset: 0x00010FEC
		protected override void OnInitializeAndValidate(ServiceEndpointElement serviceEndpointElement)
		{
			if (!string.IsNullOrEmpty(serviceEndpointElement.Contract))
			{
				throw FxTrace.Exception.AsError(new ConfigurationErrorsException(SR.DiscoveryConfigContractSpecified(serviceEndpointElement.Kind)));
			}
			if (serviceEndpointElement.ElementInformation.Properties["isSystemEndpoint"].ValueOrigin == PropertyValueOrigin.Default)
			{
				serviceEndpointElement.IsSystemEndpoint = true;
			}
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00012E44 File Offset: 0x00011044
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement)
		{
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x00012E44 File Offset: 0x00011044
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement serviceEndpointElement)
		{
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x00012E50 File Offset: 0x00011050
		private void ApplyConfiguration(ServiceEndpoint endpoint)
		{
			DiscoveryEndpoint discoveryEndpoint = (DiscoveryEndpoint)endpoint;
			discoveryEndpoint.MaxResponseDelay = this.MaxResponseDelay;
		}

		// Token: 0x040001CA RID: 458
		private ConfigurationPropertyCollection properties;
	}
}
