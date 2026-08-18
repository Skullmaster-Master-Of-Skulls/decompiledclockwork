using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x020002F5 RID: 757
	internal class SystemIPAddressInformation : IPAddressInformation
	{
		// Token: 0x06001AAA RID: 6826 RVA: 0x000806FF File Offset: 0x0007E8FF
		internal SystemIPAddressInformation(IPAddress address, AdapterAddressFlags flags)
		{
			this.address = address;
			this.transient = ((flags & AdapterAddressFlags.Transient) > (AdapterAddressFlags)0);
			this.dnsEligible = ((flags & AdapterAddressFlags.DnsEligible) > (AdapterAddressFlags)0);
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x0008072D File Offset: 0x0007E92D
		public override IPAddress Address
		{
			get
			{
				return this.address;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001AAC RID: 6828 RVA: 0x00080735 File Offset: 0x0007E935
		public override bool IsTransient
		{
			get
			{
				return this.transient;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x0008073D File Offset: 0x0007E93D
		public override bool IsDnsEligible
		{
			get
			{
				return this.dnsEligible;
			}
		}

		// Token: 0x04001AB1 RID: 6833
		private IPAddress address;

		// Token: 0x04001AB2 RID: 6834
		internal bool transient;

		// Token: 0x04001AB3 RID: 6835
		internal bool dnsEligible = true;
	}
}
