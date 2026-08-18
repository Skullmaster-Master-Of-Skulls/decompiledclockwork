using System;
using System.Security.Permissions;

namespace System.Net.Sockets
{
	// Token: 0x020005CC RID: 1484
	public class UdpClient : IDisposable
	{
		// Token: 0x06002E79 RID: 11897 RVA: 0x000CCEB2 File Offset: 0x000CBEB2
		public UdpClient() : this(AddressFamily.InterNetwork)
		{
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000CCEBC File Offset: 0x000CBEBC
		public UdpClient(AddressFamily family)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(SR.GetString("net_protocol_invalid_family", new object[]
				{
					"UDP"
				}), "family");
			}
			this.m_Family = family;
			this.createClientSocket();
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000CCF21 File Offset: 0x000CBF21
		public UdpClient(int port) : this(port, AddressFamily.InterNetwork)
		{
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000CCF2C File Offset: 0x000CBF2C
		public UdpClient(int port, AddressFamily family)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (family != AddressFamily.InterNetwork && family != AddressFamily.InterNetworkV6)
			{
				throw new ArgumentException(SR.GetString("net_protocol_invalid_family"), "family");
			}
			this.m_Family = family;
			IPEndPoint localEP;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				localEP = new IPEndPoint(IPAddress.Any, port);
			}
			else
			{
				localEP = new IPEndPoint(IPAddress.IPv6Any, port);
			}
			this.createClientSocket();
			this.Client.Bind(localEP);
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x000CCFC4 File Offset: 0x000CBFC4
		public UdpClient(IPEndPoint localEP)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (localEP == null)
			{
				throw new ArgumentNullException("localEP");
			}
			this.m_Family = localEP.AddressFamily;
			this.createClientSocket();
			this.Client.Bind(localEP);
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000CD01C File Offset: 0x000CC01C
		public UdpClient(string hostname, int port)
		{
			this.m_Buffer = new byte[65536];
			this.m_Family = AddressFamily.InterNetwork;
			base..ctor();
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.Connect(hostname, port);
		}

		// Token: 0x170009C3 RID: 2499
		// (get) Token: 0x06002E7F RID: 11903 RVA: 0x000CD06F File Offset: 0x000CC06F
		// (set) Token: 0x06002E80 RID: 11904 RVA: 0x000CD077 File Offset: 0x000CC077
		public Socket Client
		{
			get
			{
				return this.m_ClientSocket;
			}
			set
			{
				this.m_ClientSocket = value;
			}
		}

