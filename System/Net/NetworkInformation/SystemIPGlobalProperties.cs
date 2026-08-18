using System;
using System.Collections;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200062F RID: 1583
	internal class SystemIPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x060030CC RID: 12492 RVA: 0x000D2545 File Offset: 0x000D1545
		internal SystemIPGlobalProperties()
		{
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000D2550 File Offset: 0x000D1550
		internal static FixedInfo GetFixedInfo()
		{
			uint cb = 0U;
			SafeLocalFree safeLocalFree = null;
			FixedInfo result = default(FixedInfo);
			uint networkParams = UnsafeNetInfoNativeMethods.GetNetworkParams(SafeLocalFree.Zero, ref cb);
			while (networkParams == 111U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
					networkParams = UnsafeNetInfoNativeMethods.GetNetworkParams(safeLocalFree, ref cb);
					if (networkParams == 0U)
					{
						result = new FixedInfo((FIXED_INFO)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(FIXED_INFO)));
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
			if (networkParams != 0U)
			{
				throw new NetworkInformationException((int)networkParams);
			}
			return result;
		}

		// Token: 0x17000AC6 RID: 2758
		// (get) Token: 0x060030CE RID: 12494 RVA: 0x000D25D8 File Offset: 0x000D15D8
		internal FixedInfo FixedInfo
		{
			get
			{
				if (!this.fixedInfoInitialized)
				{
					lock (this)
					{
						if (!this.fixedInfoInitialized)
						{
							this.fixedInfo = SystemIPGlobalProperties.GetFixedInfo();
							this.fixedInfoInitialized = true;
						}
					}
				}
				return this.fixedInfo;
			}
		}

		// Token: 0x17000AC7 RID: 2759
		// (get) Token: 0x060030CF RID: 12495 RVA: 0x000D2630 File Offset: 0x000D1630
		public override string HostName
		{
			get
			{
				if (SystemIPGlobalProperties.hostName == null)
				{
					lock (SystemIPGlobalProperties.syncObject)
					{
						if (SystemIPGlobalProperties.hostName == null)
						{
							SystemIPGlobalProperties.hostName = this.FixedInfo.HostName;
							SystemIPGlobalProperties.domainName = this.FixedInfo.DomainName;
						}
					}
				}
				return SystemIPGlobalProperties.hostName;
			}
		}

		// Token: 0x17000AC8 RID: 2760
		// (get) Token: 0x060030D0 RID: 12496 RVA: 0x000D269C File Offset: 0x000D169C
		public override string DomainName
		{
			get
			{
				if (SystemIPGlobalProperties.domainName == null)
				{
					lock (SystemIPGlobalProperties.syncObject)
					{
						if (SystemIPGlobalProperties.domainName == null)
						{
							SystemIPGlobalProperties.hostName = this.FixedInfo.HostName;
							SystemIPGlobalProperties.domainName = this.FixedInfo.DomainName;
						}
					}
				}
				return SystemIPGlobalProperties.domainName;
			}
		}

		// Token: 0x17000AC9 RID: 2761
		// (get) Token: 0x060030D1 RID: 12497 RVA: 0x000D2708 File Offset: 0x000D1708
		public override NetBiosNodeType NodeType
		{
			get
			{
				return this.FixedInfo.NodeType;
			}
		}

		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000D2724 File Offset: 0x000D1724
		public override string DhcpScopeName
		{
			get
			{
				return this.FixedInfo.ScopeId;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x060030D3 RID: 12499 RVA: 0x000D2740 File Offset: 0x000D1740
		public override bool IsWinsProxy
		{
			get
			{
				return this.FixedInfo.EnableProxy;
			}
		}

		// Token: 0x060030D4 RID: 12500 RVA: 0x000D275C File Offset: 0x000D175C
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			ArrayList arrayList = new ArrayList();
			TcpConnectionInformation[] array = this.GetAllTcpConnections();
			foreach (TcpConnectionInformation tcpConnectionInformation in array)
			{
				if (tcpConnectionInformation.State != TcpState.Listen)
				{
					arrayList.Add(tcpConnectionInformation);
				}
			}
			array = new TcpConnectionInformation[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				array[j] = (TcpConnectionInformation)arrayList[j];
			}
			return array;
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000D27D0 File Offset: 0x000D17D0
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			ArrayList arrayList = new ArrayList();
			TcpConnectionInformation[] allTcpConnections = this.GetAllTcpConnections();
			foreach (TcpConnectionInformation tcpConnectionInformation in allTcpConnections)
			{
				if (tcpConnectionInformation.State == TcpState.Listen)
				{
					arrayList.Add(tcpConnectionInformation.LocalEndPoint);
				}
			}
			IPEndPoint[] array2 = new IPEndPoint[arrayList.Count];
			for (int j = 0; j < arrayList.Count; j++)
			{
				array2[j] = (IPEndPoint)arrayList[j];
			}
			return array2;
		}

		// Token: 0x060030D6 RID: 12502 RVA: 0x000D2850 File Offset: 0x000D1850
		private TcpConnectionInformation[] GetAllTcpConnections()
		{
			uint cb = 0U;
			SafeLocalFree safeLocalFree = null;
			SystemTcpConnectionInformation[] array = null;
			uint tcpTable = UnsafeNetInfoNativeMethods.GetTcpTable(SafeLocalFree.Zero, ref cb, true);
			while (tcpTable == 122U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
					tcpTable = UnsafeNetInfoNativeMethods.GetTcpTable(safeLocalFree, ref cb, true);
					if (tcpTable == 0U)
					{
						IntPtr intPtr = safeLocalFree.DangerousGetHandle();
						MibTcpTable mibTcpTable = (MibTcpTable)Marshal.PtrToStructure(intPtr, typeof(MibTcpTable));
						if (mibTcpTable.numberOfEntries > 0U)
						{
							array = new SystemTcpConnectionInformation[mibTcpTable.numberOfEntries];
							intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibTcpTable.numberOfEntries));
							int num = 0;
							while ((long)num < (long)((ulong)mibTcpTable.numberOfEntries))
							{
								MibTcpRow mibTcpRow = (MibTcpRow)Marshal.PtrToStructure(intPtr, typeof(MibTcpRow));
								array[num] = new SystemTcpConnectionInformation(mibTcpRow);
								intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibTcpRow));
								num++;
							}
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
			if (tcpTable != 0U && tcpTable != 232U)
			{
				throw new NetworkInformationException((int)tcpTable);
			}
			if (array == null)
			{
				return new SystemTcpConnectionInformation[0];
			}
			return array;
		}

		// Token: 0x060030D7 RID: 12503 RVA: 0x000D2984 File Offset: 0x000D1984
		public override IPEndPoint[] GetActiveUdpListeners()
		{
			uint cb = 0U;
			SafeLocalFree safeLocalFree = null;
			IPEndPoint[] array = null;
			uint udpTable = UnsafeNetInfoNativeMethods.GetUdpTable(SafeLocalFree.Zero, ref cb, true);
			while (udpTable == 122U)
			{
				try
				{
					safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
					udpTable = UnsafeNetInfoNativeMethods.GetUdpTable(safeLocalFree, ref cb, true);
					if (udpTable == 0U)
					{
						IntPtr intPtr = safeLocalFree.DangerousGetHandle();
						MibUdpTable mibUdpTable = (MibUdpTable)Marshal.PtrToStructure(intPtr, typeof(MibUdpTable));
						if (mibUdpTable.numberOfEntries > 0U)
						{
							array = new IPEndPoint[mibUdpTable.numberOfEntries];
							intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibUdpTable.numberOfEntries));
							int num = 0;
							while ((long)num < (long)((ulong)mibUdpTable.numberOfEntries))
							{
								MibUdpRow mibUdpRow = (MibUdpRow)Marshal.PtrToStructure(intPtr, typeof(MibUdpRow));
								int port = (int)mibUdpRow.localPort3 << 24 | (int)mibUdpRow.localPort4 << 16 | (int)mibUdpRow.localPort1 << 8 | (int)mibUdpRow.localPort2;
								array[num] = new IPEndPoint((long)((ulong)mibUdpRow.localAddr), port);
								intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibUdpRow));
								num++;
							}
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
			if (udpTable != 0U && udpTable != 232U)
			{
				throw new NetworkInformationException((int)udpTable);
			}
			if (array == null)
			{
				return new IPEndPoint[0];
			}
			return array;
		}

		// Token: 0x060030D8 RID: 12504 RVA: 0x000D2AF8 File Offset: 0x000D1AF8
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			return new SystemIPGlobalStatistics(AddressFamily.InterNetwork);
		}

		// Token: 0x060030D9 RID: 12505 RVA: 0x000D2B00 File Offset: 0x000D1B00
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			return new SystemIPGlobalStatistics(AddressFamily.InterNetworkV6);
		}

		// Token: 0x060030DA RID: 12506 RVA: 0x000D2B09 File Offset: 0x000D1B09
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			return new SystemTcpStatistics(AddressFamily.InterNetwork);
		}

		// Token: 0x060030DB RID: 12507 RVA: 0x000D2B11 File Offset: 0x000D1B11
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			return new SystemTcpStatistics(AddressFamily.InterNetworkV6);
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000D2B1A File Offset: 0x000D1B1A
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			return new SystemUdpStatistics(AddressFamily.InterNetwork);
		}

		// Token: 0x060030DD RID: 12509 RVA: 0x000D2B22 File Offset: 0x000D1B22
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			return new SystemUdpStatistics(AddressFamily.InterNetworkV6);
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000D2B2B File Offset: 0x000D1B2B
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			return new SystemIcmpV4Statistics();
		}

		// Token: 0x060030DF RID: 12511 RVA: 0x000D2B32 File Offset: 0x000D1B32
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			return new SystemIcmpV6Statistics();
		}

		// Token: 0x04002E51 RID: 11857
		private FixedInfo fixedInfo;

		// Token: 0x04002E52 RID: 11858
		private bool fixedInfoInitialized;

		// Token: 0x04002E53 RID: 11859
		private static string hostName = null;

		// Token: 0x04002E54 RID: 11860
		private static string domainName = null;

		// Token: 0x04002E55 RID: 11861
		private static object syncObject = new object();
	}
}
