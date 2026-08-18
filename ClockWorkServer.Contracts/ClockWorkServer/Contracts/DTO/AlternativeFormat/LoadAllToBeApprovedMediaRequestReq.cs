using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C3B RID: 3131
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllToBeApprovedMediaRequestReq : BaseReportMessageReq
	{
		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x0600417F RID: 16767 RVA: 0x000200A3 File Offset: 0x0001E2A3
		// (set) Token: 0x06004180 RID: 16768 RVA: 0x000200AB File Offset: 0x0001E2AB
		[DataMember]
		public int CampusId { get; set; }
	}
}
