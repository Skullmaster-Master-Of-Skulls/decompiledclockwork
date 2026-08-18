using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C3A RID: 3130
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllApprovedMediaRequestReq : BaseReportMessageReq
	{
		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x0600417C RID: 16764 RVA: 0x00020092 File Offset: 0x0001E292
		// (set) Token: 0x0600417D RID: 16765 RVA: 0x0002009A File Offset: 0x0001E29A
		[DataMember]
		public int CampusId { get; set; }
	}
}
