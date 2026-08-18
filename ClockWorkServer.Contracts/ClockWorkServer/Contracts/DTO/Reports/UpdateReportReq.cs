using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000316 RID: 790
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateReportReq : BaseReportMessageReq
	{
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x000086ED File Offset: 0x000068ED
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x000086F5 File Offset: 0x000068F5
		[DataMember]
		public ReportDTO Report { get; set; }
	}
}