		// Token: 0x170009C4 RID: 2500
		// (get) Token: 0x06002E81 RID: 11905 RVA: 0x000CD080 File Offset: 0x000CC080
		// (set) Token: 0x06002E82 RID: 11906 RVA: 0x000CD088 File Offset: 0x000CC088
		protected bool Active
		{
			get
			{
				return this.m_Active;
			}
			set
			{
				this.m_Active = value;
			}
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002E83 RID: 11907 RVA: 0x000CD091 File Offset: 0x000CC091
		public int Available
		{
			get
			{
				return this.m_ClientSocket.Available;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002E84 RID: 11908 RVA: 0x000CD09E File Offset: 0x000CC09E
		// (set) Token: 0x06002E85 RID: 11909 RVA: 0x000CD0AB File Offset: 0x000CC0AB
		public short Ttl
		{
			get
			{
				return this.m_ClientSocket.Ttl;
			}
			set
			{
				this.m_ClientSocket.Ttl = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002E86 RID: 11910 RVA: 0x000CD0B9 File Offset: 0x000CC0B9
		// (set) Token: 0x06002E87 RID: 11911 RVA: 0x000CD0C6 File Offset: 0x000CC0C6
		public bool DontFragment
		{
			get
			{
				return this.m_ClientSocket.DontFragment;
			}
			set
			{
				this.m_ClientSocket.DontFragment = value;
			}
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06002E88 RID: 11912 RVA: 0x000CD0D4 File Offset: 0x000CC0D4
		// (set) Token: 0x06002E89 RID: 11913 RVA: 0x000CD0E1 File Offset: 0x000CC0E1
		public bool MulticastLoopback
		{
			get
			{
				return this.m_ClientSocket.MulticastLoopback;
			}
			set
			{
				this.m_ClientSocket.MulticastLoopback = value;
			}
		}

		// Token: 0x170009C9 RID: 2505
		// (get) Token: 0x06002E8A RID: 11914 RVA: 0x000CD0EF File Offset: 0x000CC0EF
		// (set) Token: 0x06002E8B RID: 11915 RVA: 0x000CD0FC File Offset: 0x000CC0FC
		public bool EnableBroadcast
		{
			get
			{
				return this.m_ClientSocket.EnableBroadcast;
			}
			set
			{
				this.m_ClientSocket.EnableBroadcast = value;
			}
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002E8C RID: 11916 RVA: 0x000CD10A File Offset: 0x000CC10A
		// (set) Token: 0x06002E8D RID: 11917 RVA: 0x000CD117 File Offset: 0x000CC117
		public bool ExclusiveAddressUse
		{
			get
			{
				return this.m_ClientSocket.ExclusiveAddressUse;
			}
			set
			{
				this.m_ClientSocket.ExclusiveAddressUse = value;
			}
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x000CD125 File Offset: 0x000CC125
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x000CD130 File Offset: 0x000CC130
		private void FreeResources()
		{
			if (this.m_CleanedUp)
			{
				return;
			}
			Socket client = this.Client;
			if (client != null)
			{
				client.InternalShutdown(SocketShutdown.Both);
				client.Close();
				this.Client = null;
			}
			this.m_CleanedUp = true;
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x000CD16B File Offset: 0x000CC16B
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06002E91 RID: 11921 RVA: 0x000CD174 File Offset: 0x000CC174
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.FreeResources();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06002E92 RID: 11922 RVA: 0x000CD188 File Offset: 0x000CC188
		public void Connect(string hostname, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (hostname == null)
			{
				throw new ArgumentNullException("hostname");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			Exception ex = null;
			Socket socket = null;
			Socket socket2 = null;
			try
			{
				if (this.m_ClientSocket == null)
				{
					if (Socket.SupportsIPv4)
					{
						socket2 = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
					}
					if (Socket.OSSupportsIPv6)
					{
						socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
					}
				}
				foreach (IPAddress ipaddress in hostAddresses)
				{
					try
					{
						if (this.m_ClientSocket == null)
						{
							if (ipaddress.AddressFamily == AddressFamily.InterNetwork && socket2 != null)
							{
								socket2.Connect(ipaddress, port);
								this.m_ClientSocket = socket2;
								if (socket != null)
								{
									socket.Close();
								}
							}
							else if (socket != null)
							{
								socket.Connect(ipaddress, port);
								this.m_ClientSocket = socket;
								if (socket2 != null)
								{
									socket2.Close();
								}
							}
							this.m_Family = ipaddress.AddressFamily;
							this.m_Active = true;
							break;
						}
						if (ipaddress.AddressFamily == this.m_Family)
						{
							this.Connect(new IPEndPoint(ipaddress, port));
							this.m_Active = true;
							break;
						}
					}
					catch (Exception ex2)
					{
						if (NclUtilities.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
				}
			}
			catch (Exception ex3)
			{
				if (NclUtilities.IsFatal(ex3))
				{
					throw;
				}
				ex = ex3;
			}
			finally
			{
				if (!this.m_Active)
				{
					if (socket != null)
					{
						socket.Close();
					}
					if (socket2 != null)
					{
						socket2.Close();
					}
					if (ex != null)
					{
						throw ex;
					}
					throw new SocketException(SocketError.NotConnected);
				}
			}
		}

		// Token: 0x06002E93 RID: 11923 RVA: 0x000CD354 File Offset: 0x000CC354
		public void Connect(IPAddress addr, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (addr == null)
			{
				throw new ArgumentNullException("addr");
			}
			if (!ValidationHelper.ValidateTcpPort(port))
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IPEndPoint endPoint = new IPEndPoint(addr, port);
			this.Connect(endPoint);
		}

		// Token: 0x06002E94 RID: 11924 RVA: 0x000CD3AC File Offset: 0x000CC3AC
		public void Connect(IPEndPoint endPoint)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (endPoint == null)
			{
				throw new ArgumentNullException("endPoint");
			}
			this.CheckForBroadcast(endPoint.Address);
			this.Client.Connect(endPoint);
			this.m_Active = true;
		}

		// Token: 0x06002E95 RID: 11925 RVA: 0x000CD3FF File Offset: 0x000CC3FF
		private void CheckForBroadcast(IPAddress ipAddress)
		{
			if (this.Client != null && !this.m_IsBroadcast && ipAddress.IsBroadcast)
			{
				this.m_IsBroadcast = true;
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
			}
		}

		// Token: 0x06002E96 RID: 11926 RVA: 0x000CD434 File Offset: 0x000CC434
		public int Send(byte[] dgram, int bytes, IPEndPoint endPoint)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (this.m_Active && endPoint != null)
			{
				throw new InvalidOperationException(SR.GetString("net_udpconnected"));
			}
			if (endPoint == null)
			{
				return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
			}
			this.CheckForBroadcast(endPoint.Address);
			return this.Client.SendTo(dgram, 0, bytes, SocketFlags.None, endPoint);
		}

		// Token: 0x06002E97 RID: 11927 RVA: 0x000CD4B4 File Offset: 0x000CC4B4
		public int Send(byte[] dgram, int bytes, string hostname, int port)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (this.m_Active && (hostname != null || port != 0))
			{
				throw new InvalidOperationException(SR.GetString("net_udpconnected"));
			}
			if (hostname == null || port == 0)
			{
				return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
			}
			IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
			int num = 0;
			while (num < hostAddresses.Length && hostAddresses[num].AddressFamily != this.m_Family)
			{
				num++;
			}
			if (hostAddresses.Length == 0 || num == hostAddresses.Length)
			{
				throw new ArgumentException(SR.GetString("net_invalidAddressList"), "hostname");
			}
			this.CheckForBroadcast(hostAddresses[num]);
			IPEndPoint remoteEP = new IPEndPoint(hostAddresses[num], port);
			return this.Client.SendTo(dgram, 0, bytes, SocketFlags.None, remoteEP);
		}

		// Token: 0x06002E98 RID: 11928 RVA: 0x000CD588 File Offset: 0x000CC588
		public int Send(byte[] dgram, int bytes)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (dgram == null)
			{
				throw new ArgumentNullException("dgram");
			}
			if (!this.m_Active)
			{
				throw new InvalidOperationException(SR.GetString("net_notconnected"));
			}
			return this.Client.Send(dgram, 0, bytes, SocketFlags.None);
		}

		// Token: 0x06002E99 RID: 11929 RVA: 0x000CD5E4 File Offset: 0x000CC5E4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, IPEndPoint endPoint, AsyncCallback requestCallback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (datagram == null)
			{
				throw new ArgumentNullException("datagram");
			}
			if (bytes > datagram.Length)
			{
				throw new ArgumentOutOfRangeException("bytes");
			}
			if (this.m_Active && endPoint != null)
			{
				throw new InvalidOperationException(SR.GetString("net_udpconnected"));
			}
			if (endPoint == null)
			{
				return this.Client.BeginSend(datagram, 0, bytes, SocketFlags.None, requestCallback, state);
			}
			this.CheckForBroadcast(endPoint.Address);
			return this.Client.BeginSendTo(datagram, 0, bytes, SocketFlags.None, endPoint, requestCallback, state);
		}

		// Token: 0x06002E9A RID: 11930 RVA: 0x000CD67C File Offset: 0x000CC67C
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, string hostname, int port, AsyncCallback requestCallback, object state)
		{
			if (this.m_Active && (hostname != null || port != 0))
			{
				throw new InvalidOperationException(SR.GetString("net_udpconnected"));
			}
			IPEndPoint endPoint = null;
			if (hostname != null && port != 0)
			{
				IPAddress[] hostAddresses = Dns.GetHostAddresses(hostname);
				int num = 0;
				while (num < hostAddresses.Length && hostAddresses[num].AddressFamily != this.m_Family)
				{
					num++;
				}
				if (hostAddresses.Length == 0 || num == hostAddresses.Length)
				{
					throw new ArgumentException(SR.GetString("net_invalidAddressList"), "hostname");
				}
				this.CheckForBroadcast(hostAddresses[num]);
				endPoint = new IPEndPoint(hostAddresses[num], port);
			}
			return this.BeginSend(datagram, bytes, endPoint, requestCallback, state);
		}

		// Token: 0x06002E9B RID: 11931 RVA: 0x000CD717 File Offset: 0x000CC717
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, AsyncCallback requestCallback, object state)
		{
			return this.BeginSend(datagram, bytes, null, requestCallback, state);
		}

		// Token: 0x06002E9C RID: 11932 RVA: 0x000CD725 File Offset: 0x000CC725
		public int EndSend(IAsyncResult asyncResult)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.m_Active)
			{
				return this.Client.EndSend(asyncResult);
			}
			return this.Client.EndSendTo(asyncResult);
		}

		// Token: 0x06002E9D RID: 11933 RVA: 0x000CD764 File Offset: 0x000CC764
		public byte[] Receive(ref IPEndPoint remoteEP)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			int num = this.Client.ReceiveFrom(this.m_Buffer, 65536, SocketFlags.None, ref endPoint);
			remoteEP = (IPEndPoint)endPoint;
			if (num < 65536)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
				return array;
			}
			return this.m_Buffer;
		}

		// Token: 0x06002E9E RID: 11934 RVA: 0x000CD7EC File Offset: 0x000CC7EC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginReceive(AsyncCallback requestCallback, object state)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			return this.Client.BeginReceiveFrom(this.m_Buffer, 0, 65536, SocketFlags.None, ref endPoint, requestCallback, state);
		}

