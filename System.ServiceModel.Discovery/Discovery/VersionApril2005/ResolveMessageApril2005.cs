using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x0200008E RID: 142
	[MessageContract(IsWrapped = false)]
	internal class ResolveMessageApril2005
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0001101C File Offset: 0x0000F21C
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x00011024 File Offset: 0x0000F224
		[MessageBodyMember(Name = "Resolve", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public ResolveCriteriaApril2005 Resolve { get; set; }
	}
}
