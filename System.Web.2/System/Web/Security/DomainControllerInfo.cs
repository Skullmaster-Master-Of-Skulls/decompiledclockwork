using System;
using System.Runtime.InteropServices;

namespace System.Web.Security
{
	// Token: 0x020005CA RID: 1482
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	internal sealed class DomainControllerInfo
	{
		// Token: 0x0400287C RID: 10364
		public string DomainControllerName;

		// Token: 0x0400287D RID: 10365
		public string DomainControllerAddress;

		// Token: 0x0400287E RID: 10366
		public int DomainControllerAddressType;

		// Token: 0x0400287F RID: 10367
		public Guid DomainGuid;

		// Token: 0x04002880 RID: 10368
		public string DomainName;

		// Token: 0x04002881 RID: 10369
		public string DnsForestName;

		// Token: 0x04002882 RID: 10370
		public int Flags;

		// Token: 0x04002883 RID: 10371
		public string DcSiteName;

		// Token: 0x04002884 RID: 10372
		public string ClientSiteName;
	}
}
