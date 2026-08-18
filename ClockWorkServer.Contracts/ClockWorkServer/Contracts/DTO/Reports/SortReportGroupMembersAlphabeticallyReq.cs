using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000314 RID: 788
	[DataContract(Namespace = "http://tpro.ca")]
	public class SortReportGroupMembersAlphabeticallyReq : BaseReportMessageReq
	{
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06001203 RID: 4611 RVA: 0x000086CB File Offset: 0x000068CB
		// (set) Token: 0x06001204 RID: 4612 RVA: 0x000086D3 File Offset: 0x000068D3
		[DataMember]
		public int ParentReportGroupId { get; set; }
	}
}
