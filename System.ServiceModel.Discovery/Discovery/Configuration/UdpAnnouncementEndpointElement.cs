using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery.Configuration
{
	// Token: 0x020000BE RID: 190
	public class UdpAnnouncementEndpointElement : AnnouncementEndpointElement
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x00013A10 File Offset: 0x00011C10
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x00013A18 File Offset: 0x00011C18
		[ConfigurationProperty("maxAnnouncementDelay", DefaultValue = "00:00:00.500")]
		[TypeConverter(typeof(TimeSpanOrInfiniteConverter))]
		[ServiceModelTimeSpanValidator(MinValueString = "00:00:00")]
		public new TimeSpan MaxAnnouncementDelay
		{
			get
			{
				return base.MaxAnnouncementDelay;
			}
			set
			{
				base.MaxAnnouncementDelay = value;
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x0600078C RID: 1932 RVA: 0x00013A21 File Offset: 0x00011C21
		// (set) Token: 0x0600078D RID: 1933 RVA: 0x00013A33 File Offset: 0x00011C33
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

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00013A5A File Offset: 0x00011C5A
		[ConfigurationProperty("transportSettings")]
		public UdpTransportSettingsElement TransportSettings
		{
			get
			{
				return (UdpTransportSettingsElement)base["transportSettings"];
			}
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x00013A6C File Offset: 0x00011C6C
		protected internal override Type EndpointType
		{
			get
			{
				return typeof(UdpAnnouncementEndpoint);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00013A78 File Offset: 0x00011C78
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
							configurationPropertyCollection.Remove("maxAnnouncementDelay");
							configurationPropertyCollection.Add(new ConfigurationProperty("maxAnnouncementDelay", typeof(TimeSpan), DiscoveryDefaults.Udp.AppMaxDelay, new TimeSpanOrInfiniteConverter(), new TimeSpanOrInfiniteValidator(TimeSpan.Zero, TimeSpan.MaxValue), ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("multicastAddress", typeof(Uri), UdpAnnouncementEndpoint.DefaultIPv4MulticastAddress, null, null, ConfigurationPropertyOptions.None));
							configurationPropertyCollection.Add(new ConfigurationProperty("transportSettings", typeof(UdpTransportSettingsElement), null, null, null, ConfigurationPropertyOptions.None));
							this.properties = configurationPropertyCollection;
						}
					}
				}
				return this.properties;
			}
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00013B64 File Offset: 0x00011D64
		protected internal override ServiceEndpoint CreateServiceEndpoint(ContractDescription contractDescription)
		{
			return new UdpAnnouncementEndpoint(base.DiscoveryVersion, this.MulticastAddress);
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00013B78 File Offset: 0x00011D78
		protected internal override void InitializeFrom(ServiceEndpoint endpoint)
		{
			base.InitializeFrom(endpoint);
			UdpAnnouncementEndpoint udpAnnouncementEndpoint = (UdpAnnouncementEndpoint)endpoint;
			this.MaxAnnouncementDelay = udpAnnouncementEndpoint.MaxAnnouncementDelay;
			this.MulticastAddress = udpAnnouncementEndpoint.MulticastAddress;
			this.TransportSettings.InitializeFrom(udpAnnouncementEndpoint.TransportSettings);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00013BBC File Offset: 0x00011DBC
		protected override void OnInitializeAndValidate(ChannelEndpointElement channelEndpointElement)
		{
			base.OnInitializeAndValidate(channelEndpointElement);
			ConfigurationUtility.InitializeAndValidateUdpChannelEndpointElement(channelEndpointElement);
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x00013BCB File Offset: 0x00011DCB
		protected override void OnInitializeAndValidate(ServiceEndpointElement serviceEndpointElement)
		{
			base.OnInitializeAndValidate(serviceEndpointElement);
			ConfigurationUtility.InitializeAndValidateUdpServiceEndpointElement(serviceEndpointElement);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00013BDA File Offset: 0x00011DDA
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ServiceEndpointElement serviceEndpointElement)
		{
			base.OnApplyConfiguration(endpoint, serviceEndpointElement);
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00013BEB File Offset: 0x00011DEB
		protected override void OnApplyConfiguration(ServiceEndpoint endpoint, ChannelEndpointElement serviceEndpointElement)
		{
			base.OnApplyConfiguration(endpoint, serviceEndpointElement);
			this.ApplyConfiguration(endpoint);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00013BFC File Offset: 0x00011DFC
		private void ApplyConfiguration(ServiceEndpoint endpoint)
		{
			UdpAnnouncementEndpoint udpAnnouncementEndpoint = (UdpAnnouncementEndpoint)endpoint;
			udpAnnouncementEndpoint.MulticastAddress = this.MulticastAddress;
			this.TransportSettings.ApplyConfiguration(udpAnnouncementEndpoint.TransportSettings);
		}

		// Token: 0x040001D0 RID: 464
		private ConfigurationPropertyCollection properties;
	}
}
