using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C46 RID: 3142
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllCompletedStudentMediaRequestReq : BaseReportMessageReq
	{
		// Token: 0x17001838 RID: 6200
		// (get) Token: 0x060041A8 RID: 16808 RVA: 0x000201A2 File Offset: 0x0001E3A2
		// (set) Token: 0x060041A9 RID: 16809 RVA: 0x000201AA File Offset: 0x0001E3AA
		[DataMember]
		public int CampusId { get; set; }
	}
}
