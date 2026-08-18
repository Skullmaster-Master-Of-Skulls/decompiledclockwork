using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C45 RID: 3141
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllInProgressStudentMediaRequestReq : BaseReportMessageReq
	{
		// Token: 0x17001837 RID: 6199
		// (get) Token: 0x060041A5 RID: 16805 RVA: 0x00020191 File Offset: 0x0001E391
		// (set) Token: 0x060041A6 RID: 16806 RVA: 0x00020199 File Offset: 0x0001E399
		[DataMember]
		public int CampusId { get; set; }
	}
}
