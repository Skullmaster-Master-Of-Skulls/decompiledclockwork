using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A15 RID: 2581
	internal class PeerIPHelper
	{
		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06006616 RID: 26134 RVA: 0x0017C1BC File Offset: 0x0017A3BC
		// (remove) Token: 0x06006617 RID: 26135 RVA: 0x0017C1F4 File Offset: 0x0017A3F4
		public event EventHandler AddressChanged;

		// Token: 0x06006618 RID: 26136 RVA: 0x0017C229 File Offset: 0x0017A429
		public PeerIPHelper()
		{
			this.Initialize();
		}

		// Token: 0x06006619 RID: 26137 RVA: 0x0017C237 File Offset: 0x0017A437
		public PeerIPHelper(IPAddress listenAddress)
		{
			if (listenAddress == null)
			{
				throw Fx.AssertAndThrow("listenAddress expected to be non-null");
			}
			this.listenAddress = listenAddress;
			this.Initialize();
		}

		// Token: 0x0600661A RID: 26138 RVA: 0x0017C25A File Offset: 0x0017A45A
		private void Initialize()
		{
			this.localAddresses = new IPAddress[0];
			this.thisLock = new object();
		}

		// Token: 0x1700189B RID: 6299
		// (set) Token: 0x0600661B RID: 26139 RVA: 0x0017C273 File Offset: 0x0017A473
		internal int AddressChangeWaitTimeout
		{
			set
			{
				this.addressChangeHelper.Timeout = value;
			}
		}

		// Token: 0x1700189C RID: 6300
		// (get) Token: 0x0600661C RID: 26140 RVA: 0x0017C281 File Offset: 0x0017A481
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x0600661D RID: 26141 RVA: 0x0017C28C File Offset: 0x0017A48C
		public bool AddressesChanged(ReadOnlyCollection<IPAddress> addresses)
		{
			bool result = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (addresses.Count != this.localAddresses.Length)
				{
					result = true;
				}
				else
				{
					foreach (IPAddress value in this.localAddresses)
					{
						if (!addresses.Contains(value))
						{
							result = true;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600661E RID: 26142 RVA: 0x0017C30C File Offset: 0x0017A50C
		public static IPAddress CloneAddress(IPAddress source, bool maskScopeId)
		{
			IPAddress result;
			if (maskScopeId || PeerIPHelper.V4Address(source))
			{
				result = new IPAddress(source.GetAddressBytes());
			}
			else
			{
				result = new IPAddress(source.GetAddressBytes(), source.ScopeId);
			}
			return result;
		}

		// Token: 0x0600661F RID: 26143 RVA: 0x0017C348 File Offset: 0x0017A548
		private static ReadOnlyCollection<IPAddress> CloneAddresses(IPAddress[] sourceArray)
		{
			IPAddress[] array = new IPAddress[sourceArray.Length];
			for (int i = 0; i < sourceArray.Length; i++)
			{
				array[i] = PeerIPHelper.CloneAddress(sourceArray[i], false);
			}
			return new ReadOnlyCollection<IPAddress>(array);
		}

		// Token: 0x06006620 RID: 26144 RVA: 0x0017C380 File Offset: 0x0017A580
		public static ReadOnlyCollection<IPAddress> CloneAddresses(ReadOnlyCollection<IPAddress> sourceCollection, bool maskScopeId)
		{
			IPAddress[] array = new IPAddress[sourceCollection.Count];
			for (int i = 0; i < sourceCollection.Count; i++)
			{
				array[i] = PeerIPHelper.CloneAddress(sourceCollection[i], maskScopeId);
			}
			return new ReadOnlyCollection<IPAddress>(array);
		}

		// Token: 0x06006621 RID: 26145 RVA: 0x0017C3C0 File Offset: 0x0017A5C0
		private static IPAddress[] CreateAddressArray(IPAddress address)
		{
			return new IPAddress[]
			{
				PeerIPHelper.CloneAddress(address, false)
			};
		}

		// Token: 0x06006622 RID: 26146 RVA: 0x0017C3E0 File Offset: 0x0017A5E0
		public void Close()
		{
			if (this.isOpen)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.isOpen)
					{
						this.addressChangeHelper.Unregister();
						if (this.ipv6Socket != null)
						{
							this.ipv6Socket.Close();
						}
						this.isOpen = false;
						this.addressChangeHelper = null;
					}
				}
			}
		}

		// Token: 0x06006623 RID: 26147 RVA: 0x0017C458 File Offset: 0x0017A658
		private IPAddress[] GetAddresses()
		{
			List<IPAddress> list = new List<IPAddress>();
			List<IPAddress> list2 = new List<IPAddress>();
			if (this.listenAddress != null && PeerIPHelper.ValidAddress(this.listenAddress))
			{
				return PeerIPHelper.CreateAddressArray(this.listenAddress);
			}
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if (PeerIPHelper.ValidInterface(networkInterface))
				{
					IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
					if (ipproperties != null)
					{
						foreach (UnicastIPAddressInformation unicastIPAddressInformation in ipproperties.UnicastAddresses)
						{
							if (PeerIPHelper.NonTransientAddress(unicastIPAddressInformation))
							{
								if (unicastIPAddressInformation.SuffixOrigin == SuffixOrigin.Random)
								{
									list2.Add(unicastIPAddressInformation.Address);
								}
								else
								{
									list.Add(unicastIPAddressInformation.Address);
								}
							}
						}
					}
				}
			}
			if (list.Count > 0)
			{
				return PeerIPHelper.ReorderAddresses(list);
			}
			return list2.ToArray();
		}

		// Token: 0x06006624 RID: 26148 RVA: 0x0017C554 File Offset: 0x0017A754
		internal static IPAddress[] ReorderAddresses(IEnumerable<IPAddress> sourceAddresses)
		{
			List<IPAddress> list = new List<IPAddress>();
			List<IPAddress> list2 = new List<IPAddress>();
			IPAddress ipaddress = null;
			IPAddress ipaddress2 = null;
			IPAddress ipaddress3 = null;
			IPAddress ipaddress4 = null;
			IPAddress ipaddress5 = null;
			foreach (IPAddress ipaddress6 in sourceAddresses)
			{
				if (ipaddress6.AddressFamily == AddressFamily.InterNetwork)
				{
					if (ipaddress != null)
					{
						list2.Add(ipaddress6);
					}
					else
					{
						ipaddress = ipaddress6;
					}
				}
				else if (ipaddress6.AddressFamily != AddressFamily.InterNetworkV6)
				{
					list2.Add(ipaddress6);
				}
				else if (ipaddress6.IsIPv6LinkLocal || ipaddress6.IsIPv6SiteLocal)
				{
					list2.Add(ipaddress6);
				}
				else
				{
					switch (PeerIPHelper.GetAddressType(ipaddress6))
					{
					case PeerIPHelper.AddressType.Teredo:
						if (ipaddress4 == null)
						{
							ipaddress4 = ipaddress6;
						}
						else
						{
							list2.Add(ipaddress6);
						}
						break;
					case PeerIPHelper.AddressType.Isatap:
						if (ipaddress3 == null)
						{
							ipaddress3 = ipaddress6;
						}
						else
						{
							list2.Add(ipaddress6);
						}
						break;
					case PeerIPHelper.AddressType.Six2Four:
						if (ipaddress5 == null)
						{
							ipaddress5 = ipaddress6;
						}
						else
						{
							list2.Add(ipaddress6);
						}
						break;
					default:
						if (ipaddress2 != null)
						{
							list2.Add(ipaddress6);
						}
						else
						{
							ipaddress2 = ipaddress6;
						}
						break;
					}
				}
			}
			if (ipaddress5 != null)
			{
				list.Add(ipaddress5);
			}
			if (ipaddress4 != null)
			{
				list.Add(ipaddress4);
			}
			if (ipaddress3 != null)
			{
				list.Add(ipaddress3);
			}
			if (ipaddress2 != null)
			{
				list.Add(ipaddress2);
			}
			if (ipaddress != null)
			{
				list.Add(ipaddress);
			}
			list.AddRange(list2);
			return list.ToArray();
		}

		// Token: 0x06006625 RID: 26149 RVA: 0x0017C6C8 File Offset: 0x0017A8C8
		private static PeerIPHelper.AddressType GetAddressType(IPAddress address)
		{
			PeerIPHelper.AddressType result = PeerIPHelper.AddressType.Unknown;
			byte[] addressBytes = address.GetAddressBytes();
			if (BitConverter.ToUInt16(addressBytes, 0) == 544)
			{
				result = PeerIPHelper.AddressType.Six2Four;
			}
			else if (BitConverter.ToUInt32(addressBytes, 0) == 288U)
			{
				result = PeerIPHelper.AddressType.Teredo;
			}
			else if (BitConverter.ToUInt32(addressBytes, 8) == 4267573248U)
			{
				result = PeerIPHelper.AddressType.Isatap;
			}
			return result;
		}

		// Token: 0x06006626 RID: 26150 RVA: 0x0017C714 File Offset: 0x0017A914
		public static EndpointAddress GetIPEndpointAddress(EndpointAddress epr, IPAddress address)
		{
			return new EndpointAddressBuilder(epr)
			{
				Uri = PeerIPHelper.GetIPUri(epr.Uri, address)
			}.ToEndpointAddress();
		}

		// Token: 0x06006627 RID: 26151 RVA: 0x0017C740 File Offset: 0x0017A940
		public static Uri GetIPUri(Uri uri, IPAddress ipAddress)
		{
			UriBuilder uriBuilder = new UriBuilder(uri);
			if (PeerIPHelper.V6Address(ipAddress) && (ipAddress.IsIPv6LinkLocal || ipAddress.IsIPv6SiteLocal))
			{
				uriBuilder.Host = new IPAddress(ipAddress.GetAddressBytes(), ipAddress.ScopeId).ToString();
			}
			else
			{
				uriBuilder.Host = ipAddress.ToString();
			}
			return uriBuilder.Uri;
		}

		// Token: 0x06006628 RID: 26152 RVA: 0x0017C79C File Offset: 0x0017A99C
		public ReadOnlyCollection<IPAddress> GetLocalAddresses()
		{
			object obj = this.ThisLock;
			ReadOnlyCollection<IPAddress> result;
			lock (obj)
			{
				result = PeerIPHelper.CloneAddresses(this.localAddresses);
			}
			return result;
		}

		// Token: 0x06006629 RID: 26153 RVA: 0x0017C7E4 File Offset: 0x0017A9E4
		private static bool NonTransientAddress(UnicastIPAddressInformation address)
		{
			return !address.IsTransient;
		}

		// Token: 0x0600662A RID: 26154 RVA: 0x0017C7EF File Offset: 0x0017A9EF
		public static bool V4Address(IPAddress address)
		{
			return address.AddressFamily == AddressFamily.InterNetwork;
		}

		// Token: 0x0600662B RID: 26155 RVA: 0x0017C7FA File Offset: 0x0017A9FA
		public static bool V6Address(IPAddress address)
		{
			return address.AddressFamily == AddressFamily.InterNetworkV6;
		}

		// Token: 0x0600662C RID: 26156 RVA: 0x0017C808 File Offset: 0x0017AA08
		public static bool ValidAddress(IPAddress address)
		{
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			foreach (NetworkInterface networkInterface in allNetworkInterfaces)
			{
				if (PeerIPHelper.ValidInterface(networkInterface))
				{
					IPInterfaceProperties ipproperties = networkInterface.GetIPProperties();
					if (ipproperties != null)
					{
						foreach (UnicastIPAddressInformation unicastIPAddressInformation in ipproperties.UnicastAddresses)
						{
							if (address.Equals(unicastIPAddressInformation.Address))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}

		// Token: 0x0600662D RID: 26157 RVA: 0x0017C89C File Offset: 0x0017AA9C
		private static bool ValidInterface(NetworkInterface networkIf)
		{
			return networkIf.NetworkInterfaceType != NetworkInterfaceType.Loopback && networkIf.OperationalStatus == OperationalStatus.Up;
		}

		// Token: 0x0600662E RID: 26158 RVA: 0x0017C8B4 File Offset: 0x0017AAB4
		private void OnAddressChanged()
		{
			bool flag = false;
			IPAddress[] addresses = this.GetAddresses();
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.AddressesChanged(Array.AsReadOnly<IPAddress>(addresses)))
				{
					this.localAddresses = addresses;
					flag = true;
				}
			}
			if (flag)
			{
				EventHandler addressChanged = this.AddressChanged;
				if (addressChanged != null && this.isOpen)
				{
					addressChanged(this, EventArgs.Empty);
				}
			}
		}

		// Token: 0x0600662F RID: 26159 RVA: 0x0017C934 File Offset: 0x0017AB34
		public void Open()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.addressChangeHelper = new PeerIPHelper.AddressChangeHelper(new PeerIPHelper.AddressChangeHelper.AddedChangedCallback(this.OnAddressChanged));
				this.localAddresses = this.GetAddresses();
				if (Socket.OSSupportsIPv6)
				{
					this.ipv6Socket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.IP);
				}
				this.isOpen = true;
			}
		}

		// Token: 0x06006630 RID: 26160 RVA: 0x0017C9B0 File Offset: 0x0017ABB0
		public ReadOnlyCollection<IPAddress> SortAddresses(ReadOnlyCollection<IPAddress> addresses)
		{
			ReadOnlyCollection<IPAddress> readOnlyCollection = SocketAddressList.SortAddresses(this.ipv6Socket, this.listenAddress, addresses);
			if (this.listenAddress != null)
			{
				if (this.listenAddress.IsIPv6LinkLocal)
				{
					using (IEnumerator<IPAddress> enumerator = readOnlyCollection.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							IPAddress ipaddress = enumerator.Current;
							if (ipaddress.IsIPv6LinkLocal)
							{
								ipaddress.ScopeId = this.listenAddress.ScopeId;
							}
						}
						return readOnlyCollection;
					}
				}
				if (this.listenAddress.IsIPv6SiteLocal)
				{
					foreach (IPAddress ipaddress2 in readOnlyCollection)
					{
						if (ipaddress2.IsIPv6SiteLocal)
						{
							ipaddress2.ScopeId = this.listenAddress.ScopeId;
						}
					}
				}
			}
			return readOnlyCollection;
		}

		// Token: 0x04003AE0 RID: 15072
		private bool isOpen;

		// Token: 0x04003AE1 RID: 15073
		private readonly IPAddress listenAddress;

		// Token: 0x04003AE2 RID: 15074
		private IPAddress[] localAddresses;

		// Token: 0x04003AE3 RID: 15075
		private PeerIPHelper.AddressChangeHelper addressChangeHelper;

		// Token: 0x04003AE4 RID: 15076
		private Socket ipv6Socket;

		// Token: 0x04003AE5 RID: 15077
		private object thisLock;

		// Token: 0x04003AE6 RID: 15078
		private const uint Six2FourPrefix = 544U;

		// Token: 0x04003AE7 RID: 15079
		private const uint TeredoPrefix = 288U;

		// Token: 0x04003AE8 RID: 15080
		private const uint IsatapIdentifier = 4267573248U;

		// Token: 0x02000E5E RID: 3678
		private enum AddressType
		{
			// Token: 0x04004AC9 RID: 19145
			Unknown,
			// Token: 0x04004ACA RID: 19146
			Teredo,
			// Token: 0x04004ACB RID: 19147
			Isatap,
			// Token: 0x04004ACC RID: 19148
			Six2Four
		}

		// Token: 0x02000E5F RID: 3679
		private class AddressChangeHelper
		{
			// Token: 0x06008359 RID: 33625 RVA: 0x001E6294 File Offset: 0x001E4494
			public AddressChangeHelper(PeerIPHelper.AddressChangeHelper.AddedChangedCallback addressChanged)
			{
				this.addressChanged = addressChanged;
				this.timer = new IOThreadTimer(new Action<object>(this.FireAddressChange), null, true);
				NetworkChange.NetworkAddressChanged += this.OnAddressChange;
			}

			// Token: 0x0600835A RID: 33626 RVA: 0x001E62E3 File Offset: 0x001E44E3
			public void Unregister()
			{
				NetworkChange.NetworkAddressChanged -= this.OnAddressChange;
			}

			// Token: 0x0600835B RID: 33627 RVA: 0x001E62F6 File Offset: 0x001E44F6
			private void OnAddressChange(object sender, EventArgs args)
			{
				this.timer.Set(this.Timeout);
			}

			// Token: 0x0600835C RID: 33628 RVA: 0x001E6309 File Offset: 0x001E4509
			private void FireAddressChange(object asyncState)
			{
				this.timer.Cancel();
				this.addressChanged();
			}

			// Token: 0x04004ACD RID: 19149
			public int Timeout = 5000;

			// Token: 0x04004ACE RID: 19150
			private IOThreadTimer timer;

			// Token: 0x04004ACF RID: 19151
			private PeerIPHelper.AddressChangeHelper.AddedChangedCallback addressChanged;

			// Token: 0x02000F90 RID: 3984
			// (Invoke) Token: 0x0600885F RID: 34911
			public delegate void AddedChangedCallback();
		}
	}
}
