using System;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x02000389 RID: 905
	public class UdpClient : IDisposable
	{
		// Token: 0x060021DD RID: 8669 RVA: 0x000A2464 File Offset: 0x000A0664
		public UdpClient() : this(AddressFamily.InterNetwork)
		{
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x000A2470 File Offset: 0x000A0670
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

		// Token: 0x060021DF RID: 8671 RVA: 0x000A24D3 File Offset: 0x000A06D3
		public UdpClient(int port) : this(port, AddressFamily.InterNetwork)
		{
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x000A24E0 File Offset: 0x000A06E0
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

		// Token: 0x060021E1 RID: 8673 RVA: 0x000A2578 File Offset: 0x000A0778
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

		// Token: 0x060021E2 RID: 8674 RVA: 0x000A25D0 File Offset: 0x000A07D0
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

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060021E3 RID: 8675 RVA: 0x000A2623 File Offset: 0x000A0823
		// (set) Token: 0x060021E4 RID: 8676 RVA: 0x000A262B File Offset: 0x000A082B
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

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x000A2634 File Offset: 0x000A0834
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x000A263C File Offset: 0x000A083C
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

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060021E7 RID: 8679 RVA: 0x000A2645 File Offset: 0x000A0845
		public int Available
		{
			get
			{
				return this.m_ClientSocket.Available;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060021E8 RID: 8680 RVA: 0x000A2652 File Offset: 0x000A0852
		// (set) Token: 0x060021E9 RID: 8681 RVA: 0x000A265F File Offset: 0x000A085F
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

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x060021EA RID: 8682 RVA: 0x000A266D File Offset: 0x000A086D
		// (set) Token: 0x060021EB RID: 8683 RVA: 0x000A267A File Offset: 0x000A087A
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

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x060021EC RID: 8684 RVA: 0x000A2688 File Offset: 0x000A0888
		// (set) Token: 0x060021ED RID: 8685 RVA: 0x000A2695 File Offset: 0x000A0895
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

		// Token: 0x170008BB RID: 2235
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x000A26A3 File Offset: 0x000A08A3
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x000A26B0 File Offset: 0x000A08B0
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

		// Token: 0x170008BC RID: 2236
		// (get) Token: 0x060021F0 RID: 8688 RVA: 0x000A26BE File Offset: 0x000A08BE
		// (set) Token: 0x060021F1 RID: 8689 RVA: 0x000A26CB File Offset: 0x000A08CB
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

		// Token: 0x060021F2 RID: 8690 RVA: 0x000A26D9 File Offset: 0x000A08D9
		public void AllowNatTraversal(bool allowed)
		{
			if (allowed)
			{
				this.m_ClientSocket.SetIPProtectionLevel(IPProtectionLevel.Unrestricted);
				return;
			}
			this.m_ClientSocket.SetIPProtectionLevel(IPProtectionLevel.EdgeRestricted);
		}

		// Token: 0x060021F3 RID: 8691 RVA: 0x000A26F9 File Offset: 0x000A08F9
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x060021F4 RID: 8692 RVA: 0x000A2704 File Offset: 0x000A0904
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

		// Token: 0x060021F5 RID: 8693 RVA: 0x000A273F File Offset: 0x000A093F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x060021F6 RID: 8694 RVA: 0x000A2748 File Offset: 0x000A0948
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.FreeResources();
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000A275C File Offset: 0x000A095C
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
					if (Socket.OSSupportsIPv4)
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

		// Token: 0x060021F8 RID: 8696 RVA: 0x000A2904 File Offset: 0x000A0B04
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

		// Token: 0x060021F9 RID: 8697 RVA: 0x000A295C File Offset: 0x000A0B5C
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

		// Token: 0x060021FA RID: 8698 RVA: 0x000A29AF File Offset: 0x000A0BAF
		private void CheckForBroadcast(IPAddress ipAddress)
		{
			if (this.Client != null && !this.m_IsBroadcast && ipAddress.IsBroadcast)
			{
				this.m_IsBroadcast = true;
				this.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000A29E4 File Offset: 0x000A0BE4
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

		// Token: 0x060021FC RID: 8700 RVA: 0x000A2A64 File Offset: 0x000A0C64
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

		// Token: 0x060021FD RID: 8701 RVA: 0x000A2B38 File Offset: 0x000A0D38
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

		// Token: 0x060021FE RID: 8702 RVA: 0x000A2B94 File Offset: 0x000A0D94
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
			if (bytes > datagram.Length || bytes < 0)
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

		// Token: 0x060021FF RID: 8703 RVA: 0x000A2C30 File Offset: 0x000A0E30
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

		// Token: 0x06002200 RID: 8704 RVA: 0x000A2CCA File Offset: 0x000A0ECA
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public IAsyncResult BeginSend(byte[] datagram, int bytes, AsyncCallback requestCallback, object state)
		{
			return this.BeginSend(datagram, bytes, null, requestCallback, state);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x000A2CD8 File Offset: 0x000A0ED8
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

		// Token: 0x06002202 RID: 8706 RVA: 0x000A2D14 File Offset: 0x000A0F14
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
			byte[] array = new byte[num];
			Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
			return array;
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x000A2D8C File Offset: 0x000A0F8C
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

		// Token: 0x06002204 RID: 8708 RVA: 0x000A2DE8 File Offset: 0x000A0FE8
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
			byte[] array = new byte[num];
			Buffer.BlockCopy(this.m_Buffer, 0, array, 0, num);
			return array;
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x000A2E54 File Offset: 0x000A1054
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

		// Token: 0x06002206 RID: 8710 RVA: 0x000A2EF0 File Offset: 0x000A10F0
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

		// Token: 0x06002207 RID: 8711 RVA: 0x000A2F44 File Offset: 0x000A1144
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

		// Token: 0x06002208 RID: 8712 RVA: 0x000A2FC0 File Offset: 0x000A11C0
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

		// Token: 0x06002209 RID: 8713 RVA: 0x000A3030 File Offset: 0x000A1230
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

		// Token: 0x0600220A RID: 8714 RVA: 0x000A30CC File Offset: 0x000A12CC
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

		// Token: 0x0600220B RID: 8715 RVA: 0x000A3147 File Offset: 0x000A1347
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes)
		{
			return Task<int>.Factory.FromAsync<byte[], int>(new Func<byte[], int, AsyncCallback, object, IAsyncResult>(this.BeginSend), new Func<IAsyncResult, int>(this.EndSend), datagram, bytes, null);
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000A316E File Offset: 0x000A136E
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes, IPEndPoint endPoint)
		{
			return Task<int>.Factory.FromAsync<byte[], int, IPEndPoint>(new Func<byte[], int, IPEndPoint, AsyncCallback, object, IAsyncResult>(this.BeginSend), new Func<IAsyncResult, int>(this.EndSend), datagram, bytes, endPoint, null);
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x000A3198 File Offset: 0x000A1398
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<int> SendAsync(byte[] datagram, int bytes, string hostname, int port)
		{
			return Task<int>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginSend(datagram, bytes, hostname, port, callback, state), new Func<IAsyncResult, int>(this.EndSend), null);
		}

		// Token: 0x0600220E RID: 8718 RVA: 0x000A31F2 File Offset: 0x000A13F2
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public Task<UdpReceiveResult> ReceiveAsync()
		{
			return Task<UdpReceiveResult>.Factory.FromAsync((AsyncCallback callback, object state) => this.BeginReceive(callback, state), delegate(IAsyncResult ar)
			{
				IPEndPoint remoteEndPoint = null;
				byte[] buffer = this.EndReceive(ar, ref remoteEndPoint);
				return new UdpReceiveResult(buffer, remoteEndPoint);
			}, null);
		}

		// Token: 0x0600220F RID: 8719 RVA: 0x000A3217 File Offset: 0x000A1417
		private void createClientSocket()
		{
			this.Client = new Socket(this.m_Family, SocketType.Dgram, ProtocolType.Udp);
		}

		// Token: 0x04001F55 RID: 8021
		private const int MaxUDPSize = 65536;

		// Token: 0x04001F56 RID: 8022
		private Socket m_ClientSocket;

		// Token: 0x04001F57 RID: 8023
		private bool m_Active;

		// Token: 0x04001F58 RID: 8024
		private byte[] m_Buffer;

		// Token: 0x04001F59 RID: 8025
		private AddressFamily m_Family;

		// Token: 0x04001F5A RID: 8026
		private bool m_CleanedUp;

		// Token: 0x04001F5B RID: 8027
		private bool m_IsBroadcast;
	}
}