		// Token: 0x06002E9F RID: 11935 RVA: 0x000CD848 File Offset: 0x000CC848
		public byte[] EndReceive(IAsyncResult asyncResult, ref IPEndPoint remoteEP)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			EndPoint endPoint;
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				endPoint = IPEndPoint.Any;
			}
			else
			{
				endPoint = IPEndPoint.IPv6Any;
			}
			int num = this.Client.EndReceiveFrom(asyncResult, ref endPoint);
			remoteEP = (IPEndPoint)endPoint;
			if (num < 65536)
			{
				byte[] array = new byte[num];
				Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
				return array;
			}
			return this.m_Buffer;
		}

		// Token: 0x06002EA0 RID: 11936 RVA: 0x000CD8C4 File Offset: 0x000CC8C4
		public void JoinMulticastGroup(IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (multicastAddr.AddressFamily != this.m_Family)
			{
				throw new ArgumentException(SR.GetString("net_protocol_invalid_multicast_family", new object[]
				{
					"UDP"
				}), "multicastAddr");
			}
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				MulticastOption optionValue = new MulticastOption(multicastAddr);
				this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, optionValue);
				return;
			}
			IPv6MulticastOption optionValue2 = new IPv6MulticastOption(multicastAddr);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, optionValue2);
		}

		// Token: 0x06002EA1 RID: 11937 RVA: 0x000CD964 File Offset: 0x000CC964
		public void JoinMulticastGroup(IPAddress multicastAddr, IPAddress localAddress)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (this.m_Family != AddressFamily.InterNetwork)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			MulticastOption optionValue = new MulticastOption(multicastAddr, localAddress);
			this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, optionValue);
		}

		// Token: 0x06002EA2 RID: 11938 RVA: 0x000CD9B8 File Offset: 0x000CC9B8
		public void JoinMulticastGroup(int ifindex, IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (ifindex < 0)
			{
				throw new ArgumentException(SR.GetString("net_value_cannot_be_negative"), "ifindex");
			}
			if (this.m_Family != AddressFamily.InterNetworkV6)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			IPv6MulticastOption optionValue = new IPv6MulticastOption(multicastAddr, (long)ifindex);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, optionValue);
		}

		// Token: 0x06002EA3 RID: 11939 RVA: 0x000CDA34 File Offset: 0x000CCA34
		public void JoinMulticastGroup(IPAddress multicastAddr, int timeToLive)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (!ValidationHelper.ValidateRange(timeToLive, 0, 255))
			{
				throw new ArgumentOutOfRangeException("timeToLive");
			}
			this.JoinMulticastGroup(multicastAddr);
			this.Client.SetSocketOption((this.m_Family == AddressFamily.InterNetwork) ? SocketOptionLevel.IP : SocketOptionLevel.IPv6, SocketOptionName.MulticastTimeToLive, timeToLive);
		}

		// Token: 0x06002EA4 RID: 11940 RVA: 0x000CDAA4 File Offset: 0x000CCAA4
		public void DropMulticastGroup(IPAddress multicastAddr)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (multicastAddr.AddressFamily != this.m_Family)
			{
				throw new ArgumentException(SR.GetString("net_protocol_invalid_multicast_family", new object[]
				{
					"UDP"
				}), "multicastAddr");
			}
			if (this.m_Family == AddressFamily.InterNetwork)
			{
				MulticastOption optionValue = new MulticastOption(multicastAddr);
				this.Client.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.DropMembership, optionValue);
				return;
			}
			IPv6MulticastOption optionValue2 = new IPv6MulticastOption(multicastAddr);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, optionValue2);
		}

		// Token: 0x06002EA5 RID: 11941 RVA: 0x000CDB44 File Offset: 0x000CCB44
		public void DropMulticastGroup(IPAddress multicastAddr, int ifindex)
		{
			if (this.m_CleanedUp)
			{
				throw new ObjectDisposedException(base.GetType().FullName);
			}
			if (multicastAddr == null)
			{
				throw new ArgumentNullException("multicastAddr");
			}
			if (ifindex < 0)
			{
				throw new ArgumentException(SR.GetString("net_value_cannot_be_negative"), "ifindex");
			}
			if (this.m_Family != AddressFamily.InterNetworkV6)
			{
				throw new SocketException(SocketError.OperationNotSupported);
			}
			IPv6MulticastOption optionValue = new IPv6MulticastOption(multicastAddr, (long)ifindex);
			this.Client.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.DropMembership, optionValue);
		}

		// Token: 0x06002EA6 RID: 11942 RVA: 0x000CDBBF File Offset: 0x000CCBBF
		private void createClientSocket()
		{
			this.Client = new Socket(this.m_Family, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x04002C3D RID: 11325
		private const int MaxUDPSize = 65536;

		// Token: 0x04002C3E RID: 11326
		private Socket m_ClientSocket;

		// Token: 0x04002C3F RID: 11327
		private bool m_Active;

		// Token: 0x04002C40 RID: 11328
		private byte[] m_Buffer;

		// Token: 0x04002C41 RID: 11329
		private AddressFamily m_Family;

		// Token: 0x04002C42 RID: 11330
		private bool m_CleanedUp;

		// Token: 0x04002C43 RID: 11331
		private bool m_IsBroadcast;
	}
}
