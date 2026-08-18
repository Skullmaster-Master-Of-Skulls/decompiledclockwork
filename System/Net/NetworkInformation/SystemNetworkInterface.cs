using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000639 RID: 1593
	internal class SystemNetworkInterface : NetworkInterface
	{
		// Token: 0x0600314F RID: 12623 RVA: 0x000D3C22 File Offset: 0x000D2C22
		private SystemNetworkInterface()
		{
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000D3C2A File Offset: 0x000D2C2A
		internal static NetworkInterface[] GetNetworkInterfaces()
		{
			return SystemNetworkInterface.GetNetworkInterfaces(AddressFamily.Unspecified);
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000D3C34 File Offset: 0x000D2C34
		internal static int InternalLoopbackInterfaceIndex
		{
			get
			{
				int result;
				int bestInterface = (int)UnsafeNetInfoNativeMethods.GetBestInterface(16777343, out result);
				if (bestInterface != 0)
				{
					throw new NetworkInformationException(bestInterface);
				}
				return result;
			}
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000D3C5C File Offset: 0x000D2C5C
		internal static bool InternalGetIsNetworkAvailable()
		{
			if (ComNetOS.IsWinNt)
			{
				NetworkInterface[] networkInterfaces = SystemNetworkInterface.GetNetworkInterfaces();
				foreach (NetworkInterface networkInterface in networkInterfaces)
				{
					if (networkInterface.OperationalStatus == OperationalStatus.Up && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Tunnel && networkInterface.NetworkInterfaceType != NetworkInterfaceType.Loopback)
					{
						return true;
					}
				}
				return false;
			}
			uint num = 0U;
			return UnsafeWinINetNativeMethods.InternetGetConnectedState(ref num, 0U);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000D3CC8 File Offset: 0x000D2CC8
		private static NetworkInterface[] GetNetworkInterfaces(AddressFamily family)
		{
			IpHelperErrors.CheckFamilyUnspecified(family);
			if (ComNetOS.IsPostWin2K)
			{
				return SystemNetworkInterface.PostWin2KGetNetworkInterfaces(family);
			}
			FixedInfo fixedInfo = SystemIPGlobalProperties.GetFixedInfo();
			if (family != AddressFamily.Unspecified && family != AddressFamily.InterNetwork)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
			}
			SafeLocalFree safeLocalFree = null;
			uint cb = 0U;
			ArrayList arrayList = new ArrayList();
			uint adaptersInfo = UnsafeNetInfoNativeMethods.GetAdaptersInfo(SafeLocalFree.Zero, ref cb);
			while (adaptersInfo == 111U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
					adaptersInfo = UnsafeNetInfoNativeMethods.GetAdaptersInfo(safeLocalFree, ref cb);
					if (adaptersInfo == 0U)
					{
						IpAdapterInfo ipAdapterInfo = (IpAdapterInfo)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(IpAdapterInfo));
						arrayList.Add(new SystemNetworkInterface(fixedInfo, ipAdapterInfo));
						while (ipAdapterInfo.Next != IntPtr.Zero)
						{
							ipAdapterInfo = (IpAdapterInfo)Marshal.PtrToStructure(ipAdapterInfo.Next, typeof(IpAdapterInfo));
							arrayList.Add(new SystemNetworkInterface(fixedInfo, ipAdapterInfo));
						}
					}
				}
				finally
				{
					if (safeLocalFree != null)
					{
						safeLocalFree.Close();
					}
				}
			}
			if (adaptersInfo == 232U)
			{
				return new SystemNetworkInterface[0];
			}
			if (adaptersInfo != 0U)
			{
				throw new NetworkInformationException((int)adaptersInfo);
			}
			SystemNetworkInterface[] array = new SystemNetworkInterface[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				array[i] = (SystemNetworkInterface)arrayList[i];
			}
			return array;
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000D3E20 File Offset: 0x000D2E20
		private static SystemNetworkInterface[] GetAdaptersAddresses(AddressFamily family, FixedInfo fixedInfo)
		{
			uint cb = 0U;
			SafeLocalFree safeLocalFree = null;
			ArrayList arrayList = new ArrayList();
			uint adaptersAddresses = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(family, 0U, IntPtr.Zero, SafeLocalFree.Zero, ref cb);
			while (adaptersAddresses == 111U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
					adaptersAddresses = UnsafeNetInfoNativeMethods.GetAdaptersAddresses(family, 0U, IntPtr.Zero, safeLocalFree, ref cb);
					if (adaptersAddresses == 0U)
					{
						IpAdapterAddresses ipAdapterAddresses = (IpAdapterAddresses)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(IpAdapterAddresses));
						arrayList.Add(new SystemNetworkInterface(fixedInfo, ipAdapterAddresses));
						while (ipAdapterAddresses.next != IntPtr.Zero)
						{
							ipAdapterAddresses = (IpAdapterAddresses)Marshal.PtrToStructure(ipAdapterAddresses.next, typeof(IpAdapterAddresses));
							arrayList.Add(new SystemNetworkInterface(fixedInfo, ipAdapterAddresses));
						}
					}
				}
				finally
				{
					if (safeLocalFree != null)
					{
						safeLocalFree.Close();
					}
					safeLocalFree = null;
				}
			}
			if (adaptersAddresses == 232U || adaptersAddresses == 87U)
			{
				return new SystemNetworkInterface[0];
			}
			if (adaptersAddresses != 0U)
			{
				throw new NetworkInformationException((int)adaptersAddresses);
			}
			SystemNetworkInterface[] array = new SystemNetworkInterface[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				array[i] = (SystemNetworkInterface)arrayList[i];
			}
			return array;
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000D3F58 File Offset: 0x000D2F58
		private static SystemNetworkInterface[] PostWin2KGetNetworkInterfaces(AddressFamily family)
		{
			FixedInfo fixedInfo = SystemIPGlobalProperties.GetFixedInfo();
			SystemNetworkInterface[] array = null;
			try
			{
				IL_08:
				array = SystemNetworkInterface.GetAdaptersAddresses(family, fixedInfo);
			}
			catch (NetworkInformationException ex)
			{
				if ((long)ex.ErrorCode != 1L)
				{
					throw;
				}
				goto IL_08;
			}
			if (!Socket.SupportsIPv4)
			{
				return array;
			}
			uint cb = 0U;
			uint num = 0U;
			SafeLocalFree safeLocalFree = null;
			if (family == AddressFamily.Unspecified || family == AddressFamily.InterNetwork)
			{
				num = UnsafeNetInfoNativeMethods.GetAdaptersInfo(SafeLocalFree.Zero, ref cb);
				int num2 = 0;
				while (num == 111U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
						num = UnsafeNetInfoNativeMethods.GetAdaptersInfo(safeLocalFree, ref cb);
						if (num == 0U)
						{
							IntPtr intPtr = safeLocalFree.DangerousGetHandle();
							while (intPtr != IntPtr.Zero)
							{
								IpAdapterInfo ipAdapterInfo = (IpAdapterInfo)Marshal.PtrToStructure(intPtr, typeof(IpAdapterInfo));
								int i = 0;
								while (i < array.Length)
								{
									if (array[i] != null && ipAdapterInfo.index == array[i].index)
									{
										if (!array[i].interfaceProperties.Update(fixedInfo, ipAdapterInfo))
										{
											array[i] = null;
											num2++;
											break;
										}
										break;
									}
									else
									{
										i++;
									}
								}
								intPtr = ipAdapterInfo.Next;
							}
						}
					}
					finally
					{
						if (safeLocalFree != null)
						{
							safeLocalFree.Close();
						}
					}
				}
				if (num2 != 0)
				{
					SystemNetworkInterface[] array2 = new SystemNetworkInterface[array.Length - num2];
					int num3 = 0;
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j] != null)
						{
							array2[num3++] = array[j];
						}
					}
					array = array2;
				}
			}
			if (num != 0U && num != 232U)
			{
				throw new NetworkInformationException((int)num);
			}
			return array;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000D40E4 File Offset: 0x000D30E4
		internal SystemNetworkInterface(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.id = ipAdapterAddresses.AdapterName;
			this.name = ipAdapterAddresses.friendlyName;
			this.description = ipAdapterAddresses.description;
			this.index = ipAdapterAddresses.index;
			this.physicalAddress = ipAdapterAddresses.address;
			this.addressLength = ipAdapterAddresses.addressLength;
			this.type = ipAdapterAddresses.type;
			this.operStatus = ipAdapterAddresses.operStatus;
			this.ipv6Index = ipAdapterAddresses.ipv6Index;
			this.adapterFlags = ipAdapterAddresses.flags;
			this.interfaceProperties = new SystemIPInterfaceProperties(fixedInfo, ipAdapterAddresses);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000D4188 File Offset: 0x000D3188
		internal SystemNetworkInterface(FixedInfo fixedInfo, IpAdapterInfo ipAdapterInfo)
		{
			this.id = ipAdapterInfo.adapterName;
			this.name = string.Empty;
			this.description = ipAdapterInfo.description;
			this.index = ipAdapterInfo.index;
			this.physicalAddress = ipAdapterInfo.address;
			this.addressLength = ipAdapterInfo.addressLength;
			if (ComNetOS.IsWin2K && !ComNetOS.IsPostWin2K)
			{
				this.name = this.ReadAdapterName(this.id);
			}
			if (this.name.Length == 0)
			{
				this.name = this.description;
			}
			SystemIPv4InterfaceStatistics systemIPv4InterfaceStatistics = new SystemIPv4InterfaceStatistics((long)((ulong)this.index));
			this.operStatus = systemIPv4InterfaceStatistics.OperationalStatus;
			OldInterfaceType oldInterfaceType = ipAdapterInfo.type;
			if (oldInterfaceType <= OldInterfaceType.TokenRing)
			{
				if (oldInterfaceType == OldInterfaceType.Ethernet)
				{
					this.type = NetworkInterfaceType.Ethernet;
					goto IL_11B;
				}
				if (oldInterfaceType == OldInterfaceType.TokenRing)
				{
					this.type = NetworkInterfaceType.TokenRing;
					goto IL_11B;
				}
			}
			else
			{
				if (oldInterfaceType == OldInterfaceType.Fddi)
				{
					this.type = NetworkInterfaceType.Fddi;
					goto IL_11B;
				}
				switch (oldInterfaceType)
				{
				case OldInterfaceType.Ppp:
					this.type = NetworkInterfaceType.Ppp;
					goto IL_11B;
				case OldInterfaceType.Loopback:
					this.type = NetworkInterfaceType.Loopback;
					goto IL_11B;
				default:
					if (oldInterfaceType == OldInterfaceType.Slip)
					{
						this.type = NetworkInterfaceType.Slip;
						goto IL_11B;
					}
					break;
				}
			}
			this.type = NetworkInterfaceType.Unknown;
			IL_11B:
			this.interfaceProperties = new SystemIPInterfaceProperties(fixedInfo, ipAdapterInfo);
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000D42BD File Offset: 0x000D32BD
		public override string Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000D42C5 File Offset: 0x000D32C5
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600315A RID: 12634 RVA: 0x000D42CD File Offset: 0x000D32CD
		public override string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000D42D8 File Offset: 0x000D32D8
		public override PhysicalAddress GetPhysicalAddress()
		{
			byte[] array = new byte[this.addressLength];
			Array.Copy(this.physicalAddress, array, (long)((ulong)this.addressLength));
			return new PhysicalAddress(array);
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000D430B File Offset: 0x000D330B
		public override NetworkInterfaceType NetworkInterfaceType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000D4313 File Offset: 0x000D3313
		public override IPInterfaceProperties GetIPProperties()
		{
			return this.interfaceProperties;
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000D431B File Offset: 0x000D331B
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			return new SystemIPv4InterfaceStatistics((long)((ulong)this.index));
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000D4329 File Offset: 0x000D3329
		public override bool Supports(NetworkInterfaceComponent networkInterfaceComponent)
		{
			return (networkInterfaceComponent == NetworkInterfaceComponent.IPv6 && this.ipv6Index > 0U) || (networkInterfaceComponent == NetworkInterfaceComponent.IPv4 && this.index > 0U);
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06003160 RID: 12640 RVA: 0x000D4349 File Offset: 0x000D3349
		public override OperationalStatus OperationalStatus
		{
			get
			{
				return this.operStatus;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000D4354 File Offset: 0x000D3354
		public override long Speed
		{
			get
			{
				if (this.speed == 0L)
				{
					SystemIPv4InterfaceStatistics systemIPv4InterfaceStatistics = new SystemIPv4InterfaceStatistics((long)((ulong)this.index));
					this.speed = systemIPv4InterfaceStatistics.Speed;
				}
				return this.speed;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003162 RID: 12642 RVA: 0x000D438A File Offset: 0x000D338A
		public override bool IsReceiveOnly
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return (this.adapterFlags & AdapterFlags.ReceiveOnly) > (AdapterFlags)0;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000D43AE File Offset: 0x000D33AE
		public override bool SupportsMulticast
		{
			get
			{
				if (!ComNetOS.IsPostWin2K)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinXPRequired"));
				}
				return (this.adapterFlags & AdapterFlags.NoMulticast) == (AdapterFlags)0;
			}
		}

		// Token: 0x06003164 RID: 12644 RVA: 0x000D43D4 File Offset: 0x000D33D4
		[RegistryPermission(SecurityAction.Assert, Read = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}")]
		private string ReadAdapterName(string id)
		{
			RegistryKey registryKey = null;
			string text = string.Empty;
			try
			{
				string text2 = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + id + "\\Connection";
				registryKey = Registry.LocalMachine.OpenSubKey(text2);
				if (registryKey != null)
				{
					text = (string)registryKey.GetValue("Name");
					if (text == null)
					{
						text = string.Empty;
					}
				}
			}
			finally
			{
				if (registryKey != null)
				{
					registryKey.Close();
				}
			}
			return text;
		}

		// Token: 0x04002E80 RID: 11904
		private string name;

		// Token: 0x04002E81 RID: 11905
		private string id;

		// Token: 0x04002E82 RID: 11906
		private string description;

		// Token: 0x04002E83 RID: 11907
		private byte[] physicalAddress;

		// Token: 0x04002E84 RID: 11908
		private uint addressLength;

		// Token: 0x04002E85 RID: 11909
		private NetworkInterfaceType type;

		// Token: 0x04002E86 RID: 11910
		private OperationalStatus operStatus;

		// Token: 0x04002E87 RID: 11911
		private long speed;

		// Token: 0x04002E88 RID: 11912
		internal uint index;

		// Token: 0x04002E89 RID: 11913
		internal uint ipv6Index;

		// Token: 0x04002E8A RID: 11914
		private AdapterFlags adapterFlags;

		// Token: 0x04002E8B RID: 11915
		private SystemIPInterfaceProperties interfaceProperties;
	}
}
