using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000053 RID: 83
	public class UdpAnnouncementEndpoint : AnnouncementEndpoint
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000C6CA File Offset: 0x0000A8CA
		public UdpAnnouncementEndpoint() : this(UdpAnnouncementEndpoint.DefaultIPv4MulticastAddress)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000C6D7 File Offset: 0x0000A8D7
		public UdpAnnouncementEndpoint(string multicastAddress) : this(new Uri(multicastAddress))
		{
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000C6E5 File Offset: 0x0000A8E5
		public UdpAnnouncementEndpoint(Uri multicastAddress) : this(DiscoveryVersion.DefaultDiscoveryVersion, multicastAddress)
		{
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000C6F3 File Offset: 0x0000A8F3
		public UdpAnnouncementEndpoint(DiscoveryVersion discoveryVersion) : this(discoveryVersion, UdpAnnouncementEndpoint.DefaultIPv4MulticastAddress)
		{
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000C701 File Offset: 0x0000A901
		public UdpAnnouncementEndpoint(DiscoveryVersion discoveryVersion, string multicastAddress) : this(discoveryVersion, new Uri(multicastAddress))
		{
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000C710 File Offset: 0x0000A910
		public UdpAnnouncementEndpoint(DiscoveryVersion discoveryVersion, Uri multicastAddress) : base(discoveryVersion)
		{
			if (multicastAddress == null)
			{
				throw FxTrace.Exception.ArgumentNull("multicastAddress");
			}
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			this.Initialize(multicastAddress);
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000C74C File Offset: 0x0000A94C
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000C759 File Offset: 0x0000A959
		public Uri MulticastAddress
		{
			get
			{
				return this.viaBehavior.Via;
			}
			set
			{
				if (value == null)
				{
					throw FxTrace.Exception.ArgumentNull("value");
				}
				this.viaBehavior.Via = value;
				base.ListenUri = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000C787 File Offset: 0x0000A987
		[Obsolete("TranportSettings property in System.SerivceModel.Discovery.UdpAnnouncementEndpoint is obsolete. Consider using System.ServiceModel.Channels.UdpTransportBindingElement for setting the transport properties.")]
		public UdpTransportSettings TransportSettings
		{
			get
			{
				return this.udpTransportSettings;
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x0000C790 File Offset: 0x0000A990
		private void Initialize(Uri multicastAddress)
		{
			this.viaBehavior = new DiscoveryViaBehavior(multicastAddress);
			base.ListenUri = multicastAddress;
			TextMessageEncodingBindingElement textMessageEncodingBindingElement = new TextMessageEncodingBindingElement();
			textMessageEncodingBindingElement.MessageVersion = base.DiscoveryVersion.Implementation.MessageVersion;
			UdpTransportBindingElement udpTransportBindingElement = DiscoveryDefaults.Udp.CreateUdpTransportBindingElement();
			this.udpTransportSettings = new UdpTransportSettings(udpTransportBindingElement);
			CustomBinding customBinding = new CustomBinding();
			customBinding.Elements.Add(textMessageEncodingBindingElement);
			customBinding.Elements.Add(udpTransportBindingElement);
			base.MaxAnnouncementDelay = DiscoveryDefaults.Udp.AppMaxDelay;
			base.Address = new EndpointAddress(base.DiscoveryVersion.Implementation.DiscoveryAddress, new AddressHeader[0]);
			base.Binding = customBinding;
			base.Behaviors.Add(this.viaBehavior);
			base.Behaviors.Add(new UdpContractFilterBehavior());
		}

		// Token: 0x04000107 RID: 263
		public static readonly Uri DefaultIPv4MulticastAddress = DiscoveryDefaults.Udp.IPv4MulticastAddress;

		// Token: 0x04000108 RID: 264
		public static readonly Uri DefaultIPv6MulticastAddress = DiscoveryDefaults.Udp.IPv6MulticastAddress;

		// Token: 0x04000109 RID: 265
		private DiscoveryViaBehavior viaBehavior;

		// Token: 0x0400010A RID: 266
		private UdpTransportSettings udpTransportSettings;
	}
}
