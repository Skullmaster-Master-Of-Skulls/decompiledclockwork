using System;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FE RID: 766
	internal class SystemIPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x06001B2A RID: 6954 RVA: 0x0008192C File Offset: 0x0007FB2C
		internal SystemIPv4InterfaceProperties(FixedInfo fixedInfo, IpAdapterAddresses ipAdapterAddresses)
		{
			this.index = ipAdapterAddresses.index;
			this.routingEnabled = fixedInfo.EnableRouting;
			this.dhcpEnabled = ((ipAdapterAddresses.flags & AdapterFlags.DhcpEnabled) > (AdapterFlags)0);
			this.haveWins = (ipAdapterAddresses.firstWinsServerAddress != IntPtr.Zero);
			this.mtu = ipAdapterAddresses.mtu;
			this.GetPerAdapterInfo(ipAdapterAddresses.index);
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001B2B RID: 6955 RVA: 0x00081997 File Offset: 0x0007FB97
		public override bool UsesWins
		{
			get
			{
				return this.haveWins;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001B2C RID: 6956 RVA: 0x0008199F File Offset: 0x0007FB9F
		public override bool IsDhcpEnabled
		{
			get
			{
				return this.dhcpEnabled;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001B2D RID: 6957 RVA: 0x000819A7 File Offset: 0x0007FBA7
		public override bool IsForwardingEnabled
		{
			get
			{
				return this.routingEnabled;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001B2E RID: 6958 RVA: 0x000819AF File Offset: 0x0007FBAF
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return this.autoConfigEnabled;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x000819B7 File Offset: 0x0007FBB7
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return this.autoConfigActive;
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001B30 RID: 6960 RVA: 0x000819BF File Offset: 0x0007FBBF
		public override int Mtu
		{
			get
			{
				return (int)this.mtu;
			}
		}

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x000819C7 File Offset: 0x0007FBC7
		public override int Index
		{
			get
			{
				return (int)this.index;
			}
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000819D0 File Offset: 0x0007FBD0
		private void GetPerAdapterInfo(uint index)
		{
			if (index != 0U)
			{
				uint cb = 0U;
				SafeLocalFree safeLocalFree = null;
				uint perAdapterInfo = UnsafeNetInfoNativeMethods.GetPerAdapterInfo(index, SafeLocalFree.Zero, ref cb);
				while (perAdapterInfo == 111U)
				{
					try
					{
						safeLocalFree = SafeLocalFree.LocalAlloc((int)cb);
						perAdapterInfo = UnsafeNetInfoNativeMethods.GetPerAdapterInfo(index, safeLocalFree, ref cb);
						if (perAdapterInfo == 0U)
						{
							IpPerAdapterInfo ipPerAdapterInfo = (IpPerAdapterInfo)Marshal.PtrToStructure(safeLocalFree.DangerousGetHandle(), typeof(IpPerAdapterInfo));
							this.autoConfigEnabled = ipPerAdapterInfo.autoconfigEnabled;
							this.autoConfigActive = ipPerAdapterInfo.autoconfigActive;
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
				if (perAdapterInfo != 0U)
				{
					throw new NetworkInformationException((int)perAdapterInfo);
				}
			}
		}

		// Token: 0x04001AD4 RID: 6868
		private bool haveWins;

		// Token: 0x04001AD5 RID: 6869
		private bool dhcpEnabled;

		// Token: 0x04001AD6 RID: 6870
		private bool routingEnabled;

		// Token: 0x04001AD7 RID: 6871
		private bool autoConfigEnabled;

		// Token: 0x04001AD8 RID: 6872
		private bool autoConfigActive;

		// Token: 0x04001AD9 RID: 6873
		private uint index;

		// Token: 0x04001ADA RID: 6874
		private uint mtu;
	}
}
