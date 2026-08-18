using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x0200014F RID: 335
	[__DynamicallyInvokable]
	public sealed class NetTcpSecurity
	{
		// Token: 0x060009C0 RID: 2496 RVA: 0x00025FD3 File Offset: 0x000241D3
		[__DynamicallyInvokable]
		public NetTcpSecurity() : this(SecurityMode.Transport, new TcpTransportSecurity(), new MessageSecurityOverTcp())
		{
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00025FE6 File Offset: 0x000241E6
		private NetTcpSecurity(SecurityMode mode, TcpTransportSecurity transportSecurity, MessageSecurityOverTcp messageSecurity)
		{
			this.mode = mode;
			this.transportSecurity = ((transportSecurity == null) ? new TcpTransportSecurity() : transportSecurity);
			this.messageSecurity = ((messageSecurity == null) ? new MessageSecurityOverTcp() : messageSecurity);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009C2 RID: 2498 RVA: 0x00026017 File Offset: 0x00024217
		// (set) Token: 0x060009C3 RID: 2499 RVA: 0x0002601F File Offset: 0x0002421F
		[DefaultValue(SecurityMode.Transport)]
		[__DynamicallyInvokable]
		public SecurityMode Mode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.mode;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!SecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009C4 RID: 2500 RVA: 0x00026045 File Offset: 0x00024245
		// (set) Token: 0x060009C5 RID: 2501 RVA: 0x0002604D File Offset: 0x0002424D
		[__DynamicallyInvokable]
		public TcpTransportSecurity Transport
		{
			[__DynamicallyInvokable]
			get
			{
				return this.transportSecurity;
			}
			[__DynamicallyInvokable]
			set
			{
				this.transportSecurity = value;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009C6 RID: 2502 RVA: 0x00026056 File Offset: 0x00024256
		// (set) Token: 0x060009C7 RID: 2503 RVA: 0x0002605E File Offset: 0x0002425E
		[__DynamicallyInvokable]
		public MessageSecurityOverTcp Message
		{
			[__DynamicallyInvokable]
			get
			{
				return this.messageSecurity;
			}
			[__DynamicallyInvokable]
			set
			{
				this.messageSecurity = value;
			}
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x00026067 File Offset: 0x00024267
		internal BindingElement CreateTransportSecurity()
		{
			if (this.mode == SecurityMode.TransportWithMessageCredential)
			{
				return this.transportSecurity.CreateTransportProtectionOnly();
			}
			if (this.mode == SecurityMode.Transport)
			{
				return this.transportSecurity.CreateTransportProtectionAndAuthentication();
			}
			return null;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x00026094 File Offset: 0x00024294
		internal static UnifiedSecurityMode GetModeFromTransportSecurity(BindingElement transport)
		{
			if (transport == null)
			{
				return UnifiedSecurityMode.None | UnifiedSecurityMode.Message;
			}
			return UnifiedSecurityMode.Transport | UnifiedSecurityMode.TransportWithMessageCredential;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0002609E File Offset: 0x0002429E
		internal static bool SetTransportSecurity(BindingElement transport, SecurityMode mode, TcpTransportSecurity transportSecurity)
		{
			if (mode == SecurityMode.TransportWithMessageCredential)
			{
				return TcpTransportSecurity.SetTransportProtectionOnly(transport, transportSecurity);
			}
			if (mode == SecurityMode.Transport)
			{
				return TcpTransportSecurity.SetTransportProtectionAndAuthentication(transport, transportSecurity);
			}
			return transport == null;
		}

		// Token: 0x060009CB RID: 2507 RVA: 0x000260BC File Offset: 0x000242BC
		internal SecurityBindingElement CreateMessageSecurity(bool isReliableSessionEnabled)
		{
			if (this.mode == SecurityMode.Message)
			{
				return this.messageSecurity.CreateSecurityBindingElement(false, isReliableSessionEnabled, null);
			}
			if (this.mode == SecurityMode.TransportWithMessageCredential)
			{
				return this.messageSecurity.CreateSecurityBindingElement(true, isReliableSessionEnabled, this.CreateTransportSecurity());
			}
			return null;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x000260F4 File Offset: 0x000242F4
		internal static bool TryCreate(SecurityBindingElement wsSecurity, SecurityMode mode, bool isReliableSessionEnabled, BindingElement transportSecurity, TcpTransportSecurity tcpTransportSecurity, out NetTcpSecurity security)
		{
			security = null;
			MessageSecurityOverTcp messageSecurityOverTcp = null;
			if (mode == SecurityMode.Message)
			{
				if (!MessageSecurityOverTcp.TryCreate(wsSecurity, isReliableSessionEnabled, null, out messageSecurityOverTcp))
				{
					return false;
				}
			}
			else if (mode == SecurityMode.TransportWithMessageCredential && !MessageSecurityOverTcp.TryCreate(wsSecurity, isReliableSessionEnabled, transportSecurity, out messageSecurityOverTcp))
			{
				return false;
			}
			security = new NetTcpSecurity(mode, tcpTransportSecurity, messageSecurityOverTcp);
			return SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(isReliableSessionEnabled), wsSecurity, false);
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x00026147 File Offset: 0x00024347
		internal bool InternalShouldSerialize()
		{
			return this.Mode != SecurityMode.Transport || this.Transport.InternalShouldSerialize() || this.Message.InternalShouldSerialize();
		}

		// Token: 0x04000B84 RID: 2948
		internal const SecurityMode DefaultMode = SecurityMode.Transport;

		// Token: 0x04000B85 RID: 2949
		private SecurityMode mode;

		// Token: 0x04000B86 RID: 2950
		private TcpTransportSecurity transportSecurity;

		// Token: 0x04000B87 RID: 2951
		private MessageSecurityOverTcp messageSecurity;
	}
}
