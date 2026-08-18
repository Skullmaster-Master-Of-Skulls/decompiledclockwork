using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000C0 RID: 192
	public class UdpDiscoveryEndpointElement : DiscoveryEndpointElement
	{
		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600079A RID: 1946 RVA: 0x00013C3D File Offset: 0x00011E3D
		// (set) Token: 0x0600079B RID: 1947 RVA: 0x00013C45 File Offset: 0x00011E45
		[ConfigurationProperty("maxResponseDelay", DefaultValue = "00:00:00.500")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public new TimeSpan MaxResponseDelay
		{
			get
			{
				return base.MaxResponseDelay;
			}
			set
			{
				base.MaxResponseDelay = value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600079C RID: 1948 RVA: 0x00013C4E File Offset: 0x00011E4E
		// (set) Token: 0x0600079D RID: 1949 RVA: 0x00013C56 File Offset: 0x00011E56
		[ConfigurationProperty("discoveryMode", DefaultValue = ServiceDiscoveryMode.Adhoc)]
		public new ServiceDiscoveryMode DiscoveryMode
		{
			get
			{
				return base.DiscoveryMode;
			}
			set
			{
				base.DiscoveryMode = value;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00013A21 File Offset: 0x00011C21
		// (set) Token: 0x0600079F RID: 1951 RVA: 0x00013A33 File Offset: 0x00011C33
		[ConfigurationProperty("multicastAddress", DefaultValue = "soap.udp://239.255.255.250:3702")]
		public Uri MulticastAddress
		{
			get
			{
				return (Uri)base["multicastAddress"];
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				base["multicastAddress"] = value;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00013A5A File Offset: 0x00011C5A
		[ConfigurationProperty("transportSettings")]
		public UdpTransportSettingsElement TransportSettings
		{
			get
			{
				return (UdpTransportSettingsElement)base["transportSettings"];
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x00013C5F File Offset: 0x00011E5F
		protected internal override Type EndpointType
		{
			get
			{
				return typeof(UdpDiscoveryEndpoint);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00013C6C File Offset: 0x00011E6C
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
							configurationPropertyCollection.Remove("discoveryMode");
							configurationPropertyCollection.Remove("maxResponseDelay");
							configurationPropertyCollection.Add(new ConfigurationProperty("maxResponseDelay", typeof(TimeSpan), DiscoveryDefaults.Udp.AppMaxDelay, new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Zero, TimeSpan.MaxValue), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("discoveryMode", typeof(ServiceDiscoveryMode), ServiceDiscoveryMode.Adhoc, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("multicastAddress", typeof(Uri), UdpDiscoveryEndpoint.DefaultIPv4MulticastAddress, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transportSettings", typeof(UdpTransportSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00013D84 File Offset: 0x00011F84
		protected internal override ServiceEndpoint CreateServiceEndpoint(ContractDescription contractDescription)
		{
			return new UdpDiscoveryEndpoint(base.DiscoveryVersion, this.MulticastAddress);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00013D98 File Offset: 0x00011F98
		protected internal override void InitializeFrom(ServiceEndpoint endpoint)
		{
			base.InitializeFrom(endpoint);
			UdpDiscoveryEndpoint udpDiscoveryEndpoint = (UdpDiscoveryEndpoint)endpoint;
			this.MaxResponseDelay = udpDiscoveryEndpoint.MaxResponseDelay;
			this.DiscoveryMode = udpDiscoveryEndpoint.DiscoveryMode;
			this.MulticastAddress = udpDiscoveryEndpoint.MulticastAddress;
			this.TransportSettings.InitializeFrom(udpDiscoveryEndpoint.TransportSettings);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00013DE8 File Offset: 0x00011FE8
		protected override void OnInitializeAndValidate(ChannelEndpointElement channelEndpointElement)
		{
			base.OnInitializeAndValidate(channelEndpointElement);
			ConfigurationUtility.InitializeAndValidateUdpChannelEndpointElement(channelEndpointElement);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00013DF7 File Offset: 0x00011FF7
		protected override void OnInitializeAndValidate(ServiceEndpointElement serviceEndpointElement)
		{
			base.OnInitializeAndValidate(serviceEndpointElement);
			ConfigurationUtility.InitializeAndValidateUdpServiceEndpointElement(serviceEndpointElement);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00013E06 File Offset: 0x00012006
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement)
		{
			base.OnApplyConfiguration(endpoint, serviceEndpointElement);
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00013E17 File Offset: 0x00012017
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement serviceEndpointElement)
		{
			base.OnApplyConfiguration(endpoint, serviceEndpointElement);
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00013E28 File Offset: 0x00012028
		private void ApplyConfiguration(ServiceEndpoint endpoint)
		{
			UdpDiscoveryEndpoint udpDiscoveryEndpoint = (UdpDiscoveryEndpoint)endpoint;
			udpDiscoveryEndpoint.MulticastAddress = this.MulticastAddress;
			this.TransportSettings.ApplyConfiguration(udpDiscoveryEndpoint.TransportSettings);
		}

		// Token: 0x040001D1 RID: 465
		private ConfigurationPropertyCollection properties;
	}
}
