using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x02000326 RID: 806
	[DataContract(Namespace = "http://tpro.ca")]
	public class RevertClientReportBuiltByTproToLastTproChangeReq : BaseReportMessageReq
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00008874 File Offset: 0x00006A74
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0000887C File Offset: 0x00006A7C
		[DataMember]
		public int ReportId { get; set; }
	}
}
