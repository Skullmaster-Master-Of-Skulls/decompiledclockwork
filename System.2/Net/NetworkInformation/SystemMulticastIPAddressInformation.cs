using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002FC RID: 764
	internal class SystemMulticastIPAddressInformation : MulticastIPAddressInformation
	{
		// Token: 0x06001B10 RID: 6928 RVA: 0x000816B9 File Offset: 0x0007F8B9
		private SystemMulticastIPAddressInformation()
		{
		}

		// Token: 0x06001B11 RID: 6929 RVA: 0x000816C1 File Offset: 0x0007F8C1
		public SystemMulticastIPAddressInformation(SystemIPAddressInformation addressInfo)
		{
			this.innerInfo = addressInfo;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x000816D0 File Offset: 0x0007F8D0
		public override IPAddress Address
		{
			get
			{
				return this.innerInfo.Address;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001B13 RID: 6931 RVA: 0x000816DD File Offset: 0x0007F8DD
		public override bool IsTransient
		{
			get
			{
				return this.innerInfo.IsTransient;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x000816EA File Offset: 0x0007F8EA
		public override bool IsDnsEligible
		{
			get
			{
				return this.innerInfo.IsDnsEligible;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001B15 RID: 6933 RVA: 0x000816F7 File Offset: 0x0007F8F7
		public override PrefixOrigin PrefixOrigin
		{
			get
			{
				return PrefixOrigin.Other;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x000816FA File Offset: 0x0007F8FA
		public override SuffixOrigin SuffixOrigin
		{
			get
			{
				return SuffixOrigin.Other;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001B17 RID: 6935 RVA: 0x000816FD File Offset: 0x0007F8FD
		public override DuplicateAddressDetectionState DuplicateAddressDetectionState
		{
			get
			{
				return DuplicateAddressDetectionState.Invalid;
			}
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x00081700 File Offset: 0x0007F900
		public override long AddressValidLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001B19 RID: 6937 RVA: 0x00081704 File Offset: 0x0007F904
		public override long AddressPreferredLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x00081708 File Offset: 0x0007F908
		public override long DhcpLeaseLifetime
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0008170C File Offset: 0x0007F90C
		internal static MulticastIPAddressInformationCollection ToMulticastIpAddressInformationCollection(IPAddressInformationCollection addresses)
		{
			MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
			foreach (IPAddressInformation ipaddressInformation in addresses)
			{
				multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation((SystemIPAddressInformation)ipaddressInformation));
			}
			return multicastIPAddressInformationCollection;
		}

		// Token: 0x04001ACA RID: 6858
		private SystemIPAddressInformation innerInfo;
	}
}
