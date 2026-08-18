using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A7 RID: 167
	[MessageContract(IsWrapped = false)]
	internal class ResolveMessage11
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x00012308 File Offset: 0x00010508
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00012310 File Offset: 0x00010510
		[MessageBodyMember(Name = "Resolve", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public ResolveCriteria11 Resolve { get; set; }
	}
}
