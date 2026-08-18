using System;
using System.Net.Sockets;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000055 RID: 85
	public class UdpDiscoveryEndpoint : DiscoveryEndpoint
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000C8CA File Offset: 0x0000AACA
		public UdpDiscoveryEndpoint() : this(UdpDiscoveryEndpoint.GetDefaultMulticastAddress())
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000C8D7 File Offset: 0x0000AAD7
		public UdpDiscoveryEndpoint(string multicastAddress) : this(new Uri(multicastAddress))
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000C8E5 File Offset: 0x0000AAE5
		public UdpDiscoveryEndpoint(Uri multicastAddress) : this(DiscoveryVersion.DefaultDiscoveryVersion, multicastAddress)
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000C8F3 File Offset: 0x0000AAF3
		public UdpDiscoveryEndpoint(DiscoveryVersion discoveryVersion) : this(discoveryVersion, UdpDiscoveryEndpoint.GetDefaultMulticastAddress())
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000C901 File Offset: 0x0000AB01
		public UdpDiscoveryEndpoint(DiscoveryVersion discoveryVersion, string multicastAddress) : this(discoveryVersion, new Uri(multicastAddress))
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000C910 File Offset: 0x0000AB10
		public UdpDiscoveryEndpoint(DiscoveryVersion discoveryVersion, Uri multicastAddress) : base(discoveryVersion, ServiceDiscoveryMode.Adhoc)
		{
			if (multicastAddress == null)
			{
				throw FxTrace.Exception.ArgumentNull("multicastAddress");
			}
			if (discoveryVersion == null)
			{
				throw FxTrace.Exception.ArgumentNull("discoveryVersion");
			}
			base.Behaviors.Add(new DispatcherSynchronizationBehavior
			{
				AsynchronousSendEnabled = true
			});
			this.Initialize(multicastAddress);
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000C96F File Offset: 0x0000AB6F
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000C97C File Offset: 0x0000AB7C
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

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000C9AA File Offset: 0x0000ABAA
		[Obsolete("TranportSettings property in System.SerivceModel.Discovery.UdpDiscoveryEndpoint is obsolete. Consider using System.ServiceModel.Channels.UdpTransportBindingElement for setting the transport properties.")]
		public UdpTransportSettings TransportSettings
		{
			get
			{
				return this.udpTransportSettings;
			}
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000C9B4 File Offset: 0x0000ABB4
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
			base.MaxResponseDelay = DiscoveryDefaults.Udp.AppMaxDelay;
			base.Address = new EndpointAddress(base.DiscoveryVersion.Implementation.DiscoveryAddress, new AddressHeader[0]);
			base.Binding = customBinding;
			base.Behaviors.Add(this.viaBehavior);
			base.Behaviors.Add(new UdpReplyToBehavior(udpTransportBindingElement.Scheme));
			base.Behaviors.Add(new UdpContractFilterBehavior());
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000CA8A File Offset: 0x0000AC8A
		private static Uri GetDefaultMulticastAddress()
		{
			if (!Socket.OSSupportsIPv4)
			{
				return UdpDiscoveryEndpoint.DefaultIPv6MulticastAddress;
			}
			return UdpDiscoveryEndpoint.DefaultIPv4MulticastAddress;
		}

		// Token: 0x0400010B RID: 267
		public static readonly Uri DefaultIPv4MulticastAddress = DiscoveryDefaults.Udp.IPv4MulticastAddress;

		// Token: 0x0400010C RID: 268
		public static readonly Uri DefaultIPv6MulticastAddress = DiscoveryDefaults.Udp.IPv6MulticastAddress;

		// Token: 0x0400010D RID: 269
		private DiscoveryViaBehavior viaBehavior;

		// Token: 0x0400010E RID: 270
		private UdpTransportSettings udpTransportSettings;
	}
}
