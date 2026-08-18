using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F6 RID: 758
	internal class SystemIPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x06001AAE RID: 6830 RVA: 0x00080745 File Offset: 0x0007E945
		internal SystemIPGlobalProperties()
		{
		}

		// Token: 0x06001AAF RID: 6831 RVA: 0x00080750 File Offset: 0x0007E950
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

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001AB0 RID: 6832 RVA: 0x000807D8 File Offset: 0x0007E9D8
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

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00080838 File Offset: 0x0007EA38
		public override string HostName
		{
			get
			{
				if (SystemIPGlobalProperties.hostName == null)
				{
					object obj = SystemIPGlobalProperties.syncObject;
					lock (obj)
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

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001AB2 RID: 6834 RVA: 0x000808B4 File Offset: 0x0007EAB4
		public override string DomainName
		{
			get
			{
				if (SystemIPGlobalProperties.domainName == null)
				{
					object obj = SystemIPGlobalProperties.syncObject;
					lock (obj)
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

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00080930 File Offset: 0x0007EB30
		public override NetBiosNodeType NodeType
		{
			get
			{
				return this.FixedInfo.NodeType;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001AB4 RID: 6836 RVA: 0x0008094C File Offset: 0x0007EB4C
		public override string DhcpScopeName
		{
			get
			{
				return this.FixedInfo.ScopeId;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x00080968 File Offset: 0x0007EB68
		public override bool IsWinsProxy
		{
			get
			{
				return this.FixedInfo.EnableProxy;
			}
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00080984 File Offset: 0x0007EB84
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			List<TcpConnectionInformation> list = new List<TcpConnectionInformation>();
			List<SystemTcpConnectionInformation> allTcpConnections = this.GetAllTcpConnections();
			foreach (TcpConnectionInformation tcpConnectionInformation in allTcpConnections)
			{
				if (tcpConnectionInformation.State != TcpState.Listen)
				{
					list.Add(tcpConnectionInformation);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x000809F0 File Offset: 0x0007EBF0
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			List<SystemTcpConnectionInformation> allTcpConnections = this.GetAllTcpConnections();
			foreach (TcpConnectionInformation tcpConnectionInformation in allTcpConnections)
			{
				if (tcpConnectionInformation.State == TcpState.Listen)
				{
					list.Add(tcpConnectionInformation.LocalEndPoint);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001AB8 RID: 6840 RVA: 0x00080A60 File Offset: 0x0007EC60
		private List<SystemTcpConnectionInformation> GetAllTcpConnections()
		{
			uint cb = 0U;
			uint num = 0U;
			SafeLocalFree safeLocalFree = null;
			List<SystemTcpConnectionInformation> list = new List<SystemTcpConnectionInformation>();
			if (Socket.OSSupportsIPv4)
			{
				num = UnsafeNetInfoNativeMethods.GetTcpTable(SafeLocalFree.Zero, ref cb, true);
				while (num == 122U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
						num = UnsafeNetInfoNativeMethods.GetTcpTable(safeLocalFree, ref cb, true);
						if (num == 0U)
						{
							IntPtr intPtr = safeLocalFree.DangerousGetHandle();
							MibTcpTable mibTcpTable = (MibTcpTable)Marshal.PtrToStructure(intPtr, typeof(MibTcpTable));
							if (mibTcpTable.numberOfEntries > 0U)
							{
								intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibTcpTable.numberOfEntries));
								int num2 = 0;
								while ((long)num2 < (long)((ulong)mibTcpTable.numberOfEntries))
								{
									MibTcpRow mibTcpRow = (MibTcpRow)Marshal.PtrToStructure(intPtr, typeof(MibTcpRow));
									list.Add(new SystemTcpConnectionInformation(mibTcpRow));
									intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibTcpRow));
									num2++;
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
				if (num != 0U && num != 232U)
				{
					throw new NetworkInformationException((int)num);
				}
			}
			if (Socket.OSSupportsIPv6)
			{
				cb = 0U;
				num = UnsafeNetInfoNativeMethods.GetExtendedTcpTable(SafeLocalFree.Zero, ref cb, true, 23U, TcpTableClass.TcpTableOwnerPidAll, 0U);
				while (num == 122U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
						num = UnsafeNetInfoNativeMethods.GetExtendedTcpTable(safeLocalFree, ref cb, true, 23U, TcpTableClass.TcpTableOwnerPidAll, 0U);
						if (num == 0U)
						{
							IntPtr intPtr2 = safeLocalFree.DangerousGetHandle();
							MibTcp6TableOwnerPid mibTcp6TableOwnerPid = (MibTcp6TableOwnerPid)Marshal.PtrToStructure(intPtr2, typeof(MibTcp6TableOwnerPid));
							if (mibTcp6TableOwnerPid.numberOfEntries > 0U)
							{
								intPtr2 = (IntPtr)((long)intPtr2 + (long)Marshal.SizeOf(mibTcp6TableOwnerPid.numberOfEntries));
								int num3 = 0;
								while ((long)num3 < (long)((ulong)mibTcp6TableOwnerPid.numberOfEntries))
								{
									MibTcp6RowOwnerPid mibTcp6RowOwnerPid = (MibTcp6RowOwnerPid)Marshal.PtrToStructure(intPtr2, typeof(MibTcp6RowOwnerPid));
									list.Add(new SystemTcpConnectionInformation(mibTcp6RowOwnerPid));
									intPtr2 = (IntPtr)((long)intPtr2 + (long)Marshal.SizeOf(mibTcp6RowOwnerPid));
									num3++;
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
				if (num != 0U && num != 232U)
				{
					throw new NetworkInformationException((int)num);
				}
			}
			return list;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x00080CA0 File Offset: 0x0007EEA0
		public override IPEndPoint[] GetActiveUdpListeners()
		{
			uint cb = 0U;
			uint num = 0U;
			SafeLocalFree safeLocalFree = null;
			List<IPEndPoint> list = new List<IPEndPoint>();
			if (Socket.OSSupportsIPv4)
			{
				num = UnsafeNetInfoNativeMethods.GetUdpTable(SafeLocalFree.Zero, ref cb, true);
				while (num == 122U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
						num = UnsafeNetInfoNativeMethods.GetUdpTable(safeLocalFree, ref cb, true);
						if (num == 0U)
						{
							IntPtr intPtr = safeLocalFree.DangerousGetHandle();
							MibUdpTable mibUdpTable = (MibUdpTable)Marshal.PtrToStructure(intPtr, typeof(MibUdpTable));
							if (mibUdpTable.numberOfEntries > 0U)
							{
								intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibUdpTable.numberOfEntries));
								int num2 = 0;
								while ((long)num2 < (long)((ulong)mibUdpTable.numberOfEntries))
								{
									MibUdpRow mibUdpRow = (MibUdpRow)Marshal.PtrToStructure(intPtr, typeof(MibUdpRow));
									int port = (int)mibUdpRow.localPort1 << 8 | (int)mibUdpRow.localPort2;
									list.Add(new IPEndPoint((long)((ulong)mibUdpRow.localAddr), port));
									intPtr = (IntPtr)((long)intPtr + (long)Marshal.SizeOf(mibUdpRow));
									num2++;
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
				if (num != 0U && num != 232U)
				{
					throw new NetworkInformationException((int)num);
				}
			}
			if (Socket.OSSupportsIPv6)
			{
				cb = 0U;
				num = UnsafeNetInfoNativeMethods.GetExtendedUdpTable(SafeLocalFree.Zero, ref cb, true, 23U, UdpTableClass.UdpTableOwnerPid, 0U);
				while (num == 122U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
						num = UnsafeNetInfoNativeMethods.GetExtendedUdpTable(safeLocalFree, ref cb, true, 23U, UdpTableClass.UdpTableOwnerPid, 0U);
						if (num == 0U)
						{
							IntPtr intPtr2 = safeLocalFree.DangerousGetHandle();
							MibUdp6TableOwnerPid mibUdp6TableOwnerPid = (MibUdp6TableOwnerPid)Marshal.PtrToStructure(intPtr2, typeof(MibUdp6TableOwnerPid));
							if (mibUdp6TableOwnerPid.numberOfEntries > 0U)
							{
								intPtr2 = (IntPtr)((long)intPtr2 + (long)Marshal.SizeOf(mibUdp6TableOwnerPid.numberOfEntries));
								int num3 = 0;
								while ((long)num3 < (long)((ulong)mibUdp6TableOwnerPid.numberOfEntries))
								{
									MibUdp6RowOwnerPid mibUdp6RowOwnerPid = (MibUdp6RowOwnerPid)Marshal.PtrToStructure(intPtr2, typeof(MibUdp6RowOwnerPid));
									int port2 = (int)mibUdp6RowOwnerPid.localPort1 << 8 | (int)mibUdp6RowOwnerPid.localPort2;
									list.Add(new IPEndPoint(new IPAddress(mibUdp6RowOwnerPid.localAddr, (long)((ulong)mibUdp6RowOwnerPid.localScopeId)), port2));
									intPtr2 = (IntPtr)((long)intPtr2 + (long)Marshal.SizeOf(mibUdp6RowOwnerPid));
									num3++;
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
				if (num != 0U && num != 232U)
				{
					throw new NetworkInformationException((int)num);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06001ABA RID: 6842 RVA: 0x00080F2C File Offset: 0x0007F12C
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			return new SystemIPGlobalStatistics(AddressFamily.InterNetwork);
		}

		// Token: 0x06001ABB RID: 6843 RVA: 0x00080F34 File Offset: 0x0007F134
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			return new SystemIPGlobalStatistics(AddressFamily.InterNetworkV6);
		}

		// Token: 0x06001ABC RID: 6844 RVA: 0x00080F3D File Offset: 0x0007F13D
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			return new SystemTcpStatistics(AddressFamily.InterNetwork);
		}

		// Token: 0x06001ABD RID: 6845 RVA: 0x00080F45 File Offset: 0x0007F145
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			return new SystemTcpStatistics(AddressFamily.InterNetworkV6);
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x00080F4E File Offset: 0x0007F14E
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			return new SystemUdpStatistics(AddressFamily.InterNetwork);
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x00080F56 File Offset: 0x0007F156
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			return new SystemUdpStatistics(AddressFamily.InterNetworkV6);
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00080F5F File Offset: 0x0007F15F
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			return new SystemIcmpV4Statistics();
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00080F66 File Offset: 0x0007F166
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			return new SystemIcmpV6Statistics();
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00080F70 File Offset: 0x0007F170
		public override UnicastIPAddressInformationCollection GetUnicastAddresses()
		{
			using (ManualResetEvent manualResetEvent = new ManualResetEvent(false))
			{
				if (!TeredoHelper.UnsafeNotifyStableUnicastIpAddressTable(new Action<object>(SystemIPGlobalProperties.StableUnicastAddressTableCallback), manualResetEvent))
				{
					manualResetEvent.WaitOne();
				}
			}
			return SystemIPGlobalProperties.GetUnicastAddressTable();
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00080FC0 File Offset: 0x0007F1C0
		public override IAsyncResult BeginGetUnicastAddresses(AsyncCallback callback, object state)
		{
			ContextAwareResult contextAwareResult = new ContextAwareResult(false, false, this, state, callback);
			contextAwareResult.StartPostingAsyncOp(false);
			if (TeredoHelper.UnsafeNotifyStableUnicastIpAddressTable(new Action<object>(SystemIPGlobalProperties.StableUnicastAddressTableCallback), contextAwareResult))
			{
				contextAwareResult.InvokeCallback();
			}
			contextAwareResult.FinishPostingAsyncOp();
			return contextAwareResult;
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x00081004 File Offset: 0x0007F204
		public override UnicastIPAddressInformationCollection EndGetUnicastAddresses(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ContextAwareResult contextAwareResult = asyncResult as ContextAwareResult;
			if (contextAwareResult == null || contextAwareResult.AsyncObject == null || contextAwareResult.AsyncObject.GetType() != typeof(SystemIPGlobalProperties))
			{
				throw new ArgumentException(SR.GetString("net_io_invalidasyncresult"));
			}
			if (contextAwareResult.EndCalled)
			{
				throw new InvalidOperationException(SR.GetString("net_io_invalidendcall", new object[]
				{
					"EndGetStableUnicastAddresses"
				}));
			}
			contextAwareResult.InternalWaitForCompletion();
			contextAwareResult.EndCalled = true;
			return SystemIPGlobalProperties.GetUnicastAddressTable();
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00081098 File Offset: 0x0007F298
		private static void StableUnicastAddressTableCallback(object param)
		{
			EventWaitHandle eventWaitHandle = param as EventWaitHandle;
			if (eventWaitHandle != null)
			{
				eventWaitHandle.Set();
				return;
			}
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)param;
			lazyAsyncResult.InvokeCallback();
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000810C4 File Offset: 0x0007F2C4
		private static UnicastIPAddressInformationCollection GetUnicastAddressTable()
		{
			UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
			NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
			for (int i = 0; i < allNetworkInterfaces.Length; i++)
			{
				UnicastIPAddressInformationCollection unicastAddresses = allNetworkInterfaces[i].GetIPProperties().UnicastAddresses;
				foreach (UnicastIPAddressInformation address in unicastAddresses)
				{
					if (!unicastIPAddressInformationCollection.Contains(address))
					{
						unicastIPAddressInformationCollection.InternalAdd(address);
					}
				}
			}
			return unicastIPAddressInformationCollection;
		}

		// Token: 0x04001AB4 RID: 6836
		private FixedInfo fixedInfo;

		// Token: 0x04001AB5 RID: 6837
		private bool fixedInfoInitialized;

		// Token: 0x04001AB6 RID: 6838
		private static volatile string hostName = null;

		// Token: 0x04001AB7 RID: 6839
		private static volatile string domainName = null;

		// Token: 0x04001AB8 RID: 6840
		private static object syncObject = new object();
	}
}
