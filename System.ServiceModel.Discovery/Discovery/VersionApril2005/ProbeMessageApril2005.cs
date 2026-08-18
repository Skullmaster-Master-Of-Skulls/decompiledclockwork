using System;

namespace System.ServiceModel.Discovery.VersionApril2005
{
	// Token: 0x02000089 RID: 137
	[MessageContract(IsWrapped = false)]
	internal class ProbeMessageApril2005
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00010E9D File Offset: 0x0000F09D
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x00010EA5 File Offset: 0x0000F0A5
		[MessageBodyMember(Name = "Probe", Namespace = "http://schemas.xmlsoap.org/ws/2005/04/discovery")]
		public FindCriteriaApril2005 Probe { get; set; }
	}
}
