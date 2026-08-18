using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000858 RID: 2136
	internal abstract class TcpChannelListener : ConnectionOrientedTransportChannelListener
	{
		// Token: 0x06005016 RID: 20502 RVA: 0x00125C90 File Offset: 0x00123E90
		protected TcpChannelListener(TcpTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			this.listenBacklog = bindingElement.ListenBacklog;
			this.portSharingEnabled = bindingElement.PortSharingEnabled;
			this.teredoEnabled = bindingElement.TeredoEnabled;
			this.extendedProtectionPolicy = bindingElement.ExtendedProtectionPolicy;
			base.SetIdleTimeout(bindingElement.ConnectionPoolSettings.IdleTimeout);
			base.InitializeMaxPooledConnections(bindingElement.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint);
			if (!bindingElement.PortSharingEnabled && context.ListenUriMode == ListenUriMode.Unique)
			{
				this.SetupUniquePort(context);
			}
		}

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x06005017 RID: 20503 RVA: 0x00125D0F File Offset: 0x00123F0F
		public bool PortSharingEnabled
		{
			get
			{
				return this.portSharingEnabled;
			}
		}

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x06005018 RID: 20504 RVA: 0x00125D17 File Offset: 0x00123F17
		public bool TeredoEnabled
		{
			get
			{
				return this.teredoEnabled;
			}
		}

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x06005019 RID: 20505 RVA: 0x00125D1F File Offset: 0x00123F1F
		public int ListenBacklog
		{
			get
			{
				return this.listenBacklog;
			}
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x00125D27 File Offset: 0x00123F27
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(ExtendedProtectionPolicy))
			{
				return (T)((object)this.extendedProtectionPolicy);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x00125D58 File Offset: 0x00123F58
		internal Socket GetListenSocket(UriHostNameType ipHostNameType)
		{
			if (ipHostNameType == UriHostNameType.IPv4)
			{
				Socket result = this.ipv4ListenSocket;
				this.ipv4ListenSocket = null;
				return result;
			}
			Socket result2 = this.ipv6ListenSocket;
			this.ipv6ListenSocket = null;
			return result2;
		}

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x0600501C RID: 20508 RVA: 0x00125D88 File Offset: 0x00123F88
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeNetTcp;
			}
		}

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x0600501D RID: 20509 RVA: 0x00125D8F File Offset: 0x00123F8F
		internal static UriPrefixTable<ITransportManagerRegistration> StaticTransportManagerTable
		{
			get
			{
				return TcpChannelListener.transportManagerTable;
			}
		}

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x0600501E RID: 20510 RVA: 0x00125D96 File Offset: 0x00123F96
		internal override UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return TcpChannelListener.transportManagerTable;
			}
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x00125DA0 File Offset: 0x00123FA0
		internal static void FixIpv6Hostname(UriBuilder uriBuilder, Uri originalUri)
		{
			if (originalUri.HostNameType == UriHostNameType.IPv6)
			{
				string dnsSafeHost = originalUri.DnsSafeHost;
				uriBuilder.Host = "[" + dnsSafeHost + "]";
			}
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x00125DD4 File Offset: 0x00123FD4
		internal override ITransportManagerRegistration CreateTransportManagerRegistration()
		{
			Uri uri = base.BaseUri;
			if (!this.PortSharingEnabled)
			{
				UriBuilder uriBuilder = new UriBuilder(uri.Scheme, uri.Host, uri.Port);
				TcpChannelListener.FixIpv6Hostname(uriBuilder, uri);
				uri = uriBuilder.Uri;
			}
			return this.CreateTransportManagerRegistration(uri);
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x00125E1D File Offset: 0x0012401D
		internal override ITransportManagerRegistration CreateTransportManagerRegistration(Uri listenUri)
		{
			if (this.PortSharingEnabled)
			{
				return new SharedTcpTransportManager(listenUri, this);
			}
			return new ExclusiveTcpTransportManagerRegistration(listenUri, this);
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00125E38 File Offset: 0x00124038
		private Socket ListenAndBind(IPEndPoint localEndpoint)
		{
			Socket socket = new Socket(localEndpoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
			try
			{
				socket.Bind(localEndpoint);
			}
			catch (SocketException socketException)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(SocketConnectionListener.ConvertListenException(socketException, localEndpoint));
			}
			return socket;
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00125E80 File Offset: 0x00124080
		private void SetupUniquePort(BindingContext context)
		{
			IPAddress address = IPAddress.Any;
			IPAddress address2 = IPAddress.IPv6Any;
			bool flag = Socket.OSSupportsIPv4;
			bool flag2 = Socket.OSSupportsIPv6;
			if (this.Uri.HostNameType == UriHostNameType.IPv6)
			{
				flag = false;
				address2 = IPAddress.Parse(this.Uri.DnsSafeHost);
			}
			else if (this.Uri.HostNameType == UriHostNameType.IPv4)
			{
				flag2 = false;
				address = IPAddress.Parse(this.Uri.DnsSafeHost);
			}
			if (flag || flag2)
			{
				UriBuilder uriBuilder = new UriBuilder(context.ListenUriBaseAddress);
				int port = -1;
				if (!flag2)
				{
					this.ipv4ListenSocket = this.ListenAndBind(new IPEndPoint(address, 0));
					port = ((IPEndPoint)this.ipv4ListenSocket.LocalEndPoint).Port;
				}
				else if (!flag)
				{
					this.ipv6ListenSocket = this.ListenAndBind(new IPEndPoint(address2, 0));
					port = ((IPEndPoint)this.ipv6ListenSocket.LocalEndPoint).Port;
				}
				else
				{
					int[] array = new int[10];
					Random obj = TcpChannelListener.randomPortGenerator;
					lock (obj)
					{
						for (int i = 0; i < 10; i++)
						{
							array[i] = TcpChannelListener.randomPortGenerator.Next(49152, 65535);
						}
					}
					for (int j = 0; j < 10; j++)
					{
						port = array[j];
						try
						{
							this.ipv4ListenSocket = this.ListenAndBind(new IPEndPoint(address, port));
							this.ipv6ListenSocket = this.ListenAndBind(new IPEndPoint(address2, port));
							break;
						}
						catch (AddressAlreadyInUseException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
							if (this.ipv4ListenSocket != null)
							{
								this.ipv4ListenSocket.Close();
								this.ipv4ListenSocket = null;
							}
							this.ipv6ListenSocket = null;
						}
					}
					if (this.ipv4ListenSocket == null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new AddressAlreadyInUseException(SR.GetString("UniquePortNotAvailable")));
					}
				}
				uriBuilder.Port = port;
				base.SetUri(uriBuilder.Uri, context.ListenUriRelativeAddress);
				return;
			}
			if (this.Uri.HostNameType == UriHostNameType.IPv6)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("context", SR.GetString("TcpV6AddressInvalid", new object[]
				{
					this.Uri
				}));
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("context", SR.GetString("TcpV4AddressInvalid", new object[]
			{
				this.Uri
			}));
		}

		// Token: 0x0400319B RID: 12699
		private bool teredoEnabled;

		// Token: 0x0400319C RID: 12700
		private int listenBacklog;

		// Token: 0x0400319D RID: 12701
		private bool portSharingEnabled;

		// Token: 0x0400319E RID: 12702
		private Socket ipv4ListenSocket;

		// Token: 0x0400319F RID: 12703
		private Socket ipv6ListenSocket;

		// Token: 0x040031A0 RID: 12704
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x040031A1 RID: 12705
		private static Random randomPortGenerator = new Random(AppDomain.CurrentDomain.GetHashCode() | Environment.TickCount);

		// Token: 0x040031A2 RID: 12706
		private static UriPrefixTable<ITransportManagerRegistration> transportManagerTable = new UriPrefixTable<ITransportManagerRegistration>(true);
	}
}
