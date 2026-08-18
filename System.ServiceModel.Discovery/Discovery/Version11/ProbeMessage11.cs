using System;

namespace System.ServiceModel.Discovery.Version11
{
	// Token: 0x020000A1 RID: 161
	[MessageContract(IsWrapped = false)]
	internal class ProbeMessage11
	{
		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00012158 File Offset: 0x00010358
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x00012160 File Offset: 0x00010360
		[MessageBodyMember(Name = "Probe", Namespace = "http://docs.oasis-open.org/ws-dd/ns/discovery/2009/01")]
		public FindCriteria11 Probe { get; set; }
	}
}
